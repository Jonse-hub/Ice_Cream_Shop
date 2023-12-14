using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Isbutik_Uli
{
    public class IsButikFunc
    {
        // isbutikfunc, må kalde functioner fra isbutikdata
        public IsButikData data = new IsButikData();

        public void TestDatabase(List<Bestilling> bestillingsListe, List<Vare> vareListe)
        {
            data.TestDatabase(bestillingsListe, vareListe);
        }

        public void ReadVarer(List<Vare> vareListe)
        {
            data.ReadVarer(vareListe) ;
        }
        
        public void CreateVare(Vare vare)
        {
            if (vare.Id > 0)
                data.UpdateVare(vare);
            else 
                data.CreateVare(vare);

        }
        public void CreateBestilling(Bestilling bestilling)
        {
            data.CreateBestilling(bestilling);
        }
        public void ReadBestillinger(List<Bestilling> bestillingsliste, List<Vare> vareListe)
        {
            data.ReadBestillinger(bestillingsliste, vareListe);
        }
        public void UpdateVare(Vare vare)
        {
            data.UpdateVare(vare);
        }

        public void DeleteBestilling(Bestilling bestilling)
        {
            data.DeleteBestilling(bestilling);
        }
        public void DeleteVare(Vare vare) // selv tilføjet
        {
            data.DeleteVare(vare);
        }

    }
}
