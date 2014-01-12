using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour
{
	public Vector3 gravity;
	public GameObject player;

	void Awake() 
	{
		if (gravity.magnitude != 0)
		{
			Physics.gravity = gravity;
		}
	}

	void Start ()
	{
	}
	
	void Update ()
	{
	}

	//	GameObject.FindObjectOfType<Game>().SetInputDevice(InputDevice.keyboardAndMouse);
	public void SetInputDevice(InputDevice device) {
		Debug.Log("SetInputDevice");
		MouseLook2D mouselookScript = player.GetComponent<MouseLook2D>();
		JoystickLook2D joystickScript = player.GetComponent<JoystickLook2D>();

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

	public void DoSomething() {
		Debug.Log ("Did it!");
	}
}

public enum InputDevice {
	keyboardAndMouse,
	joystick
}
