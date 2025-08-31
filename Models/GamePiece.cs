using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TetrisGame.Models.Abstractions;
using TetrisGame.Models.Enums;

namespace TetrisGame.Models
{
    public class GamePiece : IGamePiece
    {
        public PieceType Type { get; private set; }
        public Position Position { get; set; }
        public bool[,] Shape { get; private set; }
        public Color Color { get; private set; }

        public GamePiece(PieceType type, Position position, bool[,] shape, Color color)
        {
            Type = type;
            Position = position;
            Shape = shape;
            Color = color;
        }

        public IGamePiece Rotate()
        {
            var rotatedShape = GetRotatedShape();
            return new GamePiece(Type, Position, rotatedShape, Color);
        }

        public IGamePiece Clone()
        {
            var clonedShape = new bool[Shape.GetLength(0), Shape.GetLength(1)];
            Array.Copy(Shape, clonedShape, Shape.Length);
            return new GamePiece(Type, Position, clonedShape, Color);
        }

        public bool[,] GetRotatedShape()
        {
            int rows = Shape.GetLength(0);
            int cols = Shape.GetLength(1);
            var rotated = new bool[cols, rows];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    rotated[j, rows - 1 - i] = Shape[i, j];
                }
            }

            return rotated;
        }
    }
}
