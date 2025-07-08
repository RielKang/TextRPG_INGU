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



            //소개글 출력
            Console.WriteLine("환영합니다! 이곳은 간단한 텍스트 RPG 게임입니다.\n이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n이름을 입력해주세요");
            string playerName = Console.ReadLine();

            //진입 선택지 출력
            Console.WriteLine("1. 상태보기\n2. 인벤토리\n3. 상점\n \n원하시는 행동을 입력해주세요");
            string actionChoice = Console.ReadLine();
            switch (actionChoice)
            {
                case "1":
                    // 상태 보기
                    Console.WriteLine($"이름: {playerName}" +
                        $"\n레벨: {playerLevel}" +
                        $"\n공격력: {playerAttack}" +
                        $"\n방어력: {playerDefense}" +
                        $"\n체력: {playerHealth}" +
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
                case "2":
                    // 인벤토리
                    Console.WriteLine("인벤토리는 아직 구현되지 않았습니다.");
                    break;
                case "3":
                    // 상점
                    Console.WriteLine("상점은 아직 구현되지 않았습니다.");
                    break;
                default:
                    Console.WriteLine("잘못된 선택입니다. 다시 시도해주세요.");
                    break;

            }

        }
    }
}
