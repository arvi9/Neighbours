using System.Collections.Generic;

namespace Neighbours.Data.Models
{
    public class City
    {
        private ICollection<District> districts;

        public City()
        {
            this.districts = new HashSet<District>();
        }
        
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<District> Districts { get
            {
                return this.districts;
            }
            set
            {
                this.districts = value;
            }
        }
    }
}