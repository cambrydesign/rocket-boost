using System.Runtime.CompilerServices;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public SceneHandler sceneHandler;
    public Vector3 checkpointPosition;

    void Awake() {
        DontDestroyOnLoad(gameObject);
        sceneHandler = new SceneHandler();
        sceneHandler.gm = this;
        FindSpawnPoint();
    }

    public void FindSpawnPoint() {
        checkpointPosition = GameObject.FindGameObjectWithTag("SpawnPoint").transform.position;
    }        
}