using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisGame.Models.Enums
{
    public enum GameAction
    {
        MoveLeft,
        MoveRight,
        SoftDrop,
        HardDrop,
        Rotate,
        Pause,
        Resume,
        Restart,
        NewGame
    }
}
