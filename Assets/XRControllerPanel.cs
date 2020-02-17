using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XRControllerPanel : MonoBehaviour
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
	[SerializeField]
	private Text txtPrimaryButton;
	[SerializeField]
	private Text txtSecondaryButton;
	[SerializeField]
	private Text txtTrigger;
	[SerializeField]
	private Text txtGrip;
	[SerializeField]
	private Text txtMenuButton;
	[SerializeField]
	private Text txtPrimary2DAxis;
	[SerializeField]
	private Text txtPrimary2DAxisX;
	[SerializeField]
	private Text txtPrimary2DAxisY;

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

	public void PrimaryButton(bool isPressed, bool isTouched)
	{
		if (isPressed)
			txtPrimaryButton.text = "Press";
		else if (isTouched)
			txtPrimaryButton.text = "Touch";
		else
			txtPrimaryButton.text = "";
	}

	public void SecondaryButton(bool isPressed, bool isTouched)
	{
		if (isPressed)
			txtSecondaryButton.text = "Press";
		else if (isTouched)
			txtSecondaryButton.text = "Touch";
		else
			txtSecondaryButton.text = "";
	}

	public void Grip(float val, bool isTouched)
	{
		txtGrip.text = val.ToString("n3");
		// TODO isTouched
	}

	public void Trigger(float val, bool isTouched)
	{
		txtTrigger.text = val.ToString("n3");
		// TODO isTouched
	}

	public void MenuButton(bool isPressed)
	{
		if (isPressed)
			txtMenuButton.text = "Press";
		else
			txtMenuButton.text = "";
	}

	public void Primary2DAxis(bool isClick, bool isTouched, Vector2 axis)
	{
		if (isClick)
			txtPrimary2DAxis.text = "Click";
		else if (isTouched)
			txtPrimary2DAxis.text = "Touch";
		txtPrimary2DAxisX.text = axis.x.ToString("n3");
		txtPrimary2DAxisY.text = axis.y.ToString("n3");
	}
}
