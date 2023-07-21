
using TMPro;
using UnityEngine;

public class SudokuBoardView : MonoBehaviour
{
    public GameObject cellPrefab;
    public Transform boardParent;

    private GameObject[,] cells;

    public void FillGrid(Grid grid)
    {
        cells = new GameObject[9, 9];

        for (int row = 0; row < 9; row++)
        {
            for (int col = 0; col < 9; col++)
            {
                GameObject cell = Instantiate(cellPrefab, boardParent);
                var cellText = cell.GetComponentInChildren<TextMeshProUGUI>();

                int num = grid.BoardCells[row,col].Value;

                cellText.text = (num == 0) ? "" : num.ToString();

                cells[row, col] = cell;
            }
        }
    }
}
