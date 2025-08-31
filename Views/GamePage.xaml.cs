using TetrisGame.ViewModels;

namespace TetrisGame.Views;

public partial class GamePage : ContentPage
{
    public GamePage(GameViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        // Set up keyboard handling
        if (BindingContext is GameViewModel viewModel)
        {
            // Register for hardware key events if available
            // This would be platform-specific implementation
        }
    }

    // Handle hardware back button on Android
    protected override bool OnBackButtonPressed()
    {
        if (BindingContext is GameViewModel viewModel && viewModel.IsGameRunning)
        {
            viewModel.PauseGameCommand.Execute(null);
            return true;
        }
        return base.OnBackButtonPressed();
    }
}