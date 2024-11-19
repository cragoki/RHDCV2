using DAL.Entities;
using DAL.Entities.Interfaces;
using DAL.Entities.MappingTables;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DAL.DbRHDCV2Context
{
    public class RHDCV2Context : DbContext
    {

        public DbSet<WorkerService> tb_worker_service { get; set; }
        public DbSet<ErrorLog> tb_error_log { get; set; }
        public DbSet<AutoRetrieverLog> tb_auto_retriever_log { get; set; }
        public DbSet<DaysEvent> tb_event { get; set; }
        public DbSet<Race> tb_race { get; set; }
        public DbSet<RaceHorse> tb_race_horse { get; set; }
        public DbSet<AgeCategory> tb_age_category { get; set; }
        public DbSet<DistanceCategory> tb_distance_category { get; set; }
        public DbSet<GoingCategory> tb_going_category { get; set; }
        public DbSet<Horse> tb_horse { get; set; }
        public DbSet<Jockey> tb_jockey { get; set; }
        public DbSet<Trainer> tb_trainer { get; set; }
        public DbSet<Alert> tb_alert { get; set; }
        public DbSet<RaceCourse> tb_race_course { get; set; }
        public DbSet<DistanceBetweenCategory> tb_distance_between_category { get; set; }
        public DbSet<AttireCategory> tb_attire_category { get; set; }
        public DbSet<Algorithm> tb_algorithm { get; set; }
        public DbSet<AlgorithmExecution> tb_algorithm_execution { get; set; }
        public DbSet<AlgorithmVariable> tb_algorithm_variable { get; set; }

        public RHDCV2Context(DbContextOptions<RHDCV2Context> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Automatically configure PK for all IEntity objects
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(IEntity).IsAssignableFrom(entityType.ClrType))
                {
                    modelBuilder.Entity(entityType.ClrType)
                                .HasKey("Id");
                }
            }

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}

