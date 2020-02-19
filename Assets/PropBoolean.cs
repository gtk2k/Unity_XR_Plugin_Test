using UnityEngine;
using UnityEngine.UI;

public class PropBoolean : MonoBehaviour, IProp
{
    public Text txtVal;

    public void Value(object val)
    {
        txtVal.text = val.ToString();
    }
}
