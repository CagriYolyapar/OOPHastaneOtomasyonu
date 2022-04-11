using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPHastaneOtomasyonu.Models
{
    public class Doktor:BaseEntity
    {

        public Brans Bransi { get; set; }

        public Doktor()
        {
            Bransi = new Brans();
        }

    }
}
