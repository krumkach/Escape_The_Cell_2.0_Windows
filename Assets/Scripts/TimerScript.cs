using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System.Linq;

public class TimerScript : MonoBehaviour
{
    //Timer is activated each time new lock is set (except tutorial)
    [SerializeField] private TMP_Text timerText;
    private bool isOn = false;
    private float startTime;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TMP_Text gameOverPanelText;

    [SerializeField] private Image limbImage;
    private float opacity = 0.0f;

    private int deathCount = 0; 
    private bool hadDied = false;
    private float totalTime = 0.0f;

    [SerializeField] private TMP_Text deathCountText;
    [SerializeField] private TMP_Text timeCountText;

    [HideInInspector]public int hintNum;

    public SoundManager sound;
    public UnlockingScript lockInfo; //For getting lock number and changing the hint number value
    public LocalizationManager localization;

    void Update()
    {
        totalTime += (1.0f * Time.deltaTime);
        if (isOn)
        {
            opacity += (0.001f * Time.deltaTime);
            ImageOpacity(opacity);

            if ((int)startTime < 1)
            {
                Death();
            }
            else
            {
                startTime -= (1.0f * Time.deltaTime);
            }
        }

        if ((int)startTime == 10)
        {
            sound.PlayWhisperSound();
        }

        timerText.text = ((int)startTime).ToString();
    }

    public void StartTimer() //This one is for using in other scripts
    {
        isOn = true;
        startTime = 60.0f;
        opacity = 0.0f;
        ImageOpacity(opacity);
    }

    public void StopTimer()
    {
        isOn = false;
    }

    void Death()
    {
        StopTimer();
        sound.PlaySound(4);
        hintNum = lockInfo.lockNumber;
        hadDied = true;
        gameOverPanel.SetActive(true);
        UpdateGameOverText();
        deathCount++;
        opacity = 0.0f;
        ImageOpacity(opacity);
    }

    public void ResetSummary()
    {
        deathCount = 0;
        hadDied = false;
        totalTime = 0.0f;
        UpdateGameOverText();
    }

    public bool GetHadDied()
    {
        return hadDied;
    }

    public void SetSummary()
    {
        int time = (int)totalTime;
        deathCountText.text = deathCount.ToString();
        timeCountText.text = time.ToString();
    }

    private void UpdateGameOverText()
    {
        gameOverPanelText.text = localization.gameLines[1][deathCount];
    }
    
    private void ImageOpacity(float opacity)
    {
        Color color = new Color(1.0f, 1.0f, 1.0f, opacity);
        color.a = opacity;
        limbImage.color = color;
    }
}
