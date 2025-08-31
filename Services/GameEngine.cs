using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisGame.Services
{
    public class GameEngine : IGameEngine
    {
        private readonly ICollisionDetector _collisionDetector;
        private readonly ILineDetector _lineDetector;
        private readonly IScoreCalculator _scoreCalculator;
        private Timer? _gameTimer;
        private DateTime _lastUpdate = DateTime.Now;
        private TimeSpan _dropSpeed = TimeSpan.FromMilliseconds(1000);

        public IGameState GameState { get; private set; }

        public event EventHandler<GameEngineEventArgs>? GameUpdated;
        public event EventHandler<GameEngineEventArgs>? GameOver;
        public event EventHandler<LinesCompletedEventArgs>? LinesCompleted;

        public GameEngine(
            IGameState gameState,
            ICollisionDetector collisionDetector,
            ILineDetector lineDetector,
            IScoreCalculator scoreCalculator)
        {
            GameState = gameState ?? throw new ArgumentNullException(nameof(gameState));
            _collisionDetector = collisionDetector ?? throw new ArgumentNullException(nameof(collisionDetector));
            _lineDetector = lineDetector ?? throw new ArgumentNullException(nameof(lineDetector));
            _scoreCalculator = scoreCalculator ?? throw new ArgumentNullException(nameof(scoreCalculator));
        }

        public void StartGame()
        {
            GameState.Status = GameStatus.Playing;
            GameState.GameBoard.Clear();
            GameState.Score = 0;
            GameState.Level = 1;
            GameState.LinesCleared = 0;
            GameState.GameTime = TimeSpan.Zero;

            SpawnNewPiece();
            _dropSpeed = _scoreCalculator.CalculateDropSpeed(GameState.Level);
            StartTimer();
        }

        public void PauseGame()
        {
            if (GameState.Status == GameStatus.Playing)
            {
                GameState.IsPaused = true;
                GameState.Status = GameStatus.Paused;
                StopTimer();
            }
        }

        public void ResumeGame()
        {
            if (GameState.Status == GameStatus.Paused)
            {
                GameState.IsPaused = false;
                GameState.Status = GameStatus.Playing;
                StartTimer();
            }
        }

        public void RestartGame()
        {
            StopTimer();
            StartGame();
        }

        public void Update()
        {
            if (GameState.Status != GameStatus.Playing || GameState.CurrentPiece == null)
                return;

            var now = DateTime.Now;
            GameState.GameTime = GameState.GameTime.Add(now - _lastUpdate);
            _lastUpdate = now;

            // Try to move piece down
            if (!MovePiece(Direction.Down))
            {
                // Piece has landed, place it and spawn new one
                GameState.GameBoard.PlacePiece(GameState.CurrentPiece);
                CheckForCompletedLines();

                if (CheckGameOver())
                {
                    EndGame();
                    return;
                }

                SpawnNewPiece();
            }

            GameUpdated?.Invoke(this, new GameEngineEventArgs(GameState));
        }

        public bool MovePiece(Direction direction)
        {
            if (GameState.CurrentPiece == null) return false;

            var newPosition = GameState.CurrentPiece.Position.Move(direction);

            if (_collisionDetector.WillCollide(GameState.GameBoard, GameState.CurrentPiece, newPosition))
                return false;

            GameState.CurrentPiece.Position = newPosition;
            return true;
        }

        public bool RotatePiece()
        {
            if (GameState.CurrentPiece == null) return false;

            if (!_collisionDetector.CanRotate(GameState.GameBoard, GameState.CurrentPiece))
                return false;

            GameState.CurrentPiece = GameState.CurrentPiece.Rotate();
            return true;
        }

        public void DropPiece()
        {
            if (GameState.CurrentPiece == null) return;

            while (MovePiece(Direction.Down))
            {
                // Continue dropping until collision
            }
        }

        public void SetDropSpeed(TimeSpan speed)
        {
            _dropSpeed = speed;
            if (_gameTimer != null)
            {
                StopTimer();
                StartTimer();
            }
        }

        private void StartTimer()
        {
            _gameTimer = new Timer(_ => Update(), null, TimeSpan.Zero, _dropSpeed);
        }

        private void StopTimer()
        {
            _gameTimer?.Dispose();
            _gameTimer = null;
        }

        private void SpawnNewPiece()
        {
            GameState.CurrentPiece = GameState.NextPiece ?? TetrominoFactory.CreateRandomPiece();
            GameState.NextPiece = TetrominoFactory.CreateRandomPiece();
        }

        private void CheckForCompletedLines()
        {
            var completedLines = _lineDetector.DetectCompletedLines(GameState.GameBoard);
            if (completedLines.Count > 0)
            {
                GameState.GameBoard.ClearLines(completedLines);
                GameState.LinesCleared += completedLines.Count;

                var lineScore = _scoreCalculator.CalculateScore(completedLines.Count, GameState.Level);
                GameState.Score += lineScore;

                GameState.Level = _scoreCalculator.CalculateLevel(GameState.LinesCleared);
                _dropSpeed = _scoreCalculator.CalculateDropSpeed(GameState.Level);
                SetDropSpeed(_dropSpeed);

                LinesCompleted?.Invoke(this, new LinesCompletedEventArgs(completedLines.Count, lineScore));
            }
        }

        private bool CheckGameOver()
        {
            return GameState.GameBoard.IsGameOver() ||
                   (GameState.CurrentPiece != null &&
                    _collisionDetector.WillCollide(GameState.GameBoard, GameState.CurrentPiece, GameState.CurrentPiece.Position));
        }

        private void EndGame()
        {
            GameState.Status = GameStatus.GameOver;
            StopTimer();
            GameOver?.Invoke(this, new GameEngineEventArgs(GameState));
        }
    }
}
