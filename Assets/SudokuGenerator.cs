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
    private const int GRID_SIZE = 9;
    private const int BOX_SIZE = 3;

    private const int DIFFICULTY_BASE = 30;

    private Random random = new Random();

    public Cell[,] DrawGrid()
    {
        var cells = new Cell[GRID_SIZE, GRID_SIZE];

        CreateGrid(cells);     
        GenerateGrid(cells);
        CleanRandomCell(cells, DIFFICULTY_BASE);
        //BlockAllFillCell(cells);
        
        //DrawBaseGrid(cells);
        //MixGrid(cells);       

        return cells;
    }

    private void CreateGrid(Cell[,] cells)
    {
        for (var row = 0; row < GRID_SIZE; row++)
        {
            for (var col = 0; col < GRID_SIZE; col++)
            {
                cells[row, col] = new Cell(row, col, 0, false);
            }
        }
    }

    private void CleanRandomCell(Cell[,] cells, int difficulty)
    {
        for(int i = 0; i < difficulty; i++)
        {
            int row = random.Next(0, 9);
            int col = random.Next(0, 9);

            if (cells[row, col].Value != 0)
            {
                cells[row, col].Value = 0;
                cells[row, col].IsActive = true;
            }
        }
    }

    private void BlockAllFillCell(Cell[,] cells)
    {
        for (var row = 0; row < GRID_SIZE; row++)
        {
            for (var col = 0; col < GRID_SIZE; col++)
            {
                if (cells[row, col].Value != 0)
                {
                    cells[row, col].IsActive = false;
                }
            }
        }
    }

    #region GenerateGrid

    private bool GenerateGrid(Cell[,] cells)
    {
        Cell emptyCell = FindEmptyCell(cells);

        if(emptyCell == null)
        {
            return true;
        }

        int[] mixedArray = GetMixedArray();
        for(int i = 0; i < mixedArray.Length; i++) 
        {
            emptyCell.Value = mixedArray[i];

            if (!ValidateCell(cells, emptyCell))
            {
                continue;
            }

            cells[emptyCell.x, emptyCell.y].Value = emptyCell.Value;

            if(GenerateGrid(cells))
            {
                return true;
            }

            cells[emptyCell.x, emptyCell.y].Value = 0;
        }

        return false;
    }

    private Cell FindEmptyCell(Cell[,] cells)
    {
        for (var row = 0; row < GRID_SIZE; row++)
        {
            for (var col = 0; col < GRID_SIZE; col++)
            {
                if (cells[row, col].Value == 0)
                {
                    return new Cell(cells[row, col]);
                }
            }
        }

        return null;
    }

    private int[] GetMixedArray()
    {
        int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        for(int i = numbers.Length - 1; i >= 0; i--)
        {
            int randomIndex = random.Next(0, numbers.Length);

            int temp = numbers[i];
            numbers[i] = numbers[randomIndex];
            numbers[randomIndex] = temp;
        }

        return numbers;
    }

    #endregion

    #region Validate

    private bool ValidateCell(Cell[,] cells, Cell cell)
    {
        return ValidateCol(cells, cell)
            && ValidateRow(cells, cell)
            && ValidateBox(cells, cell);
    }

    private bool ValidateCol(Cell[,] cells, Cell cell)
    {
        for(int i = 0; i < GRID_SIZE; i++)
        {
            if (cells[i, cell.y].Value == cell.Value && i != cell.x)
            {
                return false;
            }
        }
        return true;
    }

    private bool ValidateRow(Cell[,] cells, Cell cell)
    {
        for (int i = 0; i < GRID_SIZE; i++)
        {
            if (cells[cell.x, i].Value == cell.Value && i != cell.y)
            {
                return false;
            }
        }
        return true;
    }

    private bool ValidateBox(Cell[,] cells, Cell cell)
    {
        int xBox = (cell.x / BOX_SIZE) * BOX_SIZE;
        int yBox = (cell.y / BOX_SIZE) * BOX_SIZE;

        for (int i = xBox; i < xBox + BOX_SIZE; i++)
        {
            for (int j = yBox; j < yBox + BOX_SIZE; j++)
            {
                if (cells[i, j].Value == cell.Value && (i != cell.x && j != cell.y))
                {
                    return false;
                }
            }
        }
        return true;
    }

    #endregion

    #region MixGrid

    private void DrawBaseGrid(Cell[,] cells)
    {
        int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        const int shift = 3, shiftTransition = 4;

        Index = 0;
        for (int i = 0; i < GRID_SIZE; i++)
        {
            for (int j = 0; j < GRID_SIZE; j++)
            {
                Index++;
                cells[i, j].Value = nums[Index - 1];
            }

            if ((i + 1) % BOX_SIZE == 0)
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

        int numberMixes = random.Next(25, 45);
        for (int i = 0; i < numberMixes; i++)
        {
            int functionIndex = random.Next(0, mixFunctions.Length);
            mixFunctions[functionIndex](cells);
        }
    }

    private void SwapRandomCols(Cell[,] cells)
    {
        int area = random.Next(0, 3);
        int row1 = random.Next(1, 4), row2;
        do { row2 = random.Next(1, 4);
        } while (row2 == row1);
        SwapSelectedRows(row1 - 1 + area * BOX_SIZE, row2 - 1 + area * BOX_SIZE, cells);
    }

    private void SwapRandomRows(Cell[,] cells)
    {
        int area = random.Next(0, 3);
        int row1 = random.Next(1, 4), row2;
        do { row2 = random.Next(1, 4);
        } while (row2 == row1);
        SwapSelectedCols(row1 - 1 + area * BOX_SIZE, row2 - 1 + area * BOX_SIZE, cells);
    }

    private void TranspositionGrid(Cell[,] cells)
    {
        for (int i = 0; i < GRID_SIZE; i++)
        {
            for (int j = 0 + i; j < GRID_SIZE; j++)
            {
                Swap(ref cells[i, j], ref cells[j, i]);
            }
        }
    }

    private void SwapSelectedCols(int n, int m, Cell[,] cells) 
    {
        for(int i = 0; i < GRID_SIZE; i++)
        {
            Swap(ref cells[n, i], ref cells[m, i]);
        }
    }

    private void SwapSelectedRows(int n, int m, Cell[,] cells)
    {
        for (int i = 0; i < GRID_SIZE; i++)
        {
           Swap(ref cells[i, n], ref cells[i, m]);
        }
    }

    private void Swap(ref Cell a, ref Cell b)
    {
        var temp = a.Value;
        a.Value = b.Value;
        b.Value = temp;
    }

    #endregion MIXGRID
}