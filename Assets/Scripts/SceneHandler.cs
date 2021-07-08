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
            currentSceneIndex = 1;
        } else {
            currentSceneIndex = nextSceneIndex;
        }
        GameObject.Destroy(gm.gameObject);
        GameObject.Destroy(GameObject.FindGameObjectWithTag("PlayerUI"));
        GameManager.survivors = 0;
        GameManager.crashes = 0;
        ReloadScene();
    }

    public void LoadMainMenu() {
        GameObject.Destroy(gm.gameObject);
        GameObject.Destroy(GameObject.FindGameObjectWithTag("PlayerUI"));
        currentSceneIndex = 0;
        ReloadScene();
    }

    public void LoadScene(int sceneIndex) {
        currentSceneIndex = sceneIndex;
        ReloadScene();
    }
}