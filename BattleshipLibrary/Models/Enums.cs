using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipLibrary.Models
{
    public enum GridSpotStatus
    {
        Empty = 0,
        Ship = 1,
        Miss = 2,
        Hit = 3,
        Sunk = 4
    }
}
