using Android.Graphics;

namespace TetrisGame.Views.Controls;

public partial class NextPieceView : ContentView
{
    public static readonly BindableProperty NextPieceProperty =
        BindableProperty.Create(nameof(NextPiece), typeof(IGamePiece), typeof(NextPieceView),
            propertyChanged: OnNextPieceChanged);

    public IGamePiece? NextPiece
    {
        get => (IGamePiece?)GetValue(NextPieceProperty);
        set => SetValue(NextPieceProperty, value);
    }

    private const int CellSize = 15;

    public NextPieceView()
    {
        InitializeComponent();
    }

    private static void OnNextPieceChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is NextPieceView view)
        {
            view.UpdateNextPiece();
        }
    }

    private void UpdateNextPiece()
    {
        NextPieceCanvas.Children.Clear();

        if (NextPiece == null) return;

        // Center the piece in the canvas
        int pieceWidth = NextPiece.Shape.GetLength(1);
        int pieceHeight = NextPiece.Shape.GetLength(0);
        int offsetX = (80 - (pieceWidth * CellSize)) / 2;
        int offsetY = (80 - (pieceHeight * CellSize)) / 2;

        for (int row = 0; row < pieceHeight; row++)
        {
            for (int col = 0; col < pieceWidth; col++)
            {
                if (NextPiece.Shape[row, col])
                {
                    var rect = new Rectangle
                    {
                        Fill = new SolidColorBrush(NextPiece.Color),
                        Stroke = new SolidColorBrush(Colors.Gray),
                        StrokeThickness = 1,
                        WidthRequest = CellSize,
                        HeightRequest = CellSize
                    };

                    Canvas.SetLeft(rect, offsetX + col * CellSize);
                    Canvas.SetTop(rect, offsetY + row * CellSize);
                    NextPieceCanvas.Children.Add(rect);
                }
            }
        }
    }
}