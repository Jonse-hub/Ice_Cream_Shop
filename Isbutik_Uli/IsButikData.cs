using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Isbutik_Uli
{
    using Isbutik_Uli.DSIsbutikTableAdapters;
    using static Isbutik_Uli.DSIsbutik;

    // public classe
    public class IsButikData
    {
            // classe adgang til datasettet i sql {
            DSIsbutik isButikDataset = new DSIsbutik();
            // Field. Benyttes for at kalde metoder i klassen IsLagerDataSet herfra.
            TableAdapterManager AdapterManager = new TableAdapterManager();
            // Field. Benyttes for at kalde metoder i klassen TableAdapterManager herfra.

            public IsButikData()
            // Konstruktor. Benyttes til at oprette instanser(objekter) af vores adapters.
            {
                AdapterManager.BestillingTableAdapter = new BestillingTableAdapter();
                AdapterManager.VareTableAdapter = new VareTableAdapter();
            }
            // }}classe adgang til datasettet i sql 
        public void ReadVarer(List<Vare> vareListe) // Method. Henter alle varer fra databasen.
        {
            vareListe.Clear();  // Tøm listen før vi fylder den. På den måde forhindrer vi dubletter.
            VareDataTable vareRows = new VareDataTable();
            AdapterManager.VareTableAdapter.Fill(vareRows);

            foreach(VareRow row in vareRows)
            {
                if (row.Stykpris >= 0)  // varer med negativ pris betragter vi som logisk slettet
                {

                    Vare vare = new Vare()
                    {
                        Id = row.Id,
                        Name = row.Varenavn,
                        Price = (float)row.Stykpris,
                        Description = row.Beskrivelse,
                    };
                    vareListe.Add(vare);
                }
            }
        }

        //
        /*public float GetVarePrice(List<Vare> vareListe, int Vare.Id)
        // Find bestillingens varepris. Her lavet med en løkke, 
        // andre mere avancerede metoder kan anvendes.
        // Man kan fx bruge Find-metoden på listen eller man kan anvende LINQ.

        {
            foreach(Vare vare in vareListe)
            {
                    if(vare.Id == Vare.Id)
                    {
                        return vare.Price;
                    }
            }
            return -1.0f;
        } */

        public Vare GetVare(List<Vare> vareListe, int vareId)
        // Find varen med en bestemt vareId. Her lavet med en løkke, andre mere avancerede metoder kan anvendes.
        // Man kunne fx bruge Find-metoden på listen anvende LINQ.
        {
            foreach (Vare vare in vareListe)
            {
                if (vare.Id == vareId)
                {
                    return vare;
                }
            }
            return null;  // ingen vare i vareListe har den Id vi ledte efter
        }

        public void ReadBestillinger (List<Bestilling> bestillingsListe, List<Vare> vareListe)
        // Method. Henter alle bestillinger fra databasen.
        {
            bestillingsListe.Clear();
            // Tøm listen før vi fylder den. På den måde forhindrer vi dubletter.
            BestillingDataTable bestillingRows = new BestillingDataTable();
            AdapterManager.BestillingTableAdapter.Fill(bestillingRows);

            foreach (BestillingRow row in bestillingRows)
            {
                Bestilling bestilling = new Bestilling(row.Id, row.Antal, GetVare(vareListe, row.VareId));
                bestillingsListe.Add(bestilling);
            }
        }

        public void CreateVare(Vare vare) // gemme indholdet af vare i database
        {
            VareRow row = isButikDataset.Vare.NewVareRow();
            row.Beskrivelse = vare.Description;
            row.Varenavn = vare.Name;
            row.Stykpris = (decimal)vare.Price;
            RækkeTilDatabase(row);
        }

        private void RækkeTilDatabase(VareRow row) // tilføjer dataset til database
        {
            isButikDataset.Vare.Rows.Add(row);
            AdapterManager.VareTableAdapter.Update(isButikDataset.Vare);
        } 

        public void CreateBestilling(Bestilling bestilling) // ny bestilling til sql database
        {
            BestillingRow row = isButikDataset.Bestilling.NewBestillingRow();
            row.Antal = bestilling.Quantity;
            row.VareId = bestilling.Vare.Id;
            RækkeTilDatabase(row);
        }

        private void RækkeTilDatabase(BestillingRow row)  // Overloading, samme metode forskellig signatur
        {
            isButikDataset.Bestilling.Rows.Add(row);
            AdapterManager.BestillingTableAdapter.Update(isButikDataset.Bestilling);
        }


        public void UpdateVare(Vare vare) // præcis række skal opdateres
        {
            VareDataTable vareRows = new VareDataTable();
            AdapterManager.VareTableAdapter.Fill(vareRows);  // Hent database rows
            VareRow row = vareRows.FindById(vare.Id); // Find row
            row.Beskrivelse = vare.Description;  // Opdater row
            row.Varenavn = vare.Name;
            row.Stykpris = (decimal)vare.Price;
            AdapterManager.VareTableAdapter.Update(vareRows); // Gem forandringer
        }

        public void UpdateBestilling(Bestilling bestilling)  // præcis række skal opdateres
        {
            BestillingDataTable bestillingRows = new BestillingDataTable();
            AdapterManager.BestillingTableAdapter.Fill(bestillingRows);  // Hent database rows
            BestillingRow row = bestillingRows.FindById(bestilling.Id); // Find row
            row.Antal = bestilling.Quantity;  // Opdater row
            row.VareId = bestilling.Vare.Id;
            AdapterManager.BestillingTableAdapter.Update(bestillingRows); // Gem forandringer
        }// præcis række skal opdateres

        public void DeleteBestilling(Bestilling bestilling) // slet en bestilling
        {
            AdapterManager.BestillingTableAdapter.Delete(bestilling.Id, bestilling.Quantity, bestilling.Vare.Id);
        }

        public void DeleteVareLogisk(Vare vare) // hvis slettet vare pris er negativ
        {
            vare.Price = -99.0f;
            UpdateVare(vare);
        }


        // udkommenter 3-2 linjer ad gangen og du kan få error selvom koden er rigtig. test hvad man ikke kan.
        public void TestDatabase(List<Bestilling> bestillingsListe, List<Vare> vareListe)
        {
            //CreateVare(vareListe[0]);
            //bestillingsListe.Add(new Bestilling(vareListe[0], 6));
            //CreateBestilling(bestillingsListe[0]);

            //vareListe[0].Price = 19.98f;
            //UpdateVare(vareListe[0]);

            //ReadVarer(vareListe);
            //DeleteVareLogisk(vareListe[0]);
            //ReadVarer(vareListe);

            //ReadBestillinger(bestillingsListe, vareListe);
            //CreateBestilling(bestillingsListe[0]);

            //ReadBestillinger(bestillingsListe, vareListe);
            //bestillingsListe[2].Quantity = 1;
            //UpdateBestilling(bestillingsListe[2]);

            //DeleteBestilling(bestillingsListe[7]);
        }

        public void DeleteVare(Vare vare) // slet en bestilling, selv tilføjet
        {
            AdapterManager.VareTableAdapter.Delete(vare.Id, vare.Name, (Decimal)vare.Price);
        }

    }
}
