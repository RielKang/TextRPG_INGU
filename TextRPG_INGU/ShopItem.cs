using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_INGU
{
    internal class ShopItem
    {
        public int Price { get; }
        public bool IsPurchased { get; set; }
        public string Name { get; internal set; }
        public string StatText { get; internal set; }
        public string Description { get; internal set; }

        public ShopItem(string name, string statText, string description, int price)
        {
            Name = name;
            StatText = statText;
            Description = description;
            Price = price;
            IsPurchased = false;
        }

        public bool IsSpecial { get; }  // 상점 아이템도 특수 아이템 여부 추가

        public ShopItem(string name, string statText, string description, int price, bool isSpecial = false)
        {
            Name = name;
            StatText = statText;
            Description = description;
            Price = price;
            IsPurchased = false;
            IsSpecial = isSpecial;
        }

    }
}
