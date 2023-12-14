using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Isbutik
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //string IvareOversigt {  }
        }
        //public class Vare : IvareOversigt
        //{
        //    …
        //}


        private void Btn_AddIce_Click(object sender, RoutedEventArgs e)
        {
            // DG_Bestillinger.ItemsSource = LoadCollectionData();
            // DG_Bestillinger.Items.Add();
            
        }

        private void CBx_Is_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            switch (CBx_Is.Text)
            {
                case "Magnum":
                    TBl_IceDesciption.Text = "Dette er en magnum";
                    break;
                case "Astronaut":
                    TBl_IceDesciption.Text = "Dette er en Astronaut";
                    break;
                case "Lillebror":
                    TBl_IceDesciption.Text = "Dette er en Lillebror";
                    break;
                case "Kung fu":
                    TBl_IceDesciption.Text = "Dette er en Kung fu";
                    break;
            }
        }
    }
}
