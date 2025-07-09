namespace TextRPG_INGU
{
    internal class MainProgram
    {
        static void Main(string[] args)
        {
            // 초기 설정 값
            int playerLevel = 1;
            int playerAttack = 10;
            int playerDefense = 5;
            int playerHealth = 100;
            int playerGold = 1500;
            //인벤토리 매니저 생성
            InventoryManager playerInventory = InventoryManager.Instance;

            //소개글 출력
            Console.WriteLine("환영합니다! 이곳은 간단한 텍스트 RPG 게임입니다.\n이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n이름을 입력해주세요");
            string playerName = Console.ReadLine();

            bool isRunning = true; // 게임 루프
            while (isRunning)
            {
                //진입 선택지 출력
                Console.WriteLine("1. 상태보기\n2. 인벤토리\n3. 상점\n4. 휴식 \n5. 종료\n \n원하시는 행동을 입력해주세요");
                string actionChoice = Console.ReadLine();
                switch (actionChoice)
                {
                    case "1":
                        // 상태 보기-------장비효과 적용
                        var equippedBonus = InventoryManager.Instance.GetEquippedStats();

                        Console.WriteLine($"이름: {playerName}" +
                            $"\n레벨: {playerLevel}" +
                            $"\n공격력: {playerAttack}(+{equippedBonus.attack})" +
                            $"\n방어력: {playerDefense}(+{equippedBonus.defense})" +
                            $"\n체력: {playerHealth}(+{equippedBonus.hp})" +
                            $"\n골드: {playerGold}");
                        
                        // 선택지로 나가기 로직
                        Console.WriteLine("0. 나가기");
                        Console.WriteLine("원하시는 행동을 입력해주세요\n>>");
                        if (Console.ReadLine() == "0")
                        {
                            Console.WriteLine("이전 선택지로 돌아갑니다.");
                            break;
                        }
                        break;
                    case "2": //인벤토리 루프
                        HandleInventoryLoop(ref playerGold, playerInventory);

                        break;
                    case "3":
                        // 상점
                        Console.WriteLine("상점에 들어갑니다.");
                        // 상점 로직 추가
                        bool shopRunning = true; // 상점 루프
                        while (shopRunning)
                        {
                            Shop.Instance.ShowShop(playerGold);
                            string shopInput = Console.ReadLine();
                            switch(shopInput)
                            {
                                case "1":
                                    HandlePurchaseLoop(ref playerGold);
                                    break;
                                case "0":
                                    shopRunning = false; // 상점 루프 종료
                                    break;
                            }
                        }
                        break;
                    case "4":
                        // 휴식
                        HandleRest(ref playerHealth, ref playerGold, 100, 1000);
                        break;
                    case "5":
                        // 게임 종료
                        return;
                       
                    default:
                        Console.WriteLine("잘못된 선택입니다. 다시 시도해주세요.");
                        break;
                }
            }
        }
        // 상점 구매 루프 메서드
        static void HandlePurchaseLoop(ref int playerGold)
        {
            bool buying = true;
            while (buying)
            {
                Shop.Instance.ShowShopForPurchase(playerGold);
                string buyInput = Console.ReadLine();
                if (buyInput == "0")
                {
                    buying = false;
                    continue;
                }

                int buyIndex;
                if (int.TryParse(buyInput, out buyIndex) && buyIndex > 0 && buyIndex <= Shop.Instance.Items.Count)
                {
                    Shop.Instance.TryPurchase(buyIndex - 1, ref playerGold, InventoryManager.Instance);
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                }
            }
        }
        static void HandleInventoryLoop(ref int playerGold, InventoryManager playerInventory)
        {
            // 인벤토리
                bool inventoryRunning = true; // 인벤토리 루프
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
                        int sel;
                        if (int.TryParse(Console.ReadLine(), out sel) && sel > 0 && sel <= playerInventory.Count)
                        {
                            // 아이템 장착 관리
                            playerInventory.EquipItem(sel - 1);
                            Console.WriteLine($"{playerInventory.Items[sel - 1].Name} 아이템을 장착했습니다.");
                        }
                        else
                        {
                            Console.WriteLine("잘못된 선택입니다.");
                        }


                    }
                    // 합성 입력 처리
                    else if (invInput == "3" && playerInventory.HasAllSpecialItems())
                    {
                        SynthesisManager.TrySynthesize(ref playerGold);
                    }


                    else if ((invInput == "2" && playerInventory.Count > 0) || (invInput == "0" && playerInventory.Count == 0))
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
        static void HandleRest(ref int playerHealth, ref int playerGold, int maxHealth, int restCost)
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
                        if (playerGold < restCost)
                        {
                            Console.WriteLine("골드가 부족하여 휴식할 수 없습니다.");
                            return;
                        }
                        if (playerHealth < maxHealth)
                        {
                            playerGold -= restCost; // 골드 차감
                            playerHealth = maxHealth; // 체력 회복
                            Console.WriteLine($"휴식을 통해 체력을 회복했습니다.\n골드 {restCost} 차감 \n현재 체력: {playerHealth}/{maxHealth}");
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
