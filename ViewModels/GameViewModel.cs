using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TetrisGame.ViewModels
{
    public class GameViewModel : BaseViewModel
    {
        private readonly IGameEngine _gameEngine;
        private readonly IInputHandler _inputHandler;

        private bool _isGameRunning;
        private bool _isGamePaused;
        private string _gameStatusText = "Press Start to Play";
        private int _score;
        private int _level;
        private int _linesCleared;
        private TimeSpan _gameTime;

        public IGameState GameState => _gameEngine.GameState;

        public bool IsGameRunning
        {
            get => _isGameRunning;
            set => SetProperty(ref _isGameRunning, value);
        }

        public bool IsGamePaused
        {
            get => _isGamePaused;
            set => SetProperty(ref _isGamePaused, value);
        }

        public string GameStatusText
        {
            get => _gameStatusText;
            set => SetProperty(ref _gameStatusText, value);
        }

        public int Score
        {
            get => _score;
            set => SetProperty(ref _score, value);
        }

        public int Level
        {
            get => _level;
            set => SetProperty(ref _level, value);
        }

        public int LinesCleared
        {
            get => _linesCleared;
            set => SetProperty(ref _linesCleared, value);
        }

        public TimeSpan GameTime
        {
            get => _gameTime;
            set => SetProperty(ref _gameTime, value);
        }

        public string FormattedGameTime => $"{GameTime.Minutes:D2}:{GameTime.Seconds:D2}";

        public ICommand StartGameCommand { get; }
        public ICommand PauseGameCommand { get; }
        public ICommand RestartGameCommand { get; }
        public ICommand MoveLeftCommand { get; }
        public ICommand MoveRightCommand { get; }
        public ICommand RotateCommand { get; }
        public ICommand SoftDropCommand { get; }
        public ICommand HardDropCommand { get; }

        public GameViewModel(IGameEngine gameEngine, IInputHandler inputHandler)
        {
            _gameEngine = gameEngine ?? throw new ArgumentNullException(nameof(gameEngine));
            _inputHandler = inputHandler ?? throw new ArgumentNullException(nameof(inputHandler));

            // Initialize commands
            StartGameCommand = new Command(StartGame);
            PauseGameCommand = new Command(PauseResumeGame);
            RestartGameCommand = new Command(RestartGame);
            MoveLeftCommand = new Command(() => _gameEngine.MovePiece(Direction.Left));
            MoveRightCommand = new Command(() => _gameEngine.MovePiece(Direction.Right));
            RotateCommand = new Command(() => _gameEngine.RotatePiece());
            SoftDropCommand = new Command(() => _gameEngine.MovePiece(Direction.Down));
            HardDropCommand = new Command(() => _gameEngine.DropPiece());

            // Subscribe to events
            _gameEngine.GameUpdated += OnGameUpdated;
            _gameEngine.GameOver += OnGameOver;
            _gameEngine.LinesCompleted += OnLinesCompleted;
            _inputHandler.InputReceived += OnInputReceived;

            // Initialize from current game state
            UpdateFromGameState();
        }

        private void StartGame()
        {
            _gameEngine.StartGame();
            IsGameRunning = true;
            IsGamePaused = false;
            GameStatusText = "Playing";
        }

        private void PauseResumeGame()
        {
            if (IsGamePaused)
            {
                _gameEngine.ResumeGame();
                IsGamePaused = false;
                GameStatusText = "Playing";
            }
            else
            {
                _gameEngine.PauseGame();
                IsGamePaused = true;
                GameStatusText = "Paused";
            }
        }

        private void RestartGame()
        {
            _gameEngine.RestartGame();
            IsGameRunning = true;
            IsGamePaused = false;
            GameStatusText = "Playing";
        }

        private void OnGameUpdated(object? sender, GameEngineEventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                UpdateFromGameState();
            });
        }

        private void OnGameOver(object? sender, GameEngineEventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                IsGameRunning = false;
                IsGamePaused = false;
                GameStatusText = $"Game Over! Final Score: {Score}";
                UpdateFromGameState();
            });
        }

        private void OnLinesCompleted(object? sender, LinesCompletedEventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                // Could add special effects or sounds here
                UpdateFromGameState();
            });
        }

        private void OnInputReceived(object? sender, InputEventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                HandleGameAction(e.Action);
            });
        }

        private void HandleGameAction(GameAction action)
        {
            switch (action)
            {
                case GameAction.MoveLeft:
                    _gameEngine.MovePiece(Direction.Left);
                    break;
                case GameAction.MoveRight:
                    _gameEngine.MovePiece(Direction.Right);
                    break;
                case GameAction.SoftDrop:
                    _gameEngine.MovePiece(Direction.Down);
                    break;
                case GameAction.HardDrop:
                    _gameEngine.DropPiece();
                    break;
                case GameAction.Rotate:
                    _gameEngine.RotatePiece();
                    break;
                case GameAction.Pause:
                    PauseResumeGame();
                    break;
                case GameAction.Restart:
                    RestartGame();
                    break;
                case GameAction.NewGame:
                    StartGame();
                    break;
            }
        }

        private void UpdateFromGameState()
        {
            Score = GameState.Score;
            Level = GameState.Level;
            LinesCleared = GameState.LinesCleared;
            GameTime = GameState.GameTime;
            OnPropertyChanged(nameof(FormattedGameTime));
            OnPropertyChanged(nameof(GameState));
        }

        public void HandleKeyPress(string key)
        {
            _inputHandler.HandleKeyPress(key);
        }

        public void HandleGesture(string gesture)
        {
            _inputHandler.HandleGesture(gesture);
        }
    }
}
