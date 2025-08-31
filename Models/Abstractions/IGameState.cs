using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisGame.Models.Abstractions
{
    public interface IGameState
    {
        GameStatus Status { get; set; }
        int Score { get; set; }
        int Level { get; set; }
        int LinesCleared { get; set; }
        IGamePiece? CurrentPiece { get; set; }
        IGamePiece? NextPiece { get; set; }
        IGameBoard GameBoard { get; set; }
        TimeSpan GameTime { get; set; }
        bool IsPaused { get; set; }
        event EventHandler<GameStateChangedEventArgs>? StateChanged;
        IGameState Clone();
    }
}
