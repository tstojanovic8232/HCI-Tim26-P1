using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

// API KEY =  Z7DBOYZAU3RWWU1C
namespace KursnaListaGrafikon
{
    public class PodaciValutaTabele
    {
        public string par { get; set; }
        public string date { get; set; }
        public string open { get; set; }
        public string low { get; set; }
        public string high { get; set; }
        public string close { get; set; }

        public PodaciValutaTabele(string par, string date, string open, string low, string high, string close)
        {
            this.par = par;
            this.date = date;
            this.open = open;
            this.low = low;
            this.high = high;
            this.close = close;
        }
    }

    /// <summary>
    /// Interaction logic for TableWindow.xaml
    /// </summary>
    public partial class TableWindow : Window
    {
        public string naslovTabele { get; set; }
        public Dictionary<String, Object> podaci { get; set; }

        public ObservableCollection<PodaciValutaTabele> podaciTabele { get; set; }

        public string pocetnaValuta { get; set; }
        public string krajnjaValuta { get; set; }

        public Dictionary<string, string> linkoviTabela { get; set; }

        UcitavanjeAPI data { get; set; }

        void SetProperties()
        {


            this.Title = "Tabela svih promena";

            this.MinHeight = 500;
            this.MinWidth = 800;

            Uri iconUri = new Uri("../../Resources/KursGraf.ico", UriKind.RelativeOrAbsolute);
            this.Icon = BitmapFrame.Create(iconUri);
        }

        public TableWindow()
        {
            InitializeComponent();
            SetProperties();

            linkoviTabela = new Dictionary<string, string>();

            DataContext = this;

            var prozor = Application.Current.MainWindow;

            podaciTabele = new ObservableCollection<PodaciValutaTabele>();
            data = new UcitavanjeAPI();

            linkoviTabela = (prozor as MainWindow).linkoviTabela;

            foreach (KeyValuePair<string, string>
api in linkoviTabela)
            {
                crtajTabelu(api.Value, api.Key);
            }
        }


        private void crtajTabelu(string api, string par)
        {


            dynamic json_data = data.getData(api);

            var dict = (Dictionary<string, object>)json_data;
            var ds = dict.ElementAt(1);
            Dictionary<String, Object> lista = (Dictionary<String, Object>)ds.Value;
            naslovTabele = ds.Key.ToString();

            foreach (var elem in lista)
            {
                Dictionary<string, object> vrednosti = (Dictionary<string, object>)elem.Value;
                PodaciValutaTabele vrsta = new PodaciValutaTabele(par, elem.Key.ToString(), vrednosti["1. open"].ToString(), vrednosti["3. low"].ToString(), vrednosti["2. high"].ToString(), vrednosti["4. close"].ToString()
                    );

                podaciTabele.Add(vrsta);
            }
            podaci = lista;

        }

    }
}