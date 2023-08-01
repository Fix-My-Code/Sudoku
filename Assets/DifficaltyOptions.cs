using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DifficaltyOptions : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField _inputField;

    [SerializeField]
    private SudokuWizard _sudokuWizard;

    public void GenerateNew()
    {
        Globals.DIFFICULTY_BASE = int.Parse(_inputField.text);
        _sudokuWizard.FillBase();
    }
}
