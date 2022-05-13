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
        int row = 8;
        int col = 10;
        Tile[,] Board = new Tile[8, 10];
        Button[] BombsLocations = new Button[10];
        int maxBombs = 10;
        int bombs = 0;
        public static bool firstClick = true;
        bool gameOver = false;
        Bitmap bombImage = new Bitmap("C:\\Users\\matho347\\Desktop\\bomb.png");
        Bitmap FlagImage = new Bitmap("C:\\Users\\matho347\\Desktop\\1024px-Minesweeper_flag.svg.png");
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            int FormWidth = (row * 45) * 2 +(row * 45) / 2;
            int FormHeight = (col * 45) + (col * 45)/2;
            FindForm().Size = new Size(FormWidth, FormHeight);
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
                    t.B_b.BackColor = Color.LimeGreen;
                    t.B_b.Font = new Font("Ravie", 22);
                    t.B_b.ForeColor = Color.LimeGreen;
                    t.B_b.Click += new EventHandler(this.ButtonClicks);
                    t.B_b.MouseDown += new MouseEventHandler(this.ButtonRightClicks);
                    t.B_b.Text = string.Empty;
                    this.Controls.Add(t.B_b);
                    t.B_b.BackgroundImageLayout = ImageLayout.Stretch;
                    //t.B_b.BackgroundImage = (Image)CellImage;
                    board[i, j] = t;
                    x += 45;
                }
                y += 45;
                x = 650 / 3;

            }


        }
        public void ButtonRightClicks(object sender, MouseEventArgs e)
        {
            Button t = (Button)sender;
            Tile tile = null;
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    if (Board[i, j].B_b.Location.X == t.Location.X && Board[i, j].B_b.Location.Y == t.Location.Y)
                    {
                        tile = Board[i,j];
                    }
                }
            }
            if (e.Button == MouseButtons.Right & e.Button == MouseButtons.Left)
            {
                WinLabel.Visible = true;
            }
            if (e.Button == MouseButtons.Right)
            {
                tile.SetFlag();
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
                    //Board[rRow,rCol].B_b.BackgroundImage = (Image)bombImage;
                }
            }
        }
        private void ResetBoard()
        {
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {

                }
            }
        }
        public void ButtonClicks(object sender, EventArgs e)
        {
            Tile tile = null;
            Button t = (Button)sender;
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    if (Board[i, j].B_b.Location.X == t.Location.X && Board[i, j].B_b.Location.Y == t.Location.Y)
                    {
                        tile = Board[i, j];
                    }
                }
            }
            if (gameOver)
            {
                return;
            }
            if (tile.Flag)
            {
                return;
            }
            if (firstClick)
            {
                PlantBombs(Board, t);
                SetNumbers();
                firstClick = false;
            }
            if (tile.num == 0)
            {
                UncoverZeros(t);
            }
            else
            {
                t.BackColor = Color.LightGray;
                t.BackgroundImage = null;
                ColorNumber(t);
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
            if (CheckWin())
            {
                WinLabel.Visible = true;
            }
        }
        private void ColorNumber(Button t)
        {
            Tile tile = null;
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    if (Board[i, j].B_b.Location.X == t.Location.X && Board[i, j].B_b.Location.Y == t.Location.Y)
                    {
                        tile = Board[i, j];
                    }
                }
            }
            if (tile.num == 1)
            {
                t.Text = "1";
                t.ForeColor = Color.Blue;
            }
            else if (tile.num == 2)
            {
                t.Text = "2";
                t.ForeColor = Color.Green;
            }
            else if (tile.num == 3)
            {
                t.Text = "3";
                t.ForeColor = Color.Red;
            }
            else if (tile.num == 4)
            {
                t.Text = "4";
                t.ForeColor = Color.Purple;
            }
            else if (tile.num == 5)
            {
                t.Text = "5";
                t.ForeColor = Color.Maroon;
            }
            else if (tile.num == 6)
            {
                t.Text = "6";
                t.ForeColor = Color.Turquoise;
            }
            else if (tile.num == 7)
            {
                t.Text = "7";
                t.ForeColor = Color.Black;
            }
            else if (tile.num == 8)
            {
                t.Text = "8";
                t.ForeColor = Color.Gray;
            }
        }
        private void RevealBoard()
        {
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    for (int b = 0; b < BombsLocations.Length; b++)
                    {
                        if (Board[i,j].B_b == BombsLocations[b])
                        {
                            Board[i, j].B_b.BackgroundImage = (Image)bombImage;
                        }
                        else
                        {
                            Board[i, j].B_b.BackColor = Color.LightGray;
                            ColorNumber(Board[i, j].B_b);
                        }
                    }

                }
            }
        }
        //if double click then reveal other tiles end if
        private void UncoverZeros(Button b)
        {
            Tile t = null;

                for (int i = 0; i < row; i++)
                {
                    for (int j = 0; j < col; j++)
                    {
                        if (Board[i, j].B_b.Location.X == b.Location.X && Board[i, j].B_b.Location.Y == b.Location.Y)
                        {
                            t = Board[i, j];
                        }
                    }
                }
            if (t.num == 0)
            {
                if (t.B_x + 1 > 7)
                {
                }
                else
                {
                    if (Board[t.B_x + 1, t.B_y].num == 0 && Board[t.B_x + 1, t.B_y].B_b.BackColor == Color.LimeGreen)
                    {
                        Board[t.B_x + 1, t.B_y].B_b.BackColor = Color.LightGray;
                        UncoverZeros(Board[t.B_x + 1, t.B_y].B_b);
                    }
                    else
                    {
                        Board[t.B_x + 1, t.B_y].B_b.BackColor = Color.LightGray;
                        ColorNumber(Board[t.B_x + 1, t.B_y].B_b);
                    }
                }

                if (t.B_x+ 1 > 7 || t.B_y + 1 > 9)
                {
                }
                else
                {
                    if (Board[t.B_x + 1, t.B_y + 1].num == 0 && Board[t.B_x + 1, t.B_y + 1].B_b.BackColor == Color.LimeGreen)
                    {
                        Board[t.B_x + 1, t.B_y + 1].B_b.BackColor = Color.LightGray;
                        UncoverZeros(Board[t.B_x + 1, t.B_y + 1].B_b);
                    }
                    else
                    {
                        Board[t.B_x + 1, t.B_y + 1].B_b.BackColor = Color.LightGray;
                        ColorNumber(Board[t.B_x + 1, t.B_y + 1].B_b);

                    }
                }

                if (t.B_y + 1 > 9)
                {
                }
                else
                {
                    if (Board[t.B_x, t.B_y + 1].num == 0 && Board[t.B_x, t.B_y + 1].B_b.BackColor == Color.LimeGreen)
                    {
                        Board[t.B_x, t.B_y + 1].B_b.BackColor = Color.LightGray;
                        UncoverZeros(Board[t.B_x, t.B_y + 1].B_b);
                    }
                    else
                    {
                        Board[t.B_x, t.B_y + 1].B_b.BackColor = Color.LightGray;
                        ColorNumber(Board[t.B_x, t.B_y + 1].B_b);

                    }
                }

                if (t.B_x- 1 < 0 || t.B_y - 1 < 0)
                {
                }
                else
                {
                    if (Board[t.B_x - 1, t.B_y - 1].num == 0 && Board[t.B_x - 1, t.B_y - 1].B_b.BackColor == Color.LimeGreen)
                    {
                        Board[t.B_x - 1, t.B_y - 1].B_b.BackColor = Color.LightGray;
                        UncoverZeros(Board[t.B_x - 1, t.B_y - 1].B_b);
                    }
                    else
                    {
                        Board[t.B_x - 1, t.B_y - 1].B_b.BackColor = Color.LightGray;
                        ColorNumber(Board[t.B_x - 1, t.B_y - 1].B_b);

                    }
                }

                if (t.B_x- 1 < 0)
                {
                }
                else
                {
                    if (Board[t.B_x - 1, t.B_y].num == 0 && Board[t.B_x - 1, t.B_y].B_b.BackColor == Color.LimeGreen)
                    {
                        Board[t.B_x - 1, t.B_y].B_b.BackColor = Color.LightGray;
                        UncoverZeros(Board[t.B_x - 1, t.B_y].B_b);
                    }
                    else
                    {
                        Board[t.B_x - 1, t.B_y].B_b.BackColor = Color.LightGray;
                        ColorNumber(Board[t.B_x - 1, t.B_y].B_b);

                    }
                }

                if (t.B_y - 1 < 0)
                {
                }
                else
                {
                    if (Board[t.B_x, t.B_y - 1].num == 0 && Board[t.B_x, t.B_y - 1].B_b.BackColor == Color.LimeGreen)
                    {
                        Board[t.B_x, t.B_y - 1].B_b.BackColor = Color.LightGray;
                        UncoverZeros(Board[t.B_x, t.B_y - 1].B_b);
                    }
                    else
                    {
                        Board[t.B_x, t.B_y - 1].B_b.BackColor = Color.LightGray;
                        ColorNumber(Board[t.B_x, t.B_y - 1].B_b);

                    }
                }

                if (t.B_x+ 1 > 7 || t.B_y - 1 < 0)
                {
                    
                }
                else
                {
                    if (Board[t.B_x + 1, t.B_y - 1].num == 0 && Board[t.B_x + 1, t.B_y - 1].B_b.BackColor == Color.LimeGreen)
                    {
                        Board[t.B_x + 1, t.B_y - 1].B_b.BackColor = Color.LightGray;
                        UncoverZeros(Board[t.B_x + 1, t.B_y - 1].B_b);
                    }
                    else
                    {
                        Board[t.B_x + 1, t.B_y - 1].B_b.BackColor = Color.LightGray;
                        ColorNumber(Board[t.B_x + 1, t.B_y - 1].B_b);

                    }
                }

                if (t.B_x- 1 < 0 || t.B_y + 1 > 9)
                {
                }
                else
                {
                    if (Board[t.B_x - 1, t.B_y + 1].num == 0 && Board[t.B_x - 1, t.B_y + 1].B_b.BackColor == Color.LimeGreen)
                    {
                        Board[t.B_x - 1, t.B_y + 1].B_b.BackColor = Color.LightGray;
                        UncoverZeros(Board[t.B_x - 1, t.B_y + 1].B_b);
                    }
                    else
                    {
                        Board[t.B_x - 1, t.B_y + 1].B_b.BackColor = Color.LightGray;
                        ColorNumber(Board[t.B_x - 1, t.B_y + 1].B_b);

                    }
                }
            }
        }
        private bool CheckWin()
        {
            if (gameOver) return false;
            int total = 0;
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    if (Board[i,j].isBomb == false && Board[i,j].B_b.BackColor == Color.LightGray)
                    {
                        total++;
                    }
                }
            }
            if (total == (row * col) - maxBombs)
            {
                return true;
            }else
            {
                return false;
            }
        }
        private void SetNumbers()
        {
            int number = 0;
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    #region Checks For Bombs
                    if (Board[i, j].isBomb == false)
                        {
                            if (i + 1 > 7)
                            {
                            }
                            else if (Board[i + 1, j].isBomb)
                            {
                                number++;
                            }

                            if (i + 1 > 7 || j + 1 > 9)
                            {
                            }
                            else if (Board[i + 1, j + 1].isBomb)
                            {
                               number++;
                            }

                            if (j + 1 > 9)
                            {
                            }
                            else if (Board[i, j + 1].isBomb)
                            {
                                number++;
                            }

                            if (i - 1 < 0 || j - 1 < 0)
                            {
                            }
                            else if (Board[i - 1, j - 1].isBomb)
                            {
                                number++;
                            }

                            if (i - 1 < 0)
                            {
                            }
                            else if (Board[i - 1, j].isBomb)
                            {
                                number++;
                            }

                            if (j - 1 < 0)
                            {
                            }
                            else if (Board[i, j - 1].isBomb)
                            {
                                number++;
                            }

                            if (i + 1 > 7 || j - 1 < 0)
                            {
                            }
                            else if (Board[i + 1, j - 1].isBomb)
                            {
                                number++;
                            }

                            if (i - 1 < 0 || j + 1 > 9)
                            {
                            }
                            else if (Board[i - 1, j + 1].isBomb)
                            {
                                number++;
                            }

                        #endregion
                    }
                    Board[i, j].SetNumber(number);
                    number = 0;
                }
            }
        }
    }
}