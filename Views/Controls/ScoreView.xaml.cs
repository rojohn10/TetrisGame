namespace TetrisGame.Views.Controls;

public partial class ScoreView : ContentView
{
    public static readonly BindableProperty ScoreProperty =
        BindableProperty.Create(nameof(Score), typeof(int), typeof(ScoreView));

    public static readonly BindableProperty LevelProperty =
        BindableProperty.Create(nameof(Level), typeof(int), typeof(ScoreView));

    public static readonly BindableProperty LinesClearedProperty =
        BindableProperty.Create(nameof(LinesCleared), typeof(int), typeof(ScoreView));

    public static readonly BindableProperty GameTimeProperty =
        BindableProperty.Create(nameof(GameTime), typeof(string), typeof(ScoreView));

    public int Score
    {
        get => (int)GetValue(ScoreProperty);
        set => SetValue(ScoreProperty, value);
    }

    public int Level
    {
        get => (int)GetValue(LevelProperty);
        set => SetValue(LevelProperty, value);
    }

    public int LinesCleared
    {
        get => (int)GetValue(LinesClearedProperty);
        set => SetValue(LinesClearedProperty, value);
    }

    public string GameTime
    {
        get => (string)GetValue(GameTimeProperty);
        set => SetValue(GameTimeProperty, value);
    }

    public ScoreView()
    {
        InitializeComponent();
        BindingContext = this;
    }
}