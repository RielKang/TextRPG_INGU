using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_INGU
{
    internal class InventoryManager
    {
        //싱글턴 패턴
        private static InventoryManager instance;
        public static InventoryManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new InventoryManager();
                }
                return instance;
            }
        }

        //아이템 리스트 초기화
        private List<Item> items = new List<Item>();
        //아이템 추가
        public void AddItem(Item item)
        { 
            items.Add(item);
        }
        // 아이템 효과-----------------------------------------------------------------------------------------------
        public (int attack, int defense, int hp) GetEquippedStats()
        {
            int atk = 0, def = 0, hp = 0;

            foreach (var item in items.Where(i => i.IsEquipped))
            {
                if (item.StatText.Contains("공격력"))
                    atk += ExtractValue(item.StatText);

                if (item.StatText.Contains("방어력"))
                    def += ExtractValue(item.StatText);

                if (item.StatText.Contains("체력"))
                    hp += ExtractValue(item.StatText);
            }

            return (atk, def, hp);
        }

        // 문자열에서 +숫자 추출
        private int ExtractValue(string statText)
        {
            var match = System.Text.RegularExpressions.Regex.Match(statText, @"\+(\d+)");
            return match.Success ? int.Parse(match.Groups[1].Value) : 0;
        }
        //----------------------------------------------------------------------------------------------------

        //아이템 관리
        public void ShowItems()
        { 
            Console.WriteLine("보유중인 아이템을 관리할수 있습니다.");
            Console.WriteLine("[아이템 목록]");
            if (items.Count == 0)
            {
                Console.WriteLine("보유중인 아이템이 없습니다.");
            }
            else
            {
                // 아이템 정보 출력
                foreach (Item item in items)
                { 
                    string equipMark = item.IsEquipped ? "[E]" : "";
                    Console.WriteLine(equipMark + item.Name + item.StatText + item.Description);
                }
            }
            Console.WriteLine();
            Console.WriteLine("1. 장착관리");

            // 조건부로 합성 옵션 출력
            if (HasAllSpecialItems())
            {
                Console.WriteLine("3. 아이템 합성");
            }


            Console.WriteLine( items.Count >0 ? "2. 나가기" : "0. 나가기");
            Console.WriteLine("\n원하시는 행동을 입력해주세요");
            Console.Write(">> ");
        }
        // 아이템 장착 관리
        public void EquipItem(int index)
        {
            if (index < 0 || index >= items.Count)
            {
                Console.WriteLine("잘못된 선택입니다.");
                return;
            }

            var selectItemectedItem = items[index];
            // 이미 장착된 아이템을 다시 선택하면 장착 해제
            if (items[index].IsEquipped)
            {
                items[index].IsEquipped = false;
                Console.WriteLine($"{items[index].Name}의 장착을 해제했습니다.");
            }
            else 
            {
                
                selectItemectedItem.IsEquipped = true;
                Console.WriteLine($"{selectItemectedItem.Name}을 장착했습니다.");


            }
        }

        public int Count => items.Count;
        public List<Item> Items => items;

        public bool HasAllSpecialItems()
        {
            var specialNames = new List<string>
            {
                "찢어진 붉은 망토",
                "부러진 붉은 깃창",
                "붉은 가죽 갑옷",
                "빛바랜 붉은 샌들",
                "구멍난 붉은 두건"
            };

            return specialNames.All(name => items.Any(i => i.Name == name));
        }

    }
}
