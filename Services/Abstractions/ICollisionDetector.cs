using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisGame.Services.Abstractions
{
    public interface ICollisionDetector
    {
        bool WillCollide(IGameBoard board, IGamePiece piece, Position newPosition);
        bool CanRotate(IGameBoard board, IGamePiece piece);
        bool CanMove(IGameBoard board, IGamePiece piece, Direction direction);
    }
}
