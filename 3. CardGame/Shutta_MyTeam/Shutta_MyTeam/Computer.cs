using System.Collections.Generic;

namespace Shutta
{
    internal class Computer : Player
    {
        public Computer(int money) : base(money)
        {
        }
        
        public override CallType DecideCallType(List<Player> players, int index)
        {
            throw new System.NotImplementedException();
        }
    }
}