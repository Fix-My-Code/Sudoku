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
        var generator = new SudokuGenerator();
        var grid = generator.DrawBaseGrid();
        _boardGrid = new Grid(grid);
    }
}
