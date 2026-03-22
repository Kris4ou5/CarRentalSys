using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSys.Domain.Enums
{
    public enum RentalStatus
    {
        Pending, //Създадено
        Active, //наема тече
        Completed, //Приключи
        Canncelled //Отменен
    }
}
