using System.Runtime.CompilerServices;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{

    public GameManager gm;
    public Movement movement;

    void Start() {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        movement = GetComponent<Movement>();
    }

    void OnCollisionEnter(Collision other) { 
        if (other.relativeVelocity.magnitude > 20) {
            Debug.Log("Hit the ground too fast!");
            movement.enabled = false;
            Invoke("CallReload", 1f);
        }
        switch (other.gameObject.tag) {
            case "Friendly":
                Debug.Log("Friend");
                break;
            case "Finished":
                movement.enabled = false;
                Invoke("CallNextScene", 3f);
                break;    
            default:
                movement.enabled = false;
                Invoke("CallReload", 1f);
                break;    
        }
    }

    void CallReload() {
        gm.sceneHandler.ReloadScene();
    }

    void CallNextScene() {
        gm.sceneHandler.LoadNextScene();
    }
}
