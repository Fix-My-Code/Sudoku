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
    private Random random = new Random();

    public Cell[,] DrawGrid()
    {
        var cells = new Cell[ROW, COL];

        CreateGrid(cells);
        DrawBaseGrid(cells);

        TranspositionGrid(cells);
        //MixGrid(cells);       

        return cells;
    }

    private void CreateGrid(Cell[,] cells)
    {
        for (var row = 0; row < ROW; row++)
        {
            for (var col = 0; col < COL; col++)
            {
                cells[row, col] = new Cell(row, col, 0);
            }
        }
    }

    private void DrawBaseGrid(Cell[,] cells)
    {
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
    }

    private void MixGrid(Cell[,] cells)
    {
        Action<Cell[,]>[] mixFunctions = {
            TranspositionGrid,
            SwapRandomRows,
            SwapRandomCols
        };

        int numberMixes = random.Next(3, 15);
        for (int i = 0; i < numberMixes; i++)
        {
            int functionIndex = random.Next(0, mixFunctions.Length);
            mixFunctions[functionIndex](cells);
        }
    }

    private void SwapRandomRows(Cell[,] cells)
    {
        int area = random.Next(0, 3);
        int row1 = random.Next(0, 3), row2;
        do { row2 = random.Next(0, 3);
        } while (row2 != row1);
        SwapSelectedRows(row1 + area, row2 + area, cells);
    }

    private void SwapRandomCols(Cell[,] cells)
    {
        int area = random.Next(0, 3);
        int row1 = random.Next(0, 3), row2;
        do { row2 = random.Next(0, 3);
        } while (row2 != row1);
        SwapSelectedCols(row1 + area, row2 + area, cells);
    }

    private void TranspositionGrid(Cell[,] cells)
    {
        for (int i = 0; i < ROW; i++)
        {
            for (int j = 0; j < COL; j++)
            {
                Swap(cells[i, j].Value, cells[j, i].Value);
            }
        }
    }

    private void SwapSelectedRows(int n, int m, Cell[,] cells) 
    {
        for(int i = 0; i < COL; i++)
        {
            Swap(cells[n, i].Value, cells[m, i].Value);
        }
    }

    private void SwapSelectedCols(int n, int m, Cell[,] cells)
    {
        for (int i = 0; i < ROW; i++)
        {
            Swap(cells[i, n].Value, cells[i, m].Value);
        }
    }

    private void Swap(int a, int b)
    {
        int temp = a; a = b; b = temp;
    }
}