using System;
using UnityEngine;


public class SudokuWizard : MonoBehaviour
{
    [SerializeField]
    private SudokuBoardView _sudokuBoardView;

    public SudokuPresenter sudokuPresenter;

    [ContextMenu("Fill")]
    public void Start()
    {
        sudokuPresenter = new SudokuPresenter();
        _sudokuBoardView.FillGrid(sudokuPresenter.BoardGrid);
    }
}

public class SudokuPresenter
{
    public Grid BoardGrid => _boardGrid;
    private Grid _boardGrid;

    public SudokuPresenter()
    {
        _boardGrid = new Grid();
        _boardGrid.FillGrid();
    }

}

public class Grid
{
    public Cell[,] BoardCells => _cells;

    private Cell[,] _cells;

    public Cell[,] FillGrid()
    {
        _cells = new Cell[9,9];

        for(var row = 0; row < 9; row++)
        {
            for(var col = 0; col < 9; col++)
            {
                var value = UnityEngine.Random.Range(1, 10);
                _cells[row, col] = new Cell(row, col, value);
            }
        }
        
        return BoardCells;
    }

    public void FillGrid(Grid grid)
    {

    }
}

public class Cell
{
    public Action<int> onValueChanged;

    public int x, y;

    public int Value
    {
        get
        {
            return _value;
        }
        set
        {
            _value = value;
            onValueChanged?.Invoke(value);
        }
    }

    private int _value;

    public Cell(int x, int y, int value)
    {
        this.x = x;
        this.y = y;

        this._value = value;
    }

    public ope
}
