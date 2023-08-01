using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenCell : MonoBehaviour
{
    public int Value;
    private bool _isActive;

    public void Activate(bool state)
    {
        _isActive = state;
        gameObject.SetActive(_isActive);
    }
}
