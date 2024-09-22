namespace WindowsFormsApp1
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.enter = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.leaderboardPanel = new System.Windows.Forms.Panel();
            this.leaderboardPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("標楷體", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(757, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 40);
            this.label1.TabIndex = 0;
            this.label1.Text = "0";
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(138, 122);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(626, 463);
            this.panel1.TabIndex = 1;
            // 
            // enter
            // 
            this.enter.Location = new System.Drawing.Point(688, 83);
            this.enter.Name = "enter";
            this.enter.Size = new System.Drawing.Size(109, 29);
            this.enter.TabIndex = 1;
            this.enter.Text = "確認";
            this.enter.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(0, 83);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(625, 25);
            this.textBox1.TabIndex = 0;
            this.textBox1.Text = "輸入名字";
            // 
            // leaderboardPanel
            // 
            this.leaderboardPanel.Controls.Add(this.enter);
            this.leaderboardPanel.Controls.Add(this.textBox1);
            this.leaderboardPanel.Location = new System.Drawing.Point(43, 237);
            this.leaderboardPanel.Name = "leaderboardPanel";
            this.leaderboardPanel.Size = new System.Drawing.Size(797, 214);
            this.leaderboardPanel.TabIndex = 2;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(852, 722);
            this.Controls.Add(this.leaderboardPanel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Name = "Form2";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.leaderboardPanel.ResumeLayout(false);
            this.leaderboardPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button enter;
        private System.Windows.Forms.Panel leaderboardPanel;
    }
}