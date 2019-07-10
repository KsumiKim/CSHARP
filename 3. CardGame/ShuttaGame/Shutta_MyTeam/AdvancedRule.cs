﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shutta
{
    public class AdvancedRule : Rule
    {
        public static CallType callType = (CallType)1;
        public static void PrintMoney(List<Player> players)
        {
            for (int i = 0; i < players.Count; i++)
                Console.WriteLine($"P{i} has \\{players[i].Money}");
        }

        public static bool CanRunRound(List<Player> players)
        {
            foreach (Player player in players)
                if (player.Money <= 0)
                    return false;

            return true;
        }

        public static int FindBankrupt(List<Player> players)
        {
            foreach (Player player in players)
                if (player.Money <= 0)
                    return player.Index;

            throw new Exception("파산자 없음");
        }

        // 처음 게임을 시작할 때 순서를 결정한다. 
        public static int DecideOrder(List<Player> players)
        {
            foreach (Player player in players)
                player.PrepareRound();
            // 딜러가 각 선수들에게 1장씩 카드를 돌린다
            Dealer dealer = new Dealer();
            foreach (Player player in players)
                player.AddCard(dealer.FirstDraw());

            // 각 선수들의 족보를 계산하고 출력한다.
            for (int i = 0; i < players.Count; i++)
            {
                Player p = players[i];
                p.OrderScore(); // 각 플레이어들의 첫번째 숫자를 알려준다. 
                Console.WriteLine($"P{i} ({p[0]}) => {p.Score}"); // 각 플레이어들의 첫번째 숫자를 출력한다. 
            }

            int winnerNo = FindWinner(players)[0];

            return winnerNo;
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
                Console.WriteLine($"P[{winnerNo}]는 이번 라운드의 배수를 선택하세요. (1: 1배, 2: 2배, 4: 4배, 8: 8배)");
                inputText = Console.ReadLine();
                input = int.Parse(inputText);
                Console.WriteLine($"P[{winnerNo}]가 {input}배를 선택하여 이번 판의 판돈이 {input}배 증가하였습니다.");
            }
            else
            {   // 컴퓨터가 승자일 때, 컴퓨터는 결과에 상관없이 판돈의 두 배를 올린다. 
                // Console.WriteLine($"P[{winnerNo}] 는 2배만을 선택");
                input = random.Next(4);
                Console.WriteLine($"P[{winnerNo}]는 {input}배를 선택하여 이번 판의 판돈이 {input}배 증가하였습니다.");
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
                // 사용자가 이기면 직접 콜타입을 입력한다. 
                Player p = players[winnerNo];
                p.CalculateScore();
                Console.WriteLine($"P{winnerNo} ({p[0]}, {p[1]}) => {p.Score}");
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

        //public static int FindTajja(List<Player> players)
        //{
        //    int maxMoney = 0;
        //    foreach (Player player in players)
        //        if (player.Money > maxMoney)
        //            maxMoney = player.Money;

        //    foreach (Player player in players)
        //        if (player.Money == maxMoney)
        //        {
        //            return player.Index;
        //        }
        //    // return null;
        //    throw new Exception("타짜를 찾을 수 없음");
        //}

            // 모든 플레이어와 이긴 플레이어의 index를 매개변수로 받는다. (컴퓨터)
        public static CallType DecideCallType(List<Player> players, int index)
        {
            //Player p = players[index]; // 모든 플레이어 중 index에 해당하는 플레이어 한명
            //p.CalculateScore(); // 그 플레이어의 카드 점수를 구한다. 

            Random random = new Random();
            CallType callType = (CallType)random.Next(1, 3);

            return callType;  

            //if (p.Score < 4)
            //{
            //    return CallType.Die;
            //}
            //else if (p.Score < 10)
            //{
            //    return CallType.Call;
            //}
            //else
            //{
            //    return CallType.Betting;
            //}
            throw new NotImplementedException();
        }
    }
}
