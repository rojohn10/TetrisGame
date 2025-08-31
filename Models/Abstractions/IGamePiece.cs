using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Android.Icu.Text.Transliterator;

namespace TetrisGame.Models.Abstractions
{
    public interface IGamePiece
    {
        PieceType Type { get; }
        Position Position { get; set; }
        bool[,] Shape { get; }
        Color Color { get; }
        IGamePiece Rotate();
        IGamePiece Clone();
        bool[,] GetRotatedShape();
    }
}
