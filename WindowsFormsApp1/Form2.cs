using System;
using System.Collections.Generic;
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

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // 初始化地雷盤
            board = InitializeBoard(row, col, minesNumber);
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    Console.Write(board[i, j]);
                }
                Console.WriteLine();
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
