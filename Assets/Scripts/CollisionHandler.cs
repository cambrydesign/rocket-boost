using System.Runtime.CompilerServices;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public GameManager gm;
    public Movement movement;
    public float crashTime = 1.5f;
    public AudioClip death;
    public AudioClip win;
    public ParticleSystem deathParticles;
    public ParticleSystem winParticles;
    private Vector3 checkpointPosition;
    private AudioSource audioSource;
    private bool isTransitioning = false;

    public bool debugMode = false;

    private bool debugNoClip = false;
    private bool debugNoCollision = false;


    void Start() {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        movement = GetComponent<Movement>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update() {
        ProcessDebugInput();
        CheckKillBounds();
    }

    void CheckKillBounds() {
        if (transform.position.y < -500 && !isTransitioning) {
            StartCrashSequence();
        }
    }

    void ProcessDebugInput() {
        if (debugMode) {
            if (Input.GetKeyDown(KeyCode.C)) {
                ToggleCollisions();
            }
            if (Input.GetKeyDown(KeyCode.L)) {
                CallNextScene();
            }
            if (Input.GetKeyDown(KeyCode.N)) {
                ToggleNoClip();
            }
        }
    }

    private void ToggleCollisions() {
        debugNoCollision = !debugNoCollision;
    }

    private void ToggleNoClip()
    {
        if (!debugNoClip) {
            Debug.Log("Enabling no clip");
            debugNoClip = true;
            GetComponent<Collider>().enabled = false;
            foreach (Transform child in transform) {
                if (child.GetComponent<Collider>() != null) {
                    child.GetComponent<Collider>().enabled = false;
                }
            }
        } else {
            Debug.Log("Disabling no clip");
            debugNoClip = false;
            GetComponent<Collider>().enabled = true;
            foreach (Transform child in transform) {
                if (child.GetComponent<Collider>() != null) {
                    child.GetComponent<Collider>().enabled = true;
                }
            }
        }
    }

    void OnCollisionEnter(Collision other) { 
        if (isTransitioning || debugNoCollision) {
            return;
        }
        if (other.relativeVelocity.magnitude > 20) {
            Debug.Log("Hit the ground too fast!");
            StartCrashSequence();
            return;
        }
        switch (other.gameObject.tag) {
            case "Friendly":
                CheckpointPosition(other.transform);
                break;
            case "Finished":
                StartWinSequence();
                break;    
            default:
                StartCrashSequence();
                break;    
        }
    }

    void CheckpointPosition(Transform checkpointObject) {
        List<int> checkpoints = gm.checkpoints;
        Checkpoint checkpoint = checkpointObject.GetComponent<Checkpoint>();
        if (!checkpoints.Contains(checkpoint.id)) {
            gm.FindSurvivor();
            checkpoints.Add(checkpoint.id);
        }
        gm.checkpointPosition = checkpointObject.GetChild(0).transform.position;
    }

    void StartCrashSequence() {
        isTransitioning = true;
        GameManager.crashes++;
        audioSource.Stop();
        audioSource.PlayOneShot(death);
        movement.rb.freezeRotation = true;
        movement.em.rateOverTime = 0;
        movement.leftThrust.Stop();
        movement.rightThrust.Stop();
        GameObject.FindGameObjectWithTag("RocketLight").SetActive(false);
        deathParticles.Play();
        BreakApart();
        DisableMovement();
        Invoke("CallReload", crashTime);
    }

    void BreakApart() {
        foreach (Transform child in transform) {
            if (child.gameObject.tag != "Debris" && child.gameObject.tag != "Particle") {
                child.gameObject.SetActive(false);
            } else if (child.gameObject.tag != "Particle") {
                child.gameObject.SetActive(true);
                child.parent = null;
                Vector3 direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
                direction.Normalize();
                child.GetComponent<Rigidbody>().AddRelativeForce(direction * 1500f);   
            }
        }
    }

    void StartWinSequence() {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(win);
        movement.mainThrust.Stop();
        movement.leftThrust.Stop();
        movement.rightThrust.Stop();
        winParticles.Play();
        DisableMovement();
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
        Invoke("CallNextScene", 3f);
    }

    void DisableMovement() {
        movement.enabled = false;
        movement.currentThrust = 0;
    }

    void CallReload() {
        gm.sceneHandler.ReloadScene();
    }

    void CallNextScene() {
        gm.level.ShowWinScreen();
    }
}
