

namespace CSharp
{
    class Program
    {
        enum ClassType
        {
            None = 0,
            Knight = 1,
            Archer = 2,
            Mage = 3
        }

        struct Player
        {
            public int hp;
            public int attack;
            public int playerGold;
            public string playerTitle;
            public ClassType type;

            public Player()
            {
                hp = 0;
                attack = 0;
                playerGold = 0;
                playerTitle = "";
                type = ClassType.None;
            }
        }


        enum MonsterType
        {
            None = 0,
            Slime = 1,
            Orc = 2,
            Skeleton = 3,
        }

        struct Monster
        {
            public int hp;
            public int attack;
            public int monsterGold;

            public Monster()
            {
                hp = 0;
                attack = 0;
                monsterGold = 0;
            }
        }

        //처음 화면
        static ClassType ChooseClass()
        {
            Console.Clear();
            Console.WriteLine("─────── TEXT RPG ───────");
            Console.WriteLine("─────── 환영합니다! ───────");
            Console.WriteLine("직업을 선택해주세요:");
            Console.WriteLine("1. [기사] : 강인한 체력과 방어력을 가진 전사형 직업입니다.");
            Console.WriteLine("2. [궁수] : 뛰어난 사격 능력과 민첩성을 가진 원거리 공격형 직업입니다.");
            Console.WriteLine("3. [법사] : 강력한 마법을 사용하는 마법사형 직업입니다.");

            ClassType choice = ClassType.None;
            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    choice = ClassType.Knight;
                    break;
                case "2":
                    choice = ClassType.Archer;
                    break;
                case "3":
                    choice = ClassType.Mage;
                    break;
            }

            return choice;
        }
        //클래스 생성
        static void CreatePlayer(ClassType choice, out Player player)
        {
            player = new Player();
            player.type = choice;

            // 기사(100, 10) 궁수(75, 12) 법사(50, 15)
            switch (choice)
            {
                case ClassType.Knight:
                    player.hp = 100;
                    player.attack = 10;
                    break;
                case ClassType.Archer:
                    player.hp = 75;
                    player.attack = 12;
                    break;
                case ClassType.Mage:
                    player.hp = 50;
                    player.attack = 15;
                    break;
                default:
                    player.hp = 0;
                    player.attack = 0;
                    break;
            }
        }

        //몬스터 생성 구문
        static void CreateRandomMonster(out Monster monster)
        {
            monster = new Monster();
            Random rand = new Random();
            int randMonster = rand.Next(1, 4); // 1 ~ 3 중 랜덤 정수 리턴

            switch (randMonster)
            {
                case (int)MonsterType.Slime:
                    Console.WriteLine(" 슬라임이 스폰되었습니다!");
                    monster.hp = 20;
                    monster.attack = 2;
                    monster.monsterGold = 50;
                    break;
                case (int)MonsterType.Orc:
                    Console.WriteLine(" 오크가 스폰되었습니다!");
                    monster.hp = 40;
                    monster.attack = 7;
                    monster.monsterGold = 150;
                    break;
                case (int)MonsterType.Skeleton:
                    Console.WriteLine(" 스켈레톤이 스폰되었습니다!");
                    monster.hp = 30;
                    monster.attack = 4;
                    monster.monsterGold = 100;
                    break;
                default:
                    monster.hp = 0;
                    monster.attack = 0;
                    break;
            }
        }


        //전투 돌입 구문
        static void EnterField(ref Player player)
        {
            Console.Clear();
            Console.WriteLine(" 던전에 접속했습니다.");
            Monster monster = new Monster();
            // 랜덤으로 1~3 몬스터 중 하나를 리스폰
            CreateRandomMonster(out monster);
            Console.WriteLine("───────────────────────────────────────");
            Console.WriteLine("[1]  전투 모드 돌입");
            Console.WriteLine("[2]  일정 확률로 마을로 도망");
            Console.WriteLine("───────────────────────────────────────");

            string input = Console.ReadLine();
            if (input == "1")
            {
                Fight(ref player, ref monster);
            }
            else if (input == "2")
            {
                // 50 %
                Random rand = new Random();
                int randValue = rand.Next(0, 101);
                if (randValue <= 50)
                {
                    Console.WriteLine(" 도망치는데 성공했습니다! ");
                }
                else
                {
                    Fight(ref player, ref monster);
                }
            }
        }
        
        //몬스터와 대결
        static void Fight(ref Player player, ref Monster monster)
        {
            while (true)
            {
                // 플레이어가 몬스터 공격
                monster.hp -= player.attack;
                if (monster.hp <= 0)
                {
                    Console.Clear();
                    Console.WriteLine("───────────────────────────────────────");
                    Console.WriteLine(" 몬스터를 처치하였습니다! ");
                    Console.WriteLine($" 남은 체력: {player.hp}");
                    player.playerGold += monster.monsterGold;
                    Console.WriteLine($" 골드 {player.playerGold}개 획득했습니다!");
                    Console.WriteLine("───────────────────────────────────────");
                    Console.ReadLine();
                    break;
                }

                // 몬스터 반격
                player.hp -= monster.attack;
                if (player.hp <= 0)
                {
                    Console.Clear();
                    Console.WriteLine(" 패배했습니다! ");
                    Console.WriteLine("1000골드를 빼앗겼습니다!");
                    Console.ReadLine();
                    player.playerGold -= 1000;
                    break;
                }
            }
            
            while (true)
            {
                Console.Clear();
                Console.WriteLine("───────────────────────────────────────");
                Console.WriteLine(" 다음 선택지를 입력하세요.");
                Console.WriteLine("[1] 다음 몬스터와 맞서 싸우기");
                Console.WriteLine("[2] 마을로 돌아가기");
                Console.WriteLine("───────────────────────────────────────");

                string input = Console.ReadLine();
                if (input == "1")
                {
                    EnterField(ref player);
                    break;
                }
                else if (input == "2")
                {
                    EnterGame(ref player);
                    break;
                }
            }
        }




        //로비
        static void EnterGame(ref Player player)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine(" 마을에 접속했습니다. ");
                Console.WriteLine($" 체력: {player.hp},  공격력: {player.attack},  골드: {player.playerGold}, 칭호: {player.playerTitle}");
                Console.WriteLine();
                Console.WriteLine("───────────────────────────────────────");
                Console.WriteLine("[1]  던전으로 간다.");
                Console.WriteLine("[2]  대장간으로 간다.");
                Console.WriteLine("[3]  모험가 길드로 간다.");
                Console.WriteLine("[4]  로비로 돌아가기.");
                Console.WriteLine("───────────────────────────────────────");


                int input = int.Parse(Console.ReadLine());

                switch (input)
                {
                    case 1:
                        EnterField(ref player);
                        break;
                    case 2:
                        Weapon(ref player);
                        break;
                    case 3:
                        guild(ref player);
                        break;
                    case 4:
                        return;
                }
            }
        }

            //대장간 입장
            static void Weapon(ref Player player)
            {
                Console.Clear();
                Console.WriteLine("───────────────────────────────────────");
                Console.WriteLine(" 대장간에 접속합니다 ");
                Console.WriteLine();
                Console.WriteLine("[1]  무기 상점");
                Console.WriteLine("[2]  무기 강화");
                Console.WriteLine("───────────────────────────────────────");

                int input = int.Parse(Console.ReadLine());
                switch (input)
                {
                    case 1:
                    {
                        weaponShop(ref player);
                        break;
                    }
                    case 2:
                    {
                        weaponItnen(ref player);
                        break;
                    }
                }

            }


            //무기 상점
            static void weaponShop(ref Player player)
            {
                Console.Clear();
                Console.WriteLine("───────────────────────────────────────");
                Console.WriteLine("---- 무기상점에 접속했습니다 ----");
                Console.WriteLine($" 현재 보유한 골드: {player.playerGold}개 ");
                Console.WriteLine("---- 특가 제품 ----");
                Console.WriteLine();
                Console.WriteLine("1. [영광의 베르세르크] - 500골드");
                Console.WriteLine("2. [세이버의 엑스칼리버] - 600골드");
                Console.WriteLine("3. [여신의 축복 에이치] - 400골드");
                Console.WriteLine("4. [고대 유루마시불] - 1000골드");
                Console.WriteLine("5. 대장간으로 돌아가기");
                Console.WriteLine("───────────────────────────────────────");

                int input = int.Parse(Console.ReadLine());

                switch (input)
                {
                    case 1:
                        if (player.playerGold >= 500)
                        {
                            Console.Clear();
                            Console.WriteLine("───────────────────────────────────────");
                            Console.WriteLine(" 영광의 베르세르크를 구입하셨습니다! ");
                            Console.WriteLine(" 공격력이 상승합니다! ");
                            player.playerGold -= 500;
                            Console.WriteLine($" 남은 골드: {player.playerGold}개 ");
                            Console.WriteLine(" 로비로 돌아갑니다! ");
                            Console.WriteLine("───────────────────────────────────────");
                            player.attack += 15;
                            Console.ReadLine();
                            EnterGame(ref player);

                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("골드가 부족합니다!");
                            Console.ReadLine();
                            return;
                        }

                        break;
                    case 2:
                        if (player.playerGold >= 600)
                        {
                            Console.Clear();
                            Console.WriteLine("───────────────────────────────────────");
                            Console.WriteLine("️ 세이버의 엑스칼리버를 구입하셨습니다! ️");
                            Console.WriteLine(" 공격력이 상승합니다! ");
                            player.playerGold -= 600;
                            Console.WriteLine($" 남은 골드: {player.playerGold}개 ");
                            Console.WriteLine(" 로비로 돌아갑니다! ");
                            Console.WriteLine("───────────────────────────────────────");
                            player.attack += 30;
                            Console.ReadLine();
                            EnterGame(ref player);
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("골드가 부족합니다!");
                            Console.ReadLine();
                            return;
                        }

                        break;
                    case 3:
                        if (player.playerGold >= 400)
                        {
                            Console.Clear();
                            Console.WriteLine("───────────────────────────────────────");
                            Console.WriteLine(" 여신의 축복 '에이치'를 구입하셨습니다! ");
                            Console.WriteLine(" 체력이 상승합니다! ");
                            player.playerGold -= 400;
                            Console.WriteLine($" 남은 골드: {player.playerGold}개 ");
                            Console.WriteLine(" 로비로 돌아갑니다! ");
                            Console.WriteLine("───────────────────────────────────────");
                            player.hp += 10;
                            Console.ReadLine();
                            EnterGame(ref player);
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("골드가 부족합니다!");
                            Console.ReadLine();
                            return;
                        }

                        break;
                    case 4:
                        if (player.playerGold >= 1000)
                        {
                            Console.Clear();
                            Console.WriteLine("───────────────────────────────────────");
                            Console.WriteLine(" 고대 유루마시불을 구입하셨습니다! ");
                            Console.WriteLine(" 공격력이 상승합니다! ");
                            player.playerGold -= 1000;
                            Console.WriteLine($" 남은 골드: {player.playerGold}개 ");
                            Console.WriteLine(" 로비로 돌아갑니다! ");
                            Console.WriteLine("───────────────────────────────────────");
                            player.attack += 50;
                            Console.ReadLine();
                            EnterGame(ref player);
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("골드가 부족합니다!");
                            Console.ReadLine();
                            return;
                        }

                        break;
                    case 5:
                        return;
                }




            }

            //무기 강화
            static void weaponItnen(ref Player player)
            {
                Console.Clear();
                Console.WriteLine("───────────────────────────────────────");
                Console.WriteLine(" 무기 강화 시스템 ");
                Console.WriteLine($" 현재 보유한 골드: {player.playerGold}개 ");
                Console.WriteLine(" 강화를 원하는 검을 선택해주세요! ");
                Console.WriteLine("1. [현재 들고 있는 검 강화 +10] - 200골드");
                Console.WriteLine("2. 로비로 돌아가기!");
                Console.WriteLine("───────────────────────────────────────");


                int input = int.Parse(Console.ReadLine());

                switch (input)
                {
                    case 1:
                        if (player.playerGold >= 200)
                        {
                            Console.Clear();
                            Console.WriteLine("───────────────────────────────────────");
                            Console.WriteLine(" 강화에 성공하셨습니다! ");
                            Console.WriteLine($" 현재 공격력: {player.attack} 입니다! ");
                            Console.WriteLine("───────────────────────────────────────");
                            Console.ReadLine();
                            player.playerGold -= 200;
                            player.attack += 10;
                            break;

                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("골드가 부족합니다!");
                            Console.ReadLine();
                            return;
                        }
                    case 2:
                        return;

                }



            }

            //모험가 길드
            static void guild(ref Player player)
            {
                Console.Clear();
                Console.WriteLine("──────────────────────────────────────────");
                Console.WriteLine(" 환영합니다, 모험가 여러분! ");
                Console.WriteLine(" 휴식과 강력한 음식을 제공하는 모험가 길드입니다! ");
                Console.WriteLine(" 여러분이 원하는 것을 고르세요! ");
                Console.WriteLine();
                Console.WriteLine("1. 숙소");
                Console.WriteLine("2. 모험가 등록");
                Console.WriteLine("3. 돌아가기");
                Console.WriteLine("──────────────────────────────────────────");


                int input = int.Parse(Console.ReadLine());

                switch (input)
                {
                    case 1:
                        guildHome(ref player);
                        break;
                    case 2:
                        IDcard(ref player);
                        break;
                    case 3:
                        break;
                    case 4:
                        return;



                }





            }


            //모험가 길드 > 숙소
            static void guildHome(ref Player player)
            {
                Console.Clear();
                Console.WriteLine("──────────────────────────────────────────");
                Console.WriteLine(" 환영합니다, 용감한 모험가 여러분! ");
                Console.WriteLine(" 이곳은 모험가들을 위한 휴식과 모험의 시작을 알리는 곳입니다. ");
                Console.WriteLine(" 장비를 갖추고 모험 일지를 쓰는데 필요한 모든 시설을 갖추고 있습니다. ");
                Console.WriteLine(" 자, 어떤 행동을 하시겠습니까? ");
                Console.WriteLine();
                Console.WriteLine("1. 하루밤을 푹 쉬어간다.");
                Console.WriteLine("2. 모험을 시작한다.");
                Console.WriteLine("──────────────────────────────────────────");
                
                int input = int.Parse(Console.ReadLine());

                switch (input)
                {
                    case 1:
                        Rest(ref player);
                        break;
                    case 2:
                        return;
                }


            }
            //체력 회복
            static void Rest(ref Player player)
            {
                Console.Clear();
                Console.WriteLine("체력을 회복합니다.");

                while (player.hp < 100)
                {
                    player.hp += 10; 
                    Console.WriteLine($"현재 체력: {player.hp}");
                    if (player.hp > 100)
                    {
                        player.hp = 100; 
                    }
                }
                Console.WriteLine("체력이 모두 회복되었습니다.");
                Console.WriteLine("─────────────────────────────");
                Console.ReadLine();
            }
            
            //모험가 등록
            static void IDcard(ref Player player)
            {
                Console.Clear();
                Console.WriteLine("─────── 모험가 등록 ───────");
                Console.WriteLine("신규 모험가 등록을 환영합니다!");
                Console.WriteLine("여기서는 새로운 모험가로 등록하여 게임에 참여할 수 있습니다.");
                Console.WriteLine("각 직업별로 특별한 칭호가 부여됩니다.");

                Console.WriteLine("선택한 직업을 선택해주세요! :");
                Console.WriteLine("[1] 기사");
                Console.WriteLine("[2] 궁수");
                Console.WriteLine("[3] 법사");

                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        player.type = ClassType.Knight;
                        player.playerTitle = "용맹한 기사";
                        break;
                    case "2":
                        player.type = ClassType.Archer;
                        player.playerTitle = "명장 궁수";
                        break;
                    case "3":
                        player.type = ClassType.Mage;
                        player.playerTitle = "지식의 대가";
                        break;
                }
                Console.Clear();
                Console.WriteLine("──────────────────────────────────────────");
                Console.WriteLine($"선택하셨던 직업은 {player.type}입니다.");
                Console.WriteLine($"당신의 칭호는 '{player.playerTitle}'입니다!");
                Console.WriteLine("모험을 시작하세요!");
                Console.WriteLine("──────────────────────────────────────────");
                Console.ReadLine();

            }












            //메인 함수
            static void Main(string[] args)
            {
                while (true)
                {
                    // 직업 고르기
                    ClassType choice;
                    choice = ChooseClass();
                    if (choice == ClassType.None)
                        continue;

                    // 플레이어 캐릭터 생성
                    Player player;
                    CreatePlayer(choice, out player);
                    Console.Clear();
                    Console.WriteLine($"체력{player.hp}, 공격력{player.attack}, 골드{player.playerGold}");

                    // 게임 시작! 몬스터 생성 및 전투
                    EnterGame(ref player);
                }
            }
        }
    }
