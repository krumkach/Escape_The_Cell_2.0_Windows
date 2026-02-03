using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System.Linq;

public class TutorialScript : MonoBehaviour
{
    [SerializeField] private TMP_Text tutorialText;
    [SerializeField] private Button nextPageButton;
    [SerializeField] private Button previousPageButton;

    public LocalizationManager localization;

    private int index;
    
    void Update()
    {
        if (index == 0)
        {
            previousPageButton.interactable = false;
        }
        else if (index == localization.gameLines[5].Count - 1)
        {
            nextPageButton.interactable = false;
        }
        else
        {
            previousPageButton.interactable = true;
            nextPageButton.interactable = true;
        }
    }

    public void ChangeTutorialText(Button pressedButton) //Changing text in content window 
    {
        if (pressedButton == nextPageButton)
        {
            index++;
        }
        else
        {
            index--;
        }

        DisplayText();
    }

    public void OpenTutorial() //For going to first page when tutorial is opened
    {
        index = 0;
        DisplayText();
    }

    private void DisplayText()
    {
        tutorialText.text = localization.gameLines[5][index];
    }
}