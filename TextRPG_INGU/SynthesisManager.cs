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

            if (!inv.HasAllSpecialItems())
            {
                Console.WriteLine("아직 합성에 필요한 모든 아이템을 보유하지 않았습니다!");
                return;
            }

            if (playerGold < synthesisCost)
            {
                Console.WriteLine("골드가 부족합니다. 합성에 필요한 골드는 500G입니다.");
                return;
            }

            // 골드 차감
            playerGold -= synthesisCost;

            // 아이템 삭제
            var names = new[] {
            "찢어진 붉은 깃발 1", "찢어진 붉은 깃발 2",
            "찢어진 붉은 깃발 3", "찢어진 붉은 깃발 4", "찢어진 붉은 깃발 5"
        };

            foreach (var name in names)
            {
                var toRemove = inv.Items.FirstOrDefault(i => i.Name == name);
                if (toRemove != null) inv.Items.Remove(toRemove);
            }

            // 결과물 지급
            inv.AddItem(new Item("스파르타의 붉은 군기", "공격력 +40 / 방어력 +40 / 체력 +40 ", "스파르타를 상징하는 붉은색의 깃발이 달린 창.", "무기",false));
            Console.WriteLine("합성이 완료되었습니다! '스파르타의 붉은 군기'을(를) 획득했습니다.");
        }

    }
}
