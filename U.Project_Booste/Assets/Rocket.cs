using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

    Rigidbody rigidBody;
    AudioSource audioSource;

	// Use this for initialization
	void Start () {
        // Makes it reference the Rigidbody attached to the rocket ship.
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();  
	}
	
	// Update is called once per frame
	void Update ()
    {
        Thrusting();
        Rotate();
    }

    // Can thrust while Pivoting.
    private void Thrusting()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.up);
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }
    }

    private void Rotate()
    {
        rigidBody.freezeRotation = true; //Take manual control of turning (physics)

        // Can only turn 1 direction at a time.
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward);
        }

        rigidBody.freezeRotation = false;  // resume physics control
    }
}


