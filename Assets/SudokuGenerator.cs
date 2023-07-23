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

    private Random random = new Random();

    public Cell[,] DrawGrid()
    {
        var cells = new Cell[GRID_SIZE, GRID_SIZE];

        CreateGrid(cells);     
        GenerateGrid(cells);
        SudokuCleanCell.CleanRandomCell(cells);    

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

            if (!SudokuValidator.ValidateCell(cells, emptyCell))
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
}