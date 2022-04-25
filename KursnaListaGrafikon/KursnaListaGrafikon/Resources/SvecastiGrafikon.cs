﻿using LiveCharts;
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

        public void napraviPar(string vreme, string pocetnaValuta, string krajnjaValuta, string interval)
        {
            try
            {
                const string apiKey = "NCNMGBNZAS4KV5D2";
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

                foreach (KeyValuePair<string, dynamic> i in lista)
                {
                    double open = Double.Parse(i.Value[atributi["open"]]);
                    double close = Double.Parse(i.Value[atributi["close"]]);
                    double high = Double.Parse(i.Value[atributi["high"]]);
                    double low = Double.Parse(i.Value[atributi["low"]]);
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


            void ocistiPovrsinu()
            {
                podaci.Clear();
                labele.Clear();
            }
        }
    }
}