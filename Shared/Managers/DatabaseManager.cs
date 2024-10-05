using DAL.DbRHDCV2Context;
using DAL.Entities;
using DAL.Entities.MappingTables;
using DAL.Enums;
using Microsoft.EntityFrameworkCore;
using Shared.Constants;
using Shared.Helpers;
using Shared.Managers.Interfaces;
using Shared.Models.ScrapingModels;
using System.Diagnostics;

namespace Shared.Managers
{
    public class DatabaseManager : IDatabaseManager
    {
        private readonly RHDCV2Context _context;
        private readonly IAlertManager _alertManager;
        private readonly IErrorLogManager _error;

        public DatabaseManager(RHDCV2Context context, IAlertManager alertManager, IErrorLogManager error)
        {
            _context = context;
            _alertManager = alertManager;
            _error = error;
        }

        public async Task AddEventAndRaceData(List<EventModel> events)
        {
            var addedEvents = new List<DaysEvent>();
            try
            {
                foreach (var e in events)
                {
                    //Add Event
                    var raceCourse = await AddOrGetRaceCourse(e.CourseName);
                    var eventEntity = new DaysEvent()
                    {
                        Date = e.EventDate,
                        RaceCourseId = raceCourse,
                    };

                    _context.tb_event.Add(eventEntity);
                    await _context.SaveChangesAsync();
                    addedEvents.Add(eventEntity);
                    foreach (var race in e.Races) 
                    {
                        //Add Race
                        var raceId = await AddRace(race,e, eventEntity);

                        foreach (var raceHorse in race.RaceHorses)
                        {
                            //Add Race Horses
                            await AddRaceHorse(raceHorse, raceId);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                await _error.LogError("tb_event", "DatabaseManager.cs", "AddEventAndRaceData", ErrorType.Database, ex.StackTrace!, ex.InnerException?.Message ?? "", ex.Message!);
                await DeleteAddedEventAndRaceData(addedEvents);
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> AddRace(RaceModel race, EventModel e, DaysEvent eventEntity)
        {
            try
            {
                var ageCategoryId = await AddOrGetMappingEntity<AgeCategory>(race.AgeCategoryName);
                var goingCategoryId = await AddOrGetMappingEntity<GoingCategory>(race.GoingCategoryName);
                var distanceCategoryId = await AddOrGetMappingEntity<DistanceCategory>(race.DistanceCategoryName);

                var raceEntity = new Race()
                {
                    EventId = eventEntity.Id,
                    EventEntity = eventEntity,
                    Class = DataTypeConverterHelper.StringToInt(race.Class),
                    RaceUrl = race.RaceUrl,
                    RaceType = race.RaceType,
                    AgeCategoryId = ageCategoryId,
                    GoingCategoryId = goingCategoryId,
                    DistanceCategoryId = distanceCategoryId,
                    RaceDate = e.EventDate
                };

                _context.tb_race.Add(raceEntity);
                await _context.SaveChangesAsync();
                return raceEntity.Id;
            }
            catch (Exception ex)
            {
                await _error.LogError("tb_race", "DatabaseManager.cs", "AddRaces", ErrorType.Database, ex.StackTrace!, ex.InnerException?.Message ?? "", ex.Message!);
                throw new Exception(ex.Message);
            }
        }

        public async Task AddRaceHorse(RaceHorseModel raceHorse, int raceId) 
        {
            try 
            {
                var horseId = await AddOrGetMappingEntity<Horse>(raceHorse.HorseName);
                var jockeyId = await AddOrGetMappingEntity<Jockey>(raceHorse.JockeyName);
                var trainerId = await AddOrGetMappingEntity<Trainer>(raceHorse.TrainerName);
                var distanceBetweenId = 0;
                var attireId = 0;

                if (!String.IsNullOrEmpty(raceHorse.DistanceBetween)) 
                {
                    distanceBetweenId = await AddOrGetMappingEntity<DistanceBetweenCategory>(raceHorse.DistanceBetween);
                }
                if (!String.IsNullOrEmpty(raceHorse.Attire))
                {
                    attireId = await AddOrGetMappingEntity<AttireCategory>(raceHorse.Attire);
                }


                var raceHorseEntity = new RaceHorse()
                {
                    RaceId = raceId,
                    HorseId = horseId,
                    Weight = DataTypeConverterHelper.StringToDecimal(raceHorse.Weight.Replace("-", ".")),
                    Age = DataTypeConverterHelper.StringToInt(raceHorse.Age),
                    JockeyId = jockeyId,
                    TrainerId = trainerId,
                    Odds = DataTypeConverterHelper.ConvertFractionalToDecimalOdds(raceHorse.Odds),
                    DistanceBetweenCategoryId = distanceBetweenId == 0 ? null : distanceBetweenId,
                    AttireCategoryId = attireId == 0 ? null : attireId,
                    Position = DataTypeConverterHelper.StringToInt(raceHorse.Position),
                    Time = null
                };

                _context.tb_race_horse.Add(raceHorseEntity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await _error.LogError("tb_race_horse", "DatabaseManager.cs", "AddRaceHorse", ErrorType.Database, ex.StackTrace!, ex.InnerException?.Message ?? "", ex.Message!);
                throw new Exception(ex.Message);
            }
        }

        #region Private

        /// <summary>
        /// Add or create race course and create alert if a new racecourse is created
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private async Task<int> AddOrGetRaceCourse(string name)
        {
            var course = _context.tb_race_course.FirstOrDefault(x => x.Name == name);

            if (course != null)
            {
                return course.Id;
            }

            var newCourse = new RaceCourse()
            {
                Name = name
            };

            _context.tb_race_course.Add(newCourse);
            await _context.SaveChangesAsync();

            //Create alert for new course
            await _alertManager.CreateAlert(AlertType.NewCourse, AlertConstants.NewCourseAlert.Replace("{x}", name));

            return newCourse.Id;
        }

        /// <summary>
        /// Generic method for adding or returning id of any of the mapping tables
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        private async Task<int> AddOrGetMappingEntity<T>(string name) where T : class, new()
        {
            var dbSet = _context.Set<T>();

            // Use EF.Property in a LINQ query
            var entity = await dbSet.FirstOrDefaultAsync(e => EF.Property<string>(e, "Name") == name);

            if (entity != null)
            {
                // Get the Id property using reflection
                var idProperty = typeof(T).GetProperty("Id");
                if (idProperty != null)
                {
                    return (int)idProperty.GetValue(entity);
                }
                throw new InvalidOperationException("Id property not found.");
            }

            var newEntity = new T();

            // Set the Name property using reflection
            var nameProperty = typeof(T).GetProperty("Name");
            if (nameProperty != null && nameProperty.CanWrite)
            {
                nameProperty.SetValue(newEntity, name);
            }

            dbSet.Add(newEntity);
            await _context.SaveChangesAsync();

            // Get the Id of the newly created entity
            var newIdProperty = typeof(T).GetProperty("Id");
            if (newIdProperty != null)
            {
                return (int)newIdProperty.GetValue(newEntity);
            }

            throw new InvalidOperationException("Id property not found.");
        }

        private async Task DeleteAddedEventAndRaceData(List<DaysEvent> events)
        {
            foreach (var e in events) 
            {
                var eventEntity = _context.tb_event.Where(x => x.Id == e.Id);
                var races = _context.tb_race.Where(x => x.EventId == e.Id);

                foreach (var race in races) 
                {
                    var raceHorses = _context.tb_race_horse.Where(x => x.RaceId == race.Id);

                    await raceHorses.ExecuteDeleteAsync();
                }

                await races.ExecuteDeleteAsync();
                await eventEntity.ExecuteDeleteAsync();
            }
        }
        #endregion
    }
}
