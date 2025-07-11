using System.Numerics;
using TextRPG_INGU.Data;
using TextRPG_INGU.Managers;

namespace TextRPG_INGU.UI
{
    internal class MainProgram
    {
        static void Main(string[] args)
        {
            // 초기 설정 값
            
            

            
            
            //int playe.Gold = 1500;
            

            //인벤토리 매니저 생성
            InventoryManager playerInventory = InventoryManager.Instance;

            //소개글 출력
            Console.WriteLine("환영합니다! 이곳은 간단한 텍스트 RPG 게임입니다.\n이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n이름을 입력해주세요");
            string playerName = Console.ReadLine();
            Console.WriteLine($"안녕하세요, {playerName}님! 모험을 시작하기전 직업을 선택해주세요.\n");
            Console.WriteLine("1. 전사\n2. 마법사\n3. 도적\n 원하시는 행동을 선택해주세요.\n>>");
            string jobChoice = Console.ReadLine();
            Console.WriteLine(); // 줄바꿈
            string jobText = " "; // 직업 텍스트 초기화
            Player player = new Player(playerName, jobChoice); // 플레이어 객체 생성
            // 직업 선택지 처리
            switch (jobChoice)
            {
                case "1":
                    jobText = "전사";
                    player.Attack += 5; // 전사 공격력 증가
                    break;
                case "2":
                    jobText = "마법사";
                    player.Attack += 8; // 마법사 공격력 증가
                    player.Defense -= 2; // 마법사 방어력 감소
                    break;
                case "3":
                    jobText = "도적";
                    player.Attack += 4; // 도적 공격력 증가
                    player.Defense -= 1; // 도적 방어력 감소
                    break;
                default:
                    Console.WriteLine("잘못된 선택입니다. 기본 직업인 '전사'로 설정합니다.");
                    jobText = "무직";
                    break;
            }
            

            bool isRunning = true; // 게임 루프
            while (isRunning)
            {
                //진입 선택지 출력
                Console.WriteLine("1. 상태보기\n2. 인벤토리\n3. 상점\n4. 휴식 \n5. 종료\n \n원하시는 행동을 입력해주세요\n>>");
                string actionChoice = Console.ReadLine();
                Console.WriteLine(); // 줄바꿈
                switch (actionChoice)
                {
                    case "1":
                        // 상태 보기-------장비효과 적용
                        var equippedBonus = InventoryManager.Instance.GetEquippedStats();

                        Console.WriteLine($"이름: {playerName}" +
                            $"\n직업: {jobChoice}" +
                            $"\n레벨: {player.Level}" +
                            $"\n공격력: {player.Attack}(+{equippedBonus.attack})" +
                            $"\n방어력: {player.Defense}(+{equippedBonus.defense})" +
                            $"\n체력: {player.Health}(+{equippedBonus.hp})" +
                            $"\n골드: {player.Gold}");
                        
                        // 선택지로 나가기 로직
                        bool goBack = true; // 이전 선택지로 돌아가기 여부
                        while (goBack)
                        {
                            Console.WriteLine("0. 나가기");
                            Console.WriteLine("원하시는 행동을 입력해주세요\n>>");

                            string input = Console.ReadLine();
                            if (input == "0")
                            {
                                Console.WriteLine("이전 선택지로 돌아갑니다.");
                                break;
                            }
                            else
                            {
                                Console.WriteLine("잘못된 입력입니다. 다시 시도해주세요.");
                            }
                        }
                        break;
                    case "2": 
                        HandleInventoryLoop( playerInventory, player);

                        break;
                    case "3":
                        Console.WriteLine("상점에 들어갑니다.");
                        
                        bool shopRunning = true;
                        while (shopRunning)
                        {
                            Shop.Instance.ShowShop(player);
                            string shopInput = Console.ReadLine();
                            switch(shopInput)
                            {
                                case "1":
                                    HandlePurchaselectItemoop(player);
                                    break;
                                case "0":
                                    shopRunning = false;
                                    break;
                                default:
                                    Console.WriteLine("잘못된 입력입니다.");
                                    break;

                            }
                        }
                        break;
                    case "4":
                        HandleRest(ref player);
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("잘못된 선택입니다. 다시 시도해주세요.");
                        break;
                }
            }
        }
        // 상점 구매 루프 메서드
        static void HandlePurchaselectItemoop(Player player)
        {
            bool buying = true;
            while (buying)
            {
                Shop.Instance.ShowShopForPurchase(player);
                string buyInput = Console.ReadLine();
                if (buyInput == "0")
                {
                    buying = false;
                    continue;
                }

                int buyIndex;
                if (int.TryParse(buyInput, out buyIndex) && buyIndex > 0 && buyIndex <= Shop.Instance.Items.Count)
                {
                    bool success = Shop.Instance.TryPurchase(buyIndex - 1, player, InventoryManager.Instance);
                    if (success)
                    {
                        Console.WriteLine("Enter를 누르면 다음 화면으로 넘어갑니다.");
                        Console.WriteLine($"아이템 {Shop.Instance.Items[buyIndex - 1].Name}을(를) 구매했습니다.");
                        Console.ReadLine();
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                }
            }
        }
        static void HandleInventoryLoop(InventoryManager playerInventory, Player player)
        {
            // 인벤토리
                bool inventoryRunning = true;
                while (inventoryRunning)
                {
                    playerInventory.ShowItems();//ShowItems 안에서 조건부로 "3. 아이템 합성 출력됨"
                    string invInput = Console.ReadLine();
                    // 인벤토리 선택지, 아이템이 있다면
                    if (invInput == "1" && playerInventory.Count > 0)
                    {
                        Console.WriteLine("장착할 아이템의 번호를 입력해주세요.");
                        // 인벤토리 내부의 아이템 개수만큼 반복
                        for (int i = 0; i < playerInventory.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}.{playerInventory.Items[i].Name}");
                        }
                        // 아이템 선택
                        int selectItem;
                        if (int.TryParse(Console.ReadLine(), out selectItem) && selectItem > 0 && selectItem <= playerInventory.Count)
                        {
                            // 아이템 장착 관리
                            playerInventory.EquipItem(selectItem - 1);
                            Console.WriteLine($"{playerInventory.Items[selectItem - 1].Name} 아이템을 장착했습니다.");
                            player.ApplyEquippedStats();
                    }
                        else
                        {
                            Console.WriteLine("잘못된 선택입니다.");
                        }


                    }
                    // 합성 입력 처리
                    else if (invInput == "3" && playerInventory.HasAllSpecialItems())
                    {
                        SynthesisManager.TrySynthesize(player);
                    }


                    else if (invInput == "2" && playerInventory.Count > 0 || invInput == "0" && playerInventory.Count == 0)
                    {
                        Console.WriteLine("이전 선택지로 돌아갑니다.");
                        inventoryRunning = false; // 인벤토리 루프 종료
                    }
                    else
                    {
                        Console.WriteLine("잘못된 선택입니다.");
                    }
                }
          
            
        }
        static void HandleRest(ref Player player,int restCost = 1000)
        {
                       
            bool restRunning = true;
            while (restRunning)
            {
                Console.WriteLine("모험길드\n휴식을 통해 체력을 회복할수 있습니다.\n가격은 1000G입니다.");
                Console.WriteLine("1. 휴식하기\n2. 돌아가기\n \n원하시는 행동을 입력해주세요");
                string restChoice = Console.ReadLine();
                Console.WriteLine(); // 줄바꿈
                switch (restChoice)
                {
                    case "1":
                        if (player.Gold < restCost)
                        {
                            Console.WriteLine("골드가 부족하여 휴식할 수 없습니다.");
                            return;
                        }
                        if (player.Health < player.MaxHealth)
                        {
                            player.Gold -= restCost; // 골드 차감
                            player.Health = player.MaxHealth; // 체력 회복
                            Console.WriteLine($"휴식을 통해 체력을 회복했습니다.\n골드 {restCost} 차감 \n현재 체력: {player.Health}/{player.MaxHealth}");
                        }
                        else
                        {
                            Console.WriteLine("이미 체력이 가득 찼습니다. 휴식이 필요하지 않습니다.\n이전 선택지로 돌아갑니다.\n");

                        }
                        break;

                    case "2":
                        Console.WriteLine("이전 선택지로 돌아갑니다.");
                        return;
                    default:
                        Console.WriteLine("잘못된 선택입니다. 다시 시도해주세요.");
                        break;
                }

            }
            
                       
        }

    }
}
