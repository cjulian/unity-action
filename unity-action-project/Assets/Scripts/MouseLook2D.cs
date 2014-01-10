using UnityEngine;
using System.Collections;

public class MouseLook2D : MonoBehaviour
{
	// The object that will be rotating to look at the mouse pointer.
	public GameObject target;

	// The angle that the target is pointing in when the scene loads.
	// 0 corresponds to facing down the positive X axis.
	public float targetStartAngle = 0;

	// Use this for initialization
	void Start ()
	{
		if(!target)
		{
			target = this.gameObject;
		}
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 playerScreenPosition = Camera.main.WorldToScreenPoint(target.transform.position);
		Vector3 direction = Input.mousePosition - playerScreenPosition;
		float angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) - targetStartAngle;


		target.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
	}
}
