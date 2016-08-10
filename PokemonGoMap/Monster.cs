using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGoMap
{
    public class Monster
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }

        public DateTime Time { get; set; }
        public string Source { get; set; }
    }
}
