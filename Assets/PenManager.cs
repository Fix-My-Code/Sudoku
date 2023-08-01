using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PenManager : MonoBehaviour
{
    [SerializeField]
    public TextMeshProUGUI _text;

    public void ActiveView()
    {
        _text.color = Globals.isPenEnabled ? Color.green : Color.white;
    }
}
