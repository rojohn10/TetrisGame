using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisGame.Services
{
public class CollisionDetector : ICollisionDetector
{
    public bool WillCollide(IGameBoard board, IGamePiece piece, Position newPosition)
    {
        return !board.IsValidPosition(piece, newPosition);
    }

    public bool CanRotate(IGameBoard board, IGamePiece piece)
    {
        var rotatedPiece = piece.Rotate();
        return board.IsValidPosition(rotatedPiece, piece.Position);
    }

    public bool CanMove(IGameBoard board, IGamePiece piece, Direction direction)
    {
        var newPosition = piece.Position.Move(direction);
        return !WillCollide(board, piece, newPosition);
    }
}
}
