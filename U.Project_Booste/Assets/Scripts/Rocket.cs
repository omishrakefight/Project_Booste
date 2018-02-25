using UnityEngine.SceneManagement;
using UnityEngine;

public class Rocket : MonoBehaviour {

    [SerializeField] float ThrustSpeed = 900f;
    [SerializeField] float SpinSpeed = 175f;
    Rigidbody rigidBody;

    AudioSource audioSource;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip Death;
    [SerializeField] AudioClip Win;

    [SerializeField] ParticleSystem engineParticles;
    [SerializeField] ParticleSystem deathParticles;
    [SerializeField] ParticleSystem winParticles;

    enum State { Dead, Alive, Finish };
    State state = State.Alive;

    enum Cheat { Normal, Cheat };
    Cheat cheat = Cheat.Normal;

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
                state = State.Finish;
                audioSource.PlayOneShot(Win);
                Invoke("LoadNextLevel", 1.5f);
                winParticles.Play();
                break;
            default:
                if(cheat == Cheat.Cheat)
                {
                    return;
                }
                if(state == State.Alive)
                {
                    state = State.Dead;
                    audioSource.Stop();
                    audioSource.PlayOneShot(Death);
                    Invoke("RestartLevel", 1.5f);
                    deathParticles.Play();
                }
                break;
        }
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(0);
    }

    private void LoadNextLevel()
    {
        SceneManager.LoadScene(1);
    }

    // Update is called once per frame
    void Update ()
    {
        if(state == State.Alive)
        {
            Thrusting();
            Rotate();
            CommandKeys();
        }
    }

    private void CommandKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            SceneManager.LoadScene(1);
        }

        // Disables collisions
        else if (Input.GetKeyDown(KeyCode.C))
        {
            cheat = Cheat.Cheat;
        }
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
                audioSource.PlayOneShot(mainEngine);
            }
            engineParticles.Play();
        }
        else
        {
            audioSource.Stop();
            engineParticles.Stop();
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


