using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace RideSharePlus.Models
{ 
    public class RideShareDbInitializer : DropCreateDatabaseIfModelChanges<RideShareDB>
    {
        protected override void Seed(RideShareDB context)
        {
            context.Campus.AddOrUpdate(new Campus { CampusId = 1, Name = "Bremerton", Address = "1600 Chester Ave.", City = "Bremerton" });
            context.Campus.AddOrUpdate(new Campus { CampusId = 2, Name = "Poulsbo", Address = "1000 Olympic College Place NW", City = "Poulsbo" });
            context.Campus.AddOrUpdate(new Campus { CampusId = 3, Name = "PSNS", Address = "Naval Base Kitsap", City = "Bremerton" });
            context.Campus.AddOrUpdate(new Campus { CampusId = 4, Name = "Shelton", Address = "937 W. Alpine Way", City = "Shelton" });

            base.Seed(context);
        }
    }
}