using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisGame.Services.Abstractions
{
    public interface ILineDetector
    {
        List<int> DetectCompletedLines(IGameBoard board);
        int CountCompletedLines(IGameBoard board);
        bool HasCompletedLines(IGameBoard board);
    }
}
