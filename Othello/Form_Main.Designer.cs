namespace Othello
{
    partial class Form_Main
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip_Main = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem_Game = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Begin = new System.Windows.Forms.ToolStripMenuItem();
            this.帮助HToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox_Main = new System.Windows.Forms.PictureBox();
            this.menuStrip_Main.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Main)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip_Main
            // 
            this.menuStrip_Main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_Game,
            this.帮助HToolStripMenuItem});
            this.menuStrip_Main.Location = new System.Drawing.Point(0, 0);
            this.menuStrip_Main.Name = "menuStrip_Main";
            this.menuStrip_Main.Size = new System.Drawing.Size(800, 24);
            this.menuStrip_Main.TabIndex = 1;
            this.menuStrip_Main.Text = "menuStrip1";
            // 
            // toolStripMenuItem_Game
            // 
            this.toolStripMenuItem_Game.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_Begin});
            this.toolStripMenuItem_Game.Name = "toolStripMenuItem_Game";
            this.toolStripMenuItem_Game.Size = new System.Drawing.Size(60, 20);
            this.toolStripMenuItem_Game.Text = "游戏(&G)";
            // 
            // toolStripMenuItem_Begin
            // 
            this.toolStripMenuItem_Begin.Name = "toolStripMenuItem_Begin";
            this.toolStripMenuItem_Begin.Size = new System.Drawing.Size(114, 22);
            this.toolStripMenuItem_Begin.Text = "开始(&B)";
            this.toolStripMenuItem_Begin.Click += new System.EventHandler(this.toolStripMenuItem_Begin_Click);
            // 
            // 帮助HToolStripMenuItem
            // 
            this.帮助HToolStripMenuItem.Name = "帮助HToolStripMenuItem";
            this.帮助HToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.帮助HToolStripMenuItem.Text = "帮助(&H)";
            // 
            // pictureBox_Main
            // 
            this.pictureBox_Main.BackgroundImage = global::Othello.Properties.Resources.UserInterface;
            this.pictureBox_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox_Main.Location = new System.Drawing.Point(0, 0);
            this.pictureBox_Main.Name = "pictureBox_Main";
            this.pictureBox_Main.Size = new System.Drawing.Size(800, 624);
            this.pictureBox_Main.TabIndex = 0;
            this.pictureBox_Main.TabStop = false;
            // 
            // Form_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 624);
            this.Controls.Add(this.menuStrip_Main);
            this.Controls.Add(this.pictureBox_Main);
            this.MainMenuStrip = this.menuStrip_Main;
            this.MaximizeBox = false;
            this.Name = "Form_Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "黑白棋";
            this.Load += new System.EventHandler(this.Form_Main_Load);
            this.Shown += new System.EventHandler(this.Form_Main_Shown);
            this.menuStrip_Main.ResumeLayout(false);
            this.menuStrip_Main.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Main)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip_Main;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Game;
        private System.Windows.Forms.ToolStripMenuItem 帮助HToolStripMenuItem;
        public System.Windows.Forms.PictureBox pictureBox_Main;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Begin;
    }
}

