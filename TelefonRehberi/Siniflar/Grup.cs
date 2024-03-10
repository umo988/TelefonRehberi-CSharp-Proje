using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySqlConnector;
// umo988
namespace TelefonRehberi.Siniflar
{
    class Grup
    {
        public int GrupID { get; set; }
        public string GrupAdi { get; set; }
        public string Aciklama { get; set; }

        Veritabanibaglantisi veritabanibaglantisi;
        MySqlConnection baglanti;
        MySqlCommand komut;

        public Grup()
        {
            veritabanibaglantisi = new Veritabanibaglantisi();
            baglanti = veritabanibaglantisi.baglan();
            komut = new MySqlCommand();
            komut.Connection = baglanti;
        }

        public void GrupEkle()
        {
            try
            {
                if (baglanti.State != ConnectionState.Open)
                {
                    baglanti.Open();
                }
                komut.CommandText = "INSERT INTO gruplar(grup_adi, aciklama) VALUES(@grup_adi, @aciklama)";
                komut.Parameters.AddWithValue("@grup_adi", GrupAdi);
                komut.Parameters.AddWithValue("@aciklama", Aciklama);
                komut.ExecuteNonQuery();
                MessageBox.Show("Kayıt işlemi başarıyla gerçekleşti!");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Kayıt işleminde bir hata oluştu!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            baglanti.Close();
        }
        // umo988
        public DataTable GruplariListele()
        {
            try
            {
                komut.CommandText = "SELECT * FROM gruplar ORDER BY grup_adi ASC";
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(komut);
                DataTable grupListesi = new DataTable();
                dataAdapter.Fill(grupListesi);
                return grupListesi;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Listeleme işleminde hata oluştu!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
        // umo988
        public void GrubuGuncelle()
        {
            try
            {
                if (baglanti.State != ConnectionState.Open)
                {
                    baglanti.Open();
                }
                komut.CommandText = "UPDATE gruplar SET grup_adi=@grup_adi, aciklama=@Aciklama " + "WHERE grup_id=@grup_id";
                komut.Parameters.AddWithValue("@grup_adi", GrupAdi);
                komut.Parameters.AddWithValue("@aciklama", Aciklama);
                komut.Parameters.AddWithValue("@grup_id", GrupID);
                komut.ExecuteNonQuery();
                MessageBox.Show("Güncelleme başarıyla gerçekleşti!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Güncelleme sırasında bir hata oluştu!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            baglanti.Close();
        }
        // umo988
        public void GrupSil()
        {
            try
            {
                if (baglanti.State != ConnectionState.Open)
                {
                    baglanti.Open();
                }
                komut.CommandText = "DELETE FROM gruplar WHERE grup_id=@grup_id";
                komut.Parameters.AddWithValue("@grup_id", GrupID);
                komut.ExecuteNonQuery();
                MessageBox.Show("Silme işlemi başarılı!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Silme işlemi sırasında bir hata oluştu!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            baglanti.Close();
        }
        // umo988
    }// EN SON!
}
