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
    class Tile
    {
        Bitmap FlagImage = new Bitmap("C:\\Users\\matho347\\Desktop\\1024px-Minesweeper_flag.svg.png");

        public Button B_b;
        public int B_x;
        public int B_y;
        public bool isBomb;
        public bool isFound = false;
        public bool Flag;
        public int num;
        public Tile(Button b, int x, int y)
        {
            this.B_b = b;
            this.B_x = x;
            this.B_y = y;
        }
        public void SetFlag()
        {
            Flag = !Flag;
            B_b.BackgroundImage = Flag ? FlagImage : null;
        }
        public void SetNumber(int n)
        {
            num = n;
        }
        public void Reveal()
        {
            if (!isFound)
            {
                isFound = true;
                B_b.BackColor = Color.LightGray;
                B_b.BackgroundImage = null;
                SetNumber();
            }
        }
        private void SetNumber()
        {
            if(num == 1)
            {
                B_b.Text = "1";
                B_b.ForeColor = Color.Blue;
            }
            else if (num == 2)
            {
                B_b.Text = "2";
                B_b.ForeColor = Color.Green;
            }
            else if (num == 3)
            {
                B_b.Text = "3";
                B_b.ForeColor = Color.Red;
            }
            else if (num == 4)
            {
                B_b.Text = "4";
                B_b.ForeColor = Color.Purple;
            }
            else if (num == 5)
            {
                B_b.Text = "5";
                B_b.ForeColor = Color.Maroon;
            }
            else if (num == 6)
            {
                B_b.Text = "6";
                B_b.ForeColor = Color.Turquoise;
            }
            else if (num == 7)
            {
                B_b.Text = "7";
                B_b.ForeColor = Color.Black;
            }
            else if (num == 8)
            {
                B_b.Text = "8";
                B_b.ForeColor = Color.Gray;
            }
        }
    }
}
