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
    }
}
