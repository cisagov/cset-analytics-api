using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using CsetAnalytics.DomainModels.Models;
using CsetAnalytics.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace CsetAnalytics.DomainModels
{
    public class DatabaseInitializer
    {

        public static async Task SeedCollections(IMongoDbSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            if (client != null)
            {
                var database = client.GetDatabase(settings.DatabaseName);
                await CreateCollection(database, "assessments");
                await CreateCollection(database, "analytics_questionanswer");
                await CreateCollection(database, "sector_industries");
            }
            
        }
        
        private static async Task CreateCollection(IMongoDatabase database, string collectionName)
        {
            var filter = new BsonDocument("name", collectionName);
            var collectionCursor = await database.ListCollectionsAsync(new ListCollectionsOptions { Filter = filter });
            if (!collectionCursor.Any())
            {
                database.CreateCollection(collectionName);

            }

            if (collectionName == "sector_industries")
            {
                await SeedSectorIndustries(database);
            }
        }

        private static async Task SeedSectorIndustries(IMongoDatabase database)
        {
            var collection = database.GetCollection<BsonDocument>("sector_industries");

           
            Industry[] sector1Industries = { new Industry() { IndustryId = 1, IndustryName = "Other" },
                                             new Industry() { IndustryId = 78, IndustryName = "Basic Chemicals" },
                                             new Industry() { IndustryId = 79, IndustryName = "Specialty Products" },
                                             new Industry() { IndustryId = 80, IndustryName = "Pharmaceutical Products" },
                                             new Industry() { IndustryId = 81, IndustryName = "Consumer Products" },
                                             new Industry() { IndustryId = 82, IndustryName = "Agricultural Products" }
            };

            Industry[] sector2Industries = { new Industry() { IndustryId = 2, IndustryName = "Entertainment and Media" },
                                             new Industry() { IndustryId = 3, IndustryName = "Gaming" },
                                             new Industry() { IndustryId = 4, IndustryName = "Lodging" },
                                             new Industry() { IndustryId = 5, IndustryName = "Other" },
                                             new Industry() { IndustryId = 6, IndustryName = "Outdoor Events" },
                                             new Industry() { IndustryId = 7, IndustryName = "Public Assembly" },
                                             new Industry() { IndustryId = 8, IndustryName = "Real Estate" },
                                             new Industry() { IndustryId = 9, IndustryName = "Retail" },
                                             new Industry() { IndustryId = 10, IndustryName = "Sports Leagues" },
                                             new Industry() { IndustryId = 83, IndustryName = "Broadcasting" }

            };

            Industry[] sector3Industries = { new Industry() { IndustryId = 11, IndustryName = "Other" },
                                             new Industry() { IndustryId = 12, IndustryName = "Telecommunications" },
                                             new Industry() { IndustryId = 13, IndustryName = "Wireless Communications Service Providers" },
                                             new Industry() { IndustryId = 84, IndustryName = "Cable" },
                                             new Industry() { IndustryId = 85, IndustryName = "Satellite" },
                                             new Industry() { IndustryId = 86, IndustryName = "Wireline" }
            };


            // Format for sector industries data:

            // {sectorId, sectorName, [{industryId, IndustryName} ... ]}

            List<BsonDocument> Sectors = new List<BsonDocument>
            {
                new Sector() { SectorId = "1", SectorName = "Chemical Sector (Not Oil and Gas)", Industries = new List<Industry>(sector1Industries) }.ToBsonDocument(),
                new Sector() { SectorId = "2", SectorName = "Commercial Facilities Sector", Industries = new List<Industry>(sector2Industries) }.ToBsonDocument(),
                new Sector() { SectorId = "3", SectorName = "Communications Sector", Industries = new List<Industry>(sector3Industries) }.ToBsonDocument()
            };

            await collection.InsertManyAsync(Sectors);
        }
    }
    // Sectors

    //new Sector() { SectorName = "Chemical Sector (Not Oil and Gas)", SectorId = 1 },
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

    // Industries

    //new Sector_Industry() { SectorId = 1, IndustryId = 1, IndustryName = "Other" },
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

}
