using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CsetAnalytics.DomainModels.Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace CsetAnalytics.DomainModels
{
    public class CsetContext //: IdentityDbContext<ApplicationUser>
    {
        private readonly MongoClient _client;
        private Dictionary<string, List<string>> _databasesAndCollections;
        private readonly IMongoDatabase _database;
        private readonly IMongoDbSettings _settings;

        public CsetContext(MongoDbSettings settings)
        {
            Console.WriteLine(settings.ConnectionString);
            _settings = settings;
            _client = new MongoClient(_settings.ConnectionString);
            if (_client != null)
            {
                _database = _client.GetDatabase(_settings.DatabaseName);
            }
        }

        public IMongoQueryable<Assessment> AssessmentLinq
        {
            get
            {
                return _database.GetCollection<Assessment>(_settings.AssessmentCollectionName).AsQueryable<Assessment>();
            }
        }

        public IMongoCollection<Assessment> Assessments
        {
            get
            {
                return _database.GetCollection<Assessment>(_settings.AssessmentCollectionName);
            }
        }

        public IMongoQueryable<AnalyticQuestionAnswer> QuestionsLinq
        {
            get
            {
                return _database.GetCollection<AnalyticQuestionAnswer>(_settings.AnalyticQuestionAnswerCollectionName).AsQueryable<AnalyticQuestionAnswer>();
            }
        }

        public IMongoCollection<AnalyticQuestionAnswer> Questions
        {
            get
            {
                return _database.GetCollection<AnalyticQuestionAnswer>(_settings.AnalyticQuestionAnswerCollectionName);
            }
        }

        public async Task<Dictionary<string, List<string>>> GetDatabaseAndCollections()
        {
            if (_databasesAndCollections != null) return _databasesAndCollections;

            _databasesAndCollections = new Dictionary<string, List<string>>();
            var databasesResult = _client.ListDatabaseNames();

            await databasesResult.ForEachAsync(async databaseName =>
            {
                var collectionNames = new List<string>();
                var database = _client.GetDatabase(databaseName);
                var collectionNamesResult = database.ListCollectionNames();
                await collectionNamesResult.ForEachAsync(
                    collectionName => { collectionNames.Add(collectionName); });
                _databasesAndCollections.Add(databaseName, collectionNames);
            });

            return _databasesAndCollections;
        }


        //        public virtual DbSet<AnalyticDemographic> AnalyticDemographics { get; set; }
        //        public virtual DbSet<AnalyticQuestionAnswer> AnalyticQuestionAnswers { get; set; }
        //        public virtual DbSet<Answer_Lookup> Answer_Lookup { get; set; }
        //        public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }
        //        public virtual DbSet<Assessment> Assessments { get; set; }
        //        public virtual DbSet<Sector> Sectors { get; set; }
        //        public virtual DbSet<Sector_Industry> Sector_Industries { get; set; }
        //        public DbSet<PasswordHistory> PasswordHistories { get; set; }
        //        public DbSet<Configuration> Configurations { get; set; }



        //        public CsetContext(DbContextOptions<CsetContext> options) : base(options)
        //        {
        //        }

        //        public CsetContext() { }

        //        protected override void OnModelCreating(ModelBuilder builder)
        //        {
        //            base.OnModelCreating(builder);
        //            builder.Entity<Answer_Lookup>()
        //                .HasMany(e => e.AnalyticQuestionAnswers)
        //                .WithOne(e => e.Answer_Lookup).IsRequired()
        //                .OnDelete(DeleteBehavior.SetNull);

        //            builder.Entity<PasswordHistory>().HasOne(c => c.ApplicationUser).WithMany(c => c.PasswordHistories).HasForeignKey(f => f.AspNetUserId).HasForeignKey(f => f.CreatedUserId);
        //            builder.Entity<AnalyticQuestionAnswer>().Ignore(c => c.Assessment_Id);
        //            builder.Entity<AnalyticQuestionAnswer>().HasOne(a => a.Assessment).WithMany(q => q.Questions).HasForeignKey(f => f.Assessment_Id).OnDelete(DeleteBehavior.Cascade);
        //            builder.Entity<Assessment>().HasOne(c => c.ApplicationUser).WithMany(c => c.Assessments).HasForeignKey(f=>f.AssessmentCreatorId);

        //            builder.Entity<Answer_Lookup>().HasData(
        //                new Answer_Lookup() { Answer_Text = "A", Answer_Full_Name = "Alternate" },
        //                new Answer_Lookup() { Answer_Text = "N", Answer_Full_Name = "No" },
        //                new Answer_Lookup() { Answer_Text = "NA", Answer_Full_Name = "Not Applicable" },
        //                new Answer_Lookup() { Answer_Text = "U", Answer_Full_Name = "Unanswered" },
        //                new Answer_Lookup() { Answer_Text = "Y", Answer_Full_Name = "Yes" }
        //                );

        //            builder.Entity<AnalyticDemographic>()
        //            .HasMany(e => e.Assessments)
        //            .WithOne(e => e.AnalyticDemographic)
        //            .HasForeignKey(e => e.AnalyticDemographicId)
        //            .OnDelete(DeleteBehavior.SetNull);

        //            builder.Entity<Answer_Lookup>()
        //                .HasMany(e => e.AnalyticQuestionAnswers)
        //                .WithOne(e => e.Answer_Lookup)
        //                .HasForeignKey(e => e.Answer_Text)
        //                .OnDelete(DeleteBehavior.SetNull);

        //            builder.Entity<Sector>().HasData(new Sector() { SectorName = "Chemical Sector (Not Oil and Gas)", SectorId = 1 },
        //new Sector() { SectorName = "Commercial Facilities Sector", SectorId = 2 },
        //new Sector() { SectorName = "Communications Sector", SectorId = 3 },
        //new Sector() { SectorName = "Critical Manufacturing Sector", SectorId = 4 },
        //new Sector() { SectorName = "Dams Sector", SectorId = 5 },
        //new Sector() { SectorName = "Defense Industrial Base Sector", SectorId = 6 },
        //new Sector() { SectorName = "Emergency Services Sector", SectorId = 7 },
        //new Sector() { SectorName = "Energy Sector", SectorId = 8 },
        //new Sector() { SectorName = "Financial Services Sector", SectorId = 9 },
        //new Sector() { SectorName = "Food and Agriculture Sector", SectorId = 10 },
        //new Sector() { SectorName = "Government Facilities Sector", SectorId = 11 },
        //new Sector() { SectorName = "Healthcare and Public Health Sector", SectorId = 12 },
        //new Sector() { SectorName = "Information Technology Sector", SectorId = 13 },
        //new Sector() { SectorName = "Nuclear Reactors, Materials, and Waste Sector", SectorId = 14 },
        //new Sector() { SectorName = "Transportation Systems Sector", SectorId = 15 },
        //new Sector() { SectorName = "Water and Wastewater Systems Sector", SectorId = 16 });

        //            builder.Entity<Sector_Industry>()
        //                .HasKey(c => new { c.SectorId, c.IndustryId });
        //            builder.Entity<Sector_Industry>().HasData(
        //                new Sector_Industry() { SectorId = 1, IndustryId = 1, IndustryName = "Other" },
        //new Sector_Industry() { SectorId = 1, IndustryId = 78, IndustryName = "Basic Chemicals" },
        //new Sector_Industry() { SectorId = 1, IndustryId = 79, IndustryName = "Specialty Products" },
        //new Sector_Industry() { SectorId = 1, IndustryId = 80, IndustryName = "Pharmaceutical Products" },
        //new Sector_Industry() { SectorId = 1, IndustryId = 81, IndustryName = "Consumer Products" },
        //new Sector_Industry() { SectorId = 1, IndustryId = 82, IndustryName = "Agricultural Products" },
        //new Sector_Industry() { SectorId = 2, IndustryId = 2, IndustryName = "Entertainment and Media" },
        //new Sector_Industry() { SectorId = 2, IndustryId = 3, IndustryName = "Gaming" },
        //new Sector_Industry() { SectorId = 2, IndustryId = 4, IndustryName = "Lodging" },
        //new Sector_Industry() { SectorId = 2, IndustryId = 5, IndustryName = "Other" },
        //new Sector_Industry() { SectorId = 2, IndustryId = 6, IndustryName = "Outdoor Events" },
        //new Sector_Industry() { SectorId = 2, IndustryId = 7, IndustryName = "Public Assembly" },
        //new Sector_Industry() { SectorId = 2, IndustryId = 8, IndustryName = "Real Estate" },
        //new Sector_Industry() { SectorId = 2, IndustryId = 9, IndustryName = "Retail" },
        //new Sector_Industry() { SectorId = 2, IndustryId = 10, IndustryName = "Sports Leagues" },
        //new Sector_Industry() { SectorId = 3, IndustryId = 11, IndustryName = "Other" },
        //new Sector_Industry() { SectorId = 3, IndustryId = 12, IndustryName = "Telecommunications" },
        //new Sector_Industry() { SectorId = 3, IndustryId = 13, IndustryName = "Wireless Communications Service Providers" },
        //new Sector_Industry() { SectorId = 3, IndustryId = 83, IndustryName = "Broadcasting" },
        //new Sector_Industry() { SectorId = 3, IndustryId = 84, IndustryName = "Cable" },
        //new Sector_Industry() { SectorId = 3, IndustryId = 85, IndustryName = "Satellite" },
        //new Sector_Industry() { SectorId = 3, IndustryId = 86, IndustryName = "Wireline" },
        //new Sector_Industry() { SectorId = 4, IndustryId = 14, IndustryName = "Electrical Equipment, Appliance and Component Manufacturing" },
        //new Sector_Industry() { SectorId = 4, IndustryId = 15, IndustryName = "Machinery Manufacturing" },
        //new Sector_Industry() { SectorId = 4, IndustryId = 16, IndustryName = "Other" },
        //new Sector_Industry() { SectorId = 4, IndustryId = 17, IndustryName = "Primary Metal Manufacturing" },
        //new Sector_Industry() { SectorId = 4, IndustryId = 18, IndustryName = "Transportation and Heavy Equipment Manufacturing" },
        //new Sector_Industry() { SectorId = 4, IndustryId = 87, IndustryName = "Manufacturing" },
        //new Sector_Industry() { SectorId = 4, IndustryId = 88, IndustryName = "Heavy Machinery Manufacturing" },
        //new Sector_Industry() { SectorId = 5, IndustryId = 19, IndustryName = "Dams" },
        //new Sector_Industry() { SectorId = 5, IndustryId = 20, IndustryName = "Other" },
        //new Sector_Industry() { SectorId = 5, IndustryId = 21, IndustryName = "Private Hydropower Facilities in the US" },
        //new Sector_Industry() { SectorId = 5, IndustryId = 89, IndustryName = "Levees" },
        //new Sector_Industry() { SectorId = 5, IndustryId = 90, IndustryName = "Navigation Locks" },
        //new Sector_Industry() { SectorId = 5, IndustryId = 91, IndustryName = "Tailings and Waste Impoundments" },
        //new Sector_Industry() { SectorId = 6, IndustryId = 22, IndustryName = "Aircraft Industry" },
        //new Sector_Industry() { SectorId = 6, IndustryId = 23, IndustryName = "Ammunition" },
        //new Sector_Industry() { SectorId = 6, IndustryId = 24, IndustryName = "Combat Vehicle" },
        //new Sector_Industry() { SectorId = 6, IndustryId = 25, IndustryName = "Communications" },
        //new Sector_Industry() { SectorId = 6, IndustryId = 26, IndustryName = "Defense Contractors    " },
        //new Sector_Industry() { SectorId = 6, IndustryId = 27, IndustryName = "Electrical Industry Commodities" },
        //new Sector_Industry() { SectorId = 6, IndustryId = 28, IndustryName = "Electronics" },
        //new Sector_Industry() { SectorId = 6, IndustryId = 29, IndustryName = "Mechanical Industry Commodities" },
        //new Sector_Industry() { SectorId = 6, IndustryId = 30, IndustryName = "Missile Industry" },
        //new Sector_Industry() { SectorId = 6, IndustryId = 31, IndustryName = "Other" },
        //new Sector_Industry() { SectorId = 6, IndustryId = 32, IndustryName = "Research and Development Facilities" },
        //new Sector_Industry() { SectorId = 6, IndustryId = 33, IndustryName = "Shipbuilding Industry" },
        //new Sector_Industry() { SectorId = 6, IndustryId = 34, IndustryName = "Space" },
        //new Sector_Industry() { SectorId = 6, IndustryId = 35, IndustryName = "Structural Industry Commodities" },
        //new Sector_Industry() { SectorId = 6, IndustryId = 36, IndustryName = "Troop Support" },
        //new Sector_Industry() { SectorId = 6, IndustryId = 37, IndustryName = "Weapons" },
        //new Sector_Industry() { SectorId = 7, IndustryId = 38, IndustryName = "Emergency Management" },
        //new Sector_Industry() { SectorId = 7, IndustryId = 39, IndustryName = "Emergency Medical Services" },
        //new Sector_Industry() { SectorId = 7, IndustryId = 40, IndustryName = "Fire and Rescue Services" },
        //new Sector_Industry() { SectorId = 7, IndustryId = 41, IndustryName = "Law Enforcement    " },
        //new Sector_Industry() { SectorId = 7, IndustryId = 42, IndustryName = "Other" },
        //new Sector_Industry() { SectorId = 7, IndustryId = 43, IndustryName = "Public Works" },
        //new Sector_Industry() { SectorId = 8, IndustryId = 44, IndustryName = "Electric Power Generation, Transmission and Distribution      " },
        //new Sector_Industry() { SectorId = 8, IndustryId = 45, IndustryName = "Natural Gas      " },
        //new Sector_Industry() { SectorId = 8, IndustryId = 46, IndustryName = "Other" },
        //new Sector_Industry() { SectorId = 8, IndustryId = 47, IndustryName = "Petroleum Refineries" },
        //new Sector_Industry() { SectorId = 8, IndustryId = 92, IndustryName = "Oil and Natural Gas" },
        //new Sector_Industry() { SectorId = 9, IndustryId = 48, IndustryName = "Other" },
        //new Sector_Industry() { SectorId = 9, IndustryId = 49, IndustryName = "US Banks" },
        //new Sector_Industry() { SectorId = 9, IndustryId = 50, IndustryName = "US Credit Unions" },
        //new Sector_Industry() { SectorId = 9, IndustryId = 93, IndustryName = "Consumer Services" },
        //new Sector_Industry() { SectorId = 9, IndustryId = 94, IndustryName = "Credit and Liquidity Products" },
        //new Sector_Industry() { SectorId = 9, IndustryId = 95, IndustryName = "Investment Products" },
        //new Sector_Industry() { SectorId = 9, IndustryId = 96, IndustryName = "Risk Transfer Products" },
        //new Sector_Industry() { SectorId = 10, IndustryId = 51, IndustryName = "Beverage Manufacturing Plants" },
        //new Sector_Industry() { SectorId = 10, IndustryId = 52, IndustryName = "Food Manufacturing Plants" },
        //new Sector_Industry() { SectorId = 10, IndustryId = 53, IndustryName = "Food Services" },
        //new Sector_Industry() { SectorId = 10, IndustryId = 54, IndustryName = "Other" },
        //new Sector_Industry() { SectorId = 10, IndustryId = 97, IndustryName = "Supply" },
        //new Sector_Industry() { SectorId = 10, IndustryId = 98, IndustryName = "Processing, Packaging, and Production" },
        //new Sector_Industry() { SectorId = 10, IndustryId = 99, IndustryName = "Product Storage" },
        //new Sector_Industry() { SectorId = 10, IndustryId = 100, IndustryName = "Product Transportation" },
        //new Sector_Industry() { SectorId = 10, IndustryId = 101, IndustryName = "Product Distribution" },
        //new Sector_Industry() { SectorId = 10, IndustryId = 102, IndustryName = "Supporting Facilities" },
        //new Sector_Industry() { SectorId = 11, IndustryId = 55, IndustryName = "Local Governments" },
        //new Sector_Industry() { SectorId = 11, IndustryId = 56, IndustryName = "Other" },
        //new Sector_Industry() { SectorId = 11, IndustryId = 57, IndustryName = "State Governments" },
        //new Sector_Industry() { SectorId = 11, IndustryId = 58, IndustryName = "Territorial Governments" },
        //new Sector_Industry() { SectorId = 11, IndustryId = 59, IndustryName = "Tribal Governments" },
        //new Sector_Industry() { SectorId = 11, IndustryId = 103, IndustryName = "Public Facilities" },
        //new Sector_Industry() { SectorId = 11, IndustryId = 104, IndustryName = "Non-Public Facilities" },
        //new Sector_Industry() { SectorId = 12, IndustryId = 60, IndustryName = "Hospitals" },
        //new Sector_Industry() { SectorId = 12, IndustryId = 61, IndustryName = "Other" },
        //new Sector_Industry() { SectorId = 12, IndustryId = 62, IndustryName = "Residential Care Facilities" },
        //new Sector_Industry() { SectorId = 12, IndustryId = 105, IndustryName = "Direct Patient Care" },
        //new Sector_Industry() { SectorId = 12, IndustryId = 106, IndustryName = "Health IT" },
        //new Sector_Industry() { SectorId = 12, IndustryId = 107, IndustryName = "Health Plans and Payers" },
        //new Sector_Industry() { SectorId = 12, IndustryId = 108, IndustryName = "Fatality Management Services" },
        //new Sector_Industry() { SectorId = 12, IndustryId = 109, IndustryName = "Medical Materials" },
        //new Sector_Industry() { SectorId = 12, IndustryId = 110, IndustryName = "Support Services" },
        //new Sector_Industry() { SectorId = 13, IndustryId = 63, IndustryName = "Information Technology" },
        //new Sector_Industry() { SectorId = 13, IndustryId = 64, IndustryName = "Other" },
        //new Sector_Industry() { SectorId = 13, IndustryId = 111, IndustryName = "IT Production" },
        //new Sector_Industry() { SectorId = 13, IndustryId = 112, IndustryName = "DNS Services" },
        //new Sector_Industry() { SectorId = 13, IndustryId = 113, IndustryName = "Identity and Trust Support Management" },
        //new Sector_Industry() { SectorId = 13, IndustryId = 114, IndustryName = "Internet Content and Service Providers" },
        //new Sector_Industry() { SectorId = 13, IndustryId = 115, IndustryName = "Internet Routing and Connection" },
        //new Sector_Industry() { SectorId = 13, IndustryId = 116, IndustryName = "Incident Management" },
        //new Sector_Industry() { SectorId = 14, IndustryId = 65, IndustryName = "Operating Nuclear Power Plants" },
        //new Sector_Industry() { SectorId = 14, IndustryId = 66, IndustryName = "Other" },
        //new Sector_Industry() { SectorId = 14, IndustryId = 117, IndustryName = "Fuel Cycle Facilities" },
        //new Sector_Industry() { SectorId = 14, IndustryId = 118, IndustryName = "Nuclear Materials Transport" },
        //new Sector_Industry() { SectorId = 14, IndustryId = 119, IndustryName = "Radioactive Waste" },
        //new Sector_Industry() { SectorId = 14, IndustryId = 120, IndustryName = "Radioactive Materials" },
        //new Sector_Industry() { SectorId = 15, IndustryId = 67, IndustryName = "Aviation" },
        //new Sector_Industry() { SectorId = 15, IndustryId = 68, IndustryName = "Freight Rail" },
        //new Sector_Industry() { SectorId = 15, IndustryId = 69, IndustryName = "Highway (truck transportation)" },
        //new Sector_Industry() { SectorId = 15, IndustryId = 70, IndustryName = "Maritime" },
        //new Sector_Industry() { SectorId = 15, IndustryId = 71, IndustryName = "Mass Transit and Passenger Rail" },
        //new Sector_Industry() { SectorId = 15, IndustryId = 72, IndustryName = "Municipalities with Traffic Control Systems" },
        //new Sector_Industry() { SectorId = 15, IndustryId = 73, IndustryName = "Other" },
        //new Sector_Industry() { SectorId = 15, IndustryId = 74, IndustryName = "Pipelines (carries natural gas, hazardous liquids, and various chemicals.)" },
        //new Sector_Industry() { SectorId = 16, IndustryId = 75, IndustryName = "Other" },
        //new Sector_Industry() { SectorId = 16, IndustryId = 76, IndustryName = "Public Water Systems" },
        //new Sector_Industry() { SectorId = 16, IndustryId = 77, IndustryName = "Publicly Owned Treatment Works" });


        //            builder.Entity<Sector_Industry>()
        //                .HasMany(e => e.AnalyticDemographics)
        //                .WithOne(e => e.Sector_Industry)
        //                .HasForeignKey(e => new { e.SectorId, e.IndustryId })
        //                .OnDelete(DeleteBehavior.SetNull);
        //            builder.Entity<ApplicationUser>()
        //                .HasMany(e => e.Assessments)
        //                .WithOne(e => e.ApplicationUser)
        //                .HasForeignKey(e => e.AssessmentCreatorId)
        //                .OnDelete(DeleteBehavior.SetNull);
        //        }

        //        /// <summary>
        //        /// Returns a DbSet TEntity> instance for access to entities of the given type in the context and the underlying store
        //        /// </summary>
        //        /// <typeparam name="TEntity">The entity type for which a set should be returned</typeparam>
        //        /// <returns>An IDbSet instance for the given entity type</returns>
        //        public new DbSet<TEntity> Set<TEntity>() where TEntity : class
        //        {
        //            return base.Set<TEntity>();
        //        }

        //        /// <summary>
        //        /// Saves all changes made in this context to the underlying database
        //        /// </summary>
        //        /// <returns>The number of objects written to the underlying database</returns>
        //        public new int SaveChanges()
        //        {
        //            return base.SaveChanges();
        //        }

        //        public new EntityEntry Entry(object entity)
        //        {
        //            return base.Entry(entity);
        //        }

        //        /// <summary>
        //        /// Executes the given DDL/DML command against the database
        //        /// </summary>
        //        /// <param name="sql">The command string</param>
        //        /// <param name="parameters">The parameters to apply to the command string</param>
        //        /// <returns>The result returned by the database after executing the command</returns>
        //        public int ExecuteSqlCommand(string sql, params object[] parameters)
        //        {
        //            return this.Database.ExecuteSqlRaw(sql, parameters);
        //        }

        //        //NOTE: Dispose code and comments below generated by Resharper
        //        private bool _disposedValue = false; // To detect redundant calls

        //        protected virtual void Dispose(bool disposing)
        //        {
        //            if (!_disposedValue)
        //            {
        //                if (disposing)
        //                {
        //                    // TODO: dispose managed state (managed objects).
        //                }

        //                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
        //                // TODO: set large fields to null.

        //                _disposedValue = true;
        //            }
        //        }

        //        /// <summary>
        //        /// Disposes the context. The underlying ObjectContext and connection to the database (DbConnection) are disposed, if it
        //        /// was created by this context or ownership was passed to this context when this context was created.
        //        /// </summary>
        //        public new void Dispose()
        //        {
        //            Dispose(true);
        //        }
    }
}
