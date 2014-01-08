using UnityEngine;
using System.Collections;
[RequireComponent(typeof(Rigidbody))]

public class PlayerMovement : MonoBehaviour {
	
	public float maxSpeed = 7.0f;
	public bool useInertia = false;
	
	private ForceMode forceMode = ForceMode.Force;
	private Vector3 previousForce = Vector3.zero;
	
	void Start () {
		this.transform.rigidbody.drag = 1.0f;
	}

	void FixedUpdate () {
		Vector3 directionVector = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);

		if (directionVector != Vector3.zero) {
			// Save the direction vector magnitude before normalization for later use
			float vectorMagnitude = directionVector.magnitude;

			// Normalize the direction vector
			directionVector = directionVector / directionVector.magnitude;

			// Determine player speed based on magnitude of input.  We limit the magnitude to 1 so that
			// the speed never goes over maxSpeed.  If we didn't do this, the player could move faster
			// than maxSpeed when moving diagonally.
			float desiredSpeed = vectorMagnitude >= 1 ? maxSpeed : maxSpeed * vectorMagnitude;

			// terminal velocity = force / (mass * drag)
			// Mass and drag are set to 1, so to reach desired speed we make force equal to the desired speed.
			Vector3 forceToAdd = directionVector * desiredSpeed * this.transform.rigidbody.mass;

			ForceMode currForceMode = ForceMode.Force;

			if (forceToAdd != previousForce) {
				// We zero the velocity and change force mode to VelocityChange in order
				// to allow for instantaneous direction/velocity changes (ie. no inertia).
				// Gives player movement a more arcade-y feel.
				this.transform.rigidbody.velocity = Vector3.zero;
				currForceMode = ForceMode.Impulse;
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