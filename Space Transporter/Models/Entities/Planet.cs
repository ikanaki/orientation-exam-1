using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Space_Transporter.Models.Entities
{
    public class Planet
    {
        [Key]
        public long Id { get; set; }

        public string Name { get; set; }

        public int DockingCapacity { get; set; }

        public List<Ship> Ships { get; set; }
    }
}
