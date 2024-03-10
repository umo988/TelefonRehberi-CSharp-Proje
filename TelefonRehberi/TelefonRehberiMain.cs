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
    public partial class TelefonRehberiMain : Form
    {
        public TelefonRehberiMain()
        {
            InitializeComponent();
        }
        int seciliKisiId = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            GrupEkle grupform = new GrupEkle();
            DialogResult result = grupform.ShowDialog();
            if (result == DialogResult.Cancel)
            {
                BilgileriYukle();
            }
        }

        private void TelefonRehberiMain_Load(object sender, EventArgs e)
        {
            BilgileriYukle();
        }
        private void BilgileriYukle()
        {
            Grup grup = new Grup();
            cmbGrup.DataSource = grup.GruplariListele();
            cmbGrup.ValueMember = "grup_id";
            cmbGrup.DisplayMember = "grup_adi";
            DataTable araGrupDt = grup.GruplariListele();
            DataRow dr = araGrupDt.NewRow();
            dr["grup_adi"] = "Tümü";
            dr["grup_id"] = 0;
            araGrupDt.Rows.Add(dr);
            cmbAraGrup.DataSource = araGrupDt;
            cmbAraGrup.ValueMember = "grup_id";
            cmbAraGrup.DisplayMember = "grup_adi";
            cmbAraGrup.SelectedValue = 0;
            Kisi kisi = new Kisi();
            grdKisiler.DataSource = kisi.KisileriListele();
            // Datagridview'de nasıl gorunecegini saglayan kodlar
            grdKisiler.Columns["kisi_id"].HeaderText = "Kişi ID";
            grdKisiler.Columns["kisi_id"].Width = 0;
            grdKisiler.Columns["ad"].HeaderText = "Kişi Adı";
            grdKisiler.Columns["ad"].Width = 120;
            grdKisiler.Columns["soyad"].HeaderText = "Kişi Soyadı";
            grdKisiler.Columns["soyad"].Width = 90;
            grdKisiler.Columns["tel_no1"].HeaderText = "Telefon No 1";
            grdKisiler.Columns["tel_no1"].Width = 110;
            grdKisiler.Columns["tel_no2"].HeaderText = "Telefon No 2";
            grdKisiler.Columns["tel_no2"].Width = 110;
            grdKisiler.Columns["mail"].HeaderText = "Mail";
            grdKisiler.Columns["mail"].Width = 170;
            grdKisiler.Columns["unvan"].HeaderText = "Ünvan";
            grdKisiler.Columns["unvan"].Width = 100;
            grdKisiler.Columns["grup_adi"].HeaderText = "Kişi Grubu";
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (KayitKontrol())
            {
                Kisi kisi = new Kisi();
                kisi.KisiAdi = txtAd.Text;
                kisi.KisiSoyadi = txtSoyad.Text;
                kisi.TelNo1 = txtNumara.Text;
                kisi.TelNo2 = txtNumara2.Text;
                kisi.Mail = txtMail.Text;
                kisi.Unvan = txtUnvan.Text;
                kisi.GrupID = Int32.Parse(cmbGrup.SelectedValue.ToString());
                kisi.KisiEkle();
                grdKisiler.DataSource = kisi.KisileriListele();
                FormuTemizle();
            }
        }

        private bool KayitKontrol()
        {
            bool kontrol = false;
            if (txtAd.TextLength < 2)
            {
                MessageBox.Show("Kişinin adını giriniz");
            }
            else if (txtSoyad.TextLength < 2)
            {
                MessageBox.Show("Kişinin soyadını giriniz");
            }
            else if (txtNumara.TextLength < 10 || txtNumara.TextLength > 14)
            {
                MessageBox.Show("Kişinin telefon numarasını hatalı girdiniz. Lütfen (10-14 karakter kullanınız.)");
            }
            else if (cmbGrup.SelectedIndex < 0)
            {
                MessageBox.Show("Kişinin grubunu seçiniz");
            }
            else
            {
                kontrol = true;
            }
            return kontrol;
        }

        private void txtNumara_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtNumara2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtAraNumara_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormuTemizle();
        }

        private void FormuTemizle()
        {
            txtAd.Clear();
            txtSoyad.Clear();
            txtNumara.Clear();
            txtNumara2.Clear();
            txtMail.Clear();
            txtUnvan.Clear();
            txtAraAd.Clear();
            txtAraSoyad.Clear();
            txtAraNumara.Clear();
            grdKisiler.ClearSelection();
            seciliKisiId = -1;
        }

        private void grdKisiler_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (grdKisiler.SelectedRows!=null)
                {
                    seciliKisiId = Convert.ToInt32(grdKisiler.CurrentRow.Cells["kisi_id"].Value.ToString());
                    txtAd.Text = grdKisiler.CurrentRow.Cells["ad"].Value.ToString();
                    txtSoyad.Text = grdKisiler.CurrentRow.Cells["soyad"].Value.ToString();
                    txtNumara.Text = grdKisiler.CurrentRow.Cells["tel_no1"].Value.ToString();
                    txtNumara2.Text = grdKisiler.CurrentRow.Cells["tel_no2"].Value.ToString();
                    txtMail.Text = grdKisiler.CurrentRow.Cells["mail"].Value.ToString();
                    txtUnvan.Text = grdKisiler.CurrentRow.Cells["unvan"].Value.ToString();
                    cmbGrup.Text = grdKisiler.CurrentRow.Cells["grup_adi"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hata oluştu!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (seciliKisiId != -1)
            {
                Kisi kisi = new Kisi();
                kisi.KisiID = seciliKisiId;
                kisi.KisiSil();
                grdKisiler.DataSource = kisi.KisileriListele();
                FormuTemizle();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (seciliKisiId != -1)
            {
                Kisi kisi = new Kisi();
                kisi.KisiID = seciliKisiId;
                kisi.KisiAdi = txtAd.Text;
                kisi.KisiSoyadi = txtSoyad.Text;
                kisi.TelNo1 = txtNumara.Text;
                kisi.TelNo2 = txtNumara2.Text;
                kisi.Mail = txtMail.Text;
                kisi.Unvan = txtUnvan.Text;
                kisi.GrupID = Int32.Parse(cmbGrup.SelectedValue.ToString());
                kisi.KisiyiGuncelle();
                grdKisiler.DataSource = kisi.KisileriListele();
                FormuTemizle();
            }
        }

        private void KisiAra()
        {
            Kisi kisi = new Kisi();
            kisi.KisiAdi = txtAraAd.Text;
            kisi.KisiSoyadi = txtAraSoyad.Text;
            kisi.TelNo1 = txtAraNumara.Text;
            kisi.GrupID = Int32.Parse(cmbAraGrup.SelectedValue.ToString());
            grdKisiler.DataSource = kisi.KisiAra();
        }

        private void txtAraAd_TextChanged(object sender, EventArgs e)
        {
            KisiAra();
        }

        private void txtAraSoyad_TextChanged(object sender, EventArgs e)
        {
            KisiAra();
        }

        private void txtAraNumara_TextChanged(object sender, EventArgs e)
        {
            KisiAra();
        }

        private void cmbAraGrup_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbAraGrup_SelectionChangeCommitted(object sender, EventArgs e)
        {
            KisiAra();
        }
    }// en son!
}
