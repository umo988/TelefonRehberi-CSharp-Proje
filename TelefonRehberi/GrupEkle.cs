using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TelefonRehberi.Siniflar;
// umo988
namespace TelefonRehberi
{
    public partial class GrupEkle : Form
    {
        public GrupEkle()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {// Ekle'ye basıldıgında
            Grup grup = new Grup();
            if (txtGrupAdi.TextLength < 2)
            {
                MessageBox.Show("Grup adını giriniz");
            }
            else
            {
                grup.GrupAdi = txtGrupAdi.Text;
                grup.Aciklama = txtAciklama.Text;
                grup.GrupEkle();
                grdGruplar.DataSource = grup.GruplariListele();
                FormuTemizle();
            }
        }

        private void FormuTemizle()
        {
            txtGrupAdi.Clear();
            txtAciklama.Clear();
            grdGruplar.ClearSelection();
            seciliGrupId = -1;
        }
        int seciliGrupId = -1;
        private void GrupEkle_Load(object sender, EventArgs e)
        { // DataGridView açıldığında otomatik doldurur. umo988
            Grup grup = new Grup();
            grdGruplar.DataSource = grup.GruplariListele();
            grdGruplar.Columns["grup_id"].HeaderText = "ID";
            grdGruplar.Columns["grup_id"].Width = 30;
            grdGruplar.Columns["grup_adi"].HeaderText = "Grup Adı";
            grdGruplar.Columns["grup_adi"].Width = 100;
            grdGruplar.Columns["aciklama"].HeaderText = "Açıklama";
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {// Yenile butonuna basıldıgında
            FormuTemizle();
        }

        private void grdGruplar_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                seciliGrupId = Convert.ToInt32(grdGruplar.CurrentRow.Cells["grup_id"].Value.ToString());
                txtGrupAdi.Text = grdGruplar.CurrentRow.Cells["grup_adi"].Value.ToString();
                txtAciklama.Text = grdGruplar.CurrentRow.Cells["aciklama"].Value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Bir hata oluştu!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {// Sil'e basıldıgında
            if (seciliGrupId != -1)
            {
                Grup grup = new Grup();
                grup.GrupID = seciliGrupId;
                grup.GrupSil();
                grdGruplar.DataSource = grup.GruplariListele();
                FormuTemizle();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {// güncelle'ye basıldıgında
            if (seciliGrupId == -1)
            {
                MessageBox.Show("Grup Adını seçiniz!");
            }
            else
            {
                Grup grup = new Grup
                { // Sınıf ozelliklerine deger atamanın kolay yolu
                    GrupID = seciliGrupId,
                    GrupAdi = txtGrupAdi.Text,
                    Aciklama = txtAciklama.Text
                };
                grup.GrubuGuncelle();
                grdGruplar.DataSource = grup.GruplariListele();
                FormuTemizle();
            }
        }
    }// en son!
}
