using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisGame.Services
{
    public class LineDetector : ILineDetector
    {
        public List<int> DetectCompletedLines(IGameBoard board)
        {
            return board.GetCompletedLines();
        }

        public int CountCompletedLines(IGameBoard board)
        {
            return DetectCompletedLines(board).Count;
        }

        public bool HasCompletedLines(IGameBoard board)
        {
            return CountCompletedLines(board) > 0;
        }
    }
}
