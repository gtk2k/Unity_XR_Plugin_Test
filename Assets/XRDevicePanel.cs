using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class XRDevicePanel : MonoBehaviour
{
    public Text txtDeviceName;
    public Transform rowCharacteristics;
    public Transform colLabel;
    public Transform colValue;
    public Text prefabLabel;
    public Text prefabCharacteristics;
    public GameObject prefabSingle;
    public GameObject prefabBoolean;
    public GameObject prefabUInt32;
    public GameObject prefabVector2;
    public GameObject prefabVector3;
    public GameObject prefabQuaternion;

    private readonly Dictionary<InputFeatureUsage, IProp> props = new Dictionary<InputFeatureUsage, IProp>();
    private InputDevice device;

    public InputDevice Device
    {
        set
        {
            var featureUsages = new List<InputFeatureUsage>();
            if (!value.TryGetFeatureUsages(featureUsages)) return;
            device = value;
            setCharacteristics(device.characteristics);
            txtDeviceName.text = value.name;
            clear();
            var rt = GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(1000, 140 + (featureUsages.Count * 65));
            foreach (var usage in featureUsages)
            {
                var lbl = Instantiate(prefabLabel, colLabel);
                lbl.text = usage.name;
                Debug.Log(usage.type.Name);
                Type t = null;  
                string pn = null;
                FieldInfo f = null;
                object v = null;
                try
                {
                    t = GetType();
                    pn = $"prefab{usage.type.Name}";
                    f = t.GetField(pn);
                    v = f.GetValue(this);
                    var go = Instantiate((GameObject)GetType().GetField($"prefab{usage.type.Name}").GetValue(this), colValue);
                    var prop = (IProp)go.GetComponent($"Prop{usage.type.Name}");
                    props.Add(usage, prop);
                }
                catch (Exception ex)
                {
                    Debug.LogError(ex);
                }
            }
        }
    }

    private void setCharacteristics(InputDeviceCharacteristics characteristics)
    {
        foreach (InputDeviceCharacteristics flg in Enum.GetValues(typeof(InputDeviceCharacteristics)))
            if (flg != InputDeviceCharacteristics.None && characteristics.HasFlag(flg))
            {
                var txt = Instantiate(prefabCharacteristics, rowCharacteristics);
                txt.text = Enum.GetName(typeof(InputDeviceCharacteristics), flg);
            }
    }

    private void clear()
    {
        foreach (Transform target in colLabel.transform)
            Destroy(target.gameObject);
        foreach (Transform target in colValue.transform)
            Destroy(target.gameObject);
    }

    private void Update()
    {
        foreach (var kvp in props)
        {
            if (kvp.Key.type == typeof(bool))
            {
                if (device.TryGetFeatureValue(kvp.Key.As<bool>(), out bool val))
                    kvp.Value.Value(val);
            }
            else if (kvp.Key.type == typeof(uint))
            {
                if (device.TryGetFeatureValue(kvp.Key.As<uint>(), out uint val))
                    kvp.Value.Value(val);
            }
            else if (kvp.Key.type == typeof(float))
            {
                if (device.TryGetFeatureValue(kvp.Key.As<float>(), out float val))
                    kvp.Value.Value(val);
            }
            else if (kvp.Key.type == typeof(Vector2))
            {
                if (device.TryGetFeatureValue(kvp.Key.As<Vector2>(), out Vector2 val))
                    kvp.Value.Value(val);
            }
            else if (kvp.Key.type == typeof(Vector3))
            {
                if (device.TryGetFeatureValue(kvp.Key.As<Vector3>(), out Vector3 val))
                    kvp.Value.Value(val);
            }
            else if (kvp.Key.type == typeof(Quaternion))
            {
                if (device.TryGetFeatureValue(kvp.Key.As<Quaternion>(), out Quaternion val))
                    kvp.Value.Value(val);
            }
        }
    }
}
