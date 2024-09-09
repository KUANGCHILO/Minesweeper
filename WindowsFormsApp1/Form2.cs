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
        private int buttonSize = 50; // 按鈕的大小
        private int padding = 5; // 按鈕之間的間距

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // 初始化地雷盤
            board = InitializeBoard(row, col, minesNumber);
            Panel panel = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true // 如果按鈕數量超過 Panel 大小，啟用滾動條
            };
            this.Controls.Add(panel);

            // 創建按鈕網格
            CreateButtonGrid(panel,board);
        }
        private void CreateButtonGrid(Panel panel,int[,] board)
        {
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    Button button = new Button
                    {
                        Size = new System.Drawing.Size(buttonSize, buttonSize),
                        Location = new System.Drawing.Point(j * (buttonSize + padding), i * (buttonSize + padding)),
                        Tag = new { Row = i, Col = j, minenumber=board[i, j] ,flag=0} // 存儲行列信息
                    };

                    // 註冊點擊事件處理程序
                    button.MouseClick += Button_Click;
                    panel.Controls.Add(button);
                }
            }
        }

        // 按鈕點擊事件處理程序
        private void Button_Click(object sender, MouseEventArgs e)
        {
            Button button = sender as Button; // 確保 sender 是 Button 類型
            var tag = (dynamic)button.Tag;
            if (button != null && ((dynamic)button.Tag).flag == 0)
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (tag.minenumber == 9)
                    {
                        MessageBox.Show($"game over");
                    }
                    else
                    {
                        button.Text = $"{tag.minenumber}";
                        button.Font = new Font(button.Font.FontFamily, 16, FontStyle.Bold);
                        button.ForeColor = Color.Red;
                        button.Enabled = false;
                    }
                }
                else if (e.Button == MouseButtons.Right)
                {
                    if (button.Text== "🚩")
                    {
                        button.Text = " ";
                        ((dynamic)button.Tag).flag = 0;
                    }
                    else if (button.Text == "")
                    {
                        button.Text = "🚩";
                        ((dynamic)button.Tag).flag = 1;
                    }
                }
            }
        }

    private int[,] InitializeBoard(int row, int col, int minesNumber)
        {
            List<int> mines = GetRandomNumbers(row * col, minesNumber);
            int[,] board = new int[row, col];

            // 佈置地雷
            foreach (var mine in mines)
            {
                int mine_row = mine % row;
                int mine_col = mine / row;
                board[mine_row, mine_col] = 9;
            }

            // 計算地雷數量
            int[,] directions = { { -1, 0 }, { 1, 0 }, { 0, -1 }, { 0, 1 }, { -1, -1 }, { -1, 1 }, { 1, -1 }, { 1, 1 } };
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    if (board[i, j] == 9) continue;  // 如果這個位置是地雷，跳過
                    for (int k = 0; k < directions.GetLength(0); k++)
                    {
                        int newRow = i + directions[k, 0];
                        int newCol = j + directions[k, 1];

                        // 檢查邊界條件
                        if (newRow >= 0 && newRow < row && newCol >= 0 && newCol < col && board[newRow, newCol] == 9)
                        {
                            board[i, j]++;
                        }
                    }
                }
            }

            return board;
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

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
