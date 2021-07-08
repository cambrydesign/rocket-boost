using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public SceneHandler sceneHandler;
    public Vector3 checkpointPosition;
    public string levelName;
    public Level level;

    public static int survivors;
    public static int crashes;
    public List<int> checkpoints;

    public static GameManager _instance;

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

        checkpoints = new List<int>();

        GameObject.DontDestroyOnLoad(gameObject);
    }

    public void CallMainMenu() {
        sceneHandler.LoadMainMenu();
    }

    public void CallNextScene() {
        sceneHandler.LoadNextScene();
    }

    void OnLevelWasLoaded() {
        Debug.Log(survivors);
        level.Update();
        Debug.Log("Loaded");
    }

    public void FindSurvivor() {
        survivors ++;
        level.Update();
    }

    public void FindSpawnPoint() {
        checkpointPosition = GameObject.FindGameObjectWithTag("SpawnPoint").transform.position;
    }        
}