using UnityEngine;
using System.Collections;
[RequireComponent(typeof(Rigidbody))]

public class ShipMovement : MonoBehaviour {

	public float ShipSpeed;

	private Vector3 previousForce;

	// Use this for initialization
	void Start () {
		this.transform.rigidbody.mass = 1.0f;
		this.transform.rigidbody.drag = 1.0f;
		previousForce = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
	}

	void FixedUpdate () {
		Vector3 directionVector = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);

		if (directionVector != Vector3.zero) {
			// Normalize the direction vector
			directionVector = directionVector / directionVector.magnitude;

			// terminal velocity = force / (mass * drag)
			// Mass and drag are set to 1, so to reach desired speed we make force equal desired speed.
			Vector3 forceToAdd = directionVector * ShipSpeed;

			ForceMode currForceMode = ForceMode.Force;

			if (forceToAdd != previousForce) {
				// We zero the velocity and change force mode to VelocityChange in order
				// to allow for instantaneous direction/velocity changes (ie. no inertia).
				// Gives player movement a more arcade-y feel.  
				this.transform.rigidbody.velocity = Vector3.zero;
				currForceMode = ForceMode.VelocityChange;
			}

			this.transform.rigidbody.AddForce(forceToAdd, currForceMode);
			previousForce = forceToAdd;

		} else {
			// Immediately set velocity to zero if no directional input.  Allows character to stop on a dime.
			this.transform.rigidbody.velocity = Vector3.zero;
			previousForce = Vector3.zero;
		}
	}
}
