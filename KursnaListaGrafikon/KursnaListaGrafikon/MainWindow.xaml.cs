using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System.Web.Script.Serialization;
using System.Net;
using KursnaListaGrafikon.Resources;

namespace KursnaListaGrafikon
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const string apiKey = "M4HJM8TSKJCPJ1WA";

        LinijskiGrafikon linijski { get; set; }
        public List<string> valute;

       

        void SetProperties()
        {
            this.Title = "Promena kursa valuta";

            this.MinHeight = 500;
            this.MinWidth = 800;
            //Uri iconUri = new Uri("../../Images/Name.ico", UriKind.RelativeOrAbsolute);
            //this.Icon = BitmapFrame.Create(iconUri);
        }
        public MainWindow()
        {
            InitializeComponent();
            SetProperties();

            linijski = new LinijskiGrafikon();

            
        }
        private void NacrtajGrafikon(object sender, RoutedEventArgs e)
        {
            List<string> pocetne = new List<string>();
            foreach (ToggleButton item in pocetneValute.Children)
            {
                if (item.IsChecked == true)
                {
                    pocetne.Add(item.Name);
                }
            }
            string krajnjaValuta = "";
            foreach (ToggleButton btn in krajnjeValute.Children)
            {
                if (btn.IsChecked == true)
                {

                    krajnjaValuta = (String)btn.Content;

                }

            }
            string inter = "";
            foreach (RadioButton item in intervali.Children)
            {

                if (item.IsChecked == true)
                {

                    inter = item.Name;
                }

            }
            string period = "";
            foreach (RadioButton item in periodi.Children)
            {
                if (item.IsChecked == true)
                {
                    period = (String)item.Content;
                }
            }
            string atribut = "open";
            foreach (string pocetnaValuta in pocetne)
            {
                linijski.napraviPar(period, pocetnaValuta, krajnjaValuta, atribut, inter);

            }
            DataContext = this;

        }
        private void TableBtn_Click(object sender, RoutedEventArgs e)
        {
            var s = new TableWindow();
            s.Show();
        }

        private void Period_Checked(object sender, RoutedEventArgs e)
        {
            

        }
        private void DisableButtons(object sender, RoutedEventArgs e)
        {
            foreach (ToggleButton btn in krajnjeValute.Children)
            {
                if (btn.IsChecked == false) btn.IsEnabled = false;
                
            }  
        }

        private void EnableButtons(object sender, RoutedEventArgs e)
        {
            foreach (ToggleButton btn in krajnjeValute.Children)
            {
                if (btn.IsEnabled == false) btn.IsEnabled = true;
                
            }
        }

        private void Intraday_Checked(object sender, RoutedEventArgs e)
        {
            //Pokaži Radio buttons za 1min, 5min, 15min, 30min
            foreach (RadioButton btn in intervali.Children)
            {
                btn.Visibility = Visibility.Visible;
                intervalText.Visibility = Visibility.Visible;
            }

            Period_Checked(sender, e);
        }

        private void Intraday_Unchecked(object sender, RoutedEventArgs e)
        {
            //Sakrij Radio buttons za 1min, 5min, 15min, 30min
            foreach (RadioButton btn in intervali.Children)
            {
                btn.Visibility = Visibility.Hidden;
                intervalText.Visibility = Visibility.Hidden;
                if (btn.IsChecked == true) btn.IsChecked = false;
            }
        }

        private void Interval_Checked(object sender, RoutedEventArgs e)
        {
           
        }
        
        private void Attribute_Checked(object sender, RoutedEventArgs e)
        {
            
        }
    }
    //API key: M4HJM8TSKJCPJ1WA
}
