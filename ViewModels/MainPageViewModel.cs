using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TetrisGame.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private string _title = "Tetris Game";
        private bool _isGameActive;

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public bool IsGameActive
        {
            get => _isGameActive;
            set => SetProperty(ref _isGameActive, value);
        }

        public ICommand NavigateToGameCommand { get; }
        public ICommand ShowInstructionsCommand { get; }
        public ICommand ShowSettingsCommand { get; }

        public MainPageViewModel()
        {
            NavigateToGameCommand = new Command(NavigateToGame);
            ShowInstructionsCommand = new Command(ShowInstructions);
            ShowSettingsCommand = new Command(ShowSettings);
        }

        private async void NavigateToGame()
        {
            IsGameActive = true;
            await Shell.Current.GoToAsync("//game");
        }

        private async void ShowInstructions()
        {
            await Application.Current!.MainPage!.DisplayAlert(
                "Instructions",
                "• Use arrow keys or WASD to move pieces\n" +
                "• Up arrow or W to rotate\n" +
                "• Space to hard drop\n" +
                "• P or Escape to pause\n" +
                "• R to restart\n" +
                "• Clear horizontal lines to score points!",
                "OK");
        }

        private async void ShowSettings()
        {
            await Application.Current!.MainPage!.DisplayAlert(
                "Settings",
                "Settings feature coming soon!",
                "OK");
        }
    }
}
