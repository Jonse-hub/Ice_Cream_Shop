using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Isbutik_Uli
{
    // classe directe adgang til mainwindow.xaml.cs
    public class Vare
    {
        // properties til wpf, fra sql med get
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }

    }
}
