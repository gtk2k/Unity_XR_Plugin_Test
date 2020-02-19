using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class PropVector2 : MonoBehaviour, IProp
{
    public Text txtValX;
    public Text txtValY;

    public void Value(object obj)
    {
        var val = (Vector2)obj;
        txtValX.text = val.x.ToString("n3");
        txtValY.text = val.y.ToString("n3");
    }
}
