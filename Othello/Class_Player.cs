using System;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Othello
{
    public class Class_Player
    {
        private static int[,] map = new int[10, 10];
        private Point[,] p = new Point[10, 10];

        int color;
        Bitmap chess_black, chess_white;
        Bitmap cursor;
        Bitmap face = Properties.Resources.UserInterface;    //当前状态
        Bitmap origin = Properties.Resources.UserInterface; //原始界面

        //构造函数
        public Class_Player(int Color)//color == 1 == 黑； color == 2 == 白
        {
        }
    }
}