using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Android.Icu.Text.Transliterator;

namespace TetrisGame.Models.Abstractions
{
    public interface IGameBoard
    {
        int Width { get; }
        int Height { get; }
        Color[,] Board { get; }
        bool IsValidPosition(IGamePiece piece, Position position);
        void PlacePiece(IGamePiece piece);
        List<int> GetCompletedLines();
        void ClearLines(List<int> lines);
        bool IsGameOver();
        void Clear();
        IGameBoard Clone();
    }
}
