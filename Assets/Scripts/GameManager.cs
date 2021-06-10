using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public SceneHandler sceneHandler;
    public Vector3 checkpointPosition;
    public string levelName;
    public Level level;

    public int survivors;
    public List<int> checkpoints;

    private static GameManager _instance;

    void Awake() {
        if (_instance == null) {
            _instance = this;
        } else {
            GameObject.Destroy(gameObject);
        }
        level = new Level();
        level.gm = this;
        level.levelName = levelName;
        level.SetUI();

        level.Update();
        sceneHandler = new SceneHandler();
        sceneHandler.gm = this;
        sceneHandler.Build();
        FindSpawnPoint();
        GameObject.DontDestroyOnLoad(gameObject);
        checkpoints = new List<int>();
    }

    void OnLevelWasLoaded() {
        level.Update();
    }

    void Update() {
        
    }

    public void FindSurvivor() {
        survivors ++;
        level.Update();
    }

    public void FindSpawnPoint() {
        checkpointPosition = GameObject.FindGameObjectWithTag("SpawnPoint").transform.position;
    }        
}