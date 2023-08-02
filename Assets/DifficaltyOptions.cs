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

        if(Globals.DIFFICULTY_BASE < 0 || Globals.DIFFICULTY_BASE > 80) 
        {
            return;
        }

        _sudokuWizard.FillBase();
    }
}
