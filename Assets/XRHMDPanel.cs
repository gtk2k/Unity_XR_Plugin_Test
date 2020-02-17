using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XRHMDPanel : MonoBehaviour
{
    [SerializeField]
    private Text txtDeviceName;
    [SerializeField]
    private Text txtPositionX;
    [SerializeField]
    private Text txtPositionY;
    [SerializeField]
    private Text txtPositionZ;
    [SerializeField]
    private Text txtRotationX;
    [SerializeField]
    private Text txtRotationY;
    [SerializeField]
    private Text txtRotationZ;

    public void DeviceName(string deviceName)
    {
        txtDeviceName.text = deviceName;
    }

    public void Position(Vector3 position)
    {
        txtPositionX.text = position.x.ToString("n3");
        txtPositionY.text = position.y.ToString("n3");
        txtPositionZ.text = position.z.ToString("n3");
    }

    public void Rotation(Vector3 rotation)
    {
        txtRotationX.text = rotation.x.ToString("n3");
        txtRotationY.text = rotation.y.ToString("n3");
        txtRotationZ.text = rotation.z.ToString("n3");
    }
}
