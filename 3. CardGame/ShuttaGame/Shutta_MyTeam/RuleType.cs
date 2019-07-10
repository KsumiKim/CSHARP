using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shutta
{
    public enum RuleType
    {
        Basic = 1,
        Advanced,
        Ultimate
    }

    public enum CallType
    {
        Call = 1,
        Betting,
        Die
    }

    public enum MultipleType
    {
        Once = 1,
        Double = 2,
        Quadruple = 3,
        Octuple = 4
    }
}
