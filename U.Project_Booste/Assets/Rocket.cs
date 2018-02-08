using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

    Rigidbody rigidBody;

	// Use this for initialization
	void Start () {
        // Makes it reference the Rigidbody attached to the rocket ship.
        rigidBody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        ProcessInput();
    }

    private void ProcessInput()
    {
        // Can thrust while Pivoting.
        if (Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.up);
        }

        // Can only turn 1 direction at a time.
        if (Input.GetKey(KeyCode.A))
        {
            print("Left was pushed");
        }
        else if (Input.GetKey(KeyCode.D))
        {
            print("Right was pushed");
        }
    }
}
