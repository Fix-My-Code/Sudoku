using Meta.Cell;
using Debug = UnityEngine.Debug;

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
    public static int BOX_SIZE = 3;

    private System.Random random = new System.Random();

    public Cell[,] DrawGrid()
    {
        var cells = new Cell[GRID_SIZE, GRID_SIZE];
        CreateGrid(cells);

        int amountGeneration = 0;

        do {
            GenerateGrid(cells);
            amountGeneration++;
        } while (!IsSingleSolution(cells) && ResetZero(cells));

        Debug.Log("Количество генераций: " + amountGeneration);

        //SudokuCleanCell.CleanRandomCell(cells);    

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

    private bool ResetZero(Cell[,] cells)
    {
        for (var row = 0; row < GRID_SIZE; row++)
        {
            for (var col = 0; col < GRID_SIZE; col++)
            {
                cells[row, col].Value = 0;
            }
        }
        return true;
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

    #region SingleSolution

    private bool IsSingleSolution(Cell[,] cells, bool debugLog = false)
    {
        for (int Box = 0; Box < GRID_SIZE - BOX_SIZE; Box += BOX_SIZE)
        {
            int[] index = { Box, Box + 1, Box + 2 };

            if (!IsSingleSolutionVerticalBox(cells, index[0], index[1], debugLog) ||
                !IsSingleSolutionVerticalBox(cells, index[0], index[2], debugLog) ||
                !IsSingleSolutionVerticalBox(cells, index[1], index[2], debugLog))
            {
                return false;
            }

            if (!IsSingleSolutionHorizoncalBox(cells, index[0], index[1], debugLog) ||
                !IsSingleSolutionHorizoncalBox(cells, index[0], index[2], debugLog) ||
                !IsSingleSolutionHorizoncalBox(cells, index[1], index[2], debugLog))
            {
                return false;
            }
        }

        return true;
    }

    private bool IsSingleSolutionVerticalBox(Cell[,] cells, int row1, int row2, bool debugLog)
    {
        for (var Box = 0; Box < GRID_SIZE; Box += BOX_SIZE)
        {
            for (var colFB = Box; colFB < Box + BOX_SIZE; colFB++)
            {
                var cell1 = cells[row1, colFB];
                var cell2 = cells[row2, colFB];

                for (var colNB = colFB + BOX_SIZE; colNB < GRID_SIZE; colNB++)
                {
                    var cell3 = cells[row1, colNB];
                    var cell4 = cells[row2, colNB];

                    if (cell1.Value == cell4.Value && cell2.Value == cell3.Value)
                    {
                        if (debugLog)
                        {
                            Debug.Log(cell1.x + "" + cell1.y + " " + cell1.Value);
                            Debug.Log(cell2.x + "" + cell2.y + " " + cell2.Value);
                            Debug.Log(cell3.x + "" + cell3.y + " " + cell3.Value);
                            Debug.Log(cell4.x + "" + cell4.y + " " + cell4.Value);
                        }

                        return false;
                    }
                }
            }
        }

        return true;
    }

    private bool IsSingleSolutionHorizoncalBox(Cell[,] cells, int col1, int col2, bool debugLog)
    {
        for (var Box = 0; Box < GRID_SIZE; Box += BOX_SIZE)
        {
            for (var rowFB = Box; rowFB < Box + BOX_SIZE; rowFB++)
            {
                var cell1 = cells[rowFB, col1];
                var cell2 = cells[rowFB, col2];

                for (var rowNB = rowFB + BOX_SIZE; rowNB < GRID_SIZE; rowNB++)
                {
                    var cell3 = cells[rowNB, col1];
                    var cell4 = cells[rowNB, col2];

                    if (cell1.Value == cell4.Value && cell2.Value == cell3.Value)
                    {
                        if (debugLog)
                        {
                            Debug.Log(cell1.x + "" + cell1.y + " " + cell1.Value);
                            Debug.Log(cell2.x + "" + cell2.y + " " + cell2.Value);
                            Debug.Log(cell3.x + "" + cell3.y + " " + cell3.Value);
                            Debug.Log(cell4.x + "" + cell4.y + " " + cell4.Value);
                        }

                        return false;
                    }
                }
            }
        }

        return true;
    }

    #endregion
}