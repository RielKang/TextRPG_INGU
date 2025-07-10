using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_INGU
{
    internal class ShopItemDatabase
    {
        public static List<ShopItem> LoadShopItems()
        {
            return new List<ShopItem>
            {
                new ShopItem("초보자의 철검", "공격력 +5", "낡은 철제 검", 100, false, "무기"),
                new ShopItem("초보자의 가죽 갑옷", "방어력 +3", "가벼운 가죽 갑옷", 80, false, "갑옷"),
                new ShopItem("찢어진 붉은 망토", "체력 +1", "찢어진 낡은 망토다. 어떤 문양이 그려져 있던 자국이 있다.", 50, true, "망토"),
                new ShopItem("부러진 붉은 깃창", "공격력 +1", "부러진 붉은 깃창이다. 문양의 일부가 훼손되어서 알아볼수 없다.", 50, true, "무기"),
                new ShopItem("붉은 가죽 갑옷", "방어력 +1", "낡은 붉은 갑옷이다. 알 수 없는 문양이 새겨져 있다.", 50, true, "갑옷"),
                new ShopItem("빛바랜 붉은 샌들", "체력 +1", "빛바랜 붉은 샌들이다. 뭉개진 단추에 어떤 문양이 새겨져있었던거 같다.", 50, true, "신발"),
                new ShopItem("구멍난 붉은 두건", "체력 +1", "구멍난 붉은 두건이다. 어떤 문양이 희미하게 그려져 있다.", 50, true, "투구"),
             // 필요에 따라 계속 추가 가능
            };
        }
       
        // 합성 결과 아이템 데이터 (특수 아이템 아님)
        public static string SynthesisResultItemName = "스파르타의 붉은 깃창";// 합성 결과 아이템 이름 변경 가능!!
        public static Item GetSynthesisResultItem()
        {
            
            return new Item(
                SynthesisResultItemName,
                "공격력 +40 / 방어력 +40 / 체력 +40",
                "스파르타를 상징하는 붉은 깃발이 달린 창",
                "무기",
                false
            );
        }


    }
}
