using System.Runtime.CompilerServices;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public GameManager gm;
    public Movement movement;
    public float crashTime = 1.5f;
    public AudioClip death;
    public AudioClip win;
    private Vector3 checkpointPosition;
    private AudioSource audioSource;
    private bool isTransitioning = false;

    void Start() {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        movement = GetComponent<Movement>();
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision other) { 
        if (isTransitioning) {
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

    void CheckpointPosition(Transform checkpoint) {
        gm.checkpointPosition = checkpoint.GetChild(0).transform.position;
    }

    void StartCrashSequence() {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(death);
        DisableMovement();
        Invoke("CallReload", crashTime);
    }

    void StartWinSequence() {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(win);
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
        gm.sceneHandler.LoadNextScene();
    }
}
