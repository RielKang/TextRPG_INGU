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
            // 모든 아이템의 장착 해제
            foreach (var item in items)
            {
                item.IsEquipped = false;
            }
            // 선택 아이템 장착
            items[index].IsEquipped = true;
            Console.WriteLine($"{items[index].Name}을 장착했습니다");

        }
        public int Count => items.Count;
        public List<Item> Items => items;
    }
}
