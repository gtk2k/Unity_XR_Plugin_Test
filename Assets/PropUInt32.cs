using System;
using UnityEngine;
using UnityEngine.UI;

public class PropUInt32 : MonoBehaviour, IProp
{
    public Text txtVal;

    public void Value(object arg)
    {
        txtVal.text = arg.ToString();
    }
}
