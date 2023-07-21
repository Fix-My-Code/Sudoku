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
        var generator = new SudokuGenerator();
        var sds = generator.DrawBaseGrid();
        _sudokuBoardView.FillGrid(new Grid(sds));
    }
}

public class SudokuPresenter
{
    public Grid BoardGrid => _boardGrid;
    private Grid _boardGrid;

    public SudokuPresenter()
    {
        var generator = new SudokuGenerator();
        var grid = generator.DrawBaseGrid();
    }
}

public class Grid
{
    public Cell[,] BoardCells => _cells;

    private Cell[,] _cells;

    public Grid(Cell[,] cells)
    {
        _cells = cells;
    }

    public Cell[,] FillGrid()
    {
        _cells = new Cell[9, 9];

        for (var row = 0; row < 9; row++)
        {
            for (var col = 0; col < 9; col++)
            {
                var value = UnityEngine.Random.Range(1, 10);
                _cells[row, col] = new Cell(row, col, value);
            }
        }

        return BoardCells;
    }

    public Grid FillGrid(Cell[,] cells)
    {
        return new Grid(cells);
    }
}
