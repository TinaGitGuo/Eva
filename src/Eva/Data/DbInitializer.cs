using EVA.Data;
using EVA.Models.ManageViewModels;
using System;
using System.Linq;

namespace EVA.Models
{
    public class DbInitializer
    {
        public static void Initialize(EVAContext context)
        {
            context.Database.EnsureCreated();

            if (context.Locations.Any())
            {
                return; //DB seeded
            }

            var locations = new Location[]
            {
                new Location { iLocation="Indoor"},
                new Location {iLocation="Outdoor" }
            };

            foreach (Location l in locations)
            {
                context.Locations.Add(l);
            }
            context.SaveChanges();

            var divisions = new Division[]
           {
                new Division { iDivision="Aquatics"},
                new Division { iDivision="Electrical"},
                new Division { iDivision="Back Office"}
           };

            foreach (Division d in divisions)
            {
                context.Divisions.Add(d);
            }
            context.SaveChanges();

            var departments = new Department[]
            {
                new Department { DivisionID=1,iDepartment="Construction"},
                new Department { DivisionID=1,iDepartment="Maintenance"},
                new Department { DivisionID=1,iDepartment="Operations"},
                new Department { DivisionID=1,iDepartment="Environmental"}

            };

            foreach(Department i in departments)
            {
                context.Departments.Add(i);
            }

            context.SaveChanges();

            var staffs = new Staff[]
            {
                //new Staff {DivisionID=1, StaffName="Connor Gordon",HireDate=DateTime.Parse("03/03/2017")  },
                //new Staff { DivisionID=1, StaffName="David Watson", HireDate=DateTime.Parse("23/09/1996")},
                //new Staff {DivisionID=1, StaffName="Carrie Barretto", HireDate=DateTime.Parse("01/09/2015") },
                //new Staff {DivisionID=1, StaffName="Scott Tuckerman", HireDate=DateTime.Parse("18/01/2016") },
                new Staff {DivisionID=1, StaffName="Dave Hastings" },
                new Staff {DivisionID=1, StaffName="Garry Rhodes" },
                new Staff {DivisionID=1, StaffName="Jamie Prieto" },
                new Staff {DivisionID=1, StaffName="Peter Lambert" }
            };

            foreach(Staff s in staffs)
            {
                context.Staffs.Add(s);
            }
            context.SaveChanges();



            var eqType = new EqType[]
            {
                new EqType {EquipmentType="Circulation Pump", EQFunction="Provides circulation of the water body" },
                new EqType {EquipmentType="Foot Valve", EQFunction="Prevents back flow of water out of the circulation pump system on stopping so water will start again when power is restored" },
                new EqType {EquipmentType="Vacuum Gauge", EQFunction="Reading of vacuum, often termed head" },
                new EqType {EquipmentType="Pre-pump Strainer", EQFunction="Capture particulates and debris prior to the pump assists in preventing pump impellor becoming blocked" },
                new EqType {EquipmentType="Check Valve", EQFunction="Non return valve.  Main function is to assist in preventing blow back on pump stop pushing water out of the pre-pump strainer and possible pump damage" },
                new EqType {EquipmentType="Pump Discharge Pressure Gauge", EQFunction ="Gauge measures the back pressure from the system all the way back to the pump.  Important information to understand the pump is performing correctly."},
                new EqType {EquipmentType="Dosing Sample Take Off", EQFunction="Directs water to the dosing controller" },
                new EqType {EquipmentType="Chlorine injector", EQFunction="Introduces chlorine to water flow providing sanitation" },
                new EqType {EquipmentType="Chlorine Booster Pump", EQFunction = "Boosts water flow and pressure prior to chlorine gas ejector" },
                new EqType {EquipmentType="Chlorine Gas Ejector", EQFunction="Creates a vacuum condition to suck chlorine gas into the water" },
                new EqType {EquipmentType="Chlorine Gas Rate Meter", EQFunction="Measures dosage rate of chlorine gas being introduced to system" },
                new EqType {EquipmentType="Chlorine Probe", EQFunction="This probe measures chlorine as chlorine in line with the test kit" },
                new EqType {EquipmentType="ORP Probe", EQFunction="ORP Probes measure Oxidation Reduction Potential.  This is in theory the kill rate for the sanitiser.  Method of measurement is not recognised by Health and meaningless outside of the instrument" },
                new EqType {EquipmentType="pH Probe", EQFunction="Monitoring of the water pH measurement" },
                new EqType {EquipmentType="Chemical Controller", EQFunction="Using proprietary software takes the probe measurements and calculates a chemical dose" },
                new EqType {EquipmentType="Chlorine Dosing Pump", EQFunction="Adds chlorine to water as the chemical controller instructs" },
                new EqType {EquipmentType="Acid Dosing Pump", EQFunction="Adds an acid to the water as the chemical controller instructs" },
                new EqType {EquipmentType="Alkali Dosing Pump", EQFunction="Adds an alkali to the water as the chemical controller instructs" },
                new EqType {EquipmentType="Chlorine Tank", EQFunction="Holds a larger volume of chlorine for dosing" },
                new EqType {EquipmentType="Acid Tank", EQFunction="Holds a larger volume of acid for dosing" },
                new EqType {EquipmentType="Chlorine Bund", EQFunction="Container designed to provide 110% holding capcity of the chlorine tank in the event of a spill or leak from the tank" },
                new EqType {EquipmentType="Acid Bund", EQFunction="Container designed to provide 110% holding capcity of the acid tank in the event of a spill or leak from tank" },
                new EqType {EquipmentType="Alkali Tank", EQFunction="Holds a larger volume of alkali for dosing" },
                new EqType {EquipmentType="Tank Stirrer", EQFunction="Stirs the water and chemical to assist in holding the chemical in solution for dosing" },
                new EqType {EquipmentType="Pipework", EQFunction="Guides and contains water to the various locations for treatment" },
                new EqType {EquipmentType="Sand Filter", EQFunction="Using sand as media; removes suspended matter from the water and provides cleaned filtrate" },
                new EqType {EquipmentType="Cartridge Filter", EQFunction="Using cartridge as media; removes suspended matter from the water and provides cleaned filtrate"  },
                new EqType {EquipmentType="Diatomaceous Earth Filter", EQFunction="Using a diatomaceous earth, perlite or cellulose fibre coats septums to provide removal of suspended matter from water" },
                new EqType {EquipmentType="Heat Exhanger", EQFunction="Uses heat from another source, such as a boiler, to provide heating to the water" },
                new EqType {EquipmentType="Heat Booster Pump", EQFunction="Boosts flow and pressure to Heat Exchanger" },
                new EqType {EquipmentType="Solar Booster Pump", EQFunction="Boosts flow and pressure to Solar system" },
                new EqType {EquipmentType="Solar Pool Heating", EQFunction="Sits in sun and warms water passing through it" },
                new EqType {EquipmentType="Solar Controller", EQFunction="Takes temperature readings from pipe work, from roof and any other location; compares all and instructs solar booster pump to run according to gains" },
                new EqType {EquipmentType="Heatpac", EQFunction="Complete gas heating system manufactured from multiple units for heat sources building in redundancy" },
                new EqType {EquipmentType="Heat Pump", EQFunction="Reverse cycle air conditioning unit transfers heat from air to water (typically)" },
                new EqType {EquipmentType="Atmospheric Gas Heater", EQFunction="Water is passed to the heater directly.  Typically used in domestic situation.  Limited lifespan typically 5 years" },
                new EqType {EquipmentType="Forced Fire Gas Heater", EQFunction="This is a special type of gas heater.  Air is forced into the combustion chamber with air being blown in and compressed before being ignited.  Improves efficiency.  Typical life span 20 years" },
                new EqType {EquipmentType="Electrical Control Board", EQFunction="Master control for all electrical items in the plant" },
                new EqType {EquipmentType="Flow Switch", EQFunction="Flow switch provides no flow protection to pumps and associated equipment" },
                new EqType {EquipmentType="Ultrasonic Level Control", EQFunction="Uses ultrasonic beam to detect levels" },

            };

            foreach (EqType e in eqType)
            {
                context.EqTypes.Add(e);
            }

            context.SaveChanges();

            var construction = new Construction[]
            {
                new Construction {iConstruction="Concrete Plaster" },
                new Construction {iConstruction="Tiled" },
                new Construction {iConstruction="Concrete Painted" },
                new Construction {iConstruction="Vinyl Liner" },
                new Construction {iConstruction="Fiberglass" }
            };

            foreach (Construction o in construction)
            {
                context.Constructions.Add(o);
            }

            context.SaveChanges();


            var condition = new Condition[]
            {
                new Condition {EqCondition="Okay and serviceable" },
                new Condition {EqCondition="Okay needs service shortly" },
                new Condition {EqCondition="Not Okay needs service" },
                new Condition {EqCondition="Not Okay should be replacd" },
                new Condition {EqCondition="Okay but needs replacment" }
            };

            foreach (Condition c in condition)
            {
                context.Conditions.Add(c);
            }

            context.SaveChanges();


            var makes = new Make[]
            {
                new Make {EqMake="Regent" },
                new Make {EqMake="Sta - Rite" },
                new Make {EqMake="Astral" },
                new Make {EqMake="Jandy" },
                new Make {EqMake="Hayward" },
                new Make {EqMake="Waterco" },
                new Make {EqMake="Poolrite" },
                new Make {EqMake="Davey" },
                new Make {EqMake="Southern Cross" },
                new Make {EqMake="Aqua Plus" },
                new Make {EqMake="Prominent" },
                new Make {EqMake="Stranco" },
                new Make {EqMake="Betz" },
                new Make {EqMake="Pool Controls" },
                new Make {EqMake="Accent" },
                new Make {EqMake="Speck" },
                new Make {EqMake="Chadson" },
                new Make {EqMake="Aqua Demand" }
            };

            foreach (Make m in makes)
            {
                context.Makes.Add(m);
            }

            context.SaveChanges();

            var positions = new Position[]
            {
                new Position { iPosition="School Bursar"},
                new Position {iPosition="Property Manager" },
                new Position {iPosition="Shift Engineer" },
                new Position {iPosition="Building Manager" },
                new Position {iPosition="Strata Manager" },
                new Position {iPosition="Accountant" }
            };

            foreach (Position p in positions)
            {
                context.Positions.Add(p);
            }
            context.SaveChanges();

            var contactType = new ContactType[]
            {
                new ContactType {iContactType="Landline" },
                new ContactType {iContactType="Mobile" },
                new ContactType {iContactType="Email" },
                new ContactType {iContactType="Twitter" },
                new ContactType {iContactType="Personal Mobile" },
                new ContactType {iContactType="Home Landline" }
            };

            foreach (ContactType c in contactType)
            {
                context.ContactTypes.Add(c);
            }

            context.SaveChanges();

            var healthCategory = new HealthCategory[]
            {
                new HealthCategory {CategoryID=1,PeakLoad=1 ,CategoryDescription="Spas", TurnoverTime=15,WaterAllowance=10,DailyLoad=9.6M, Sand=400, DE=60,Cartridge=12 },
                new HealthCategory {CategoryID=2,PeakLoad=2, CategoryDescription="Extreme", TurnoverTime=30,WaterAllowance=8,DailyLoad=6, Sand=400, DE=60,Cartridge=12 },
                new HealthCategory {CategoryID=3,PeakLoad=2, CategoryDescription="Very High", TurnoverTime=60,WaterAllowance=7,DailyLoad=3.4M, Sand=400, DE=60,Cartridge=12 },
                new HealthCategory {CategoryID=4,PeakLoad=2.5M, CategoryDescription="High", TurnoverTime=90,WaterAllowance=6,DailyLoad=3, Sand=600, DE=80,Cartridge=15 },
                new HealthCategory {CategoryID=5,PeakLoad=2.5M, CategoryDescription="Moderate", TurnoverTime=120,WaterAllowance=5.4M,DailyLoad=2.2M, Sand=600, DE=80,Cartridge=15 },
                new HealthCategory {CategoryID=6,PeakLoad=2.5M, CategoryDescription="Light", TurnoverTime=150,WaterAllowance=5,DailyLoad=1.9M, Sand=600, DE=80,Cartridge=15 },
                new HealthCategory {CategoryID=7,PeakLoad=3.5M, CategoryDescription="Low", TurnoverTime=210,WaterAllowance=4.8M,DailyLoad=1.4M, Sand=700, DE=80,Cartridge=15 },
                new HealthCategory {CategoryID=8,PeakLoad=3.5M, CategoryDescription="Very Low", TurnoverTime=300,WaterAllowance=4.8M,DailyLoad=1M, Sand=700, DE=80,Cartridge=15 }
            };

            foreach(HealthCategory h in healthCategory)
            {
                context.HealthCategorys.Add(h);
            }

            context.SaveChanges();

            var healthFirstAid = new HealthFirstAid[]
           {
                new HealthFirstAid {FirstAidItem="Resusitation Notice" },
                new HealthFirstAid {FirstAidItem="Examination Couch" },
                new HealthFirstAid {FirstAidItem="Hand wash basin with reticulated potable water." },
                new HealthFirstAid {FirstAidItem="Communication System(e.g. phone with emergency number posted)" },
                new HealthFirstAid {FirstAidItem="One GPO Outlet" },
                new HealthFirstAid {FirstAidItem="Workbench for preparation, cleaning and sterilisation of items" },
                new HealthFirstAid {FirstAidItem="Storage for first aid supplies and equipment" },
                new HealthFirstAid {FirstAidItem="Washable Floor" }
           };

            foreach (HealthFirstAid h in healthFirstAid)
            {
                context.HealthFirstAids.Add(h);
            }

            context.SaveChanges();

            var healthFirstAidItem = new HealthFirstAidKit[]
            {
                new HealthFirstAidKit {HealthID=1, AidItem="Resusciatation equipment for children and adults" },
                new HealthFirstAidKit {HealthID=2, AidItem="First Aid Kit" },
                new HealthFirstAidKit {HealthID=3, AidItem="Spinal Board and Extraction Collars" },
                new HealthFirstAidKit {HealthID=4, AidItem="Two Pillows and Blankets" },
                new HealthFirstAidKit {HealthID=5, AidItem="Pocket Mask and Disposable Gloves" },
                new HealthFirstAidKit {HealthID=6, AidItem="Stretcher" },
                new HealthFirstAidKit {HealthID=7, AidItem="2nd Resuscitation Notice" }

            };

            foreach(HealthFirstAidKit h in healthFirstAidItem)
            {
                context.HealthFirstAidKits.Add(h);
            }

            context.SaveChanges();



            var healthGroup = new HealthGroup[]
            {
                new HealthGroup {GroupID=1,AccessLimit="Public access with limited restrictions", Activity="Non - Structured",
                    TechnicalOperator ="On site at all times", PatronSupervision="All patrons supervised directly", EmergencyCare="On site at all times" },
                new HealthGroup {GroupID=2,AccessLimit="Restricted to discrete users and user groups", Activity="Structured with activity leader present",
                    TechnicalOperator ="Not on site at all times", PatronSupervision="All patrons supervised directly", EmergencyCare="On site at all times" },
                new HealthGroup {GroupID=3,AccessLimit="Restricted to discrete users and user groups", Activity="Non -structured with no activity leader",
                    TechnicalOperator ="Not on site at all times", PatronSupervision="No direct supervision", EmergencyCare="Able to respond in a reasonable time(sect 6.2.5)" },
                new HealthGroup {GroupID=4,AccessLimit="Restricted to owners/occupiers/residents and guest", Activity="Non -structured with no activity leader",
                    TechnicalOperator ="Not on site at all times", PatronSupervision="No direct supervision", EmergencyCare="Not on site" }
            };

            foreach (HealthGroup h in healthGroup)
            {

                context.HealthGroups.Add(h);
            }

            context.SaveChanges();

            var healthGroupKit = new HealthGroupToKit[]
            {
                new HealthGroupToKit {GroupID=1,HealthID=1},
                new HealthGroupToKit {GroupID=1,HealthID=2},
                new HealthGroupToKit {GroupID=1,HealthID=3},
                new HealthGroupToKit {GroupID=1,HealthID=4},
                new HealthGroupToKit {GroupID=1,HealthID=5},
                new HealthGroupToKit {GroupID=1,HealthID=6},
                new HealthGroupToKit {GroupID=2,HealthID=2},
                new HealthGroupToKit {GroupID=2,HealthID=4},
                new HealthGroupToKit {GroupID=2,HealthID=5},
                new HealthGroupToKit {GroupID=2,HealthID=6},
                new HealthGroupToKit {GroupID=3,HealthID=2},
                new HealthGroupToKit {GroupID=3,HealthID=4},
                new HealthGroupToKit {GroupID=3,HealthID=5},
                new HealthGroupToKit {GroupID=3,HealthID=6},
                new HealthGroupToKit {GroupID=4, HealthID=7 }

            };

            foreach(HealthGroupToKit h in healthGroupKit)
            {
                context.HealthGroupToKits.Add(h);
            }

            context.SaveChanges();

            var healthToilet = new HealthToilet[]
            {
                new HealthToilet {GroupID=1, LoadingFactor=2.3m,FemaleWaterCloset=40, MaleWaterCloset=60, MaleUrinals=60, ShowersPerPatron=40, Handbasins=60  },
                new HealthToilet {GroupID=2, LoadingFactor=2.3m,FemaleWaterCloset=40, MaleWaterCloset=60, MaleUrinals=60, ShowersPerPatron=40, Handbasins=60  },
                new HealthToilet {GroupID=3, LoadingFactor=2.3m,FemaleWaterCloset=40, MaleWaterCloset=60, MaleUrinals=60, ShowersPerPatron=40, Handbasins=60  }
            };

            foreach(HealthToilet t in healthToilet)
            {
                context.HealthToilets.Add(t);
            }

            context.SaveChanges();

            var testFrequency = new HealthWaterTestingFrequency[]
            {
                new HealthWaterTestingFrequency {GroupID=1,Frequency="At least once every 4 hours" },
                new HealthWaterTestingFrequency {GroupID=2,Frequency="At least 3 times per day" },
                new HealthWaterTestingFrequency {GroupID=3,Frequency="At least twice a day" },
                new HealthWaterTestingFrequency {GroupID=4,Frequency="At least once every day" },
            };

            foreach(HealthWaterTestingFrequency f in testFrequency)
            {
                context.HealthWaterTestingFrequencys.Add(f);
            }

            context.SaveChanges();

            var healthGroupAid = new HealthGroupToAid[]
            {
                new HealthGroupToAid {GroupID=1, FirstAidID=1 },
                new HealthGroupToAid {GroupID=1, FirstAidID=2 },
                new HealthGroupToAid {GroupID=1, FirstAidID=3 },
                new HealthGroupToAid {GroupID=1, FirstAidID=4 },
                new HealthGroupToAid {GroupID=1, FirstAidID=5 },
                new HealthGroupToAid {GroupID=1, FirstAidID=6 },
                new HealthGroupToAid {GroupID=1, FirstAidID=7 },
                new HealthGroupToAid {GroupID=1, FirstAidID=8 },
                new HealthGroupToAid {GroupID=2, FirstAidID=1 },
                new HealthGroupToAid {GroupID=2, FirstAidID=3 },
                new HealthGroupToAid {GroupID=2, FirstAidID=5 },
                new HealthGroupToAid {GroupID=2, FirstAidID=7 },
                new HealthGroupToAid {GroupID=2, FirstAidID=8 },
                new HealthGroupToAid {GroupID=3, FirstAidID=1 },
                new HealthGroupToAid {GroupID=3, FirstAidID=3 },
                new HealthGroupToAid {GroupID=3, FirstAidID=5 },
                new HealthGroupToAid {GroupID=3, FirstAidID=7 },
                new HealthGroupToAid {GroupID=3, FirstAidID=8 },
                new HealthGroupToAid {GroupID=4, FirstAidID=1 }
            };

            foreach (HealthGroupToAid h in healthGroupAid)
            {
                context.HealthGroupToAids.Add(h);
            }

            context.SaveChanges();

            var siteType = new SiteType[]
           {

                new SiteType { SType="Aquatic Centre", GroupID=1 },
                new SiteType { SType="Water Slide", GroupID=1 },
                new SiteType { SType="Water Park", GroupID=1 },
                new SiteType { SType="Paid Entry Public Facility", GroupID=1 },
                new SiteType { SType="School", GroupID=2 },
                new SiteType { SType="Learn to Swim Centre", GroupID=2 },
                new SiteType { SType="Learn to Dive", GroupID=2 },
                new SiteType { SType="Nursing Home", GroupID=2 },
                new SiteType { SType="Hospital", GroupID=2 },
                new SiteType { SType="Hydrotherapy", GroupID=2 },
                new SiteType { SType="Physiotherapy", GroupID=2 },
                new SiteType { SType="Communnity (Restricted User Group)", GroupID=3 },
                new SiteType { SType="Hotel", GroupID=3 },
                new SiteType { SType="Model", GroupID=3 },
                new SiteType { SType="Resort", GroupID=3 },
                new SiteType { SType="Camp Site", GroupID=3 },
                new SiteType { SType="Lodging House", GroupID=3 },
                new SiteType { SType="Staff Accomodation", GroupID=3 },
                new SiteType { SType="Adults Only Facility", GroupID=3 },
                new SiteType { SType="Bed and Breakfast", GroupID=4 },
                new SiteType { SType="Farm Stay", GroupID=4 },
                new SiteType { SType="Residential Apartments", GroupID=4 },
                new SiteType { SType="Retirement Home", GroupID=4 },
                new SiteType { SType="Lifestyle Village", GroupID=4 },
                new SiteType { SType="Golf Club", GroupID=4 },
                new SiteType { SType="Residential Based Club", GroupID=4 },
                new SiteType { SType="Member Only Access", GroupID=4 }

           };

            foreach (SiteType s in siteType)
            {
                context.SiteTypes.Add(s);
            }
            context.SaveChanges();

            var poolType = new PoolType[]
           {
                new PoolType {iPoolType="Spa", CategoryID=1},
                new PoolType {iPoolType="Leisure Bubble Pool", CategoryID=1 },
                new PoolType {iPoolType="Toddlers Pool", CategoryID=2 },
                new PoolType {iPoolType="Water Slide Landing Pool",CategoryID=2 },
                new PoolType {iPoolType="Spray Pad",CategoryID=2 },
                new PoolType {iPoolType="Leisure Pool Shallow (0.3-0.8m)",CategoryID=3 },
                new PoolType {iPoolType="Hydrotherapy Pool",CategoryID=3 },
                new PoolType {iPoolType="Learn to Swim Pool",CategoryID=4 },
                new PoolType {iPoolType="Wave Pool",CategoryID=4 },
                new PoolType {iPoolType="Leisure Pool Medium Depth Indoor",CategoryID=4 },
                new PoolType {iPoolType="Leisure Pool Full Depth Heated(0.8-1.4m)",CategoryID=5 },
                new PoolType {iPoolType="Lazy River",CategoryID=5 },
                new PoolType {iPoolType="Leisure Pool Outdoor Medium Depth",CategoryID=5 },
                new PoolType {iPoolType="School Pool Heated",CategoryID=6 },
                new PoolType {iPoolType="Health Club Pool",CategoryID=6 },
                new PoolType {iPoolType="Body Corporate Pool" ,CategoryID=6},
                new PoolType {iPoolType="Caravan Park Pool",CategoryID=6 },
                new PoolType {iPoolType="Hotel Pool",CategoryID=6 },
                new PoolType {iPoolType="Motel Pool",CategoryID=6 },
                new PoolType {iPoolType="Leisure Pool Full Depth Outdoor",CategoryID=6 },
                new PoolType {iPoolType="School Pool Unheated",CategoryID=7 },
                new PoolType {iPoolType="Hotel Pool Unheated",CategoryID=7 },
                new PoolType {iPoolType="Motel Pool Unheated",CategoryID=7 },
                new PoolType {iPoolType="Diving Pool",CategoryID=8 },
                new PoolType {iPoolType="Water Polo Pool",CategoryID=8 }


           };

            foreach (PoolType p in poolType)
            {
                context.PoolTypes.Add(p);
            }

            context.SaveChanges();
        }
    }
}

