using Meta.Cell;
using UnityEngine;

public class SudokuValidator : MonoBehaviour
{
    private const int BOX_SIZE = 3;
    private const int GRID_SIZE = 9;

    public static bool ValidateCell(Cell[,] cells, Cell cell)
    {
        return ValidateCol(cells, cell)
            && ValidateRow(cells, cell)
            && ValidateBox(cells, cell);
    }

    private static bool ValidateCol(Cell[,] cells, Cell cell)
    {
        for (int i = 0; i < GRID_SIZE; i++)
        {
            if (cells[i, cell.y].Value == cell.Value && i != cell.x)
            {
                return false;
            }
        }
        return true;
    }

    private static bool ValidateRow(Cell[,] cells, Cell cell)
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

    private static bool ValidateBox(Cell[,] cells, Cell cell)
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
}