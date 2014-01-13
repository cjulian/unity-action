using UnityEngine;
using System.Collections;

public class JoystickLook2D_Instant : MonoBehaviour
{
	// The object that will be rotating to look at the mouse pointer.
	public GameObject obj;
	
	// The angle that the target is pointing in when the scene loads.
	// 0 corresponds to facing down the positive X axis.
	public float objStartAngle = 0;
	
	// Use this for initialization
	void Start ()
	{
		if(!obj)
		{
			obj = this.gameObject;
		}
	}
	
	// Update is called once per frame
	void Update () {
		// Right analog
		float inputX = Input.GetAxisRaw("AimHorizontal");
		float inputY = Input.GetAxisRaw("AimVertical");

		// if no input detected from right stick...
		if(inputX == 0 && inputY == 0) {
			
			// then check if there's input on left stick and use that.
			float inputX2 = Input.GetAxisRaw("Horizontal");
			float inputY2 = Input.GetAxisRaw("Vertical");

			if(inputX2 != 0 || inputY2 != 0) {
				inputX = inputX2;
				inputY = inputY2;
				
			// Otherwise, there's no input so return and don't rotate the object
			} else {
				return;
			}
		}
		
		float angle = (Mathf.Atan2(inputY, inputX) * Mathf.Rad2Deg) - objStartAngle;
		obj.transform.rotation = Quaternion.Euler(new Vector3(0, -angle, 0));
	}
}