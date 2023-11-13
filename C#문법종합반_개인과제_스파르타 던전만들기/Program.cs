using System.Reflection.PortableExecutable;
using System.Security.Cryptography.X509Certificates;

namespace C_문법종합반_개인과제_스파르타_던전만들기
{
    internal class Program
    {

        private static Character player;

        static void Main(string[] args)
        {
            GameDataSetting();
            GameIntro();
        }

        static void GameDataSetting()
        {
            //캐릭터 정보 세팅
            player = new Character("Momo", "마법사", 1, 20, 5, 100, 2000);
            //아이템 정보 세팅
        }


        static void GameIntro()   //시작 화면
        {
            Console.Clear();   //현재 환경에서 가능한 경우, 콘솔에 기록된 메시지를 모두 지운다.

            Console.WriteLine();
            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("1. 상태보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int input = CheckValidInput(1, 2);

            switch (input)
            {
                case 1:
                    MyCharacterInfo();
                    break;
                case 2:
                    MyCharacterInventory();
                    break;
            }
        }


        static void MyCharacterInfo()   //캐릭터 상태창
        {
            Console.Clear();

            Console.WriteLine();
            Console.WriteLine("상태 보기");
            Console.WriteLine($"Lv. {player.Level}");
            Console.WriteLine($"{player.Name}({player.Job})");

            //이전 능력치 저장
            int prevAtk = player.Atk;
            int prevDef = player.Def;
            int prevHp = player.Hp;

            //장착한 아이템들의 능력치 업데이트
            if (player.IsRobeEquipped) player.Def += 5;
            if (player.IsStaffEquipped) player.Atk += 2;
            if (player.IsEarringEquipped) player.Hp += 5;


            //상승한 능력치 표시 
            //아이템 장착한 경우에만 능력치를 더해줌.
            Console.WriteLine($"공격력 : {player.Atk} ({(player.Atk - prevAtk > 0 ? $"+{player.Atk - prevAtk}" : "")})");
            Console.WriteLine($"방어력 : {player.Def} ({(player.Def - prevDef > 0 ? $"+{player.Def - prevDef}" : "")})");
            Console.WriteLine($"체력 : {player.Hp} ({(player.Hp - prevHp > 0 ? $"+{player.Hp - prevHp}" : "")})");

            Console.WriteLine($"Gold : {player.Gold}");
            Console.WriteLine();
            Console.WriteLine("0. 나가기");

            int input = CheckValidInput(0, 0);

            switch (input)
            {
                case 0:
                    //아이템을 해제한 경우, 이전 능력치로 돌아가도록 업데이트.
                    if (player.IsRobeEquipped) player.Def = prevDef;
                    if (player.IsStaffEquipped) player.Atk = prevAtk;
                    if (player.IsEarringEquipped) player.Hp = prevHp;

                    GameIntro();
                    break;
            }
        }

        static void MyCharacterInventory()   //캐릭터 인벤토리

        {
            Console.Clear();

            Console.WriteLine();
            Console.WriteLine("인벤토리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");
            Console.WriteLine($"{(player.IsRobeEquipped ? "(E)" : "")}  스승님이 주신 로브 | 방어력 + 5 | 마법사 자격을 얻은 기념으로, 스승님께서 새로 지어주신 로브다.");
            Console.WriteLine($"{(player.IsStaffEquipped ? "(E)" : "")}  낡은 지팡이 | 공격력 + 2 | 나무를 엮어만든 지팡이. 여차하면 몽둥이로 쓸 수 있다.");
            Console.WriteLine($"{(player.IsEarringEquipped ? "(E)" : "")}  작은 귀걸이 | 체력 + 5 | 마탑에 들어오기 전, 친구와 나눠 낀 귀걸이.");
            Console.WriteLine();
            Console.WriteLine("1. 장착관리");
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int input = CheckValidInput(0, 1);
            switch (input)
            {
                case 0:
                    GameIntro();
                    break;
                case 1:
                    EquipItem();
                    break;
            }
        }


        //장비 창장하자아아ㅏ
        static void EquipItem()   //아이템 장착하기

        {
            Console.Clear();

            Console.WriteLine();
            Console.WriteLine("장착하고 싶은 아이템을 선택하세요.");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");   //장착했다고 표시하기 위해서 E를 하기 위해서는.
            Console.WriteLine($"1. {(player.IsRobeEquipped ? "(E)" : "")} 스승님이 주신 로브 | 방어력 + 5 | 마법사 자격을 얻은 기념으로, 스승님께서 새로 지어주신 로브다.");
            Console.WriteLine($"2. {(player.IsStaffEquipped ? "(E)" : "")} 낡은 지팡이 | 공격력 + 2 | 나무를 엮어만든 지팡이. 여차하면 몽둥이로 쓸 수 있다.");
            Console.WriteLine($"3. {(player.IsEarringEquipped ? "(E)" : "")} 작은 귀걸이 | 체력 + 5 | 마탑에 들어오기 전, 친구와 나눠 낀 귀걸이.");
            Console.WriteLine();
            Console.WriteLine("0. 나가기");

            //이전 능력치 저장
            int prevAtk = player.Atk;
            int prevDef = player.Def;
            int prevHp = player.Hp;

            int input = CheckValidInput(0, 3);

            switch (input)
            {
                case 0:
                    MyCharacterInventory();
                    break;
                case 1:
                    if (player.IsRobeEquipped)
                    {
                        player.Def = prevDef;
                        Console.WriteLine("로브를 해제했습니다. 방어력이 초기화됩니다.");
                    }
                    else
                    {
                        player.Def = prevDef;
                        Console.WriteLine("로브를 장착했습니다. 방어력이 5 증가합니다.");
                    }    
                    player.IsRobeEquipped = !player.IsRobeEquipped;
                    break;
                case 2:
                    if (player.IsStaffEquipped)
                    {
                        player.Atk = prevAtk;
                        Console.WriteLine("지팡이를 해제했습니다. 공격력이 초기화됩니다.");
                    }
                    else
                    {
                        player.Atk = prevAtk;
                        Console.WriteLine("지팡이를 착용했습니다. 공격력이 2 증가합니다.");
                    }    
                    player.IsStaffEquipped = !player.IsStaffEquipped;
                    break;
                case 3:
                    if (player.IsEarringEquipped)
                    {
                        player.Hp = prevHp;
                        Console.WriteLine("귀걸이를 해제했습니다. 체력이 초기화됩니다.");
                    }
                    else
                    {
                        player.Hp = prevHp;
                        Console.WriteLine("귀걸이를 착용했습니다. 체력이 5 증가합니다.");
                    }
                    player.IsEarringEquipped = !player.IsEarringEquipped;
                    break;
            }
 
            //아이템을 장착한 후, 다시 인벤토리 화면으로 돌아가기
            MyCharacterInventory();
        }




        //잘못된 입력일 경우.
        static int CheckValidInput (int min, int max)
        {
            while (true) 
            {
                string input = Console.ReadLine();

                bool parseSuccess = int.TryParse(input, out var ret);
                if (parseSuccess)
                {
                    if (ret >= min && ret <= max)
                        return ret;
                }
                Console.WriteLine("잘못된 입력입니다.");
            }
        }
    }


    public class Character
    {
        public string Name { get; }
        public string Job { get; }     
        public int Level { get; }
        public int Atk { get; set; }
        public int Def { get; set;  }
        public int Hp { get; set; }
        public int Gold { get; set; }

        // 아이템 장착 여부를 나타내는 프로퍼티 추가
        public bool IsRobeEquipped { get; set; }
        public bool IsStaffEquipped { get; set; }
        public bool IsEarringEquipped { get; set; }


        public Character(string name, string job, int level, int atk, int def, int hp, int gold)
        {
            Name = name;
            Job = job;         //Console.WriteLine($"{player.Name}({player.Job})");
            Level = level;     //Console.WriteLine($"Lv. {player.Level}");
            Atk = atk;         //Console.WriteLine($"공격력 : {player.Atk}");
            Def = def;         // Console.WriteLine($"방어력 : {player.Def}");
            Hp = hp;           //Console.WriteLine($"체력 : {player.Hp}");
            Gold = gold;       //Console.WriteLine($"Gold : {player.Gold}");

            //아이템 장착 여부 초기화
            IsRobeEquipped = false;
            IsStaffEquipped = false;
            IsEarringEquipped = false;
        }
    }
}