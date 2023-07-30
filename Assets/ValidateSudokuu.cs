using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ValidateSudokuu : MonoBehaviour
{
    [SerializeField]
    private SudokuWizard sudokuWizard;

    [SerializeField]
    private Button button;

    void Start()
    {
        button.onClick.AddListener(sudokuWizard.ValidateSudoku);
    }
}
