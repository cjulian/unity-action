using UnityEngine;
using System.Collections;

public class JoystickLook2DGradual : MonoBehaviour {

	// The object that will be rotating to look at the mouse pointer.
	public GameObject obj;

	// The speed of rotation.  Higher numbers means faster rotation.
	public float rotationSpeed = 4;
	
	// The angle that the object is facing in when the script starts.
	// 360 > objStartAngle >= 0
	// 0 corresponds to facing down the positive X axis.
	public float objStartAngle = 0;

	// used for tracking input across frame updates
	private float prevInputX = 0;
	private float prevInputY = 0;


	void Start () {
		if(!obj) {
			obj = this.gameObject;
		}

		objStartAngle %= 360;
		getCoordinatesFromAngle(objStartAngle, out prevInputX, out prevInputY);
	}
	

	void Update () {
		// Get right stick input
		float inputX = Input.GetAxisRaw("AimHorizontal");
		float inputY = Input.GetAxisRaw("AimVertical");

		// If no input detected from right stick...
		if(inputX == 0 && inputY == 0) {

			// then use the left stick input.
			inputX = Input.GetAxisRaw("Horizontal");
			inputY = Input.GetAxisRaw("Vertical");

			// If no input detected from left stick...
			if(inputX == 0 && inputY == 0) {

				// then use whatever the last input was.
				inputX = prevInputX;
				inputY = prevInputY;
			}
		}
		
		float angle = (Mathf.Atan2(inputY, inputX) * Mathf.Rad2Deg) - objStartAngle;
		Quaternion targetRotation = Quaternion.Euler(new Vector3(0, -angle, 0));

		if(obj.transform.rotation != targetRotation) {
			angle = (Quaternion.Angle(obj.transform.rotation, targetRotation));
			obj.transform.rotation = Quaternion.Slerp(obj.transform.rotation, targetRotation, rotationSpeed * 180 / angle * Time.deltaTime);
		}

		prevInputX = inputX;
		prevInputY = inputY;
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
