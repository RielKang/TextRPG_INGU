using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_INGU
{
    
    internal class Shop
    {
        private static Shop instance;
        public static Shop Instance
        {
            get
            {
                if (instance == null)
                    instance = new Shop();
                return instance;
            }
        }


        private List<ShopItem> shopItems;

        public Shop()
        {
            shopItems = ShopItemDatabase.LoadShopItems(); // 데이터베이스에서 아이템 로딩
        }

        public void ShowShop(int playerGold)
        {
            Console.WriteLine($"[보유 골드] {playerGold}G\n[아이템 목록]");
            for (int i = 0; i < shopItems.Count; i++)
            {
                var item = shopItems[i];
                string priceText = item.IsPurchased ? "구매 완료" : $"{item.Price}G";
                Console.WriteLine($"{i + 1}. {item.Name} | {item.StatText} | {item.Description} | {priceText}");
            }
            Console.WriteLine();
            Console.WriteLine("1. 아이템 구매");
            Console.WriteLine("0. 나가기");
            Console.WriteLine("\n원하시는 행동을 입력해 주세요:");
            Console.Write(">> ");

        }

        public bool TryPurchase(int index, ref int playerGold, InventoryManager inventory)
        {
            if (index < 0 || index >= shopItems.Count) return false;

            var item = shopItems[index];
            if (item.IsPurchased || playerGold < item.Price) return false;

            playerGold -= item.Price;
            item.IsPurchased = true;

            var newItem = new Item(item.Name, item.StatText, item.Description, item.EquipType, item.IsSpecial);
            inventory.AddItem(newItem);

            Console.WriteLine($"{item.Name}을 구매했습니다.");
            return true;
        }
        public void ShowShopForPurchase(int playerGold)
        {
            Console.WriteLine($"[보유 골드] {playerGold}G\n[아이템 구매 목록]");

            for (int i = 0; i < shopItems.Count; i++)
            {
                var item = shopItems[i];
                string priceText = item.IsPurchased ? "구매 완료" : $"{item.Price}G";
                Console.WriteLine($"{i + 1}. {item.Name} | {item.StatText} | {item.Description} | {priceText}");
            }

            Console.WriteLine("\n0. 상점 나가기");
            Console.WriteLine("원하시는 행동을 입력해 주세요:");
            Console.WriteLine(">> ");
        }

        public List<ShopItem> Items => shopItems;
    }
}
