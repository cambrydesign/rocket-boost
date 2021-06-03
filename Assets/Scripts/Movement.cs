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
    public float directionalThrust = 450f;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust() {
        if (Input.GetKey(KeyCode.X)) {
            currentThrust = 0;
        } else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S)) {
            //
        } else if (Input.GetKey(KeyCode.W)) {
            if (currentThrust < maxThrust) {
                currentThrust += acceleration * Time.deltaTime;
            }
        } else if (Input.GetKey(KeyCode.S)) {
            if (currentThrust > minThrust) {
                currentThrust -= acceleration * Time.deltaTime;
            }
        }
        if (currentThrust != 0) {
            if (!audioSource.isPlaying) {
                audioSource.Play();
            }
            rb.AddRelativeForce(Vector3.up * currentThrust * Time.deltaTime);
        } else {
            audioSource.Stop();
        }
    }

    void ProcessRotation() {
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D)) 
        {
            Debug.Log("Equal rotation!");
        } else if (Input.GetKey(KeyCode.A)) {
            ApplyRotation(directionalThrust);
        } else if (Input.GetKey(KeyCode.D)) {
            ApplyRotation(-directionalThrust);
        }
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;
    }
}
