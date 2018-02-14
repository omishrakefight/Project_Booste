using UnityEngine.SceneManagement;
using UnityEngine;

public class Rocket : MonoBehaviour {

    [SerializeField] float ThrustSpeed = 900f;
    [SerializeField] float SpinSpeed = 175f;
    Rigidbody rigidBody;
    AudioSource audioSource;

	// Use this for initialization
	void Start () {
        // Makes it reference the Rigidbody attached to the rocket ship.
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();  
	}

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Finish":
                print("Hit Finish");
                SceneManager.LoadScene(1);
                break;
            default:
                print("DEAD");
                SceneManager.LoadScene(0);
                break;
        }
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
        // Acceleration based on time
        float FrameSpeedAccel = ThrustSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.Space))
        {

            rigidBody.AddRelativeForce(Vector3.up * FrameSpeedAccel);

            // Stops it from spamming sound
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

        float FrameSpeedRotation = SpinSpeed * Time.deltaTime;

        // Can only turn 1 direction at a time.
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * FrameSpeedRotation);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * FrameSpeedRotation);
        }

        rigidBody.freezeRotation = false;  // resume physics control
    }
}


