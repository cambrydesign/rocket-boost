using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level
{
    public string levelName;
    public int survivors = 0;
    public int totalSurvivors = 0;
    
    private Text currentSurvivors;
    private Text maxSurvivors;
    private Text level;

    public void SetUI() {
        GameObject[] checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint");
        foreach (GameObject checkpoint in checkpoints) {
            totalSurvivors ++;
        }
        currentSurvivors = GameObject.FindGameObjectWithTag("CurrentSurvivors").GetComponent<Text>();
        maxSurvivors = GameObject.FindGameObjectWithTag("MaxSurvivors").GetComponent<Text>();
        level = GameObject.FindGameObjectWithTag("LevelName").GetComponent<Text>();
        maxSurvivors.text = totalSurvivors.ToString();
        currentSurvivors.text = survivors.ToString();
        level.text = levelName;
    }

    public void Update() {
        maxSurvivors.text = totalSurvivors.ToString();
        currentSurvivors.text = survivors.ToString();
        level.text = levelName;
    }

    public void FindSurvivor() {
        survivors ++;
        currentSurvivors.text = survivors.ToString();
    }
}
