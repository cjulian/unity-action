using UnityEngine;
using System.Collections;
[RequireComponent(typeof(Rigidbody))]

// This script requires a circular capsule collider to prevent jittering during collisions.
// A good starting point for responsive control is:
// mass = 10, drag = 17, forceToAdd = 2400
//
// This script can be used for either topdown or sidescroller movement.
// For topdown physics, make sure to move directionVector Vertical input to z-axis
// or through code.
public class PlayerMovement : MonoBehaviour {
	
	public float maxForceToAdd = 2400f;

	void Awake () {

	}

	void Start () {
	}

	void FixedUpdate () {
		// Get player input
		Vector2 moveAxis = InputManager.GetMoveAxisVector();
		Vector3 directionVector = new Vector3(moveAxis.x, 0, moveAxis.y);

		if (directionVector != Vector3.zero) {
			// Save the direction vector magnitude before normalization for later use
			float vectorMagnitude = directionVector.magnitude;
			
			// Normalize the direction vector
			directionVector = directionVector / directionVector.magnitude;

			// Add force based on magnitude of player input... but never let force be higher than maxForceToAdd
			float currForceToAdd = Mathf.Min(maxForceToAdd, maxForceToAdd * vectorMagnitude);

			// Add force in the direction of input
			this.transform.rigidbody.AddForce(directionVector * currForceToAdd, ForceMode.Force);		
		}
	}
}