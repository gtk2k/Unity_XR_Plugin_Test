using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class XRInputDevice : MonoBehaviour
{
    public GameObject DevicePanelPrefab;
    public Transform Container;

    private readonly HashSet<InputDevice> devices = new HashSet<InputDevice>();
    private readonly Dictionary<InputDevice, XRDevicePanel> panels = new Dictionary<InputDevice, XRDevicePanel>();

    void OnEnable()
    {
        InputDevices.deviceConnected += OnDeviceConnected;
        InputDevices.deviceDisconnected += OnDeviceDisconnected;
        Application.onBeforeRender += OnBeforeRender;
        discoverConnectedDevices();
    }

    void OnDisable()
    {
        InputDevices.deviceConnected -= OnDeviceConnected;
        InputDevices.deviceConnected -= OnDeviceDisconnected;
        Application.onBeforeRender -= OnBeforeRender;
    }

    void OnDeviceConnected(InputDevice device)
    {
        addDevice(device);
    }

    void OnDeviceDisconnected(InputDevice device)
    {
        removeDevice(device);
    }

    void OnBeforeRender()
    {
        discoverConnectedDevices();
    }

    void discoverConnectedDevices()
    {
        var xrNodes = Enum.GetValues(typeof(XRNode));
        foreach (XRNode xrNode in xrNodes)
        {
            var device = InputDevices.GetDeviceAtXRNode(xrNode);
            if (device.isValid)
                addDevice(device);
        }
    }

    void addDevice(InputDevice device)
    {
        if (devices.Contains(device)) return;
        devices.Add(device);
        var go = Instantiate(DevicePanelPrefab, Vector3.zero, Quaternion.identity, Container);
        go.transform.position = go.transform.localPosition = Vector3.zero;
        var scale = go.transform.localScale;
        scale.x = 0.5f;
        scale.y = 0.5f;
        go.transform.localScale = scale;
        var rt = go.GetComponent<RectTransform>();
        var panel = go.GetComponent<XRDevicePanel>();
        panel.Device = device;
        panels.Add(device, panel);
    }

    void removeDevice(InputDevice device)
    {
        if (devices.Contains(device))
            devices.Remove(device);
        if (panels.ContainsKey(device))
        {
            Destroy(panels[device].gameObject);
            panels.Remove(device);
        }
    }
}

