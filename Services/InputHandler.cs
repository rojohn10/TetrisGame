using Android.Views.InputMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TetrisGame.Models.Enums;
using TetrisGame.Services.Abstractions;

namespace TetrisGame.Services
{
    public class InputHandler : IInputHandler
    {
        private readonly Dictionary<string, GameAction> _inputMappings = new();

        public event EventHandler<InputEventArgs>? InputReceived;

        public InputHandler()
        {
            SetupDefaultMappings();
        }

        public void HandleKeyPress(string key)
        {
            if (_inputMappings.TryGetValue(key.ToLower(), out var action))
            {
                InputReceived?.Invoke(this, new InputEventArgs(action, key));
            }
        }

        public void HandleGesture(string gesture)
        {
            if (_inputMappings.TryGetValue(gesture.ToLower(), out var action))
            {
                InputReceived?.Invoke(this, new InputEventArgs(action, gesture));
            }
        }

        public void RegisterInputAction(string input, GameAction action)
        {
            _inputMappings[input.ToLower()] = action;
        }

        private void SetupDefaultMappings()
        {
            _inputMappings["arrowleft"] = GameAction.MoveLeft;
            _inputMappings["a"] = GameAction.MoveLeft;
            _inputMappings["arrowright"] = GameAction.MoveRight;
            _inputMappings["d"] = GameAction.MoveRight;
            _inputMappings["arrowdown"] = GameAction.SoftDrop;
            _inputMappings["s"] = GameAction.SoftDrop;
            _inputMappings["arrowup"] = GameAction.Rotate;
            _inputMappings["w"] = GameAction.Rotate;
            _inputMappings["space"] = GameAction.HardDrop;
            _inputMappings["enter"] = GameAction.HardDrop;
            _inputMappings["p"] = GameAction.Pause;
            _inputMappings["escape"] = GameAction.Pause;
            _inputMappings["r"] = GameAction.Restart;

            // Gesture mappings
            _inputMappings["swipeleft"] = GameAction.MoveLeft;
            _inputMappings["swiperight"] = GameAction.MoveRight;
            _inputMappings["swipedown"] = GameAction.SoftDrop;
            _inputMappings["tap"] = GameAction.Rotate;
            _inputMappings["doubletap"] = GameAction.HardDrop;
        }
    }
}


public class GameEngineEventArgs : EventArgs
{
    public IGameState GameState { get; }

    public GameEngineEventArgs(IGameState gameState)
    {
        GameState = gameState;
    }
}

public class LinesCompletedEventArgs : EventArgs
{
    public int LinesCleared { get; }
    public int Score { get; }

    public LinesCompletedEventArgs(int linesCleared, int score)
    {
        LinesCleared = linesCleared;
        Score = score;
    }
}

public class InputEventArgs : EventArgs
{
    public GameAction Action { get; }
    public string Input { get; }

    public InputEventArgs(GameAction action, string input)
    {
        Action = action;
        Input = input;
    }
}
