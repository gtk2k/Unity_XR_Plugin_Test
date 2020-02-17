using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class XRInputDevice : MonoBehaviour
{
    //Keep this around to avoid creating heap garbage
    static List<InputDevice> devices = new List<InputDevice>();
    InputDevice trackedDevice;

    InputDevice xrHMD;
    InputDevice leftXRController;
    InputDevice rightXRController;

    [SerializeField]
    XRHMDPanel xrHMDPanel;
    [SerializeField]
    XRControllerPanel leftXRControllerPanel;
    [SerializeField]
    XRControllerPanel rightXRControllerPanel;

    void OnEnable()
    {
        InputDevices.deviceConnected += OnDeviceConnected;
        InputDevices.deviceDisconnected += OnDeviceDisconnected;
        Application.onBeforeRender += OnBeforeRender;

        xrHMD = InputDevices.GetDeviceAtXRNode(XRNode.Head);
        leftXRController = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
        rightXRController = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
    }

    void OnDisable()
    {
        InputDevices.deviceConnected -= OnDeviceConnected;
        InputDevices.deviceConnected -= OnDeviceDisconnected;
        Application.onBeforeRender -= OnBeforeRender;
    }

    void Update()
    {
        if (leftXRController.isValid)
            TrackToDevice(leftXRController, leftXRControllerPanel);
        if (rightXRController.isValid)
            TrackToDevice(rightXRController, rightXRControllerPanel);
    }

    void OnDeviceConnected(InputDevice device)
    {
        if (!xrHMD.isValid)
        {
            if (device.name == "Oculus Rift S")
            {
                xrHMD = device;
                xrHMDPanel.DeviceName(xrHMD.name);
            }
        }
        if (!leftXRController.isValid)
        {
            if (device.name == "Oculus Touch Controller - Left")
            {
                leftXRController = device;
                leftXRControllerPanel.DeviceName(leftXRController.name);
            }
        }
        if (!rightXRController.isValid)
        {
            if (device.name == "Oculus Touch Controller - Right")
            {
                rightXRController = device;
                rightXRControllerPanel.DeviceName(rightXRController.name);
            }
        }
    }

    void OnDeviceDisconnected(InputDevice device)
    {
        if (device == xrHMD)
            xrHMD = new InputDevice();
        if (device == leftXRController)
            leftXRController = new InputDevice();
        if (device == rightXRController)
            rightXRController = new InputDevice();
    }

    void OnBeforeRender()
    {
        if (xrHMD.isValid)
            TrackToDevice(xrHMD, xrHMDPanel);
        if (leftXRController.isValid)
            TrackToDevice(leftXRController, leftXRControllerPanel);
        if (rightXRController.isValid)
            TrackToDevice(rightXRController, rightXRControllerPanel);
    }

    void TrackToDevice(InputDevice trackedDevice, XRHMDPanel xrControllerPanel)
    {
        var position = Vector3.zero;
        var rotation = Quaternion.identity;

        if (trackedDevice.TryGetFeatureValue(CommonUsages.devicePosition, out position))
            xrHMDPanel.Position(position);

        if(trackedDevice.TryGetFeatureValue(CommonUsages.deviceRotation, out rotation))
            xrHMDPanel.Rotation(rotation.eulerAngles);
    }

    void TrackToDevice(InputDevice trackedDevice, XRControllerPanel xrControllerPanel)
    {
        var position = Vector3.zero;
        var rotation = Quaternion.identity;
        var primaryButton = false;
        var primaryTouch = false;
        var secondaryButton = false;
        var secondaryTouch = false;
        var grip = 0f;
        var gripButton = false;
        var trigger = 0f;
        var triggerButton = false;
        var menuButton = false;
        var primary2DAxisClick = false;
        var primary2DAxisTouch = false;
        var primary2DAxis = Vector2.zero;

        if (trackedDevice.TryGetFeatureValue(CommonUsages.devicePosition, out position))
            xrControllerPanel.Position(position);

        if (trackedDevice.TryGetFeatureValue(CommonUsages.deviceRotation, out rotation))
            xrControllerPanel.Rotation(rotation.eulerAngles);

        if (trackedDevice.TryGetFeatureValue(CommonUsages.primaryButton, out primaryButton))
            xrControllerPanel.PrimaryButton(primaryButton, primaryTouch);
        if (trackedDevice.TryGetFeatureValue(CommonUsages.primaryTouch, out primaryTouch))
            xrControllerPanel.PrimaryButton(primaryButton, primaryTouch);

        if (trackedDevice.TryGetFeatureValue(CommonUsages.secondaryButton, out secondaryButton))
            xrControllerPanel.SecondaryButton(secondaryButton, secondaryTouch);
        if (trackedDevice.TryGetFeatureValue(CommonUsages.secondaryTouch, out secondaryTouch))
            xrControllerPanel.SecondaryButton(secondaryButton, secondaryTouch);

        if (trackedDevice.TryGetFeatureValue(CommonUsages.grip, out grip))
            xrControllerPanel.Grip(grip, gripButton);

        if (trackedDevice.TryGetFeatureValue(CommonUsages.gripButton, out gripButton))
            xrControllerPanel.Grip(grip, gripButton);

        if (trackedDevice.TryGetFeatureValue(CommonUsages.trigger, out trigger))
            xrControllerPanel.Trigger(trigger, triggerButton);

        if (trackedDevice.TryGetFeatureValue(CommonUsages.triggerButton, out triggerButton))
            xrControllerPanel.Trigger(trigger, triggerButton);

        if (trackedDevice.TryGetFeatureValue(CommonUsages.menuButton, out menuButton))
            xrControllerPanel.MenuButton(menuButton);

        if (trackedDevice.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out primary2DAxisClick))
            xrControllerPanel.Primary2DAxis(primary2DAxisClick, primary2DAxisTouch, primary2DAxis);
        if (trackedDevice.TryGetFeatureValue(CommonUsages.primary2DAxisTouch, out primary2DAxisTouch))
            xrControllerPanel.Primary2DAxis(primary2DAxisClick, primary2DAxisTouch, primary2DAxis);
        if (trackedDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out primary2DAxis))
            xrControllerPanel.Primary2DAxis(primary2DAxisClick, primary2DAxisTouch, primary2DAxis);
    }
}

