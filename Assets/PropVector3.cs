using UnityEngine;
using UnityEngine.UI;

public class PropVector3 : MonoBehaviour, IProp
{
    public Text txtValX;
    public Text txtValY;
    public Text txtValZ;

    public void Value(object arg)
    {
        var val = (Vector3)arg;
        txtValX.text = val.x.ToString("n3");
        txtValY.text = val.y.ToString("n3");
        txtValZ.text = val.y.ToString("n3");
    }
}
