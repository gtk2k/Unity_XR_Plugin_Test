using UnityEngine;
using UnityEngine.UI;

public class PropSingle : MonoBehaviour, IProp
{
    public Text txtVal;
    public int decimalPlace = 3;

    public void Value(object arg)
    {
        txtVal.text = ((float)arg).ToString($"n{decimalPlace}");
    }
}
