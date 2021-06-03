using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler
{
    public int currentSceneIndex = 0;
    public GameManager gm;

    public void ReloadScene() {
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void LoadNextScene() {
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings) {
            currentSceneIndex = 0;
        } else {
            currentSceneIndex = nextSceneIndex;
        }
        ReloadScene();
        gm.FindSpawnPoint();
    }

    public void LoadScene(int sceneIndex) {
        currentSceneIndex = sceneIndex;
        ReloadScene();
    }
}