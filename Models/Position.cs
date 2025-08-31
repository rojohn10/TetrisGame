using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TetrisGame.Models.Enums;

namespace TetrisGame.Models
{
    public record Position(int X, int Y)
    {
        public Position Move(Direction direction) => direction switch
        {
            Direction.Left => this with { X = X - 1 },
            Direction.Right => this with { X = X + 1 },
            Direction.Down => this with { Y = Y + 1 },
            Direction.Up => this with { Y = Y - 1 },
            _ => this
        };

        public Position Move(int deltaX, int deltaY) => new(X + deltaX, Y + deltaY);
    }
}
