using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shutta
{
    public abstract class Player
    {
         public static int _index = 0;
        public Player(int money)
        {
            Money = money;
            Index = _index;
            _index++;
            _cards = new List<Card>();
            _firstcard = new List<Card>();
            NumOfHands = 2;
        }

        public int Money { get; set; }
        public int Index { get; private set; }

        private readonly List<Card> _cards;
        private readonly List<Card> _firstcard;

        public void AddCard(Card card)
        {
            _cards.Add(card);
            _firstcard.Add(card);
        }

        public void ChangeCard(Card card)
        {
            _cards.RemoveAt(0);
            _cards.Insert(0, card);
        }

        public virtual int CalculateScore()
        {
            if (_cards[0].No == _cards[1].No)
                return Score = _cards[0].No * 10; // 10 ~ 100
            else
                return Score = (_cards[0].No + _cards[1].No) % 10; // 0 ~ 9
        }

        public virtual void OrderScore()
        {
            Score = _firstcard[0].No; // 카드숫자
        }

        public int Score { get; set; }
        public int NumOfHands { get; set; }

        // indexer
        public Card this[int index]
        {
            get
            {
                return _cards[index];
            }
        }

        public void PrepareRound()
        {
            _cards.Clear();
            _firstcard.Clear();
            Score = 0;
        }

        public abstract CallType DecideCallType(List<Player> players, int index);
    }
}
