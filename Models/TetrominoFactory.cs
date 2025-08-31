using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisGame.Models
{
    public static class TetrominoFactory
    {
        private static readonly Dictionary<PieceType, (bool[,] shape, Color color)> PieceDefinitions = new()
        {
            [PieceType.I] = (new bool[,] { { true, true, true, true } }, Colors.Cyan),
            [PieceType.O] = (new bool[,] { { true, true }, { true, true } }, Colors.Yellow),
            [PieceType.T] = (new bool[,] { { false, true, false }, { true, true, true } }, Colors.Purple),
            [PieceType.S] = (new bool[,] { { false, true, true }, { true, true, false } }, Colors.Green),
            [PieceType.Z] = (new bool[,] { { true, true, false }, { false, true, true } }, Colors.Red),
            [PieceType.J] = (new bool[,] { { true, false, false }, { true, true, true } }, Colors.Blue),
            [PieceType.L] = (new bool[,] { { false, false, true }, { true, true, true } }, Colors.Orange)
        };

        public static IGamePiece CreatePiece(PieceType type, Position? position = null)
        {
            if (!PieceDefinitions.TryGetValue(type, out var definition))
                throw new ArgumentException($"Unknown piece type: {type}");

            position ??= new Position(5, 0); // Default spawn position
            return new GamePiece(type, position, definition.shape, definition.color);
        }

        public static IGamePiece CreateRandomPiece(Position? position = null)
        {
            var types = Enum.GetValues<PieceType>();
            var randomType = types[Random.Shared.Next(types.Length)];
            return CreatePiece(randomType, position);
        }

        public static List<PieceType> GetAllPieceTypes() => Enum.GetValues<PieceType>().ToList();
    }

}
