using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace WindowsFormsApp4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=MANGTAY\\SQLDERS;Initial Catalog=DbSINAVOGRENCI;Integrated Security=True");
        DbSINAVOGRENCIEntities db = new DbSINAVOGRENCIEntities();

        private void btnderslistesi_Click(object sender, EventArgs e)
            // sql command ile ado.net kullanarak verileri çekme işlemi bu şekilde 
        {
            SqlCommand komut1 = new SqlCommand("Select * from tbldersler",baglanti); /// bağlantı
            SqlDataAdapter da = new SqlDataAdapter(komut1); // datayı ttut ve getir
            DataTable dt = new DataTable(); // tabloya eklemesini gerçekleştir
            da.Fill(dt);// doldur
            dataGridView1.DataSource = dt; //kaynak gösterimi
        }

        private void Form1_Load(object sender, EventArgs e)
        {     
        }

        private void btnlistele_Click(object sender, EventArgs e)
        {
            /*DbSINAVOGRENCIEntities db = new DbSINAVOGRENCIEntities();*/ // entity kullanarak db isimli DbSINAVOGRENCIEntities sınıfından bir nesne ürettim
            dataGridView1.DataSource = db.TBLOGRENCIs.ToList(); // dataGridView1 içerisine bu verileri listeleme komutu ile getirdim
            dataGridView1.Columns[3].Visible = false; // üçüncü stun gelmedi bu yöntem birdir
            dataGridView1.Columns[4].Visible = false;
        }

        private void btnnotlist_Click(object sender, EventArgs e)
        {
            //dataGridView1.DataSource = db.TBLNOTLARs.ToList(); 
            var query = from item in db.TBLNOTLARs
                        select new { item.NOTID, item.OGR, item.DERS, item.SINAV1,item.SINAV2,item.SINAV3 };
            dataGridView1.DataSource = query.ToList(); // not listesinden istediğim verileri aldım
        }

        private void btnkayıt_Click(object sender, EventArgs e)
        {
            TBLOGRENCI t = new TBLOGRENCI();
            t.AD = txtad.Text;
            t.SOYAD = txtoyad.Text;
            db.TBLOGRENCIs.Add(t); // TBL ÖĞRENCİLER İÇERİSİNDEN T ADINDA BİR NESNE TÜRETTİM VE DAHA SONRA BU NESNENİN İÇERİSİNE VERİLERİMİ YOLLADIM
            db.SaveChanges();
            MessageBox.Show("Başarılı");

        }

        private void button1_Click(object sender, EventArgs e)
        {
            TBLDERSLER d = new TBLDERSLER();
            d.DERSAD = textBox11.Text;
            db.TBLDERSLERs.Add(d);
            db.SaveChanges();
            MessageBox.Show("Ders Eklendi");
            // Yukarıdaki kod bloğu tbldersler içerisinden bir nesne alır ve u nesnenin özellikleri ile db içerisine bir kayıt işlemi sağlar 
        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtid.Text); // öğrenci id kısmına girilen değikenden alıp id kısmına aldım 
            var x = db.TBLOGRENCIs.Find(id); // silmek istediğimiz parameteyi buldurma aşaması 
            db.TBLOGRENCIs.Remove(x); // find ile bulunan değeri al ve sil
            db.SaveChanges();
            MessageBox.Show("Öğrenci Sistemden Silindi"); // ilişkili değerleri silmede sorun çıkabilir ilişkili tabloda silme olmaz aktif ve pasif olarak ileride ekleme yapacağım 



        }

        private void btnguncelle_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtid.Text); // öğrenci id kısmını bir int değere aldım
            var x = db.TBLOGRENCIs.Find(id); // aldığım id değerini aldım var üzerine find methodu ile aldım 
            x.AD = txtad.Text;  // aldığım id değerine yeni bir ad değişkeni ataması yaptım
            x.SOYAD = txtoyad.Text; // ""   ""          ""  """
            x.FOTO = txtfoto.Text; 
            db.SaveChanges(); // bunu yapmazsam eğer değişiklkler kaydedilmemiş olacaktır 
            MessageBox.Show("Öğrenci Bilgileri Güncellendi"); // mesaj kısmı
            // x değeri id girdiğim satır değerini tutuyor daha sonra ad ile güncelleme yapmaktayım


        }

        

        private void btnprosedur_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.NOTLISTESI(); // datagrid üzerine direkt olarak notlistesi olarak bir prosedür yolladım


        }

        private void btnbul_Click(object sender, EventArgs e)
        {
            
            
        }

        private void btnhesapla_Click(object sender, EventArgs e)
        {
            int a, b, c ;
            a = int.Parse(txts1.Text);
            b = int.Parse(txts2.Text);
            c = int.Parse(txts3.Text);
            int ortalama = (a + b + c) / 3;
            textBox5.Text = ortalama.ToString();
        }

        private void btnsınavgüncelle_Click(object sender, EventArgs e)
        {
            
            int id = int.Parse(textboxidd.Text); 
            var x = db.TBLNOTLARs.Find(id); 
            x.SINAV1 = Convert.ToInt16(txts1.Text);  
            x.SINAV2 = Convert.ToInt16(txts2.Text);
            x.SINAV3 = Convert.ToInt16(txts3.Text);
            db.SaveChanges(); 
            MessageBox.Show("Öğrenci Bilgileri Güncellendi"); // mesaj kısmı
            dataGridView1.DataSource = db.NOTLISTESI();
        }



    }
}
