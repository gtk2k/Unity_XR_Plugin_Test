using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class Prop : MonoBehaviour, IProp
{
    public Text txtVal;

    public void Value(object arg) {
        txtVal.text = (string)arg;
    }
}