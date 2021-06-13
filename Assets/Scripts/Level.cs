using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    private GameObject winScreen;
    private TMP_Text winSurvivors;
    private TMP_Text winCrashes;
    private GameObject star1;
    private GameObject star2;
    private GameObject star3;

    private Color gold = new Color(0.9339623f, 0.8079672f, 0.1630028f, 1f);


    public void SetUI() {
        GameObject[] checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint");
        foreach (GameObject checkpoint in checkpoints) {
            totalSurvivors ++;
        }

        winScreen = GameObject.FindGameObjectWithTag("WinScreen");
        winSurvivors = GameObject.FindGameObjectWithTag("WinSurvivors").GetComponent<TMP_Text>();
        winCrashes = GameObject.FindGameObjectWithTag("WinCrashes").GetComponent<TMP_Text>();
        star1 = GameObject.FindGameObjectWithTag("Star1");
        star2 = GameObject.FindGameObjectWithTag("Star2");
        star3 = GameObject.FindGameObjectWithTag("Star3");
        winScreen.SetActive(false);

        currentSurvivors = GameObject.FindGameObjectWithTag("CurrentSurvivors").GetComponent<Text>();
        maxSurvivors = GameObject.FindGameObjectWithTag("MaxSurvivors").GetComponent<Text>();
        level = GameObject.FindGameObjectWithTag("LevelName").GetComponent<Text>();

        maxSurvivors.text = totalSurvivors.ToString();
        currentSurvivors.text = survivors.ToString();

        level.text = levelName;
    }

    public void Update() {
        //Debug.Log(gm.survivors);
        survivors = GameManager.survivors;
        //Debug.Log(survivors);
        maxSurvivors.text = totalSurvivors.ToString();
        currentSurvivors.text = survivors.ToString();
    }

    public void ShowWinScreen() {
        winScreen.SetActive(true);
        Debug.Log(survivors);
        Debug.Log(totalSurvivors);
        float foundPercentage = ((float)survivors/(float)totalSurvivors);
        Debug.Log(foundPercentage);
        //winSurvivors.text = Math.Round((foundPercentage*100), 0).ToString() + "%";
        winSurvivors.text = (foundPercentage*100).ToString() + "%";
        winCrashes.text = GameManager.crashes.ToString();
        if (foundPercentage == 1) {
            star3.GetComponent<Image>().color = gold;
            star2.GetComponent<Image>().color = gold;
            star1.GetComponent<Image>().color = gold;
        } else if (foundPercentage >= 0.5) {
            star2.GetComponent<Image>().color = gold;
            star1.GetComponent<Image>().color = gold;
        } else {
            star1.GetComponent<Image>().color = gold;
        }
    }
}
