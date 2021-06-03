using System.Runtime.CompilerServices;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public SceneHandler sceneHandler;

    void Start() {
        DontDestroyOnLoad(gameObject);
        sceneHandler = new SceneHandler();
    }        
}