using UnityEngine;
using System.Collections;

public class JoystickLook2D : MonoBehaviour {

	// The object that will be rotating based on joystick input.
	public GameObject obj;

	// Rotation speed, based on the time it should take to turn 180 degrees.
	public float turnTime180 = 0.3f;

	// If useAimAxesOnly is true then the object will continue pointing in the 
	// most recently input direction when aim axes input is zero.
	// If false then the object will use the Horizontal/Vertical axes
	// as aim input when aim axes input is zero.
	public bool useAimAxesOnly = true;

	// The last direction input
	private Quaternion lastRotation = Quaternion.identity;


	void Start () {
		// if no object assigned, then use this game object
		if (!obj) {	obj = this.gameObject; }

		// use the objects scene rotation to start
		lastRotation = obj.transform.rotation;
	}
	

	void Update () {
		// Get right stick input
		Vector2 aimInput = InputManager.GetAimAxisVector();
		float inputX = aimInput.x;
		float inputY = aimInput.y;

		bool useLastRotation = false;

		// If aim axes input is zero...
		if (inputX == 0 && inputY == 0) {

			if (useAimAxesOnly) {
				// then use the last rotation...
				useLastRotation = true;

			} else {
				// or use the horizontal/vertical axes input if allowed.
				aimInput = InputManager.GetMoveAxisVector();
				inputX = aimInput.x;
				inputY = aimInput.y;

				// If there is no input at all, use the last rotation.
				if (inputX == 0 && inputY == 0) {
					useLastRotation = true;
				}
			}
		}

		// get rotation based on input or use last rotation
		Quaternion targetRotation;

		if (!useLastRotation || lastRotation == Quaternion.identity) {
			if (inputX == 0 && inputY == 0) {
				targetRotation = Quaternion.identity;
			} else {
				targetRotation = Quaternion.LookRotation(new Vector3(inputX, 0.0f, inputY), Vector3.up);
			}
		} else {
			targetRotation = lastRotation;
		}

		lastRotation = targetRotation;

		// rotate the object gradually based on time required to turn 180 degrees and angle of target rotation
		float angle = Quaternion.Angle(obj.transform.rotation, targetRotation);
		if (angle > 0.1) {
			obj.transform.rotation = Quaternion.Slerp(obj.transform.rotation, targetRotation, (180/ angle) * (Time.deltaTime / turnTime180));
		}	
	}
}
