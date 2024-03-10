using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TelefonRehberi.Siniflar
{
    class Kisi // umo988
    {
        public int KisiID { get; set; }
        public string KisiAdi { get; set; }
        public string KisiSoyadi { get; set; }
        public string TelNo1 { get; set; }
        public string TelNo2 { get; set; }
        public string Mail { get; set; }
        public string Unvan { get; set; }
        public int GrupID { get; set; }

        Veritabanibaglantisi veritabanibaglantisi;
        MySqlConnection baglanti;
        MySqlCommand komut;

        public Kisi()
        {
            veritabanibaglantisi = new Veritabanibaglantisi();
            baglanti = veritabanibaglantisi.baglan();
            komut = new MySqlCommand();
            komut.Connection = baglanti;
        }
        // umo988
        public void KisiEkle()
        {
            try
            {
                if (baglanti.State != ConnectionState.Open)
                {
                    baglanti.Open();
                }
                komut.CommandText = "INSERT INTO kisiler(ad, soyad, tel_no1, tel_no2, mail, unvan, grup_id) " +
                                    "VALUES (@ad, @soyad, @tel_no1, @tel_no2, @mail, @unvan, @grup_Id)";
                komut.Parameters.AddWithValue("@ad", KisiAdi);
                komut.Parameters.AddWithValue("@soyad", KisiSoyadi);
                komut.Parameters.AddWithValue("@tel_no1", TelNo1);
                komut.Parameters.AddWithValue("@tel_no2", TelNo2);
                komut.Parameters.AddWithValue("@mail", Mail);
                komut.Parameters.AddWithValue("@unvan", Unvan);
                komut.Parameters.AddWithValue("@grup_Id", GrupID);
                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Kayıt işlemi başarılı! Kişi eklendi.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Kayıt işleminde bir sorun oluştu! Kayıt edilemedi.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // umo988
        public DataTable KisileriListele()
        {
            try
            {
                komut.CommandText = "SELECT kisi_id, ad, soyad, tel_no1, tel_no2, mail, unvan, grup_adi " +
                                    "FROM kisiler, gruplar " +
                                    "WHERE kisiler.grup_id = gruplar.grup_id " +
                                    "ORDER BY ad ASC, soyad ASC";
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(komut);
                DataTable kisiListesi = new DataTable();
                dataAdapter.Fill(kisiListesi);
                return kisiListesi;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Listeleme işleminde bir hata oluştu!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
        // umo988

        public void KisiyiGuncelle()
        {
            try
            {
                if (baglanti.State != ConnectionState.Open)
                    baglanti.Open();
                komut.CommandText = "UPDATE kisiler SET ad=@ad, soyad=@soyad, tel_no1=@telNo1, tel_no2=@telNo2, " +
                                    "mail=@mail, unvan=@unvan, grup_id=@grupId WHERE kisi_id=@kisiId";
                komut.Parameters.AddWithValue("@ad", KisiAdi);
                komut.Parameters.AddWithValue("@soyad", KisiSoyadi);
                komut.Parameters.AddWithValue("@telNo1", TelNo1);
                komut.Parameters.AddWithValue("@telNo2", TelNo2);
                komut.Parameters.AddWithValue("@mail", Mail);
                komut.Parameters.AddWithValue("@unvan", Unvan);
                komut.Parameters.AddWithValue("@grupId", GrupID);
                komut.Parameters.AddWithValue("@kisiId", KisiID);
                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Güncelleme işlemi başarıyla gerçekleşti!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Güncelleme sırasında bir hata oluştu!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // umo988
        public void KisiSil()
        {
            try
            {
                if (baglanti.State != ConnectionState.Open)
                    baglanti.Open();
                komut.CommandText = "DELETE FROM kisiler WHERE kisi_id=@kisiId";
                komut.Parameters.AddWithValue("kisiId", KisiID);
                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Silme işlemi başarılı!");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Silme işleminde hata oluştu!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // umo988
        public DataTable KisiAra()
        {// umo988
            try
            {
                if (GrupID == 0)
                {
                    komut.CommandText = "SELECT kisi_id, ad, soyad, tel_no1, tel_no2, mail, unvan, grup_adi " +
                                        "FROM kisiler, gruplar " +
                                        "WHERE kisiler.grup_id = gruplar.grup_id " +
                                        "AND ad LIKE @ad AND soyad LIKE @soyad " +
                                        "AND (tel_no1 LIKE @TelNo OR tel_no2 LIKE @TelNo) " +
                                        "AND gruplar.grup_id = @GrupID " +
                                        "ORDER BY ad ASC, soyad ASC";
                }
                else
                {
                    komut.CommandText = "SELECT kisi_id, ad, soyad, tel_no1, tel_no2, mail, unvan, grup_adi " +
                                        "FROM kisiler, gruplar " +
                                        "WHERE kisiler.grup_id = gruplar.grup_id " +
                                        "AND ad LIKE @ad AND soyad LIKE @soyad " +
                                        "AND (tel_no1 LIKE @TelNo OR tel_no2 LIKE @TelNo) " +
                                        "AND gruplar.grup_id = @GrupID " +
                                        "ORDER BY ad ASC, soyad ASC";
                }
                komut.Parameters.AddWithValue("@ad", KisiAdi + "%");
                komut.Parameters.AddWithValue("@soyad", KisiSoyadi + "%");
                komut.Parameters.AddWithValue("@TelNo", TelNo1 + "%");
                komut.Parameters.AddWithValue("@GrupID", GrupID + "%");
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(komut);
                DataTable arananKisiListesi = new DataTable();
                dataAdapter.Fill(arananKisiListesi);
                return arananKisiListesi;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Arama işleminde bir hata oluştu!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }// umo988
        }
    }// EN SON!
}
