using System.Runtime.CompilerServices;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public SceneHandler sceneHandler;
    public Vector3 checkpointPosition;

    private static GameManager _instance;

    void Awake() {
        if (_instance == null) {
            _instance = this;
        } else {
            GameObject.Destroy(gameObject);
        }
        sceneHandler = new SceneHandler();
        sceneHandler.gm = this;
        sceneHandler.Build();
        FindSpawnPoint();
        GameObject.DontDestroyOnLoad(gameObject);
    }

    public void FindSpawnPoint() {
        checkpointPosition = GameObject.FindGameObjectWithTag("SpawnPoint").transform.position;
    }        
}