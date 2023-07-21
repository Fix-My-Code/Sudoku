using System;

public class SudokuGenerator
{
    public int Index
    {
        get
        {
            return _index;
        }
        set
        {
            if (value > 9)
            {
                value -= 9;
            }

            _index = value;
            
        }
    }

    private int _index;
    private const int ROW = 9, COL = 9;


    public Cell[,] DrawBaseGrid()
    {
        var cells = new Cell[9, 9];

        for (var row = 0; row < 9; row++)
        {
            for (var col = 0; col < 9; col++)
            {
                cells[row, col] = new Cell(row, col, 0);
            }
        }

        int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        const int shift = 3, shiftTransition = 4;

        Index = 0;
        for (int i = 0; i < ROW; i++)
        {
            for (int j = 0; j < COL; j++)
            {
                Index++;
                cells[i, j].Value = nums[Index - 1];
            }

            if ((i + 1) % 3 == 0)
            {
                Index += shiftTransition;
            }
            else
            {
                Index += shift;
            }
        }

        return cells;
    }

    public void TranspositionGrid(Cell[,] cells)
    {
        for (int i = ROW; i < ROW + 1; i++)
        {
            for (int j = COL; j < COL + 1; j++)
            {
                cells[i, j].Value = cells[j, i].Value;
            }
        }
    }




    //private Random random;

    //public SudokuGenerator()
    //{
    //    board = new int[9, 9];
    //    random = new Random();
    //}

    //public int[,] GenerateSudokuBoard()
    //{
    //    FillDiagonalBlocks();
    //    FillRemainingCells(0, 3);
    //    return board;
    //}

    //private void FillDiagonalBlocks()
    //{
    //    for (int block = 0; block < 9; block += 3)
    //    {
    //        FillBlock(block, block);
    //    }
    //}

    //private void FillBlock(int row, int col)
    //{
    //    int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
    //    ShuffleArray(nums);

    //    int index = 0;
    //    for (int i = row; i < row + 3; i++)
    //    {
    //        for (int j = col; j < col + 3; j++)
    //        {
    //            board[i, j] = nums[index];
    //            index++;
    //        }
    //    }
    //}

    //private void ShuffleArray(int[] arr)
    //{
    //    int n = arr.Length;
    //    for (int i = 0; i < n; i++)
    //    {
    //        int randIndex = random.Next(i, n);
    //        int temp = arr[i];
    //        arr[i] = arr[randIndex];
    //        arr[randIndex] = temp;
    //    }
    //}

    //private bool IsValid(int row, int col, int num)
    //{
    //    return IsRowSafe(row, num) && IsColumnSafe(col, num) && IsBoxSafe(row - row % 3, col - col % 3, num);
    //}

    //private bool IsRowSafe(int row, int num)
    //{
    //    for (int col = 0; col < 9; col++)
    //    {
    //        if (board[row, col] == num)
    //        {
    //            return false;
    //        }
    //    }
    //    return true;
    //}

    //private bool IsColumnSafe(int col, int num)
    //{
    //    for (int row = 0; row < 9; row++)
    //    {
    //        if (board[row, col] == num)
    //        {
    //            return false;
    //        }
    //    }
    //    return true;
    //}

    //private bool IsBoxSafe(int boxStartRow, int boxStartCol, int num)
    //{
    //    for (int row = 0; row < 3; row++)
    //    {
    //        for (int col = 0; col < 3; col++)
    //        {
    //            if (board[row + boxStartRow, col + boxStartCol] == num)
    //            {
    //                return false;
    //            }
    //        }
    //    }
    //    return true;
    //}

    //private bool FillRemainingCells(int row, int col)
    //{
    //    if (row == 9)
    //    {
    //        return true;
    //    }

    //    if (col == 9)
    //    {
    //        return FillRemainingCells(row + 1, 0);
    //    }

    //    if (board[row, col] != 0)
    //    {
    //        return FillRemainingCells(row, col + 1);
    //    }

    //    for (int num = 1; num <= 9; num++)
    //    {
    //        if (IsValid(row, col, num))
    //        {
    //            board[row, col] = num;

    //            if (FillRemainingCells(row, col + 1))
    //            {
    //                return true;
    //            }

    //            board[row, col] = 0;
    //        }
    //    }

    //    return false;
    //}
}