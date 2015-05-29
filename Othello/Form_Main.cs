using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Threading;

namespace Othello
{
    public partial class Form_Main : Form
    {
        Class_Game game = new Class_Game();
        
        public Form_Main()
        {
            InitializeComponent();
        }

        private void Form_Main_Load(object sender, EventArgs e)
        {
            game.form_main = this;
            game.RegisterControlor();
            pictureBox_Main.BackColor = Color.Transparent;  
        }

        private void Form_Main_Shown(object sender, EventArgs e)
        {
            game.UpdatePoint();
            game.UpdateHint(1);
        }


        private void toolStripMenuItem_Begin_Click(object sender, EventArgs e)
        {
        }
    }
}