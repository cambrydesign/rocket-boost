using System.Runtime.CompilerServices;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public SceneHandler sceneHandler;
    public Vector3 checkpointPosition;
    public string levelName;
    public Level level;

    private static GameManager _instance;

    void Awake() {
        if (_instance == null) {
            _instance = this;
        } else {
            GameObject.Destroy(gameObject);
        }
        RefreshLevel();
        sceneHandler = new SceneHandler();
        sceneHandler.gm = this;
        sceneHandler.Build();
        FindSpawnPoint();
        GameObject.DontDestroyOnLoad(gameObject);
    }

    public void RefreshLevel() {
        level = new Level();
        level.levelName = levelName;
        level.SetUI();
    }

    public void FindSpawnPoint() {
        checkpointPosition = GameObject.FindGameObjectWithTag("SpawnPoint").transform.position;
    }        
}