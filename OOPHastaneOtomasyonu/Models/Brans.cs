using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPHastaneOtomasyonu.Models
{
    public class Brans:BaseEntity
    {
        public List<Doktor> Doktorlar { get; set; }

        public Brans()
        {
            Doktorlar = new List<Doktor>();
        }
    }
}
