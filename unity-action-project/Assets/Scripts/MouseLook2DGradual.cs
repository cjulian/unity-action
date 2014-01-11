using UnityEngine;
using System.Collections;

public class MouseLook2DGradual : MonoBehaviour
{
	public GameObject obj;
	public float rotationSpeed = 3;

	// Use this for initialization
	void Start ()
	{
		if(!obj)
		{
			obj = this.gameObject;
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		Plane playerPlane = new Plane(Vector3.up, obj.transform.position);
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		float hitDist = 0.0f;

		if(playerPlane.Raycast(ray, out hitDist)) 
		{
			Vector3 targetPoint = ray.GetPoint(hitDist);
			Quaternion targetRotation = Quaternion.LookRotation(targetPoint - obj.transform.position);
			float rotationAngle = (Quaternion.Angle(obj.transform.rotation, targetRotation));
			obj.transform.rotation = Quaternion.Slerp(obj.transform.rotation, targetRotation, (rotationSpeed / (rotationAngle / 180)) * Time.deltaTime);
		}
	}
}
