using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetSimulator
{
    public class Item
    {
        
        public string Name { get; set; }

        
        public ItemType Type { get; set; }

       
        public List<PetType> CompatibleWith { get; set; }

        
        public PetStat AffectedStat { get; set; }

        
        public int EffectAmount { get; set; }

        
        public float Duration { get; set; }
    }
}
