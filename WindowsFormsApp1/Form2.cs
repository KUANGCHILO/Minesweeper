using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

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
            // 初始化 Enter 按鈕事件
            enter.Click += Enter_Click;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            board = InitializeBoard(row, col, minesNumber);
            panel1.Size = new Size((buttonSize + padding) * col, (buttonSize + padding) * row);
            this.Controls.Add(panel1);

            CreateButtonGrid(panel1, board);
            timer1.Enabled = true;

            leaderboardPanel.Visible = false; // 初始隱藏排行榜輸入區域
        }

        private void CreateButtonGrid(Panel panel1, int[,] board)
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
                        Form1 form1 = new Form1();
                        form1.Show();
                        this.Hide();
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
                            int score;
                            int.TryParse(label1.Text, out score);
                            MessageBox.Show($"過關，使用{label1.Text}秒");
                            check_rankfile_exist();
                            if (check_rank(score))
                            {
                                leaderboardPanel.Visible = true; // 顯示排行榜輸入區域
                            }
                            else
                            {
                                Form3 form3 = new Form3();
                                form3.Show();
                                this.Hide();
                            }

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
                    foreach (Button btn in panel1.Controls.OfType<Button>())
                    {
                        ButtonTag btnTag = btn.Tag as ButtonTag;

                        if (btnTag != null && btnTag.Row == newRow && btnTag.Col == newCol && btn.Enabled)
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
                if (control is Button button)
                {
                    ButtonTag tag = (ButtonTag)button.Tag;

                    if (button.Enabled && tag.MineNumber != 9)
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
            int num;
            if (int.TryParse(label1.Text, out num))
            {
                num += 1;
                label1.Text = num.ToString();
            }
        }

        private void check_rankfile_exist()
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\data.csv");
            filePath = Path.GetFullPath(filePath); // 確保獲取絕對路徑

            if (!File.Exists(filePath))
            {
                using (StreamWriter writer = new StreamWriter(filePath, false, Encoding.UTF8))
                {
                    writer.WriteLine("姓名,成績,時間");
                }
            }
        }

        private bool check_rank(int time)
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\data.csv");
            filePath = Path.GetFullPath(filePath); // 確保獲取絕對路徑

            // 讀取現有的 CSV 檔案資料
            List<string> lines = new List<string>(File.ReadAllLines(filePath));

            // 遍歷每一行，跳過表頭，僅檢查前 10 名
            for (int i = 1; i < Math.Min(lines.Count, 11); i++)
            {
                // 將行分割成字段 (姓名, 成績, 時間)
                string[] columns = lines[i].Split(',');

                // 比較時間，找到比參數時間大的，並且返回 true
                if (int.TryParse(columns[1], out int currentTime) && time < currentTime)
                {
                    return true;
                }
            }
            // 如果沒有找到比傳入時間更大的成績，檢查是否未滿 10 名
            if (lines.Count - 1 < 10)
            {
                return true;
            }
            return false;
        }

        private void Enter_Click(object sender, EventArgs e)
        {
            // 確認名字輸入後執行的邏輯
            string playerName = textBox1.Text;

            if (!string.IsNullOrEmpty(playerName))
            {
                update_rank(playerName, int.Parse(label1.Text));

                // 隱藏 TextBox 和 Enter 按鈕
                textBox1.Visible = false;
                enter.Visible = false;

                // 停止監聽 Enter 按鈕的點擊事件
                enter.Click -= Enter_Click;

                MessageBox.Show("名字已保存！");
            }
            else
            {
                MessageBox.Show("請輸入有效名字！");
            }
            Form3 form3 = new Form3();
            form3.Show();
            this.Hide();
        }

        private void update_rank(string name, int time)
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\data.csv");
            filePath = Path.GetFullPath(filePath); // 確保獲取絕對路徑

            // 讀取現有的 CSV 檔案資料
            List<string> lines = new List<string>(File.ReadAllLines(filePath));

            bool updated = false;

            // 遍歷每一行，跳過表頭
            for (int i = 1; i < lines.Count; i++)
            {
                // 將行分割成字段 (姓名, 成績, 時間)
                string[] columns = lines[i].Split(',');

                // 比較時間，找到比參數時間大的，並且替換
                if (int.TryParse(columns[1], out int currentTime) && time < currentTime)
                {
                    lines.Insert(i, $"{name},{time},{DateTime.Now}");
                    updated = true;
                    break;
                }
            }

            // 如果沒有更好的時間，則新增一行
            if (!updated)
            {
                lines.Add($"{name},{time},{DateTime.Now}");
            }

            // 用 UTF-8 覆蓋寫入檔案
            using (StreamWriter writer = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                foreach (string line in lines)
                {
                    writer.WriteLine(line);
                }
            }
        }
    }
}
