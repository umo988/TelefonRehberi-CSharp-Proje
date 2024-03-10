using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using System.Configuration;

namespace TelefonRehberi.Siniflar
{
    class Veritabanibaglantisi
    {
        string baglantiCumlesi = ConfigurationManager.ConnectionStrings["TelefonRehberiBaglantiCumlesi"].ConnectionString;

        public MySqlConnection baglan()
        {
            MySqlConnection baglanti = new MySqlConnection(baglantiCumlesi);
            MySqlConnection.ClearPool(baglanti);
            return baglanti;
        }
    }
}
