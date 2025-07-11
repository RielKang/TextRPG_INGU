using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_INGU.Data
{
    
    public class ShopItem
    {
        public string Name { get; }
        public string StatText { get; }
        public string Description { get; }
        public int Price { get; }
        public bool IsPurchased { get; set; }
        public bool IsSpecial { get; }
        public string EquipType { get; }

        public ShopItem(string name, string statText, string description, int price, bool isSpecial = false, string equipType = "")
        {
            Name = name;
            StatText = statText;
            Description = description;
            Price = price;
            IsSpecial = isSpecial;
            EquipType = equipType;
            IsPurchased = false;
        }
    }

}
