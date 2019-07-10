using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shutta
{
    public class UltimateRule : AdvancedRule
    {
        private const int ValueOfHands = 3000;

        // 세 명의 플레이어를 가짐 (한명 사용자 / 두명 컴퓨터)

        // 1. 사용자의 카드의 점수가 5점 이하일 경우, 300원을 추가로 내고 새로운 카드를 뽑는다. 
        // -> 플레이어들 끼리 카드 바꾸기는 나중에 추가.

        // 2. 사용자의 돈이 500원 이하일 경우, 손을 하나 주고 3000원을 추가한다. 

        // 승자가 두명일 경우 각각의 카드를 비교하여 높은 숫자의 카드를 가진사람이 승리한다. 

            // todo: static 메서드와 오버라이딩
        public new static bool CanRunRound(List<Player> players)
        {
            for (int i = 0; i < players.Count; i++)
            {
                if (players[i].Money <= BetMoney)
                {
                    if (i == 0)
                    {
                        Console.WriteLine("한쪽 손을 내어주고 3000원을 얻으시겠습니까?(y/n)");
                        if (Console.ReadLine().Equals("y"))
                        {
                            players[0].NumOfHands--;
                            players[0].Money += ValueOfHands;
                            if (players[0].NumOfHands <= 0)
                            {
                                Console.WriteLine($"{players[0]}가 양 손을 모두 잃어 게임을 끝냅니다.");
                                return false;
                            }
                            
                            return true;
                        }
                        else 
                            Console.WriteLine("돈이 부족하여 더이상 게임에 참여할 수 없습니다.");
                        return false;
                    }
                    //else if (i == 1 || i == 2)
                    Console.WriteLine("게임을 진행하기 위해서는 베팅 금액보다 보유 금액이 더 많아야 합니다." +
                        "게임을 종료합니다.");
                        return false;
                }
            }
            return true;
        }

        public static int RunRound(List<Player> players, int winnerNo)
        {
            // 각 선수가 이전 라운드에서 받은 카드를 클리어한다.
            foreach (Player player in players)
                player.PrepareRound();

            // 이전 라운드의 승자는 이번 라운드의 베팅 배수를 결정한다.
            // 단, 1라운드일 경우 선을 결정하여 베팅 배수를 결정한다.
            string inputText = "";
            int input = 0;
            Random random = new Random();
            if (winnerNo == 0) // 사용자가 이기면
            {
                Console.WriteLine($"P[{winnerNo}] 는 이번 라운드의 배수를 선택하세요. (1: 1배, 2: 2배, 3: 4배, 4: 8배)");
                inputText = Console.ReadLine();
                input = int.Parse(inputText);
                MultipleType multipleType = (MultipleType)input;
                Console.WriteLine($"P[{winnerNo}]는 {(int)multipleType}배를 선택하여 이번 판의 판돈이 {(int)multipleType}배 증가하였습니다.");
            }
            else
            {   // 컴퓨터가 승자일 때, 컴퓨터는 결과에 상관없이 판돈의 두 배를 올린다. 
                // Console.WriteLine($"P[{winnerNo}] 는 2배만을 선택");
                input = random.Next(1, 4);
                MultipleType multipleType = (MultipleType)input;
                Console.WriteLine($"P[{winnerNo}]는 {(int)multipleType}배를 선택하여 이번 판의 판돈이 {(int)multipleType}배 증가하였습니다.");
                // input = 2;
            }

            // 선수들이 학교를 간다
            int totalBetMoney = 0;

            foreach (Player player in players)
            {
                player.Money -= BetMoney * input;
                totalBetMoney += BetMoney * input;
            }

            // 딜러가 각 선수들에게 2장씩 카드를 돌린다
            Dealer dealer = new Dealer();
            foreach (Player player in players)
                for (int i = 0; i < 2; i++)
                    player.AddCard(dealer.Draw());


            if (winnerNo == 0)
            {
                Player p = players[winnerNo];
                p.CalculateScore();
                Console.WriteLine($"P{winnerNo} ({p[0]}, {p[1]}) => {p.Score}");


                // 사용자의 카드의 점수가 5점 이하일 경우, 300원을 추가로 내고 새로운 카드를 뽑는다. 
                if (p.Score <= 5)
                {
                    Console.WriteLine("300원을 추가로 지불하고 첫번째 카드를 바꾸시겠습니까? (y/n)");

                    if (Console.ReadLine().Equals("y"))
                    {
                        p.ChangeCard(dealer.Draw());
                        p.CalculateScore();
                        p.Money -= 300;
                        Console.WriteLine($"P{winnerNo}의 바꾼 카드: ({p[0]}, {p[1]}) => {p.Score}");
                        PrintMoney(p);
                    }
                    else
                    {
                        Console.WriteLine("카드를 변경하지 않고 게임을 진행합니다.");
                    }
                }
                Console.WriteLine("콜 유형를 선택하세요. (1: 콜(기본), 2: 베팅(+100원 * 배수), 3: 다이(포기, 1/2만 돌려받음))");
                inputText = Console.ReadLine();
                input = int.Parse(inputText);
                callType = (CallType)input; // 숫자로 입력받은 콜 타입을 콜타입 타입으로 형변환한다. 
                Console.WriteLine($"{callType}을 선택하셨습니다.");
            }
            else
            {   // 컴퓨터가 이기면 콜타입을 결정한다. 
                callType = DecideCallType(players, winnerNo);
                Console.WriteLine($"P[{winnerNo}]가 {callType}을 선택하였습니다.");
            }


            if (callType == CallType.Die)
            {
                Player p = players[winnerNo];
                p.Money += BetMoney * input / 2;
                totalBetMoney -= BetMoney * input / 2;
            }
            //if ( callType == CallType.Die)
            //{
            //    foreach (Player player in players)
            //    {
            //        if(player.Index == winnerNo)
            //        {
            //            player.Money += BetMoney * input / 2;
            //            totalBetMoney -= BetMoney * input / 2;
            //        }   
            //    }

            //    foreach (Player player in players)
            //    {
            //        if (player.Index != winnerNo)
            //        {
            //            player.Money += totalBetMoney / 2;
            //        }
            //    }


            //}
            else if (callType == CallType.Betting)
            {
                foreach (Player player in players)
                {
                    player.Money -= BetMoney * input;
                    totalBetMoney += BetMoney * input;
                }
            }

            // 각 선수들의 족보를 계산하고 출력한다.
            for (int i = 0; i < players.Count; i++)
            {
                Player p = players[i];

                p.CalculateScore();
                Console.WriteLine($"P{i} ({p[0]}, {p[1]}) => {p.Score}");
            }

            // 승자와 패자를 가린다.
            // 승자에게 모든 베팅 금액을 준다.
            if (FindWinner(players).Count >= 2) // 승자가 두명이면 
            {
                int winner0 = FindWinner(players)[0];
                int winner1 = FindWinner(players)[1];

                players[winner0].Money += totalBetMoney / 2; // 전체 베팅머니를 반씩 나눠가진다. 
                players[winner1].Money += totalBetMoney / 2;

                winnerNo = AdvancedRule.DecideOrder(players);
                return winnerNo;
            }
            else
            {
                winnerNo = FindWinner(players)[0];
                players[winnerNo].Money += totalBetMoney;
                return winnerNo;
            }

        }

        public static List<int> FindWinner(List<Player> players)
        {
            // return players.OrderByDescending(x => x.Score).First();
            int count = 0;
            List<int> playerIndex = new List<int>();
            int maxScore = 0;
            foreach (Player player in players)
                if (player.Score > maxScore)
                    maxScore = player.Score;

            foreach (Player player in players)
                if (player.Score == maxScore)
                {
                    playerIndex.Add(player.Index);
                    count++;
                }
            return playerIndex;
            // return null;
            throw new Exception("승자를 찾을 수 없음");
        }

        private static void PrintMoney(Player player)
        {
            Console.WriteLine($"보유한 돈에서 300원이 차감되어 남은 돈은 {player.Money}입니다.");
        }
    }
}
