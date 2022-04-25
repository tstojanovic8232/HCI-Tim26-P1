using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Windows.Controls;
namespace KursnaListaGrafikon.Resources
{
    public class LinijskiGrafikon : System.Windows.Controls.UserControl
    {
        public Dictionary<string, string> atributi  { get; set; }
        public Dictionary<string, string> periodi { get; set; }
        public List<string> labele { get; set; }
        public SeriesCollection podaci { get; set; }
        public Func<double, string> YFormatter { get; }

        public LinijskiGrafikon()
            
        {
            atributi = new Dictionary<string, string>();
            atributi.Add("open","1. open");
            atributi.Add("close", "2. close");
            atributi.Add("high", "3. high");
            atributi.Add("low", "4. low");
            periodi = new Dictionary<string, string>();
            periodi.Add("Intraday", "FX_INTRADAY");
            periodi.Add("Daily", "FX_DAILY");
            periodi.Add("Weekly", "FX_WEEKLY");

            periodi.Add("Monthly", "FX_MONTHLY");
            labele = new List<string>();
            podaci = new SeriesCollection();
            YFormatter = value => value.ToString("C");
        }
        
        public void napraviPar(string vreme,string pocetnaValuta,string krajnjaValuta,string atribut,string interval)
        {
            const string apiKey = "NCNMGBNZAS4KV5D2";
            string query = null;
            if (interval != "")
            {
                query = $"https://www.alphavantage.co/query?function={periodi[vreme]}&from_symbol={pocetnaValuta}&to_symbol={krajnjaValuta}&interval={interval}&apikey={apiKey}";
            }
            else {
                query = $"https://www.alphavantage.co/query?function={periodi[vreme]}&from_symbol={pocetnaValuta}&to_symbol={krajnjaValuta}&apikey={apiKey}";
            }
            UcitavanjeAPI ucitavanje = new UcitavanjeAPI();
            dynamic pod=ucitavanje.getData(query);
            Dictionary<string, dynamic>.KeyCollection kljucevi = pod.Keys;
            string kljuc;
            kljuc=(interval =="") ? vreme : interval;
            dynamic lista = pod[$"Time Series FX ({kljuc})"];
            ChartValues<double> vrednosti = new ChartValues<double>();

            foreach (KeyValuePair<string, dynamic> i in lista)
            {
                double val = Double.Parse(i.Value[atributi[atribut]]);
                labele.Add(i.Key);
                vrednosti.Add(val);
            }
            LineSeries lineSeries = new LineSeries
            {
                Title = $"{pocetnaValuta}-{krajnjaValuta}",
                Values = vrednosti,
                PointGeometry = null,
                PointGeometrySize = 15
            };
            podaci.Add(lineSeries);
        }
        public void ocistiPovrsinu()
        {
            podaci.Clear();
            labele.Clear();
        }
    }
}
