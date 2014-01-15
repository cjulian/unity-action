using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

	public GameObject player;
	public static float deadZoneSize = 0.2f;
	public InputDevice inputDevice = InputDevice.keyboardAndMouse;

	private static Vector2 moveAxis = Vector2.zero;
	private static Vector2 aimAxis = Vector2.zero;
	private delegate void InputReaderDelegate();
	private InputReaderDelegate getInput;

	// Use this for initialization
	void Awake () {
		SetInputDevice(inputDevice);

		if (inputDevice == InputDevice.joystick) {
			getInput = GetInputDeadzone;
		} else {
			getInput = GetInputRaw;
		}
	}
	
	// Read and store the move and aim axes values
	void Update () {
		getInput();
	}

	// Read mouse and keyboard input
	void GetInputRaw() {
		moveAxis = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
		aimAxis = new Vector2(Input.GetAxisRaw("AimHorizontal"), Input.GetAxisRaw("AimVertical"));
	}

	// Read joystick input
	void GetInputDeadzone() {
		moveAxis = new Vector2(Input.GetAxisRaw("JoyHorizontal"), Input.GetAxisRaw("JoyVertical"));
		if (moveAxis.magnitude > deadZoneSize) {
			moveAxis = (moveAxis / moveAxis.magnitude) * ((moveAxis.magnitude - deadZoneSize) / (1 - deadZoneSize));
		} else {
			moveAxis = Vector2.zero;
		}
		
		aimAxis = new Vector2(Input.GetAxisRaw("AimHorizontal"), Input.GetAxisRaw("AimVertical"));
		if (aimAxis.magnitude > deadZoneSize) {
			aimAxis = (aimAxis / aimAxis.magnitude) * ((aimAxis.magnitude - deadZoneSize) / (1 - deadZoneSize));
		} else {
			aimAxis = Vector2.zero;
		}
	}

	// Return the Horizontal and Vertical axes input in a Vector2
	public static Vector2 GetMoveAxisVector() {
		return moveAxis;
	}

	// Return the AimHorizontal and AimVertical axes input in a Vector2
	public static Vector2 GetAimAxisVector() {
		return aimAxis;
	}

	// Set the input device, mouse/keyboard or joystick
	public void SetInputDevice(InputDevice device) {
		MouseLook2D mouselookScript = player.GetComponent<MouseLook2D>();
		JoystickLook2D joystickScript = player.GetComponent<JoystickLook2D>();

		if(mouselookScript && joystickScript) {
			switch(device) {
			case InputDevice.keyboardAndMouse:
				mouselookScript.enabled = true;
				joystickScript.enabled = false;
				break;
				
			case InputDevice.joystick:
				mouselookScript.enabled = false;
				joystickScript.enabled = true;
				break;
			}
		}
	}
}

public enum InputDevice {
	keyboardAndMouse,
	joystick
}
