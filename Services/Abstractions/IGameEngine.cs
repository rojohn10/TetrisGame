using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisGame.Services.Abstractions
{
    public interface IGameEngine
    {
        IGameState GameState { get; }
        event EventHandler<GameEngineEventArgs>? GameUpdated;
        event EventHandler<GameEngineEventArgs>? GameOver;
        event EventHandler<LinesCompletedEventArgs>? LinesCompleted;

        void StartGame();
        void PauseGame();
        void ResumeGame();
        void RestartGame();
        void Update();
        bool MovePiece(Direction direction);
        bool RotatePiece();
        void DropPiece();
        void SetDropSpeed(TimeSpan speed);
    }
}
