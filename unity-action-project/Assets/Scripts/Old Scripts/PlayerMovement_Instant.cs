using UnityEngine;
using System.Collections;
[RequireComponent(typeof(Rigidbody))]


// This script can be used for either topdown or sidescroller movement.
// For topdown physics, make sure to change the gravity settings to -z through project settings
// or through code.
public class PlayerMovement_Instant : MonoBehaviour {
	
	public float maxSpeed = 7.0f;

	void Start () {
		this.transform.rigidbody.drag = 1.0f;
	}

	void FixedUpdate () {
		// Get player input
		Vector3 directionVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
		
		if (directionVector != Vector3.zero) {
			// Save the direction vector magnitude before normalization for later use
			float vectorMagnitude = directionVector.magnitude;
			
			// Normalize the direction vector
			directionVector = directionVector / directionVector.magnitude;
			
			// Determine player speed based on magnitude of input (vectorMagnitude).  We limit the magnitude to 1 so that
			// the speed never goes over maxSpeed.  If we didn't do this, the player could move faster
			// than maxSpeed when moving diagonally on keyboard.
			float desiredSpeed = vectorMagnitude >= 1 ? maxSpeed : maxSpeed * vectorMagnitude;

			// Get rid of any momentum
			this.transform.rigidbody.velocity = Vector3.zero;

			// Add enough force to reach desired speed.
			// This is done instantaneously using ForceMode.Impulse.
			this.transform.rigidbody.AddForce(directionVector * desiredSpeed * transform.rigidbody.mass, ForceMode.Impulse);
			
		} else {
			// Get rid of any momentum
			this.transform.rigidbody.velocity = Vector3.zero;
		}
	}
}