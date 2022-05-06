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
        public Button B_b;
        public int B_x;
        public int B_y;
        public bool isBomb;
        public Tile(Button b, int x, int y)
        {
            this.B_b = b;
            this.B_x = x;
            this.B_y = y;
        }
    }
}
