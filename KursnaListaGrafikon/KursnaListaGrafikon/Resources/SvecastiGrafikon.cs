using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.Defaults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace KursnaListaGrafikon.Resources
{
    public class SvecastiGrafikon
    {
        public Dictionary<string, string> atributi { get; set; }
        public Dictionary<string, string> periodi { get; set; }
        public List<string> labele { get; set; }
        public SeriesCollection podaci { get; set; }
        public Func<double, string> YFormatter { get; }

        public SvecastiGrafikon()

        {
            atributi = new Dictionary<string, string>();
            atributi.Add("open", "1. open");
            atributi.Add("high", "2. high");
            atributi.Add("low", "3. low");
            atributi.Add("close", "4. close");
            periodi = new Dictionary<string, string>();
            periodi.Add("Intraday", "FX_INTRADAY");
            periodi.Add("Daily", "FX_DAILY");
            periodi.Add("Weekly", "FX_WEEKLY");

            periodi.Add("Monthly", "FX_MONTHLY");
            labele = new List<string>();
            podaci = new SeriesCollection();
            YFormatter = value => value.ToString("C");
        }

        public void napraviPar(string vreme, string pocetnaValuta, string krajnjaValuta, string interval)
        {
            try
            {
                const string apiKey = "M4HJM8TSKJCPJ1WA";
                string query = null;
                if (interval != "")
                {
                    query = $"https://www.alphavantage.co/query?function={periodi[vreme]}&from_symbol={pocetnaValuta}&to_symbol={krajnjaValuta}&interval={interval}&apikey={apiKey}";
                }
                else
                {
                    query = $"https://www.alphavantage.co/query?function={periodi[vreme]}&from_symbol={pocetnaValuta}&to_symbol={krajnjaValuta}&apikey={apiKey}";
                }
                UcitavanjeAPI ucitavanje = new UcitavanjeAPI();



                dynamic pod = ucitavanje.getData(query);
                Dictionary<string, dynamic>.KeyCollection kljucevi = pod.Keys;

                string kljuc;
                kljuc = (interval == "") ? vreme : interval;
                dynamic lista = pod[$"Time Series FX ({kljuc})"];
                ChartValues<OhlcPoint> vrednosti = new ChartValues<OhlcPoint>();
                double open, close, high, low;
                List<double> ohlc_vrednosti = new List<double>();

                foreach (KeyValuePair<string, dynamic> i in lista)
                {   
                    foreach(KeyValuePair<string, dynamic> j in i.Value)
                    {
                        ohlc_vrednosti.Add(double.Parse(j.Value));
                    }
                    open = ohlc_vrednosti[0];
                    close = ohlc_vrednosti[3];
                    high = ohlc_vrednosti[1];
                    low = ohlc_vrednosti[2];
                    OhlcPoint val = new OhlcPoint(open, high, low, close);
                    labele.Add(i.Key);
                    vrednosti.Add(val);
                }
                CandleSeries candleSeries = new CandleSeries
                {
                    Title = $"{pocetnaValuta}-{krajnjaValuta}",
                    Values = vrednosti
                };
                podaci.Add(candleSeries);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }


            
        }
        public void ocistiPovrsinu()
            {
                podaci.Clear();
                labele.Clear();
            }
    }
}
