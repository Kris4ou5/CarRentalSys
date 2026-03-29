using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRentalSys.Domain.Enums;

namespace CarRentalSys.Domain.RentalObjects
{
    public class Damage
    {
        private Random rnd = new Random();
        public string Description { get; set; }
        public DamageSeverity DmgSeverity { get; set; }
        public decimal damageCost { get; private set; }

        public Damage(string description, DamageSeverity dmgSeverity) 
        { 
           Description = description;
            DmgSeverity = dmgSeverity;
            
            if(DmgSeverity ==  DamageSeverity.Minor) damageCost = rnd.Next(50 ,200);
            if (DmgSeverity == DamageSeverity.Moderate) damageCost = rnd.Next(200, 500);
            if(dmgSeverity == DamageSeverity.Severe) damageCost = rnd.Next(500, 1000);
        }
    }
}
