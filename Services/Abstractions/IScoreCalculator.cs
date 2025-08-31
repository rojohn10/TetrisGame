using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisGame.Services.Abstractions
{
    public interface IScoreCalculator
    {
        int CalculateScore(int linesCleared, int level);
        int CalculateLevel(int totalLinesCleared);
        TimeSpan CalculateDropSpeed(int level);
        int CalculateLineScore(int linesCleared);
    }
}
