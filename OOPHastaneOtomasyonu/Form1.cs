using OOPHastaneOtomasyonu.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOPHastaneOtomasyonu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //Onemli Degişkenlerimiz

        #region
        List<Brans> bolumler;
        List<Randevu> rnler;

        string[] saatler; //performans acısından ve fixed elemanlar oldugu icin dizi yaptık
        #endregion

        //Button olusturma kodları ve olusturulan buttonun eventi

        #region
        public void Olustur()
        {
            flowLayoutPanel1.Controls.Clear();

            for (int i = 0; i < 30; i++)
            {
                Button btn = new Button();
                btn.Text = saatler[i];

                foreach (Randevu item in rnler)
                {
                    if (item.Tarih==dtpRandevu.Value&&item.Zaman==btn.Text&&item.DoktorIsim==cmbRandevuDoktor.SelectedItem)
                    {
                        btn.BackColor = Color.Red; //eger kayıtlı bir randevu varsa buttonun arka plan rengini kırmızı olarak ayarladık...
                    }
                }

                flowLayoutPanel1.Controls.Add(btn); //ilgili kontrolu flowlayoutpanel'inin icine yerlestiriyoruz.

                btn.Click += Btn_Click; //ilgili kontrolün event'ini yaratıyoruz.

            }
        }

        #endregion

        //Olusturulan buttonların event'inin görevi
        #region

        private void Btn_Click(object sender, EventArgs e)
        {
            if (txtTcKimlik.Text==string.Empty||txtHastaIsim.Text==string.Empty||cmbRandevuBrans.SelectedIndex<0||cmbRandevuDoktor.SelectedIndex<0)

            {
                MessageBox.Show("Lutfen randevu bilgilerinizi kontrol ediniz");
                return;
            }

            Button btn = sender as Button; //tıkladıgımız buttonu yakalayıp bir degişkene attık

            if (btn.BackColor==Color.Red)
            {
                MessageBox.Show($"Doktorumuz {btn.Text} saatinde maalesef doludur.");
            }
            else
            {
                DialogResult mb = MessageBox.Show("Randevu almak istediginize emin misiniz?","Randevu",MessageBoxButtons.YesNo,MessageBoxIcon.Information);

                if (mb==DialogResult.Yes)
                {
                    btn.BackColor = Color.Red;
                    Randevu r = new Randevu();
                    r.Isim = txtHastaIsim.Text;
                    r.Zaman = btn.Text;
                    r.DoktorIsim = cmbRandevuDoktor.SelectedItem as Doktor;
                    r.Bolumu = cmbRandevuBrans.SelectedItem as Brans;
                    r.TcNo = txtTcKimlik.Text;
                    r.Tarih = dtpRandevu.Value;

                    rnler.Add(r);//olusturulan randevuları kontrol edebilmek icin bir listeye ekliyoruz.

                }

            }

        }

        #endregion




        private void Form1_Load(object sender, EventArgs e)
        {
            bolumler = new List<Brans>();
            rnler = new List<Randevu>();

            saatler = new string[]
            {
"09:00","09:10","09:20","09.30","09:40","09:50",
"10:00","10:10","10:20","10.30","10:40","10:50",
"11:00","11:10","11:20","11.30","11:40","11:50",
"12:00","12:10","12:20","12.30","12:40","12:50",
"13:00","13:10","13:20","13.30","13:40","13:50",
                                 
            };

            dtpRandevu.MinDate = DateTime.Now.AddDays(1); //böylece minimum tarihi sistem tarihinden bir gün sonrası olarak belirlemiş oluyoruz ve yanlıs tarih secilme durumunu ortadan kaldırıyoruz...
            Olustur();
        }

        private void btnBransEkle_Click(object sender, EventArgs e)
        {
            foreach (Brans item in bolumler)
            {
                if (txtBranslar.Text==item.Isim)
                {
                    MessageBox.Show("Böyle bir brans zaten eklenmiş");
                    return;
                }
            }


            Brans b = new Brans();

            b.Isim = txtBranslar.Text;

            bolumler.Add(b); //olusturdugumuz bransı bolumler isimli listemize ekledik

            cmbBranslar.DataSource = cmbRandevuBrans.DataSource = bolumler.ToList(); //Bir datasource property'si sizden object deger ister...Ancak bu veri kaynagı property'si kendisine verilen degeri listeye dökebilmek icin (ilgili degerin listelenmesi gerektigini anlamak icin) ilgili atılacak degerin ToList() metodu ile bir listeye dönüsmesi gerekir.
            txtBranslar.Clear();
        }

        private void btnDoktorEkle_Click(object sender, EventArgs e)
        {
            Doktor d = new Doktor();
            d.Isim = txtDoktor.Text;

            Brans b = cmbBranslar.SelectedItem as Brans;

            b.Doktorlar.Add(d); //olusturdugumuz doktoru b isimli Brans nesnesinin Doktorlari isimli property'sine(List<Doktor> tipinde) ekliyoruz

            d.Bransi = b; //d,Doktor tipinde bir nesnedir. d isimli nesnenin Bransi isimli property'si de Brans tipinde bir property'dir.

            cmbRandevuDoktor.DataSource = b.Doktorlar.ToList();
        }

        private void dtpRandevu_ValueChanged(object sender, EventArgs e)
        {
            Olustur();
        }

        private void cmbRandevuBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            Brans b = cmbRandevuBrans.SelectedItem as Brans;
            cmbRandevuDoktor.DataSource = b.Doktorlar.ToList();
        }

        private void cmbRandevuDoktor_SelectedIndexChanged(object sender, EventArgs e)
        {
            Olustur();
        }

        private void cmbBranslar_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((cmbBranslar.SelectedItem as Brans).Doktorlar.Count==0)
            {
                cmbRandevuDoktor.Text = string.Empty;
            }
        }
    }
}
