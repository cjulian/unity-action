using UnityEngine;
using System.Collections;

public class MouseLook2D : MonoBehaviour
{
	// The object that will be rotating based on joystick input.
	public GameObject obj;
	
	// Rotation speed, based on the time it should take to turn 180 degrees.
	public float turnTime180 = 0.3f;
	
	void Start ()
	{
		// if no object assigned, then use this game object
		if (!obj) {	obj = this.gameObject; }
	}
	
	void Update () {
		// get current object position in screen space
		Vector3 objScreenPosition = Camera.main.WorldToScreenPoint(obj.transform.position);

		// get the direction from the object position to the mouse position
		Vector3 targetDirection = Input.mousePosition - objScreenPosition;

		// get the quaternion corresponding to rotation that would have the object looking in the target direction
		Quaternion targetRotation = Quaternion.LookRotation(new Vector3(targetDirection.x, 0.0f, targetDirection.y), Vector3.up);

		// rotate the object gradually in that direction based on time required to turn 180 degrees and angle of target rotation
		float angle = Quaternion.Angle(obj.transform.rotation, targetRotation);
		if (angle > 0.1) {
			obj.transform.rotation = Quaternion.Slerp(obj.transform.rotation, targetRotation, (180/ angle) * (Time.deltaTime / turnTime180));
		}	
	}
}
