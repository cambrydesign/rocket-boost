using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    public float currentThrust = 0;
    public float acceleration = 500f;
    public float minThrust = -1000f;
    public float maxThrust = 5000f;
    public float rotationalThrust = 450f;
    public float directionalThrust = 2000f;
    private AudioSource audioSource;
    public AudioClip thrust;
    public ParticleSystem mainThrust;
    public ParticleSystem leftThrust;
    public ParticleSystem rightThrust;
    public ParticleSystem leftPushThrust;
    public ParticleSystem rightPushThrust;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        transform.position = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().checkpointPosition;
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
        ProcessSideThrust();
        CheckKillBounds();
    }

    void CheckKillBounds() {
        if (transform.position.y < -100) {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().sceneHandler.ReloadScene();
        }
    }

    void ProcessThrust()
    {
        HandleMainInput();
        HandleMainThrust();
    }

    private void HandleMainInput()
    {
        if (Input.GetKey(KeyCode.X))
        {
            currentThrust = 0;
        }
        else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S))
        {
            //
        }
        else if (Input.GetKey(KeyCode.W))
        {
            if (currentThrust < maxThrust)
            {
                currentThrust += acceleration * Time.deltaTime;
            }
        }
        else if (Input.GetKey(KeyCode.S))
        {
            if (currentThrust > minThrust)
            {
                currentThrust -= acceleration * Time.deltaTime;
            }
        }
    }

    private void HandleMainThrust()
    {
        if (currentThrust != 0)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(thrust);
            }
            if (!mainThrust.isPlaying)
            {
                mainThrust.Play();
            }
            rb.AddRelativeForce(Vector3.up * currentThrust * Time.deltaTime);
        }
        else
        {
            audioSource.Stop();
            mainThrust.Stop();
        }
    }

    void ProcessRotation() {
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D)) {
            StopRotateThrust();
        } else if (Input.GetKey(KeyCode.A)) {
            ApplyRotation(rotationalThrust);
            if (!leftThrust.isPlaying) {
                StartLeftRotateThrust();
            }
        } else if (Input.GetKey(KeyCode.D)) {
            ApplyRotation(-rotationalThrust);
            if (!rightThrust.isPlaying) {
                StartRightRotateThrust();
            }
        } else {
            StopRotateThrust();
        }
    }

    private void StopRotateThrust()
    {
        leftThrust.Stop();
        rightThrust.Stop();
    }

    private void StartLeftRotateThrust()
    {
        leftThrust.Play();
        rightThrust.Stop();
    }

    private void StartRightRotateThrust()
    {
        rightThrust.Play();
        leftThrust.Stop();
    }

    void ProcessSideThrust() {
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
        {
            Debug.Log("Equal force!");
            StopSideThrust();
        }
        else if (Input.GetKey(KeyCode.Q)) {
            if (!leftPushThrust.isPlaying)
            {
                StartLeftThrust();
            }
            rb.AddRelativeForce(Vector3.left * directionalThrust * Time.deltaTime);
        } else if (Input.GetKey(KeyCode.E)) {
            if (!rightPushThrust.isPlaying)
            {
                StartRightThrust();
            }
            rb.AddRelativeForce(Vector3.right * directionalThrust * Time.deltaTime);
        } else {
            StopSideThrust();
        }
    }

    private void StartRightThrust()
    {
        rightPushThrust.Play();
        leftPushThrust.Stop();
    }

    private void StartLeftThrust()
    {
        leftPushThrust.Play();
        rightPushThrust.Stop();
    }

    private void StopSideThrust()
    {
        leftPushThrust.Stop();
        rightPushThrust.Stop();
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;
    }
}
