using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MindSweeper
{
    public partial class Form1 : Form
    {
        Tile[,] Board = new Tile[8, 10];
        Button[] BombsLocations = new Button[10];
        int row = 8;
        int col = 10;
        int maxBombs = 10;
        int bombs = 0;
        public static bool firstClick = true;
        bool gameOver = false;
        Bitmap bombImage = new Bitmap("C:\\Users\\matho347\\Desktop\\bomb.png");
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            CreateBoard(Board);
        }
        private void CreateBoard(Tile[,] board)
        {
            int x = 650 / 3;
            int y = 1000 / 9;
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {

                    Button b = new Button();
                    Tile t = new Tile(b, i, j);
                    t.B_b.Location = new Point(x, y);
                    t.B_b.Size = new Size(45, 45);
                    t.B_b.Visible = true;
                    t.B_b.BackColor = Color.Green;
                    t.B_b.Font = new Font("Ravie", 22);
                    t.B_b.ForeColor = Color.Red;
                    t.B_b.Click += new EventHandler(this.ButtonClicks);
                    t.B_b.Text = string.Empty;
                    this.Controls.Add(t.B_b);
                    t.B_b.BackgroundImageLayout = ImageLayout.Stretch;
                    board[i, j] = t;
                    x += 51;
                }
                y += 51;
                x = 650 / 3;

            }


        }

        private void PlantBombs(Tile[,] board, Button b)
        {

            for (int m = 0; m < 10; m++)
            {
                if (bombs == maxBombs)
                {
                    return;
                }
                int rCol = new Random().Next(0, 9);
                int rRow = new Random().Next(0, 7);
                if (board[rRow, rCol].isBomb || board[rRow, rCol].B_b == b)
                {
                    m--;
                }
                else
                {

                    board[rRow, rCol].isBomb = true;
                    BombsLocations[m] = board[rRow, rCol].B_b;
                    bombs++;
                    Board[rRow,rCol].B_b.BackgroundImage = (Image)bombImage;
                }
            }
        }
        public void ButtonClicks(object sender, EventArgs e)
        {

            if (gameOver)
            {
                return;
            }
            Button t = (Button)sender;
            t.BackColor = Color.LightGray;

            if (firstClick)
            {
                PlantBombs(Board, t);
                SetNumbers();
                firstClick = false;
            }
            for (int i = 0; i < BombsLocations.Length; i++)
            {
                if (t == BombsLocations[i])
                {
                    t.BackColor = Color.Red;
                    t.BackgroundImage = (Image)bombImage;
                    RevealBoard();
                    GameOverLabel.Visible = true;
                    gameOver = true;
                }
            }
        }
        private void RevealBoard()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    for (int b = 0; b < BombsLocations.Length; b++)
                    {
                        if (Board[i,j].B_b == BombsLocations[b])
                        {
                            Board[i, j].B_b.BackgroundImage = (Image)bombImage;
                        }
                    }

                }
            }
        }
        private void SetNumbers()
        {
            int number = 0;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    for (int b = 0; b < BombsLocations.Length; b++)
                    {
                        #region Checks For Bombs
                        if (Board[i,j].B_b != BombsLocations[b])
                        {
                            if (i + 1 > 7)
                            {
                            }
                            else if (Board[i + 1, j].B_b == BombsLocations[b])
                            {
                                number++;
                            }

                            if (i + 1 > 7 || j + 1 > 9)
                            {
                            }
                            else if (Board[i + 1, j + 1].B_b == BombsLocations[b])
                            {
                               number++;
                            }

                            if (j + 1 > 9)
                            {
                            }
                            else if (Board[i, j + 1].B_b == BombsLocations[b])
                            {
                                number++;
                            }

                            if (i - 1 < 0 || j - 1 < 0)
                            {
                            }
                            else if (Board[i - 1, j - 1].B_b == BombsLocations[b])
                            {
                                number++;
                            }

                            if (i - 1 < 0)
                            {
                            }
                            else if (Board[i - 1, j].B_b == BombsLocations[b])
                            {
                                number++;
                            }

                            if (j - 1 < 0)
                            {
                            }
                            else if (Board[i, j - 1].B_b == BombsLocations[b])
                            {
                                number++;
                            }

                            if (i + 1 > 7 || j - 1 < 0)
                            {
                            }
                            else if (Board[i + 1, j - 1].B_b == BombsLocations[b])
                            {
                                number++;
                            }

                            if (i - 1 < 0 || j + 1 > 9)
                            {
                            }
                            else if (Board[i - 1, j + 1].B_b == BombsLocations[b])
                            {
                                number++;
                            }

                        }
                        #endregion
                    }
                    if (number==0)
                    {
                        Board[i, j].B_b.Text = "";
                    }
                    else
                    {
                        Board[i, j].B_b.Text = number.ToString();
                    }
                    number = 0;
                }
            }
        }
    }
}