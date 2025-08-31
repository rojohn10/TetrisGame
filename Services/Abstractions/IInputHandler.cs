using Android.Views.InputMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisGame.Services.Abstractions
{
    public interface IInputHandler
    {
        event EventHandler<InputEventArgs>? InputReceived;
        void HandleKeyPress(string key);
        void HandleGesture(string gesture);
        void RegisterInputAction(string input, GameAction action);
    }
}
