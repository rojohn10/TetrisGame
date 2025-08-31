using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisGame.Services
{
    public class ScoreCalculator : IScoreCalculator
    {
        private static readonly Dictionary<int, int> LineScoreMultipliers = new()
        {
            [1] = 40,   // Single
            [2] = 100,  // Double  
            [3] = 300,  // Triple
            [4] = 1200  // Tetris
        };

        public int CalculateScore(int linesCleared, int level)
        {
            if (linesCleared <= 0 || linesCleared > 4) return 0;

            return LineScoreMultipliers[linesCleared] * (level + 1);
        }

        public int CalculateLevel(int totalLinesCleared)
        {
            return Math.Max(1, totalLinesCleared / 10 + 1);
        }

        public TimeSpan CalculateDropSpeed(int level)
        {
            var baseSpeed = 1000; // 1 second
            var speedIncrease = Math.Max(50, baseSpeed - (level - 1) * 50);
            return TimeSpan.FromMilliseconds(speedIncrease);
        }

        public int CalculateLineScore(int linesCleared)
        {
            return LineScoreMultipliers.GetValueOrDefault(linesCleared, 0);
        }
    }
}
