using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRentalSys.Domain.Enums;

namespace CarRentalSys.Domain.RentalObjects
{
    public class Inspections
    {
        public int Id { get; private set; }
        public int RentalID { get; private set; }
        public List<Damage> Damages { get; private set; } = new List<Damage>();
        public decimal TotalCost { get; private set; }

        public Inspections(int id, int rentalid, List<Damage> damages) 
        {  
          Id = id;
          RentalID = rentalid;
          Damages = damages;
        }

        public void CalculateDamagesCost()
        {
            foreach (var dmg in Damages)
            {
                TotalCost += dmg.damageCost;
            }
        }


    }
}
