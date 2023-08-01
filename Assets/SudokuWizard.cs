using UnityEngine;
using System.Collections.Generic;
using Meta.Cell;

public class SudokuWizard : MonoBehaviour
{
    [SerializeField]
    private SudokuBoardView _sudokuBoardView;

    [SerializeField]
    public Grid Grid;

    public SudokuPresenter sudokuPresenter;

    public void Awake()
    {
        sudokuPresenter = new SudokuPresenter();
        FillBase();
    }

    public void Fill(Grid grid)
    {
        sudokuPresenter.BoardGrid = grid;
        sudokuPresenter.CellsView = _sudokuBoardView.FillGrid(grid);
    }

    [ContextMenu("Fill")]
    public void FillBase()
    {
        var generator = new SudokuGenerator();
        var grid = new Grid(generator.DrawGrid());
        sudokuPresenter.BoardGrid = grid;
        sudokuPresenter.CellsView = _sudokuBoardView.FillGrid(grid);
    }

    [ContextMenu("Validate")]
    public void ValidateSudoku()
    {
        foreach (var cell in sudokuPresenter.BoardGrid.BoardCells)
        {
            if(!SudokuValidator.ValidateCell(sudokuPresenter.BoardGrid.BoardCells, cell))
            {
                return;
            }
        }
        FillBase();
    }

    [ContextMenu("ImportAndFill")]
    public void Import()
    {
        var a = Serializer.DeserializeFromFile<Grid>("SudokuPreset.txt");
        
        sudokuPresenter.BoardGrid = a;
        _sudokuBoardView.FillGrid(a);
    }

    [ContextMenu("Export")]
    public void Export()
    {
        Serializer.Serialize(sudokuPresenter.BoardGrid, "SudokuPreset.txt");
    }
}

public class SudokuPresenter
{
    public Grid BoardGrid
    {
        get => _boardGrid;
        set => _boardGrid = value;
    }

    public List<CellPresenter> CellsView;

    private Grid _boardGrid;

    public SudokuPresenter()
    {

    }
}