using System.Runtime.CompilerServices;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{

    public GameManager gm;
    public Movement movement;
    public float crashTime = 1.5f;
    private Vector3 checkpointPosition;

    void Start() {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        movement = GetComponent<Movement>();
    }

    void OnCollisionEnter(Collision other) { 
        if (other.relativeVelocity.magnitude > 20) {
            Debug.Log("Hit the ground too fast!");
            StartCrashSequence();
        } else {
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
    }

    void CheckpointPosition(Transform checkpoint) {
        gm.checkpointPosition = checkpoint.GetChild(0).transform.position;
    }

    void StartCrashSequence() {
        DisableMovement();
        Invoke("CallReload", crashTime);
    }

    void StartWinSequence() {
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
