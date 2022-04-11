using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPHastaneOtomasyonu.Models
{
    public abstract class BaseEntity
    {
        public string Isim { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        public DateTime DeletedDate { get; set; }

        public BaseEntity()
        {
            CreatedDate = DateTime.Now;
        }

        public override string ToString()
        {
            return Isim;
        }
    }
}
