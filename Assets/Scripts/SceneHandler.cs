using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler
{
    public int currentSceneIndex;
    public GameManager gm;

    public void Build() {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

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
    }

    public void LoadScene(int sceneIndex) {
        currentSceneIndex = sceneIndex;
        ReloadScene();
    }
}