using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        private int[,] board;
        private int row = 7;
        private int col = 7;
        private int minesNumber = 10;
        private int buttonSize = 50; 
        private int padding = 5; 

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

            board = InitializeBoard(row, col, minesNumber);
            panel1.AutoScroll = true;
            panel1.Size = new Size((buttonSize + padding) * col, (buttonSize + padding) * row);
            this.Controls.Add(panel1);


            CreateButtonGrid(panel1,board);
            timer1.Enabled = true;
        }
        private void CreateButtonGrid(Panel panel1,int[,] board)
        {
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    Button button = new Button
                    {
                        Size = new System.Drawing.Size(buttonSize, buttonSize),
                        Location = new System.Drawing.Point(j * (buttonSize + padding), i * (buttonSize + padding)),
                        Tag = new ButtonTag
                        {
                            Row = i,
                            Col = j,
                            MineNumber = board[i, j], 
                            Flag = 0 
                        }
                    };

                    button.MouseDown += Button_Click;
                    panel1.Controls.Add(button);
                }
            }
        }

        private void Button_Click(object sender, MouseEventArgs e)
        {
            Button button = sender as Button; 
            if (button != null)
            {
               
                ButtonTag tag = (ButtonTag)button.Tag;

                if (e.Button == MouseButtons.Left && tag.Flag == 0)
                {
                    if (tag.MineNumber == 9)
                    {
                        MessageBox.Show($"Game over");
                    }
                    else
                    {
                        OpenButton(button);

                        
                        if (tag.MineNumber == 0)
                        {
                            OpenSurroundingButtons(tag.Row, tag.Col);
                        }
                        if (CheckVictory())
                        {
                            timer1.Enabled = false;
                            MessageBox.Show($"過關，使用{label1.Text}秒");
                        }
                    }
                }
                else if (e.Button == MouseButtons.Right)
                {
                    if (button.Text == "🚩")
                    {
                        button.Text = string.Empty;
                        tag.Flag = 0; 
                    }
                    else if (string.IsNullOrEmpty(button.Text))
                    {
                        button.Text = "🚩"; 
                        tag.Flag = 1; 
                    }

                    button.Tag = tag;
                }
            }
        }


        private int[,] InitializeBoard(int row, int col, int minesNumber)
        {
            List<int> mines = GetRandomNumbers(row * col, minesNumber);
            int[,] board = new int[row, col];

            foreach (var mine in mines)
            {
                int mine_row = mine % row;
                int mine_col = mine / row;
                board[mine_row, mine_col] = 9;
            }

            int[,] directions = { { -1, 0 }, { 1, 0 }, { 0, -1 }, { 0, 1 }, { -1, -1 }, { -1, 1 }, { 1, -1 }, { 1, 1 } };
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    if (board[i, j] == 9) continue;  
                    for (int k = 0; k < directions.GetLength(0); k++)
                    {
                        int newRow = i + directions[k, 0];
                        int newCol = j + directions[k, 1];

                        if (newRow >= 0 && newRow < row && newCol >= 0 && newCol < col && board[newRow, newCol] == 9)
                        {
                            board[i, j]++;
                        }
                    }
                }
            }

            return board;
        }

        private void OpenButton(Button button)
        {
            ButtonTag tag = (ButtonTag)button.Tag;
            if (!button.Enabled) return; 

            button.Text = tag.MineNumber == 0 ? string.Empty : tag.MineNumber.ToString();
            button.Font = new Font(button.Font.FontFamily, 16, FontStyle.Bold);
            button.ForeColor = Color.Red;
            button.Enabled = false;
        }

        private void OpenSurroundingButtons(int row, int col)
        {
            int[,] directions = {
        { -1, 0 }, { 1, 0 }, { 0, -1 }, { 0, 1 },
        { -1, -1 }, { -1, 1 }, { 1, -1 }, { 1, 1 }
    };

            for (int i = 0; i < directions.GetLength(0); i++)
            {
                int newRow = row + directions[i, 0];
                int newCol = col + directions[i, 1];

                if (newRow >= 0 && newRow < this.row && newCol >= 0 && newCol < this.col)
                {
                    foreach (Button btn in panel1.Controls) 
                    {
                        ButtonTag btnTag = (ButtonTag)btn.Tag;
                        if (btnTag.Row == newRow && btnTag.Col == newCol && btn.Enabled)
                        {
                            OpenButton(btn);

                            if (btnTag.MineNumber == 0)
                            {
                                OpenSurroundingButtons(newRow, newCol);
                            }
                        }
                    }
                }
            }
        }
        private bool CheckVictory()
        {
            foreach (Control control in this.panel1.Controls) 
            {
                Button button = control as Button;
                if (button != null)
                {
                    ButtonTag tag = (ButtonTag)button.Tag;

                    if (button.Enabled && tag.Flag == 0)
                    {
                        return false;
                    }

                }
            }
            return true; 
        }


        private List<int> GetRandomNumbers(int n, int k)
        {
            Random random = new Random();
            List<int> numbers = Enumerable.Range(0, n).ToList();

            List<int> randomNumbers = new List<int>();
            for (int i = 0; i < k; i++)
            {
                int index = random.Next(numbers.Count);
                randomNumbers.Add(numbers[index]);
                numbers.RemoveAt(index);
            }

            return randomNumbers;
        }
        public class ButtonTag
        {
            public int Row { get; set; }
            public int Col { get; set; }
            public int MineNumber { get; set; }
            public int Flag { get; set; }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int num = Convert.ToInt32(label1.Text)+1;
            label1.Text = num.ToString();
        }

    }
}
