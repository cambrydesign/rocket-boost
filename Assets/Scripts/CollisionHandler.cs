using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    void OnCollisionEnter(Collision other) { 
        Debug.Log(other.relativeVelocity.magnitude);
        if (other.relativeVelocity.magnitude > 30) {
            Debug.Log("Hit the ground too fast!");
            ReloadLevel();
        }
        switch (other.gameObject.tag) {
            case "Friendly":
            Debug.Log("Friend");
                break;
            case "Finished":
                Debug.Log("Finish");
                break;
            case "Fuel":
                Debug.Log("Fuel");
                break;    
            default:
                ReloadLevel();
                break;    
        }
    }

void ReloadLevel() {
        SceneManager.LoadScene(0);
    }
}
