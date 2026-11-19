using System;
using System.Drawing;
using System.Windows.Forms;

namespace Snakegame
{
    public partial class Snakegame : Form
    {
        private Snake snake;
        private Food food;
        private int smallFoodCount = 0;
        private int score = 0;
        private Random random = new Random();

        public Snakegame()
        {
            InitializeComponent();

            KeyPreview = true;

            lblScore.Text = "Score: 0";
            snake = new Snake(6);
            CreateFood();

            cmbLevel.Items.Add("Easy");
            cmbLevel.Items.Add("Medium");
            cmbLevel.Items.Add("Hard");
            cmbLevel.SelectedIndex = 1;

            cmbLevel.SelectedIndexChanged +=
                cmbLevel_SelectedIndexChanged;

            timer1.Interval = 150;
            timer1.Stop();
        }

        private void cmbLevel_SelectedIndexChanged(
            object sender, EventArgs e)
        {
            if (cmbLevel.SelectedItem.ToString() == "Easy")
                timer1.Interval = 250;
            else if (cmbLevel.SelectedItem.ToString() == "Medium")
                timer1.Interval = 150;
            else if (cmbLevel.SelectedItem.ToString() == "Hard")
                timer1.Interval = 75;
        }

        private void CreateFood()
        {
            Point position;

            do
            {
                int x = random.Next(
                    0, pnlGame.ClientSize.Width / 10) * 10;

                int y = random.Next(
                    7, pnlGame.ClientSize.Height / 10) * 10;

                position = new Point(x, y);

            } while (snake.body.Contains(position));

            if (smallFoodCount >= 5)
                food = new LargeFood(position);
            else
                food = new SmallFood(position);
        }

        private void pnlGame_Paint(
            object sender, PaintEventArgs e)
        {
            foreach (Point circle in snake.body)
            {
                e.Graphics.FillEllipse(
                    Brushes.Red,
                    circle.X, circle.Y,
                    snake.circleSize, snake.circleSize);
            }

            if (food != null)
            {
                using (SolidBrush brush =
                    new SolidBrush(food.GetColor()))
                {
                    e.Graphics.FillEllipse(
                        brush,
                        food.Position.X,
                        food.Position.Y,
                        food.Size,
                        food.Size);
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            snake.Move();

            Point head = snake.body[0];

            // Check border collision
            if (head.X <= 0 || head.Y <= 0 ||
                head.X + snake.circleSize >=
                    pnlGame.ClientSize.Width ||
                head.Y + snake.circleSize >=
                    pnlGame.ClientSize.Height)
            {
                timer1.Stop();
                cmbLevel.Enabled = true;

                MessageBox.Show("Game Over! Score: " + score);
                return;
            }

            // Check self-collision
            for (int i = 1; i < snake.body.Count; i++)
            {
                if (head == snake.body[i])
                {
                    timer1.Stop();
                    cmbLevel.Enabled = true;

                    MessageBox.Show(
                        "GAME OVER!\nSnake touched itself!" +
                        "\nFinal Score: " + score);

                    return;
                }
            }

            // Check food collision
            Rectangle headRect = new Rectangle(
                head.X, head.Y,
                snake.circleSize, snake.circleSize);

            Rectangle foodRect = new Rectangle(
                food.Position.X, food.Position.Y,
                food.Size, food.Size);

            if (headRect.IntersectsWith(foodRect))
            {
                snake.Grow(food.GetGrowth());
                score += food.GetPoints();

                if (food is LargeFood)
                    smallFoodCount = 0;
                else
                    smallFoodCount++;

                lblScore.Text = "Score: " + score;

                CreateFood();
            }

            pnlGame.Invalidate();
        }

        private void Snakegame_KeyDown(
            object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.P)
            {
                timer1.Stop();
            }
            else if (e.KeyCode == Keys.Up ||
                     e.KeyCode == Keys.W)
            {
                snake.ChangeDirection(0);
            }
            else if (e.KeyCode == Keys.Right ||
                     e.KeyCode == Keys.D)
            {
                snake.ChangeDirection(1);
            }
            else if (e.KeyCode == Keys.Down ||
                     e.KeyCode == Keys.S)
            {
                snake.ChangeDirection(2);
            }
            else if (e.KeyCode == Keys.Left ||
                     e.KeyCode == Keys.A)
            {
                snake.ChangeDirection(3);
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            cmbLevel.Enabled = false;
            this.Focus();
            timer1.Start();
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            timer1.Stop();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            cmbLevel.Enabled = true;

            snake = new Snake(6);
            score = 0;
            smallFoodCount = 0;

            lblScore.Text = "Score: 0";

            CreateFood();
            pnlGame.Invalidate();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}