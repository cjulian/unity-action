using UnityEngine;
using System.Collections;
[RequireComponent(typeof(Rigidbody))]


// This script requires a circular capsule collider to prevent jittering during collisions.
// A good starting point for responsive control is:
// mass = 10, drag = 17, forceToAdd = 2400
//
// This script can be used for either topdown or sidescroller movement.
// For topdown physics, make sure to change the gravity settings to -z through project settings
// or through code.
public class PlayerMovementInertia : MonoBehaviour {
	
	public float maxForceToAdd = 2400f;

	void Start () {
	}

	void FixedUpdate () {
		// Get player input
		Vector3 directionVector = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
		
		if (directionVector != Vector3.zero) {
			// Save the direction vector magnitude before normalization for later use
			float vectorMagnitude = directionVector.magnitude;
			
			// Normalize the direction vector
			directionVector = directionVector / directionVector.magnitude;

			// Add force based on magnitude of player input... but never let force be higher than maxForceToAdd
			float currForceToAdd = Mathf.Min(maxForceToAdd, maxForceToAdd * vectorMagnitude);

			// Add enough force to change velocity to the desired speed.
			// This is done instantaneously using ForceMode.Impulse.
			// To reach desired speed we make the magnitude of the force equal to the desired speed (assuming drag is 1).
			this.transform.rigidbody.AddForce(directionVector * currForceToAdd, ForceMode.Force);		
		}
	}
}