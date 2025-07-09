using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_INGU
{
    internal class Item
    {
        public string Name { get; }
        public string Description { get; }
        public string StatText { get; }
        public bool IsEquipped { get; set; }

        public Item(string name, string statText, string description)
        {
            Name = name;
            StatText = statText;
            Description = description;
            IsEquipped = false;
        }

        // Item.cs
        public bool IsSpecial { get; } // 이 아이템이 합성용 특수 아이템인지
        public string EquipType { get; } // 아이템의 장착 타입 (무기, 방어구 등)
        public Item(string name, string statText, string description, string equipType = "", bool isSpecial = false)
        {
            Name = name;
            StatText = statText;
            Description = description;
            EquipType = equipType;
            IsEquipped = false;
            IsSpecial = isSpecial;
        }

    }
}
