using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FruitBurstGame
{
    public enum GameState
    {
        Running,
        Paused,
        GameOver
    }

    public enum GameObjectType
    {
        Player,
        NormalBerry,
        GoldenBerry,
        Arrow
    }
}

