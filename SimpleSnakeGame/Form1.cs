using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleSnakeGame
{
    public partial class Form1 : Form
    {
        private List<Circle> Snake = new List<Circle>();
        private Circle food = new Circle();

    
        public Form1()
        {
            InitializeComponent();

            new GlobalSettings();

            gameTimer.Interval = 1000 / GlobalSettings.Speed;
            gameTimer.Tick += UpdateScreen;
            gameTimer.Start();

            StartGame();
        }

        private void StartGame() 
        {
            lblGameOver.Visible = false;
            lblGameWin.Visible = false;

            new GlobalSettings();
            Snake.Clear();
            Circle head = new Circle();
            head.X = 5;
            head.Y = 15;
            Snake.Add(head);

            lblScore.Text = GlobalSettings.Score.ToString();
            GenerateFood();
        }

        private void GenerateFood() 
        {
            int maxXPos = PictureBox1.Size.Width / GlobalSettings.Width;
            int maxYPos = PictureBox1.Size.Height / GlobalSettings.Height;

            Random rnd = new Random();
            food = new Circle { X = rnd.Next(0, maxXPos), Y = rnd.Next(0, maxYPos) };

            CheckFoodPosition();


        }

        private void CheckFoodPosition() 
        {
            if (Snake.Count > 417)  
            {
                GameOverWin();
            }


            for (int i = 0; i < Snake.Count; i++)
            {
                if (food.X == Snake[i].X && food.Y == Snake[i].Y)
                {
                    GenerateFood();
                }
            }
        }

        private void GameOverWin()
        {
            GlobalSettings.GameOver = true;
            lblGameOver.Visible = false;
            lblGameWin.Visible = true;

            string gameovertext = "Congratulations you win \n wow really good job \n your mom must be proud \n press enter to win again" ;
            lblGameWin.Text = gameovertext.ToUpper();
        }

        private void UpdateScreen(object sender, EventArgs e) 
        {
            if (GlobalSettings.GameOver == true) 
            {
                if (Input.KeyPressed(Keys.Enter)) 
                {
                    StartGame();
                }
            }
            else 
            {
                if (Input.KeyPressed(Keys.Right) && GlobalSettings.direction != Direction.Left)
                    GlobalSettings.direction = Direction.Right;
                else
                if (Input.KeyPressed(Keys.Left) && GlobalSettings.direction != Direction.Right)
                    GlobalSettings.direction = Direction.Left;
                else
                if (Input.KeyPressed(Keys.Up) && GlobalSettings.direction != Direction.Down)
                    GlobalSettings.direction = Direction.Up;
                else
                if (Input.KeyPressed(Keys.Down) && GlobalSettings.direction != Direction.Up)
                    GlobalSettings.direction = Direction.Down;

                MovePlayer();

            }

            PictureBox1.Invalidate();

        }

        private void PictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics canvas = e.Graphics;
            Brush snakeColour;
            if(GlobalSettings.GameOver != true) 
            {
                for (int i = 0; i < Snake.Count; i++) 
                {
                    if (i == 0) { snakeColour = Brushes.Black; }
                    else { snakeColour = Brushes.Green; }

                    canvas.FillEllipse(snakeColour,
                        new Rectangle(Snake[i].X * GlobalSettings.Width,
                        Snake[i].Y * GlobalSettings.Height,
                        GlobalSettings.Width, GlobalSettings.Height
                        ));

                    canvas.FillEllipse(Brushes.Red,
                        new Rectangle(food.X * GlobalSettings.Width,
                        food.Y * GlobalSettings.Height,
                        GlobalSettings.Width,
                        GlobalSettings.Height));

                } 
            }
            else
            {
                string gameOver = "Game over \n Your final score is : "
                    + GlobalSettings.Score + " \n Press 'Enter' to try again";

                lblGameOver.Text = gameOver.ToUpper();
                // lblGameOver.Visible = true;
            }

        }

        private void MovePlayer() 
        {
            for (int i = Snake.Count -1; i>=0; i--) 
            {
                if(i == 0) 
                {
                    switch (GlobalSettings.direction)
                    {
                        case Direction.Right:
                            Snake[i].X++;
                            break;
                        case Direction.Left:
                            Snake[i].X--;
                            break;
                        case Direction.Up:
                            Snake[i].Y--;
                            break;
                        case Direction.Down:
                            Snake[i].Y++;
                            break;
                    }


                    int maxXPos = PictureBox1.Size.Width / GlobalSettings.Width;
                    int maxYPos = PictureBox1.Size.Height / GlobalSettings.Height;

                    if (Snake[i].X < 0 || Snake[i].Y < 0 ||
                        Snake[i].X >= maxXPos || Snake[i].Y >= maxYPos){ Die(); } 

                    for (int j = 1; j < Snake.Count; j++) 
                    {
                        if(Snake[i].X == Snake[j].X &&
                            Snake[i].Y == Snake[j].Y) { Die(); }

                    }

                    if(Snake[0].X == food.X && Snake[0].Y == food.Y) { Eat(); }
                     

                }
                else
                {
                    Snake[i].X = Snake[i - 1].X;
                    Snake[i].Y = Snake[i - 1].Y;
                }
            }
        }

        private void Die()
        {
             GlobalSettings.GameOver = true;
            lblGameOver.Visible = true;
        }

        private void Eat() 
        {
            Circle food = new Circle();
            food.X = Snake[Snake.Count - 1].X;
            food.Y = Snake[Snake.Count - 1].Y;
            Snake.Add(food);

            GlobalSettings.Score += GlobalSettings.Points;
            lblScore.Text = GlobalSettings.Score.ToString();
            GenerateFood();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            Input.ChangeState(e.KeyCode, true);
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            Input.ChangeState(e.KeyCode, false);
        }
    }
}
