using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Create/ColorConfig")]
public class ColorConfig : ScriptableObject
{
    [Header("Active")]
    public Color activeBackground;
    public Color activeText;

    [Header("Selectable")]
    public Color selectableBackground;
    public Color selectableText;

    [Header("Based")]
    public Color basedBackground;
    public Color basedText;

    [Header("SameValue")]
    public Color sameBackground;
    public Color sameText;

    [Header("Area")]
    public Color areaColor;

    [Header("Error")]
    public Color errorColor;
}
