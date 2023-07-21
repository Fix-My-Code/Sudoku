using System;

public class SudokuGenerator
{
    private int[,] board;
    private Random random;

    public SudokuGenerator()
    {
        board = new int[9, 9];
        random = new Random();
    }

    public int[,] GenerateSudokuBoard()
    {
        FillDiagonalBlocks();
        FillRemainingCells(0, 3);
        return board;
    }

    private void FillDiagonalBlocks()
    {
        for (int block = 0; block < 9; block += 3)
        {
            FillBlock(block, block);
        }
    }

    private void FillBlock(int row, int col)
    {
        int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        ShuffleArray(nums);

        int index = 0;
        for (int i = row; i < row + 3; i++)
        {
            for (int j = col; j < col + 3; j++)
            {
                board[i, j] = nums[index];
                index++;
            }
        }
    }

    private void ShuffleArray(int[] arr)
    {
        int n = arr.Length;
        for (int i = 0; i < n; i++)
        {
            int randIndex = random.Next(i, n);
            int temp = arr[i];
            arr[i] = arr[randIndex];
            arr[randIndex] = temp;
        }
    }

    private bool IsValid(int row, int col, int num)
    {
        return IsRowSafe(row, num) && IsColumnSafe(col, num) && IsBoxSafe(row - row % 3, col - col % 3, num);
    }

    private bool IsRowSafe(int row, int num)
    {
        for (int col = 0; col < 9; col++)
        {
            if (board[row, col] == num)
            {
                return false;
            }
        }
        return true;
    }

    private bool IsColumnSafe(int col, int num)
    {
        for (int row = 0; row < 9; row++)
        {
            if (board[row, col] == num)
            {
                return false;
            }
        }
        return true;
    }

    private bool IsBoxSafe(int boxStartRow, int boxStartCol, int num)
    {
        for (int row = 0; row < 3; row++)
        {
            for (int col = 0; col < 3; col++)
            {
                if (board[row + boxStartRow, col + boxStartCol] == num)
                {
                    return false;
                }
            }
        }
        return true;
    }

    private bool FillRemainingCells(int row, int col)
    {
        if (row == 9)
        {
            return true;
        }

        if (col == 9)
        {
            return FillRemainingCells(row + 1, 0);
        }

        if (board[row, col] != 0)
        {
            return FillRemainingCells(row, col + 1);
        }

        for (int num = 1; num <= 9; num++)
        {
            if (IsValid(row, col, num))
            {
                board[row, col] = num;

                if (FillRemainingCells(row, col + 1))
                {
                    return true;
                }

                board[row, col] = 0;
            }
        }

        return false;
    }
}
