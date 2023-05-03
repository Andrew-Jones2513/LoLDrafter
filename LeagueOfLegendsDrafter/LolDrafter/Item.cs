using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LolDrafter
{
    public class Item
    {
        // Properties
        public string Name { get; set; }
        public string Description { get; set; }
        public int Cost { get; set; }
        public System.Drawing.Image Image { get; set; }

        // Constructor
        public Item() { }

        public Item(string name, string description, int cost, System.Drawing.Image image)
        {
            Name = name;
            Description = description;
            Cost = cost;
            Image = image;
        }
    }
}
