using UnityEngine;
using System.Collections;

public class MouseLook2D_Instant : MonoBehaviour
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
		Vector3 objScreenPosition = Camera.main.WorldToScreenPoint(obj.transform.position);
		Vector3 targetDirection = Input.mousePosition - objScreenPosition;
		float angle = (Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg) - objStartAngle;

		obj.transform.rotation = Quaternion.Euler(new Vector3(0, -angle, 0));
	}
}
