using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using CsetAnalytics.DomainModels.Models;
using CsetAnalytics.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CsetAnalytics.DomainModels
{
    public class DatabaseInitializer
    {

        public static void SeedCollections(IMongoDbSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            if (client != null)
            {
                var database = client.GetDatabase(settings.DatabaseName);
                CreateCollection(database, "assessments");
                CreateCollection(database, "analytics_questionanswer");
                CreateCollection(database, "sector_industries");
            }
            
        }
        
        private static void CreateCollection(IMongoDatabase database, string collectionName)
        {
            var filter = new BsonDocument("name", collectionName);
            var collectionCursor = database.ListCollections(new ListCollectionsOptions { Filter = filter });
            if (!collectionCursor.Any())
            {
                database.CreateCollection(collectionName);

                if (collectionName == "sector_industries")
                {
                    SeedSectorIndustries(database);
                }
            }
           
        }

        private static void SeedSectorIndustries(IMongoDatabase database)
        {
            IMongoCollection<Sector> collection = database.GetCollection<Sector>("sector_industries");

           
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
                                             new Industry() { IndustryId = 83, IndustryName = "Broadcasting"}, 
                                             new Industry() { IndustryId = 84, IndustryName = "Cable" },
                                             new Industry() { IndustryId = 85, IndustryName = "Satellite" },
                                             new Industry() { IndustryId = 86, IndustryName = "Wireline" }
            };

            Industry[] sector4Industires = { new Industry() { IndustryId = 14, IndustryName = "Electrical Equipment, Appliance and Component Manufacturing" },
                                             new Industry() { IndustryId = 15, IndustryName = "Machinery Manufacturing" },
                                             new Industry() { IndustryId = 16, IndustryName = "Other" },
                                             new Industry() { IndustryId = 17, IndustryName = "Primary Metal Manufacturing" },
                                             new Industry() { IndustryId = 18, IndustryName = "Transportation and Heavy Equipment Manufacturing" },
                                             new Industry() { IndustryId = 87, IndustryName = "Manufacturing" },
                                             new Industry() { IndustryId = 88, IndustryName = "Heavy Machinery Manufacturing" }
            };

            Industry[] sector5Industires = { new Industry() { IndustryId = 19, IndustryName = "Dams" },
                                             new Industry() { IndustryId = 20, IndustryName = "Other" },
                                             new Industry() { IndustryId = 21, IndustryName = "Private Hydropower Facilities in the US" },
                                             new Industry() { IndustryId = 89, IndustryName = "Levees" },
                                             new Industry() { IndustryId = 90, IndustryName = "Navigation Locks" },
                                             new Industry() { IndustryId = 91, IndustryName = "Tailings and Waste Impoundments" },
            };

            Industry[] sector6Industries = { new Industry() { IndustryId = 22, IndustryName = "Aircraft Industry" },
                                             new Industry() { IndustryId = 23, IndustryName = "Ammunition" },
                                             new Industry() { IndustryId = 24, IndustryName = "Combat Vehicle" },
                                             new Industry() { IndustryId = 25, IndustryName = "Communications" },
                                             new Industry() { IndustryId = 26, IndustryName = "Defense Contractors" },
                                             new Industry() { IndustryId = 27, IndustryName = "Electrical Industry Commodities" },
                                             new Industry() { IndustryId = 28, IndustryName = "Electronics" },
                                             new Industry() { IndustryId = 29, IndustryName = "Mechanical Industry Commodities" },
                                             new Industry() { IndustryId = 30, IndustryName = "Missile Industry" },
                                             new Industry() { IndustryId = 31, IndustryName = "Other" },
                                             new Industry() { IndustryId = 32, IndustryName = "Research and Development Facilities" },
                                             new Industry() { IndustryId = 33, IndustryName = "Shipbuilding Industry" },
                                             new Industry() { IndustryId = 34, IndustryName = "Space" },
                                             new Industry() { IndustryId = 35, IndustryName = "Structural Industry Commodities" },
                                             new Industry() { IndustryId = 36, IndustryName = "Troop Support" },
                                             new Industry() { IndustryId = 37, IndustryName = "Weapons" },
            };

            Industry[] sector7Industries = { new Industry() { IndustryId = 38, IndustryName = "Emergency Management" },
                                             new Industry() { IndustryId = 39, IndustryName = "Emergency Medical Services" },
                                             new Industry() { IndustryId = 40, IndustryName = "Fire and Rescue Services" },
                                             new Industry() { IndustryId = 41, IndustryName = "Law Enforcement" },
                                             new Industry() { IndustryId = 42, IndustryName = "Other" },
                                             new Industry() { IndustryId = 43, IndustryName = "Public Works" },
            };

            Industry[] sector8Industries = { new Industry() { IndustryId = 44, IndustryName = "Electric Power Generation, Transmission and Distribution" },
                                             new Industry() { IndustryId = 45, IndustryName = "Natural Gas" },
                                             new Industry() { IndustryId = 46, IndustryName = "Other" },
                                             new Industry() { IndustryId = 47, IndustryName = "Petroleum Refineries" },
                                             new Industry() { IndustryId = 92, IndustryName = "Oil and Natural Gas" },
            };

            Industry[] sector9industries = { new Industry() { IndustryId = 48, IndustryName = "Other" },
                                             new Industry() { IndustryId = 49, IndustryName = "US Banks" },
                                             new Industry() { IndustryId = 50, IndustryName = "US Credit Unions" },
                                             new Industry() { IndustryId = 93, IndustryName = "Consumer Services" },
                                             new Industry() { IndustryId = 94, IndustryName = "Credit and Liquidity Products" },
                                             new Industry() { IndustryId = 95, IndustryName = "Investment Products" },
                                             new Industry() { IndustryId = 96, IndustryName = "Risk Transfer Products" },
            };

            Industry[] sector10Industries = { new Industry() { IndustryId = 51, IndustryName = "Beverage Manufacturing Plants" },
                                              new Industry() { IndustryId = 52, IndustryName = "Food Manufacturing Plants" },
                                              new Industry() { IndustryId = 53, IndustryName = "Food Services" },
                                              new Industry() { IndustryId = 54, IndustryName = "Other" },
                                              new Industry() { IndustryId = 97, IndustryName = "Supply" },
                                              new Industry() { IndustryId = 98, IndustryName = "Processing, Packaging, and Production" },
                                              new Industry() { IndustryId = 99, IndustryName = "Product Storage" },
                                              new Industry() { IndustryId = 100, IndustryName = "Product Transportation" },
                                              new Industry() { IndustryId = 101, IndustryName = "Product Distribution" },
                                              new Industry() { IndustryId = 102, IndustryName = "Supporting Facilities" },
            };

            Industry[] sector11Industries = { new Industry() { IndustryId = 55, IndustryName = "Local Governments" },
                                              new Industry() { IndustryId = 56, IndustryName = "Other" },
                                              new Industry() { IndustryId = 57, IndustryName = "State Governments" },
                                              new Industry() { IndustryId = 58, IndustryName = "Territorial Governments" },
                                              new Industry() { IndustryId = 59, IndustryName = "Tribal Governments" },
                                              new Industry() { IndustryId = 103, IndustryName = "Public Facilities" },
                                              new Industry() { IndustryId = 104, IndustryName = "Non-Public Facilities" },
            };

            Industry[] sector12Industries = { new Industry() { IndustryId = 60, IndustryName = "Hospitals" },
                                              new Industry() { IndustryId = 61, IndustryName = "Other" },
                                              new Industry() { IndustryId = 62, IndustryName = "Residential Care Facilities" },
                                              new Industry() { IndustryId = 105, IndustryName = "Direct Patient Care" },
                                              new Industry() { IndustryId = 106, IndustryName = "Health IT" },
                                              new Industry() { IndustryId = 107, IndustryName = "Health Plans and Payers" },
                                              new Industry() { IndustryId = 108, IndustryName = "Fatality Management Services" },
                                              new Industry() { IndustryId = 109, IndustryName = "Medical Materials" },
                                              new Industry() { IndustryId = 110, IndustryName = "Support Services" },
            };

            Industry[] sector13Industries = { new Industry() { IndustryId = 63, IndustryName = "Information Technology" },
                                              new Industry() { IndustryId = 64, IndustryName = "Other" },
                                              new Industry() { IndustryId = 111, IndustryName = "IT Production" },
                                              new Industry() { IndustryId = 112, IndustryName = "DNS Services" },
                                              new Industry() { IndustryId = 113, IndustryName = "Identity and Trust Support Management" },
                                              new Industry() { IndustryId = 114, IndustryName = "Internet Content and Service Providers" },
                                              new Industry() { IndustryId = 115, IndustryName = "Internet Routing and Connection" },
                                              new Industry() { IndustryId = 116, IndustryName = "Incident Management" },
            };

            Industry[] sector14Industries = { new Industry() { IndustryId = 65, IndustryName = "Operating Nuclear Power Plants" },
                                              new Industry() { IndustryId = 66, IndustryName = "Other" },
                                              new Industry() { IndustryId = 117, IndustryName = "Fuel Cycle Facilities" },
                                              new Industry() { IndustryId = 118, IndustryName = "Nuclear Materials Transport" },
                                              new Industry() { IndustryId = 119, IndustryName = "Radioactive Waste" },
                                              new Industry() { IndustryId = 120, IndustryName = "Radioactive Materials" },
            };

            Industry[] sector15Industries = { new Industry() { IndustryId = 67, IndustryName = "Aviation" },
                                              new Industry() { IndustryId = 68, IndustryName = "Freight Rail" },
                                              new Industry() { IndustryId = 69, IndustryName = "Highway (truck transportation)" },
                                              new Industry() { IndustryId = 70, IndustryName = "Maritime" },
                                              new Industry() { IndustryId = 71, IndustryName = "Mass Transit and Passenger Rail" },
                                              new Industry() { IndustryId = 72, IndustryName = "Municipalities with Traffic Control Systems" },
                                              new Industry() { IndustryId = 73, IndustryName = "Other" },
                                              new Industry() { IndustryId = 74, IndustryName = "Pipelines (carries natural gas, hazardous liquids, and various chemicals.)" },
            };

            Industry[] sector16Industries = { new Industry() { IndustryId = 75, IndustryName = "Other" },
                                              new Industry() { IndustryId = 76, IndustryName = "Public Water Systems" },
                                              new Industry() { IndustryId = 77, IndustryName = "Publicly Owned Treatment Works" }
            };

            // Format for sector industries data:

            // {sectorId, sectorName, [{industryId, IndustryName} ... ]}

            List<Sector> Sectors = new List<Sector>
            {
                new Sector() { SectorName = "Chemical Sector (Not Oil and Gas)", SectorId = "1", Industries = new List<Industry>(sector1Industries) },
                new Sector() { SectorName = "Commercial Facilities Sector", SectorId = "2", Industries = new List<Industry>(sector2Industries) },
                new Sector() { SectorName = "Communications Sector", SectorId = "3", Industries = new List<Industry>(sector3Industries) },
                new Sector() { SectorName = "Critical Manufacturing Sector", SectorId = "4", Industries = new List<Industry>(sector4Industires) },
                new Sector() { SectorName = "Dams Sector", SectorId = "5", Industries = new List<Industry>(sector5Industires) },
                new Sector() { SectorName = "Defense Industrial Base Sector", SectorId = "6", Industries = new List<Industry>(sector6Industries) },
                new Sector() { SectorName = "Emergency Services Sector", SectorId = "7", Industries = new List<Industry>(sector7Industries) },
                new Sector() { SectorName = "Energy Sector", SectorId = "8", Industries = new List<Industry>(sector8Industries) },
                new Sector() { SectorName = "Financial Services Sector", SectorId = "9", Industries = new List<Industry>(sector9industries)},
                new Sector() { SectorName = "Food and Agriculture Sector", SectorId = "10", Industries = new List<Industry>(sector10Industries) },
                new Sector() { SectorName = "Government Facilities Sector", SectorId = "11", Industries = new List<Industry>(sector11Industries) },
                new Sector() { SectorName = "Healthcare and Public Health Sector", SectorId = "12", Industries = new List<Industry>(sector12Industries) },
                new Sector() { SectorName = "Information Technology Sector", SectorId = "13", Industries = new List<Industry>(sector13Industries) },
                new Sector() { SectorName = "Nuclear Reactors, Materials, and Waste Sector", SectorId = "14", Industries = new List<Industry>(sector14Industries) },
                new Sector() { SectorName = "Transportation Systems Sector", SectorId = "15", Industries = new List<Industry>(sector15Industries) },
                new Sector() { SectorName = "Water and Wastewater Systems Sector", SectorId = "16", Industries = new List<Industry>(sector16Industries) }
            };
    
            collection.InsertMany(Sectors);
           
        }
    }
}
