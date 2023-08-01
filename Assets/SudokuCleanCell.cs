using Meta.Cell;
using UnityEngine;

public class SudokuCleanCell : MonoBehaviour
{
    private static System.Random random = new System.Random();

    public static void CleanRandomCell(Cell[,] cells)
    {
        for (int i = 0; i < Globals.DIFFICULTY_BASE; i++)
        {
            int row = random.Next(0, 9);
            int col = random.Next(0, 9);

            if (cells[row, col].Value != 0)
            {
                cells[row, col].Value = 0;
                cells[row, col].IsActive = true;
                continue;
            }
            i--;
        }
    }
}