using Android.Graphics;
using Microsoft.Maui.Controls.Shapes;
using TetrisGame.Models.Abstractions;

namespace TetrisGame.Views.Controls;

public partial class GameBoardView : ContentView
{
    public static readonly BindableProperty GameStateProperty =
        BindableProperty.Create(nameof(GameState), typeof(IGameState), typeof(GameBoardView),
            propertyChanged: OnGameStateChanged);

    public IGameState? GameState
    {
        get => (IGameState?)GetValue(GameStateProperty);
        set => SetValue(GameStateProperty, value);
    }

    private const int CellSize = 20;

    public GameBoardView()
    {
        InitializeComponent();
    }

    private static void OnGameStateChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is GameBoardView view)
        {
            view.UpdateGameBoard();
        }
    }

    private void UpdateGameBoard()
    {
        if (GameState?.GameBoard == null) return;

        GameCanvas.Children.Clear();

        var board = GameState.GameBoard;

        // Draw placed pieces
        for (int row = 0; row < board.Height; row++)
        {
            for (int col = 0; col < board.Width; col++)
            {
                var cellColor = board.Board[row, col];
                if (cellColor != Colors.Transparent)
                {
                    DrawCell(col, row, cellColor);
                }
            }
        }

        // Draw current piece
        if (GameState.CurrentPiece != null)
        {
            DrawPiece(GameState.CurrentPiece);
        }

        // Draw grid lines
        DrawGridLines();
    }

    private void DrawCell(int col, int row, Color color)
    {
        var rect = new Rectangle
        {
            Fill = new SolidColorBrush(color),
            Stroke = new SolidColorBrush(Colors.Gray),
            StrokeThickness = 1,
            WidthRequest = CellSize,
            HeightRequest = CellSize
        };

        Canvas.SetLeft(rect, col * CellSize);
        Canvas.SetTop(rect, row * CellSize);
        GameCanvas.Children.Add(rect);
    }

    private void DrawPiece(IGamePiece piece)
    {
        for (int row = 0; row < piece.Shape.GetLength(0); row++)
        {
            for (int col = 0; col < piece.Shape.GetLength(1); col++)
            {
                if (piece.Shape[row, col])
                {
                    int boardCol = piece.Position.X + col;
                    int boardRow = piece.Position.Y + row;

                    if (boardRow >= 0 && boardRow < GameState!.GameBoard.Height &&
                        boardCol >= 0 && boardCol < GameState.GameBoard.Width)
                    {
                        DrawCell(boardCol, boardRow, piece.Color);
                    }
                }
            }
        }
    }

    private void DrawGridLines()
    {
        var board = GameState!.GameBoard;

        // Vertical lines
        for (int col = 0; col <= board.Width; col++)
        {
            var line = new Line
            {
                X1 = col * CellSize,
                Y1 = 0,
                X2 = col * CellSize,
                Y2 = board.Height * CellSize,
                Stroke = new SolidColorBrush(Colors.DarkGray),
                StrokeThickness = 0.5
            };
            GameCanvas.Children.Add(line);
        }

        // Horizontal lines
        for (int row = 0; row <= board.Height; row++)
        {
            var line = new Line
            {
                X1 = 0,
                Y1 = row * CellSize,
                X2 = board.Width * CellSize,
                Y2 = row * CellSize,
                Stroke = new SolidColorBrush(Colors.DarkGray),
                StrokeThickness = 0.5
            };
            GameCanvas.Children.Add(line);
        }
    }
}