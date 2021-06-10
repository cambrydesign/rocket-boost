using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level
{
    [SerializeField]
    public string levelName;
    [SerializeField]
    public int survivors = 0;
    [SerializeField]
    public int totalSurvivors = 0;
    [SerializeField]
    public GameManager gm;
    
    private Text currentSurvivors;
    private Text maxSurvivors;
    private Text level;

    public void SetUI() {
        GameObject[] checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint");
        foreach (GameObject checkpoint in checkpoints) {
            totalSurvivors ++;
        }
        survivors = gm.survivors;
        currentSurvivors = GameObject.FindGameObjectWithTag("CurrentSurvivors").GetComponent<Text>();
        maxSurvivors = GameObject.FindGameObjectWithTag("MaxSurvivors").GetComponent<Text>();
        level = GameObject.FindGameObjectWithTag("LevelName").GetComponent<Text>();
        maxSurvivors.text = totalSurvivors.ToString();
        currentSurvivors.text = survivors.ToString();
        level.text = levelName;
    }

    public void Update() {
        survivors = gm.survivors;
        maxSurvivors.text = totalSurvivors.ToString();
        currentSurvivors.text = survivors.ToString();
    }
}
