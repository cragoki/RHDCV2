using DAL.DbRHDCV2Context;
using DAL.Entities;
using DAL.Entities.MappingTables;

namespace Shared.Managers
{
    public class MappingTableManager : IMappingTableManager
    {
        private readonly RHDCV2Context _context;

        public MappingTableManager(RHDCV2Context context)
        {
            _context = context;
        }

        #region PUBLIC
        public async Task<int> AddOrReturnAgeCategory(string age)
        {
            int result = 0;

            if (string.IsNullOrEmpty(age))
            {
                throw new ArgumentNullException("Age Type is null");
            }

            var existing = _context.tb_age_category.Where(x => x.Name == age).ToList().FirstOrDefault();

            if (existing == null)
            {
                //Add
                var toAdd = new AgeCategory()
                {
                    Name = age
                };

                _context.tb_age_category.Add(toAdd);
                await _context.SaveChangesAsync();

                result = toAdd.Id;
            }
            else
            {
                result = existing.Id;
            }

            return result;
        }

        public async Task<int> AddOrReturnDistanceCategory(string distance)
        {
            int result = 0;

            if (string.IsNullOrEmpty(distance))
            {
                throw new ArgumentNullException("Distance Type is null");
            }

            var existing = _context.tb_distance_category.Where(x => x.Name == distance).ToList().FirstOrDefault();

            if (existing == null)
            {
                //Add
                var toAdd = new DistanceCategory()
                {
                    Name = distance
                };

                _context.tb_distance_category.Add(toAdd);
                await _context.SaveChangesAsync();

                result = toAdd.Id;
            }
            else
            {
                result = existing.Id;
            }

            return result;
        }

        public async Task<int> AddOrReturnGoingCategory(string going)
        {
            int result = 0;

            if (string.IsNullOrEmpty(going))
            {
                throw new ArgumentNullException("Going Type is null");
            }

            var existing = _context.tb_going_category.Where(x => x.Name == going).ToList().FirstOrDefault();

            if (existing == null)
            {
                //Add
                var toAdd = new GoingCategory()
                {
                    Name = going
                };

                _context.tb_going_category.Add(toAdd);
                await _context.SaveChangesAsync();

                result = toAdd.Id;
            }
            else
            {
                result = existing.Id;
            }

            return result;
        }

        public async Task<int> AddOrReturnHorse(string horseName)
        {
            int result = 0;

            if (string.IsNullOrEmpty(horseName))
            {
                throw new ArgumentNullException("Horse name is null");
            }

            var existing = _context.tb_horse.Where(x => x.Name == horseName).ToList().FirstOrDefault();

            if (existing == null)
            {
                //Add
                var toAdd = new Horse()
                {
                    Name = horseName
                };

                _context.tb_horse.Add(toAdd);
                await _context.SaveChangesAsync();

                result = toAdd.Id;
            }
            else
            {
                result = existing.Id;
            }

            return result;
        }

        public async Task<int> AddOrReturnJockey(string jockeyName)
        {
            int result = 0;

            if (string.IsNullOrEmpty(jockeyName))
            {
                throw new ArgumentNullException("Jockey name is null");
            }

            var existing = _context.tb_jockey.Where(x => x.Name == jockeyName).ToList().FirstOrDefault();

            if (existing == null)
            {
                //Add
                var toAdd = new Jockey()
                {
                    Name = jockeyName
                };

                _context.tb_jockey.Add(toAdd);
                await _context.SaveChangesAsync();

                result = toAdd.Id;
            }
            else
            {
                result = existing.Id;
            }

            return result;
        }

        public async Task<int> AddOrReturnTrainer(string trainerName)
        {
            int result = 0;

            if (string.IsNullOrEmpty(trainerName))
            {
                throw new ArgumentNullException("Trainer name is null");
            }

            var existing = _context.tb_trainer.Where(x => x.Name == trainerName).ToList().FirstOrDefault();

            if (existing == null)
            {
                //Add
                var toAdd = new Trainer()
                {
                    Name = trainerName
                };

                _context.tb_trainer.Add(toAdd);
                await _context.SaveChangesAsync();

                result = toAdd.Id;
            }
            else
            {
                result = existing.Id;
            }

            return result;
        }
        #endregion

        #region PRIVATE
        #endregion
    }
}
