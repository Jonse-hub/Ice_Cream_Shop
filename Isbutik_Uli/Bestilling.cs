using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Isbutik_Uli
{
    // bestillings klassen
    public class Bestilling
    {
        
        private float price;
        private int quantity;
        private float sum;

        public int Id { get; set; }
        public Vare Vare { get; set; }
        public int Quantity
        {
            get { return quantity; }
            set
            {
                if(quantity != value)
                {
                    quantity = value;
                    sum = Quantity * price;
                }
            }
        }
        public float Sum
        {
            get { return sum; }
            set
            {
                if (sum != value)
                {
                    sum = value;
                }
            }
        }
        public Bestilling (Vare vare, int quantity)
        {
            Id = vare.Id;
            Quantity = quantity;
            Sum = Quantity * vare.Price;
            
        }

        // nemmere bestilling
        public Bestilling(int id, int quantity, Vare vare)
        {
            Id = id;
            Vare = vare;
            Quantity = quantity;
            price = vare.Price;
            Sum = Quantity * vare.Price;
        }

    }
}
