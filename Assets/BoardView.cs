using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BoardView : MonoBehaviour
{
    public GameObject cellPrefab;
    public Transform boardParent;
    public RectTransform cellRect;

    private GameObject[,] cells;

    [ContextMenu("Generate")]
    void Start()
    {
        GenerateBoard();
    }

    private void GenerateBoard()
    {
        cells = new GameObject[9, 9];

        for (int row = 0; row < 9; row++)
        {
            for (int col = 0; col < 9; col++)
            {
                GameObject cell = Instantiate(cellPrefab, boardParent);
                var cellText = cell.GetComponentInChildren<TextMeshProUGUI>();

                int num = 1;

                cellText.text = (num == 0) ? "" : num.ToString();

                cells[row, col] = cell;
            }
        }
    }
}
