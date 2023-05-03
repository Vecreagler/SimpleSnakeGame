using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleSnakeGame
{

    public enum Direction 
    { 
        Up,
        Down,
        Left,
        Right
    };

    class GlobalSettings
    {
        public static int Width { get; set; }
        public static int Height { get; set; }
        public static int Speed { get; set; }
        public static int Score { get; set; }
        public static int Points { get; set; }
        public static bool GameOver { get; set; }
        public static Direction direction { get; set; }

        public GlobalSettings() 
        {
            Width = 20;
            Height = 20;
            Speed = 6;
            Score = 0;
            Points = 100;
            GameOver = false;
            direction = Direction.Up;
        }
    }
}
