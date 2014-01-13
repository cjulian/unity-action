using UnityEngine;
using System.Collections;

public class JoystickLook2D_Old : MonoBehaviour {

	// The object that will be rotating to look at the mouse pointer.
	public GameObject obj;

	// The speed of rotation.  Higher numbers means faster rotation.
	public float rotationSpeed = 4;
	
	// The angle that the object is facing in when the script starts.
	// 360 > objStartAngle >= 0
	// 0 corresponds to facing down the positive X axis.
	public float objStartAngle = 0;


	// If true then last aim direction is used when no direction input detected
	// If false, movement direction is used as aim direction when no input detected
	public bool preserveDirection = true;

	// used for tracking input across frame updates
	private float prevInputX = 0;
	private float prevInputY = 0;
	private Quaternion targetRotation;
	private bool targetRotationSet = false;


	void Start () {
		if(!obj) {
			obj = this.gameObject;
		}

		objStartAngle %= 360;
		getCoordinatesFromAngle(objStartAngle, out prevInputX, out prevInputY);
	}
	

	void Update () {
		bool usePrevRotation = false;

		// Get right stick input
		float inputX = Input.GetAxisRaw("AimHorizontal");
		float inputY = Input.GetAxisRaw("AimVertical");

		// Check if right stick position has no input
		if(inputX == 0 && inputY == 0) {

			// Use the left stick input if we're not preserving direction
			if(!preserveDirection) {
				inputX = Input.GetAxisRaw("Horizontal");
				inputY = Input.GetAxisRaw("Vertical");
			}

			// If preserving direction or no input detected
			// then use the previous frame's input
			if(preserveDirection || (inputX == 0 && inputY == 0)) {
				inputX = prevInputX;
				inputY = prevInputY;
				usePrevRotation = true;
			}
		}

		prevInputX = inputX;
		prevInputY = inputY;
		float angle = 0;

		// calculate new target angle if no target has been set or if
		// we don't want to use the previous rotation
		if(!usePrevRotation || !targetRotationSet) {
			angle = (Mathf.Atan2(inputY, inputX) * Mathf.Rad2Deg) - objStartAngle;
			targetRotation = Quaternion.Euler(new Vector3(0, -angle, 0));
			targetRotationSet = true;
		}

		angle = (Quaternion.Angle(obj.transform.rotation, targetRotation));

		if(angle > 0.1) {
			obj.transform.rotation = Quaternion.Slerp(obj.transform.rotation, targetRotation, rotationSpeed * (180 / angle) * Time.deltaTime);
		}
	}


	// Given an angle, sets x and y to values corresponding to that angle.
	// eg. for angle = 0, x = 1, y = 0
	void getCoordinatesFromAngle(float angle, out float x, out float y) {
		if(angle == 90){
			x = 0;
			y = 1;
		} else if(angle == 270){
			x = 0;
			y = -1;
		} else {
			y = Mathf.Tan(angle * (Mathf.Deg2Rad));

			if (y >= 0) {
				if (angle < 90) {
					x = 1;
				} else {
					x = -1;
				}
			} else {
				if (angle > 270) {
					x = 1;
				} else {
					x = -1;
					y *= -1;
				}
			}
		}	
	}
}
