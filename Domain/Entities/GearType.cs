using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class GearType : BaseEntity
    {
        public string Type { get; set; } 
        public ICollection<Car> Cars { get; set; }
    }
}
