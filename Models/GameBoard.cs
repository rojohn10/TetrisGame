using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TetrisGame.Models.Abstractions;

namespace TetrisGame.Models
{
    public class GameBoard : IGameBoard
    {
        public int Width { get; }
        public int Height { get; }
        public Color[,] Board { get; private set; }

        public GameBoard(int width = 10, int height = 20)
        {
            Width = width;
            Height = height;
            Board = new Color[Height, Width];
            Clear();
        }

        public bool IsValidPosition(IGamePiece piece, Position position)
        {
            for (int row = 0; row < piece.Shape.GetLength(0); row++)
            {
                for (int col = 0; col < piece.Shape.GetLength(1); col++)
                {
                    if (!piece.Shape[row, col]) continue;

                    int boardRow = position.Y + row;
                    int boardCol = position.X + col;

                    if (boardRow < 0 || boardRow >= Height ||
                        boardCol < 0 || boardCol >= Width ||
                        Board[boardRow, boardCol] != Colors.Transparent)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public void PlacePiece(IGamePiece piece)
        {
            for (int row = 0; row < piece.Shape.GetLength(0); row++)
            {
                for (int col = 0; col < piece.Shape.GetLength(1); col++)
                {
                    if (piece.Shape[row, col])
                    {
                        int boardRow = piece.Position.Y + row;
                        int boardCol = piece.Position.X + col;

                        if (boardRow >= 0 && boardRow < Height &&
                            boardCol >= 0 && boardCol < Width)
                        {
                            Board[boardRow, boardCol] = piece.Color;
                        }
                    }
                }
            }
        }

        public List<int> GetCompletedLines()
        {
            var completedLines = new List<int>();

            for (int row = 0; row < Height; row++)
            {
                bool isComplete = true;
                for (int col = 0; col < Width; col++)
                {
                    if (Board[row, col] == Colors.Transparent)
                    {
                        isComplete = false;
                        break;
                    }
                }

                if (isComplete)
                {
                    completedLines.Add(row);
                }
            }

            return completedLines;
        }

        public void ClearLines(List<int> lines)
        {
            lines.Sort((a, b) => b.CompareTo(a)); // Sort descending

            foreach (int line in lines)
            {
                // Move all lines above down
                for (int row = line; row > 0; row--)
                {
                    for (int col = 0; col < Width; col++)
                    {
                        Board[row, col] = Board[row - 1, col];
                    }
                }

                // Clear top line
                for (int col = 0; col < Width; col++)
                {
                    Board[0, col] = Colors.Transparent;
                }
            }
        }

        public bool IsGameOver()
        {
            // Check if any blocks in the top rows
            for (int col = 0; col < Width; col++)
            {
                if (Board[0, col] != Colors.Transparent)
                    return true;
            }
            return false;
        }

        public void Clear()
        {
            for (int row = 0; row < Height; row++)
            {
                for (int col = 0; col < Width; col++)
                {
                    Board[row, col] = Colors.Transparent;
                }
            }
        }

        public IGameBoard Clone()
        {
            var clone = new GameBoard(Width, Height);
            Array.Copy(Board, clone.Board, Board.Length);
            return clone;
        }
    }
}
