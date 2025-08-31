using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TetrisGame.Models.Abstractions;
using TetrisGame.Models.Enums;

namespace TetrisGame.Models
{
    public class GameState : IGameState
    {
        private GameStatus _status;
        private int _score;
        private int _level;
        private int _linesCleared;
        private bool _isPaused;

        public GameStatus Status
        {
            get => _status;
            set
            {
                _status = value;
                OnStateChanged();
            }
        }

        public int Score
        {
            get => _score;
            set
            {
                _score = value;
                OnStateChanged();
            }
        }

        public int Level
        {
            get => _level;
            set
            {
                _level = value;
                OnStateChanged();
            }
        }

        public int LinesCleared
        {
            get => _linesCleared;
            set
            {
                _linesCleared = value;
                OnStateChanged();
            }
        }

        public IGamePiece? CurrentPiece { get; set; }
        public IGamePiece? NextPiece { get; set; }
        public IGameBoard GameBoard { get; set; }
        public TimeSpan GameTime { get; set; }

        public bool IsPaused
        {
            get => _isPaused;
            set
            {
                _isPaused = value;
                OnStateChanged();
            }
        }

        public event EventHandler<GameStateChangedEventArgs>? StateChanged;

        public GameState(IGameBoard gameBoard)
        {
            GameBoard = gameBoard;
            Status = GameStatus.NotStarted;
            Score = 0;
            Level = 1;
            LinesCleared = 0;
            GameTime = TimeSpan.Zero;
            IsPaused = false;
        }

        public IGameState Clone()
        {
            var clone = new GameState(GameBoard.Clone())
            {
                Status = Status,
                Score = Score,
                Level = Level,
                LinesCleared = LinesCleared,
                CurrentPiece = CurrentPiece?.Clone(),
                NextPiece = NextPiece?.Clone(),
                GameTime = GameTime,
                IsPaused = IsPaused
            };
            return clone;
        }

        protected virtual void OnStateChanged()
        {
            StateChanged?.Invoke(this, new GameStateChangedEventArgs(this));
        }
    }
}

public class GameStateChangedEventArgs : EventArgs
{
    public IGameState GameState { get; }

    public GameStateChangedEventArgs(IGameState gameState)
    {
        GameState = gameState;
    }
}
