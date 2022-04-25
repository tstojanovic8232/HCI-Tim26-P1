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
        public string date { get; set; }
        public string open { get; set; }
        public string low { get; set; }
        public string high { get; set; }
        public string close { get; set; }

        public PodaciValutaTabele(string date, string open, string low, string high, string close)
        {
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
        public string tableTittle { get; set; }
        public Dictionary<String, Object> tableData { get; set; }

        public ObservableCollection<PodaciValutaTabele> podaciTabele { get; set; }

        public string pocetnaValuta { get; set; }
        public string krajnjaValuta { get; set; }

        Dictionary<string, string> linkoviTabela { get; set; }

        UcitavanjeAPI data { get; set; }

        void SetProperties()
        {


            this.Title = "Tabela svih promena";

            this.MinHeight = 500;
            this.MinWidth = 800;
            //Uri iconUri = new Uri("../../Images/Name.ico", UriKind.RelativeOrAbsolute);
            //this.Icon = BitmapFrame.Create(iconUri);
        }

        public TableWindow()
        {
            InitializeComponent();
            SetProperties();



            DataContext = this;

            var window = Application.Current.MainWindow; 

            podaciTabele = new ObservableCollection<PodaciValutaTabele>();
            data = new UcitavanjeAPI();

            linkoviTabela = (window as MainWindow).linkoviTabela;
            

        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        

        private void crtajTabelu(string api)
        {

            podaciTabele.Clear(); //da se prvo isprazne prethodni podaci

            dynamic json_data = data.getData(api); //ovde prosledjivati potrebne parametre pa prilagoditi tamo api

            var dictionary = (Dictionary<string, object>)json_data;
            var dataSet = dictionary.ElementAt(1);
            Dictionary<String, Object> listData = (Dictionary<String, Object>)dataSet.Value;
            tableTittle = dataSet.Key.ToString();

            foreach (var elem in listData)
            {
                Dictionary<string, object> valueData = (Dictionary<string, object>)elem.Value;
                PodaciValutaTabele rowData = new PodaciValutaTabele(elem.Key.ToString(), valueData["1. open"].ToString(), valueData["3. low"].ToString(), valueData["2. high"].ToString(), valueData["4. close"].ToString()
                    );

                podaciTabele.Add(rowData);
            }
            tableData = listData;

            NotifyPropertyChanged("currencyData");

        }

       
    }
}
