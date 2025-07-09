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
                new ShopItem("초보자의 낡은 철검 |", "공격력 +5 }", "낡아서 부러질거 같은 철제 검이다. |", 100,false ,"무기"),
                new ShopItem("초보자의 가죽 갑옷 |", "방어력 +3 |", "가벼운 가죽으로 만들어진 갑옷이다. |", 80, false ,"갑옷"),
                new ShopItem("초보자의 가죽 투구 |", "방어력 +1 |", "가벼운 가죽으로 만들어진 투구이다. |", 50, false, "투구"),
                //new ShopItem("초보자의 활 |", "공격력 +4", "기본적인 활입니다.", 120, false, "무기"),
                //new ShopItem("초보자의 마법봉 |", "공격력 +6", "기본적인 마법봉.", 150, false, "무기"),
                new ShopItem("초보자의 방패 |", "방어력 +2 |", "나무로 만들어진 기본적인 방패이다. |", 70, false, "방패"),
                new ShopItem("초보자의 가죽 장갑 |", "방어력 +1 |", "가벼운 가죽으로 만들어진 장갑이다. |", 40, false, "장갑"),
                new ShopItem("초보자의 가죽 장화 |", "방어력 +1 |", "가벼운 가죽으로 만들어진 장화이다. |", 60, false, "신발"),
                new ShopItem("초보자의 모직 망토 |", "체력 +1 |", "두꺼운 모직으로 만든 낡은 망토이다. 방어력은 기대하기 어렵다. |", 50, false, "망토"),
                //new ShopItem("용병의 사슬갑옷", "방어력 +5", "용병의 사슬갑옷이다. 여기저기 녹슬어 있어서 관리가 필요해 보인다.", 90, false, "갑옷"),
                //new ShopItem("용병의 철투구", "방어력 +3", "용병의 철투구다. 소문에 따르면 냄비로 더많이 사용되었다고 한다.", 70, false, "투구"),
                //new ShopItem("용병의 사슬장갑", "방어력 +2", "용병의 사슬장갑이다. 무기 대신 사용했는지 핏자국이 묻어있다.", 50, false, "장갑"),
                //new ShopItem("용병의 사슬장화", "방어력 +2", "용병의 사슬장화다. 발목을 잘 잡아줘서 안정감이 있다.", 80, false, "신발"),
                new ShopItem("브로드 소드 |", "공격력 +7", "표준 규격의 브로드 소드이다. 튼튼하고 날이 날카롭게 잘 서있다. |", 200, false, "무기"),
                //new ShopItem("기사의 플레이트 아머", "방어력 +7", "기사의 갑옷이다. 튼튼하지만 무겁다.", 450, false, "갑옷"),
                //new ShopItem("기사의 헬름", "방어력 +5", "기사의 투구다. 머리를 잘 보호해준다.", 300, false, "투구"),
                //new ShopItem("기사의 건틀렛", "방어력 +4", "기사의 건틀렛이다. 손을 잘 보호해준다.", 150, false, "장갑"),
                //new ShopItem("기사의 부츠", "방어력 +5", "기사의 부츠다. 발목을 잘 잡아줘서 안정감이 있다.", 200, false, "신발"),
                new ShopItem("붉은 수정 반지 |", "체력 +10 |", "따뜻한 기운이 느껴지는 반지이다. 왠지 모르겠지만 고양감이 느껴진다. |", 500, false, "장신구"),
                new ShopItem("찢어진 붉은 깃발 1 |", "체력 +1 |", "붉은색의 깃발 조각이다. 다른 4 조각을 모두 모아야 알거 같다. |", 50, true, "망토"),
                new ShopItem("찢어진 붉은 깃발 2 |", "공격력 +1 |", "붉은색의 깃발 조각이다. 다른 4 조각을 모두 모아야 알거 같다. |", 50, true, "무기"),
                new ShopItem("찢어진 붉은 깃발 3 |", "방어력 +1 |", "붉은색의 깃발 조각이다. 다른 4 조각을 모두 모아야 알거 같다. |", 50, true, "갑옷"),
                new ShopItem("찢어진 붉은 깃발 4 |", "체력 +1 |", "붉은색의 깃발 조각이다. 다른 4 조각을 모두 모아야 알거 같다. |", 50, true, "신발"),
                new ShopItem("찢어진 붉은 깃발 5 |", "체력 +1 |", "붉은색의 깃발 조각이다. 다른 4 조각을 모두 모아야 알거 같다. |", 50, true, "투구")
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
