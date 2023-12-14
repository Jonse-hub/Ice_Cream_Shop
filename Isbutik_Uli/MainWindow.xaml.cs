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

namespace Isbutik_Uli
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // må ikke kalde directe fra IsButikData
        public IsButikFunc func = new IsButikFunc();
        public Vare vare = new Vare();
        //public Bestilling bestilling = new Bestilling(vare.Name, 8);

        // lister med vare og bestillinger, 
        public List<Vare> vareListe = new List<Vare>();
        public List<Bestilling> bestillingsListe = new List<Bestilling>();

        bool isEditing = false;

        public MainWindow()
        {
            InitializeComponent();

            func.ReadVarer(vareListe);
            //vareListe.Add(new Vare { Name = "Magnum", Description = "Vores klassiker", Price = 25.15f });
            //vareListe.Add(new Vare { Name = "Trafiklys", Description = "Vores bestseller", Price = 17.80f });
            //vareListe.Add(new Vare { Name = "Magnum hvid", Description = "Faktisk snarere cremefarvet", Price = 26.25f });
            //bestillingsListe.Add(new Bestilling(vareListe[2], 4));
            //bestillingsListe.Add(new Bestilling(vareListe[0], 7));

            //bestillingsListe.Add(new Bestilling(vareListe[1], 12));

            func.TestDatabase(bestillingsListe, vareListe);

            CBx_Is.ItemsSource = vareListe;

            //

            //func.CreateBestilling(new Bestilling(1, 3, vareListe[1])); hvis ingen i
            DG_Bestillinger.ItemsSource = bestillingsListe;
            // TBl_IceDesciption.Text = (CBx_Is.SelectedItem as Vare).Description; skal have "Magnum" isSelected="True"

            //func.EksempelMedtode();


        }
        private void Btn_AddIce_Click(object sender, RoutedEventArgs e)
        {
            // DG_Bestillinger.ItemsSource = LoadCollectionData();
            // DG_Bestillinger.Items.Add();


            //bestillingsListe.Add(new Bestilling(vareListe[CBx_Is.SelectedIndex], Convert.ToInt32(Btx_AntalIs.Text)));
            try {
                bestillingsListe.Add(new Bestilling(0, Convert.ToInt32(Btx_AntalIs.Text), vareListe[CBx_Is.SelectedIndex])); // skal have id i starten ellers virker ikke.

                DG_Bestillinger.Items.Refresh(); // refresh Datagrid så den viser nye bestilling

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Fejl i bestilling");
            }

        }

        //private void gemnyVare()
        //{
        //    CBx_Is.Items.Refresh();
        //}
        // 
        private void CBx_Is_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CBx_Is.SelectedItem != null) { // Skifter når ikke nul, for at undgå crash, ved redigering af vare
                TBl_IceDesciption.Text = (CBx_Is.SelectedItem as Vare).Description;

                if(isEditing == true)
                {
                    isEditing = false;
                    Btn_RedigerVare.Content = "Rediger vare";
                }
                
            }

            //switch (CBx_Is.Text)
            //{
            //    case "Magnum":


            //        break;
            //    case "Astronaut":
            //        TBl_IceDesciption.Text = "Dette er en Astronaut";
            //        break;
            //    case "Lillebror":
            //        TBl_IceDesciption.Text = "Dette er en Lillebror";
            //        break;
            //    case "Kung fu":
            //        TBl_IceDesciption.Text = "Dette er en Kung fu";
            //        break;
            //}
        }

        private void Btn_AddVare_Click(object sender, RoutedEventArgs e)
        {
            //if ()
            //{
            //    func.UpdateVare(vare);
            //}

            // Id= Convert.ToInt32(TB_Id.Text),  vil ikke ændre id, den tæller kun op
            try
            {
                //vareListe.Add(new Vare { Name = TB_Varenavn.Text, Description = TB_Beskrivelse.Text, Price = float.Parse(TB_Stykpris.Text) });

                func.CreateVare(new Vare { Name = TB_Varenavn.Text, Description = TB_Beskrivelse.Text, Price = float.Parse(TB_Stykpris.Text) });
                func.ReadVarer(vareListe);
                TB_Varenavn.Text = "";
                TB_Stykpris.Text = "";
                TB_Beskrivelse.Text = "";
                CBx_Is.Items.Refresh();

                if (isEditing == true)
                {
                    isEditing = false;
                    Btn_RedigerVare.Content = "Rediger vare";
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Fejl i Vare");
            }
        

        }
            
        

        private void Btn_RemoveIce_Click(object sender, RoutedEventArgs e)
        {
            // DG_Bestillinger.SelectedItem
            if(DG_Bestillinger.SelectedItem != null)
            {
                bestillingsListe.Remove(bestillingsListe[DG_Bestillinger.SelectedIndex]);
                DG_Bestillinger.Items.Refresh();
            }
            
        }

        private void Btn_Bestil_Click(object sender, RoutedEventArgs e)
        {
            bestillingsListe.Clear();
            DG_Bestillinger.Items.Refresh();
        }

        private void Btn_GemBestillinger_Click(object sender, RoutedEventArgs e)
        {
            // foreach loop func.CreateBestilling(new Bestilling (bestil.Id, bestil.VareId, bestil.Quantity, bestil.Sum)) ;
            

            foreach (Bestilling bestil in bestillingsListe) // gemmer bestillinger i database.
            {
                //bestillingsListe.Remove(bestil);
                //func.DeleteBestilling(bestil); // delete all
                func.CreateBestilling(bestil);


                //bestillingsListe.Add(bestil); // not working
            }
            bestillingsListe.Clear();
            DG_Bestillinger.Items.Refresh();
        }

        private void Btn_RedigerVare_Click(object sender, RoutedEventArgs e) // ændre vare i liste, sætter den nye vare i database
        {
            //CBx_Is.SelectedItem as Vare.
            //vare = CBx_Is.SelectedItem as Vare;
            //func.UpdateVare(new Vare { Id = (CBx_Is.SelectedItem as Vare).Id, Name = TB_Varenavn.Text, Description = TB_Beskrivelse.Text, Price = float.Parse(TB_Stykpris.Text) });
            //func.ReadVarer(vareListe);
            if (CBx_Is.SelectedItem != null) {
                if (isEditing == true) // vareListe[CBx_Is.SelectedIndex].Id != 0
                {
                    vareListe[CBx_Is.SelectedIndex] = new Vare { Id = (CBx_Is.SelectedItem as Vare).Id, Name = TB_Varenavn.Text, Description = TB_Beskrivelse.Text, Price = float.Parse(TB_Stykpris.Text) };
                    func.UpdateVare(vareListe[CBx_Is.SelectedIndex]);

                    isEditing = false;
                    Btn_RedigerVare.Content = "Rediger vare";
                }
                else
                {
                    isEditing = true;
                    TB_Varenavn.Text = (CBx_Is.SelectedItem as Vare).Name;
                    TB_Stykpris.Text = (CBx_Is.SelectedItem as Vare).Price.ToString();
                    TB_Beskrivelse.Text = (CBx_Is.SelectedItem as Vare).Description;

                    Btn_RedigerVare.Content = "Lav Ændring";
                }

                

                /*try { 


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Fejl i Vare");
                } */
            }
        }

        private void Btn_IndlæsBestillinger_Click(object sender, RoutedEventArgs e)
        {
            func.ReadBestillinger(bestillingsListe, vareListe);
            DG_Bestillinger.Items.Refresh();
        }

        private void Btn_DeleteVare_Click(object sender, RoutedEventArgs e)
        {
            if (CBx_Is.SelectedItem != null)
            {
                //CBx_Is.SelectedIndex
                func.DeleteVare(vareListe[CBx_Is.SelectedIndex]);
                func.ReadVarer(vareListe);

                CBx_Is.Items.Refresh();
            }
        }
    }
}
