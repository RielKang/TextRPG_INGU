using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_INGU.Managers;

namespace TextRPG_INGU.Data
{
    public class Player
    {
        public string Name { get; set; }// 플레이어 이름
        public string Job { get; set; } // 직업
        public int Level { get; set; } = 1;// 레벨
        public int Experience { get; set; } // 경험치
        public int Gold { get; set; } = 1500;// 골드
        public int Health { get; set; } = 100;// 현재 체력
        public int MaxHealth { get; set; }= 100;// 최대 체력
        public int Attack { get; set; } = 10; // 공격력
        public int Defense { get; set; } = 0; // 방어력 (추가 기능으로 방어력도 추가 가능)
        public InventoryManager Inventory { get; private set; }// 인벤토리 아이템 목록
        public Player(string name, string job)
        {
            Name = name;
            Job = job; // 기본 직업 설정
            Level = 1;
            Experience = 0;
            Gold = 1500;
            Health = 100;
            MaxHealth = 100;
            Attack = 10;
            Defense = 5; // 기본 방어력 설정
            Inventory = InventoryManager.Instance;
        }
        public void AddGold(int amount)
        {
            Gold += amount;
        }

        public void AddExperience(int amount)
        {
            Experience += amount;
            while (Experience >= 100) // 레벨업 조건
            {
                Level++;
                Experience -= 100; // 남은 경험치
                MaxHealth += 10; // 레벨업 시 최대 체력 증가
                Health = MaxHealth; // 체력 회복
                Console.WriteLine($"{Name} 님이 레벨업했습니다! 현재 레벨: {Level}, 최대 체력: {MaxHealth}");
            }
        }
        // 플레이어 상태 출력
        public void ShowStatus()
        {
            Console.WriteLine($"이름: {Name}, 직업: {Job}, 레벨: {Level}, 경험치: {Experience}, 골드: {Gold}");
            Console.WriteLine($"체력: {Health}/{MaxHealth}");
            Console.WriteLine("인벤토리 아이템:");
            foreach (var item in Inventory.Items)
            {
                Console.WriteLine($"- {item.Name} ({item.StatText})");
            }
        }
        public void ApplyEquippedStats()
        {
            var stats = Inventory.GetEquippedStats();
            Attack += stats.attack;
            Defense += stats.defense;
            Health += stats.hp;
        }



        public void RemoveGold(int amount)
        {
            if (Gold >= amount)
                Gold -= amount;
            else
                throw new InvalidOperationException("Not enough gold.");
        }
    }
}
