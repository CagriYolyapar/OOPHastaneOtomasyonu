using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPHastaneOtomasyonu.Models
{
    public class Randevu:BaseEntity
    {
        public Doktor DoktorIsim { get; set; }

        public Brans Bolumu { get; set; } //Raporlamada rahatlık olması acısından yazıyoruz...Yoksa DoktorIsim property'sinden de bölüme ulasabiliriz.

        public string Zaman { get; set; }

        public DateTime Tarih { get; set; }

        public string TcNo { get; set; }



    }
}
