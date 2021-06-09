using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    private static PlayerUI _instance;

    void Awake() {
        if (_instance == null) {
            _instance = this;
        } else {
            GameObject.Destroy(gameObject);
        }
        GameObject.DontDestroyOnLoad(gameObject);
    }
}
