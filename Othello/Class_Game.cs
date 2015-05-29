using System;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;


namespace Othello
{
    public class Class_Game
    {
        bool runAnimation = true;

        public Form_Main form_main;

        public Class_Player player_black = new Class_Player(1);
        public Class_Player player_white = new Class_Player(2);

        private int[,] map = new int[10, 10];

        private PictureBox[,] Pb_face = new PictureBox[9, 9];

        int turn = 1;

        int r_AI, c_AI;

        Bitmap chess_black, chess_white;
        Bitmap hint_black, hint_white;
        Bitmap cursor, cursor_red;
        Bitmap face = new Bitmap(800, 600);           //棋子
        
        public Class_Game()
        {
            //加载图片
            chess_black = new Bitmap(Properties.Resources.black);
            chess_white = new Bitmap(Properties.Resources.white);
            hint_black = new Bitmap(Properties.Resources.pre_black);
            hint_white = new Bitmap(Properties.Resources.pre_white);
            cursor = new Bitmap(Properties.Resources.cursor);
            cursor_red = new Bitmap(Properties.Resources.cursor_red);

            //初始化棋盘
            for (int i = 1; i <= 8; i++)
            {
                for (int j = 1; j <= 8; j++)
                {
                    map[i, j] = 0;
                }
            }

            map[4, 5] = map[5, 4] = 1;
            map[4, 4] = map[5, 5] = 2;

            //初始化棋子PictureBox
            for (int i = 1; i <= 8; i++)
            {
                for (int j = 1; j <= 8; j++)
                {
                    Pb_face[i, j] = new PictureBox();

                    Pb_face[i, j].Name = "PB" + i.ToString() + j.ToString();
                  
                    Pb_face[i, j].Location = new Point(102 + (j - 1) * 52,  164 + (i - 1) * 52);
                    Pb_face[i, j].Size = new Size(50, 50);
                    Pb_face[i, j].Image = Properties.Resources._null;

                    Pb_face[i, j].MouseEnter += Class_Game_MouseEnter;
                    Pb_face[i, j].MouseLeave += Class_Game_MouseLeave;
                    Pb_face[i, j].MouseClick += Class_Game_MouseClick;

                }
            }


            //初始化棋子背景
            for (int i = 1; i <= 8; i++)
            {
                for (int j = 1; j <= 8; j++)
                {
                Bitmap bg = new Bitmap(50, 50);

                    Image image = Properties.Resources.UserInterface;
                    Graphics g = Graphics.FromImage(bg);

                    Rectangle imgRect = new Rectangle(Pb_face[i, j].Location.X, Pb_face[i, j].Location.Y, 50, 50);
                    Rectangle destRect = new Rectangle(0, 0, 50, 50);

                    g.DrawImage(image, destRect, imgRect, GraphicsUnit.Pixel);

                    Pb_face[i, j].BackgroundImage = bg;

                    g.Dispose();
                }
            }
        }


        //鼠标单击
        void Class_Game_MouseClick(object sender, MouseEventArgs e)
        {
            PictureBox pb = sender as PictureBox;

            int i = int.Parse(pb.Name[2].ToString());
            int j = int.Parse(pb.Name[3].ToString());

            SetPoint(j, i);
     //     UpdatePoint();
            UpdateHint(turn);
            DisplayCursor(ref pb, cursor);

            if (c_AI != 0 && r_AI != 0)
            {
                DisplayCursor(ref Pb_face[r_AI, c_AI], cursor_red);
            }
        }

        
        //鼠标离开
        void Class_Game_MouseLeave(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;

            int i = int.Parse(pb.Name[2].ToString());
            int j = int.Parse(pb.Name[3].ToString());

            pb.Image = Properties.Resources._null;

            MakePoint(i, j);
            MakeHint(i, j, turn);


            if (c_AI != 0 && r_AI != 0)
            {
                DisplayCursor(ref Pb_face[r_AI, c_AI], cursor_red);
            }
        }


        //鼠标进入
        void Class_Game_MouseEnter(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            
            DisplayCursor(ref pb, cursor);
        }


        //显示所有棋子
        public void UpdatePoint()
        {
            for (int i = 1; i <= 8; i++)
            {
                for (int j = 1; j <= 8; j++)
                {
                    MakePoint(i, j);
                }
            }
        }


        private bool IsInPoint(Point[] p, int n, int x, int y)
        {
            for (int i = 1; i <= n; i++)
            {
                if (p[i].X == x && p[i].Y == y)
                    return true;
            }

            return false;
        }


        //显示棋子（列表中棋子除外）
        public void UpdatePoint(Point []p,int n)
        {
            for (int i = 1; i <= 8; i++)
            {
                for (int j = 1; j <= 8; j++)
                {
                    if (!IsInPoint(p, n, i, j))
                    {
                        MakePoint(i, j);
                    }
                }
            }
        }


        //显示所有可用点
        public void UpdateHint(int turn)
        {
            for (int i = 1; i <= 8; i++)
            {
                for (int j = 1; j <= 8; j++)
                {
                    MakeHint(i, j, turn);
                }
            }
        }


        //注册控件
        public void RegisterControlor()
        {
            //注册棋子PictureBox
            for (int i = 1; i <= 8; i++)
            {
                for (int j = 1; j <= 8; j++)
                {
                    form_main.Controls.Add(Pb_face[i, j]);
                }
            }

            form_main.pictureBox_Main.SendToBack();
        }


        //落子
        public void SetPoint(int x, int y)
        {
            if (turn == 1)
            {
                if (IsAvailable(x, y, turn))
                {
                    map[y, x] = 1;
                    Reverse(y, x, turn);

                    if (IsOver() == 1)
                        MessageBox.Show("黑棋胜利");
                    else if (IsOver() == 2)
                        MessageBox.Show("白棋胜利");
                    else if (IsOver() == -1)
                        MessageBox.Show("平局");

                    if (IsOver() != 0)
                        return;

                    if (CountAvailable(2) != 0)
                        turn = 2;
                    else
                        MessageBox.Show("白棋无棋可走");


              //     AI_Easy();
                }
            }
            else
            {
                if (IsAvailable(x, y, turn))
                {
                    map[y, x] = 2;
                    Reverse(y, x, turn);

                    if (IsOver() == 1)
                        MessageBox.Show("黑棋胜利");
                    else if (IsOver() == 2)
                        MessageBox.Show("白棋胜利");
                    else if (IsOver() == -1)
                        MessageBox.Show("平局");

                    if (IsOver() != 0)
                        return;

                    if (CountAvailable(1) != 0)
                        turn = 1;
                    else
                        MessageBox.Show("黑棋无棋可走");
                }
            }

        }


        //判断鼠标是否在棋盘内
        public bool IsCursorAvailabe(int x, int y)
        {
            if (!((102 <= x && x <= 515) && (164 <= y && y <= 577)))
                return false;

            return true;
        }

        
        //是否合法
        private bool IsAvailable(int y, int x, int turn)
        {
            if (map[x, y] != 0)
                return false;

            for (int i = x - 1; i >= 1; i--)//向上
            {
                if (map[i, y] == 0)
                    break;

                if (map[i, y] == turn)
                {
                    if (x - i >= 2)
                        return true;
                    else
                        break;
                }
            }

            for (int i = x + 1; i <= 8; i++)//向下
            {
                if (map[i, y] == 0)
                    break;

                if (map[i, y] == turn)
                {
                    if (i - x >= 2)
                        return true;
                    else
                        break;
                }
            }

            for (int i = y - 1; i >= 1; i--)//向左
            {
                if (map[x, i] == 0)
                    break;

                if (map[x, i] == turn)
                {
                    if (y - i >= 2)
                        return true;
                    else
                        break;
                }
            }

            for (int i = y + 1; i <= 8; i++)//向右
            {
                if (map[x, i] == 0)
                    break;

                if (map[x, i] == turn)
                {
                    if (i - y >= 2)
                        return true;
                    else
                        break;
                }
            }

            for (int i = x - 1, j = y - 1; i >= 1 && j >= 1; i--, j--)//向左上
            {
                if (map[i, j] == 0)
                    break;

                if (map[i, j] == turn)
                {
                    if (x - i >= 2)
                        return true;
                    else
                        break;
                }
            }

            for (int i = x - 1, j = y + 1; i >= 1 && j <= 8; i--, j++)//向右上
            {
                if (map[i, j] == 0)
                    break;

                if (map[i, j] == turn)
                {
                    if (x - i >= 2)
                        return true;
                    else
                        break;
                }
            }

            for (int i = x + 1, j = y - 1; i <= 8 && j >= 1; i++, j--)//向左下
            {
                if (map[i, j] == 0)
                    break;

                if (map[i, j] == turn)
                {
                    if (i - x >= 2)
                        return true;
                    else
                        break;
                }
            }

            for (int i = x + 1, j = y + 1; i <= 8 && j <= 8; i++, j++)//向右下
            {
                if (map[i, j] == 0)
                    break;

                if (map[i, j] == turn)
                {
                    if (i - x >= 2)
                        return true;
                    else
                        break;
                }
            }

            return false;
        }


        //反转
        private void Reverse(int x, int y, int turn)
        {
            Point[] point = new Point[100];
            int n = 0;

            for (int i = x - 1; i >= 1; i--)//向上
            {
                if (map[i, y] == 0)
                    break;

                if (map[i, y] == turn && x - i >= 2)
                {
                    for (int ii = x - 1; ii > i; ii--)
                    {
                        map[ii, y] = turn;

                        n++;
                        point[n] = new Point(ii, y);
                    }

                    break;
                }              
            }

            for (int i = x + 1; i <= 8; i++)//向下
            {
                if (map[i, y] == 0)
                    break;

                if (map[i, y] == turn && i - x >= 2)
                {
                    for (int ii = x + 1; ii < i; ii++)
                    {
                        map[ii, y] = turn;

                        n++;
                        point[n] = new Point(ii, y);
                    }

                    break;
                }
            }

            for (int i = y - 1; i >= 1; i--)//向左
            {
                if (map[x, i] == 0)
                    break;

                if (map[x, i] == turn && y - i >= 2)
                {
                    for (int ii = y - 1; ii > i; ii--)
                    {
                        map[x, ii] = turn;

                        n++;
                        point[n] = new Point(x, ii);
                    }
                    break;
                }
            }

            for (int i = y + 1; i <= 8; i++)//向右
            {
                if (map[x, i] == 0)
                    break;

                if (map[x, i] == turn && i - y >= 2)
                {
                    for (int ii = y + 1; ii < i; ii++)
                    {
                        map[x, ii] = turn;

                        n++;
                        point[n] = new Point(x, ii);
                    }

                    break;
                }
            }

            for (int i = x - 1, j = y - 1; i >= 1 && j >= 1; i--, j--)//向左上
            {
                if (map[i, j] == 0)
                    break;

                if (map[i, j] == turn && x - i >= 2)
                {
                    for (int ii = x - 1, jj = y - 1; ii > i && jj > j; ii--, jj--)
                    {
                        map[ii, jj] = turn;

                        n++;
                        point[n] = new Point(ii, jj);
                    }

                    break;
                }       
            }

            for (int i = x - 1, j = y + 1; i >= 1 && j <= 8; i--, j++)//向右上
            {
                if (map[i, j] == 0)
                    break;

                if (map[i, j] == turn && x - i >= 2)
                {
                    for (int ii = x - 1, jj = y + 1; ii > i && jj < j; ii--, jj++)
                    {
                        map[ii, jj] = turn;

                        n++;
                        point[n] = new Point(ii, jj);
                    }

                    break;
                }
            }

            for (int i = x + 1, j = y - 1; i <= 8 && j >= 1; i++, j--)//向左下
            {
                if (map[i, j] == 0)
                    break;

                if (map[i, j] == turn && i - x >= 2)
                {
                    for (int ii = x + 1, jj = y - 1; ii < i && jj > j; ii++, jj--)
                    {
                        map[ii, jj] = turn;

                        n++;
                        point[n] = new Point(ii, jj);
                    }

                    break;
                }
            }

            for (int i = x + 1, j = y + 1; i <= 8 && j <= 8; i++, j++)//向右下
            {
                if (map[i, j] == 0)
                    break;

                if (map[i, j] == turn && i - x >= 2)
                {
                    for (int ii = x + 1, jj = y + 1; ii < i && jj < j; ii++, jj++)
                    {
                        map[ii, jj] = turn;

                        n++;
                        point[n] = new Point(ii, jj);
                    }

                     break;
                }
            }

            if (runAnimation)
            {
                RunReverseAnimations(point, n, turn);

                UpdatePoint(point, n);
            }
            else
            {
                UpdatePoint();
            }
        }


        //执行反转动画
        private  void RunReverseAnimations(Point[] p, int n, int turn)
        {
            for (int i = 1; i <= n; i++)
            {
                System.Threading.Thread thread = new System.Threading.Thread(new System.Threading. ParameterizedThreadStart(RunReverseAnimation));

                Class_AnimateParameters ap = new Class_AnimateParameters(p[i].X, p[i].Y, turn,i,ref thread);

                thread.Start(ap);
            }
        }


        //执行反转动画
        private void RunReverseAnimation(object ap)
        {
            Class_AnimateParameters parameter = ap as Class_AnimateParameters;

            System.Threading.Thread.Sleep(parameter.index * 250 -250);

            float step = (float)0.05;


            //if (parameter.turn == 1)
            //    Pb_face[parameter.x, parameter.y].Image = chess_black;
            //else
            //    Pb_face[parameter.x, parameter.y].Image = chess_white;
            for (float alpha = 0; alpha < 1.01+step; alpha += step)
            {
                ReverseAnimation(parameter.x, parameter.y, parameter.turn, alpha);
          System.Threading  .    Thread.Sleep((int)(step * 500));
            }

            parameter.thread.Abort();
        }


        //执行反转动画计时器
       private void timer_Tick(object sender, EventArgs e)
        {
        }


        //反转动画
        private    void  ReverseAnimation(int x, int y, int turn,float alpha)
        {
            //turn = 1 白变黑
            //turn = 2 黑变白


            if (alpha > 1)
            {
                try
                {
                    if (turn == 1)
                    {
                        Pb_face[x, y].Image = chess_black;
                    }
                    else
                    {
                        Pb_face[x, y].Image = chess_white;
                    }
                }
                catch
                {
                    MessageBox.Show("Error1");
                }

                return;
            }

            try
            {
                //越来越深
                float[][] nArray1 = { new float[] { 1, 0, 0, 0, 0 }, new float[] { 0, 1, 0, 0, 0 }, new float[] { 0, 0, 1, 0, 0 }, new float[] { 0, 0, 0, (float)alpha, 0 }, new float[] { 0, 0, 0, 0, 1 } };
                //越来越浅
                float[][] nArray2 = { new float[] { 1, 0, 0, 0, 0 }, new float[] { 0, 1, 0, 0, 0 }, new float[] { 0, 0, 1, 0, 0 }, new float[] { 0, 0, 0, 1 - (float)alpha, 0 }, new float[] { 0, 0, 0, 0, 1 } };

                ColorMatrix matrix1 = new ColorMatrix(nArray1);
                ColorMatrix matrix2 = new ColorMatrix(nArray2);
                ImageAttributes attributes1 = new ImageAttributes();
                attributes1.SetColorMatrix(matrix1, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                ImageAttributes attributes2 = new ImageAttributes();
                attributes2.SetColorMatrix(matrix2, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                //原始颜色
                Bitmap p1 = new Bitmap(Properties.Resources._null);
             //   Bitmap p1 = new Bitmap(Pb_face[x, y].Image);
                Graphics g = Graphics.FromImage(p1);
                Rectangle rect = new Rectangle(0, 0, 50, 50);

                if (turn == 1)//白变黑
                {
                    g.DrawImage(chess_white, new Rectangle(0, 0, 50, 50), 0, 0, 50, 50, GraphicsUnit.Pixel, attributes2);
                    //变化后的颜色
                    Bitmap p2 = new Bitmap(p1);
                    Graphics g2 = Graphics.FromImage(p2);
                    g2.DrawImage(chess_black, new Rectangle(0, 0, 50, 50), 0, 0, 50, 50, GraphicsUnit.Pixel, attributes1);

                    Pb_face[x, y].Image = p2;

                    g2.Dispose();
                }
                else//黑变白
                {
                    g.DrawImage(chess_black, new Rectangle(0, 0, 50, 50), 0, 0, 50, 50, GraphicsUnit.Pixel, attributes2);
                    Bitmap p2 = new Bitmap(p1);
                    Graphics g2 = Graphics.FromImage(p2);
                    g2.DrawImage(chess_white, new Rectangle(0, 0, 50, 50), 0, 0, 50, 50, GraphicsUnit.Pixel, attributes1);

                    Pb_face[x, y].Image = p2;

                    g2.Dispose();
                }

                g.Dispose();
            }
            catch
            {
                MessageBox.Show("Error2") ;
            }
        }


        //判断是否游戏结束
        private int IsOver()
        {
            int sum_black = 0, sum_white = 0;

            for (int i = 1; i <= 8; i++)
            {
                for (int j = 1; j <= 8; j++)
                {
                    if (map[i, j] == 1)
                        sum_black++;
                    if (map[i, j] == 2)
                        sum_white++;
                }
            }

            if (sum_black == 0)
                return 2;
            if (sum_white == 0)
                return 1;

            if (sum_black + sum_white == 64 || (CountAvailable(1) == 0 && CountAvailable(2) == 0))
            {
                if (sum_black > sum_white)
                    return 1;
                else if (sum_black < sum_white)
                    return 0;
                else
                    return -1;
            }



            return 0;
        }


        //可用点数量
        private int CountAvailable(int turn)
        {
            int sum = 0;

            for (int i = 1; i <= 8; i++)
            {
                for (int j = 1; j <= 8; j++)
                {
                    if (IsAvailable(j, i, turn))
                    {
                        sum++;
                    }
                }
            }

            return sum;
        }


        //显示棋子
        private void MakePoint(int x, int y)
        {
            if (map[x, y] == 0)
            {
                Pb_face[x, y].Image = Properties.Resources._null;
                return;
            }

            if (map[x, y] == 1)
                Pb_face[x, y].Image = chess_black;
            else
                Pb_face[x, y].Image = chess_white;
        }


        //显示光标
        private void DisplayCursor(ref PictureBox pb, Bitmap c)
        {
            Bitmap bitmap = new Bitmap(pb.Image);

            Graphics g = Graphics.FromImage(bitmap);

            g.DrawImage(c, 0, 0);
            g.Dispose();

            pb.Image = bitmap;
        }


        //显示可用点
        private void MakeHint(int x, int y, int turn)
        {
            if (IsAvailable(y, x, turn))
            {
                if (turn == 1)
                    Pb_face[x, y].Image = hint_black;
                else
                    Pb_face[x, y].Image = hint_white;
            }
        }

        //----------------------------------------------------

        //简单AI
        
        public void AI_Easy()
        {
            Point[] p = new Point[50];
            int np = 0;

            for (int i = 1; i <= 8; i++)
            {
                for (int j = 1; j <= 8; j++)
                {
                    if (IsAvailable(i, j, turn))
                    {
                        np++;
                        p[np] = new Point(i, j);
                    }
                }
            }

            Random random = new Random(DateTime.Now.Millisecond);
            int pp = random.Next(np) + 1;

            c_AI = r_AI = 0;

            c_AI= p[pp].X;
            r_AI= p[pp].Y;

            DisplayCursor(ref Pb_face[r_AI, c_AI], cursor_red);
            map[r_AI, c_AI] = 2;
            Reverse(r_AI, c_AI, turn);

            if (IsOver() == 1)
                MessageBox.Show("黑棋胜利");
            else if (IsOver() == 2)
                MessageBox.Show("白棋胜利");
            else if (IsOver() == -1)
                MessageBox.Show("平局");

            if (IsOver() != 0)
                return;

            if (CountAvailable(1) != 0)
                turn = 1;
            else
                MessageBox.Show("黑棋无棋可走");
        }
    }

    public class Class_AnimateParameters
    {
        public int x, y, turn, index;
        public  System.Threading.Thread thread;

        public Class_AnimateParameters(int xx, int yy, int Turn, int Index, ref  System.Threading.Thread t)
        {
            x = xx;
            y = yy;
            turn = Turn;
            index = Index;

            thread = t;
        }
    }
}