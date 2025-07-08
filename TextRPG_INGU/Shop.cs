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

        private List<ShopItem> shopItems; // 상점 아이템 목록
        public Shop()
        {
            shopItems = new List<ShopItem>
            {
                new ShopItem("초보자의 철검", "공격력 +5", "기본적인 철제 검입니다.", 100),
                new ShopItem("가죽 갑옷", "방어력 +3", "가벼운 가죽으로 만들어진 갑옷입니다.", 80),
                new ShopItem("회복 물약", "체력 회복 +20", "피로를 회복시켜주는 물약입니다.", 50)
            };
        }
        // 이미 구매한 아이템 체크
        public void SetPurchased(string itemName)
        { 
            foreach (var item in shopItems)
            {
                if (item.Name == itemName)
                {
                    item.IsPurchased = true;
                }
            }
        }

        // 상점 기본 화면
        public void ShowShop(int playerGold)
        {
            Console.WriteLine("상점\n필요한 아이템을 얻을 수 있는 상점입니다.\n");
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{playerGold}G");
            Console.WriteLine("[아이템 목록]");
            foreach (var item in shopItems)
            {
                string priceText = item.IsPurchased ? "구매 완료" : $"{item.Price}G";
                Console.WriteLine($"- {item.Name, -10} | {item.StatText, -8} | {item.Description, -30} | {priceText}"); // 보간 문법 사용. {글자수, 너비}
            }
            Console.WriteLine();
            Console.WriteLine("1. 아이템 구매");
            Console.WriteLine("0. 상점 나가기");
            Console.WriteLine("\n 원하시는 행동을 입력해 주세요.");
            Console.WriteLine(">>");
        }
        // 아이템 구매시 아이템 앞에 번호 추가
        public void ShowShopForPurchase(int playerGold)
        {
            Console.WriteLine("상점\n필요한 아이템을 얻을 수 있는 상점입니다.\n");
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{playerGold}G");
            Console.WriteLine("[아이템 목록]");
            for (int i = 0; i < shopItems.Count; i++)
            {
                var item = shopItems[i];
                string priceText = item.IsPurchased ? "구매 완료" : $"{item.Price}G";
                Console.WriteLine($"{i + 1}. {item.Name, -10} | {item.StatText, -8} | {item.Description, -30} | {priceText}");
            }
            Console.WriteLine();
            Console.WriteLine("0. 상점 나가기");
            Console.WriteLine("\n 원하시는 행동을 입력해 주세요.");
            Console.WriteLine(">>");
        }
        // 상점 아이템 구매
        public bool TryPurchase(int index, ref int playerGold, InventoryManager inventory)
        {
            if (index < 0 || index >= shopItems.Count)
            { 
                Console.WriteLine("잘못된 입력입니다.");
                return false;
            }
            var item = shopItems[index];
            if (item.IsPurchased)
            {
                Console.WriteLine("이미 구매한 아이템입니다.");
                return false;
            }
            if (playerGold < item.Price)
            {
                Console.WriteLine("골드가 부족합니다.");
                return false;
            }
            playerGold -= item.Price; // 골드 차감
            item.IsPurchased = true; // 아이템 구매 완료
            inventory.AddItem(new Item(item.Name, item.StatText, item.Description)); // 인벤토리에 아이템 추가
            Console.WriteLine("구매를 완료했습니다.");
            return true;
        }

        public List<ShopItem> Items => shopItems;
    }
}
