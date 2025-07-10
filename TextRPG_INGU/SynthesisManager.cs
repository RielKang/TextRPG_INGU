using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_INGU
{
    internal class SynthesisManager
    {
        public static void TrySynthesize(ref int playerGold)
        {
            int synthesisCost = 500;
            var inv = InventoryManager.Instance;

            // 합성 아이템 필터링: IsSpecial == true
            var synthesisItems = inv.Items.Where(item => item.IsSpecial).ToList();

            if (synthesisItems.Count < 5)
            {
                Console.WriteLine("합성 재료가 부족합니다! 총 5개의 특수 아이템이 필요합니다.");
                return;
            }

            if (playerGold < synthesisCost)
            {
                Console.WriteLine("골드가 부족합니다. 합성에는 500G가 필요합니다.");
                return;
            }

            // 골드 차감
            playerGold -= synthesisCost;

            // 특수 아이템 제거
            foreach (var item in synthesisItems)
            {
                inv.Items.Remove(item);
            }

            // 결과 아이템 지급: ShopItemDatabase에서 가져오기
            var resultItem = ShopItemDatabase.GetSynthesisResultItem();
            inv.AddItem(resultItem);

            Console.WriteLine($"합성이 완료되었습니다! '{resultItem.Name}'을(를) 획득했습니다.");
        }


    }
}
