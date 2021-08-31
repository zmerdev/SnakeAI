using SnakeForms.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnakeForms
{
    public partial class GameWindow : Form
    {
        private bool _aiEnabled = true;
        private Game Game;
        private Timer GameTimer;

        public GameWindow()
        {
            Game = new Game(_aiEnabled);


            InitializeComponent();
            CenterToScreen();
            ClientSize = new Size(Game.Width * 20, Game.Height * 20);

            // Create a timer for the GameLoop method
            GameTimer = new Timer();
            GameTimer.Tick += GameLoop;
            var tickrate = 100;
            if (_aiEnabled)
                tickrate = 7;
            GameTimer.Interval = 10;

            GameTimer.Start();
        }

        private void GameWindow_Paint(object sender, PaintEventArgs e)
        {
            Draw();
        }

        private void GameLoop(object sender, System.EventArgs e)
        {
            if (Game.GameOver)
            {
                var formGraphics = CreateGraphics();
                Font drawFont = new Font("Arial", 16);
                SolidBrush drawBrush = new SolidBrush(Color.White);
                float x = ((Game.Width * 20) / 2) - 40;
                float y = 50.0F;
                StringFormat drawFormat = new StringFormat();
                formGraphics.DrawString("Game Over", drawFont, drawBrush, x, y, drawFormat);
                
                formGraphics.Dispose();
                drawBrush.Dispose();

                this.ResetButton.Visible = true;
                return;
            }

            Game.Tick();
            Invalidate();
        }

        private void Draw()
        {
            DrawFood();
            DrawSnake();
        }

        private void DrawSnake()
        {
            foreach (var cell in Game.Snake.QueueCells)
            {
                DrawRect(cell.X * 20 + 1, cell.Y * 20 + 1, Color.Green, 18);
            }
        }

        private void DrawFood()
        {
            DrawRect(Game.Apple.X * 20 + 1, Game.Apple.Y * 20 + 1, Color.OrangeRed, 18);
        }

        private void DrawRect(int x, int y, Color color, int size = 20)
        {
            Graphics g = CreateGraphics();
            SolidBrush brush = new SolidBrush(color);
            g.FillRectangle(brush, new Rectangle(x, y, size, size));
            brush.Dispose();
            g.Dispose();
        }

        private void GameWindow_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (_aiEnabled)
                return;

            switch (e.KeyChar)
            {
                case 'w':
                    Game.Snake.ChangeTo = Direction.Up;
                    break;
                case 'a':
                    Game.Snake.ChangeTo = Direction.Left;
                    break;
                case 's':
                    Game.Snake.ChangeTo = Direction.Down;
                    break;
                case 'd':
                    Game.Snake.ChangeTo = Direction.Right;
                    break;
            }
        }

        //reset button
        private void button1_Click(object sender, EventArgs e)
        {
            Game = new Game(_aiEnabled);
            ResetButton.Visible = false;
            Focus();
        }
    }
}
