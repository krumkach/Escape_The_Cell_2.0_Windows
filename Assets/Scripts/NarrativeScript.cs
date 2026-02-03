using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System.Linq;

public class NarrativeScript : MonoBehaviour
{
    [SerializeField] private TMP_Text narrativeText;
    [SerializeField] private GameObject actionButton;
    [SerializeField] private GameObject pickButton;
    [SerializeField] private GameObject theEndButton;
    [SerializeField] private TMP_Text actionButtonText;

    [SerializeField] private List<Sprite> sprites = new List<Sprite>();
    [SerializeField] private Image narrativeImage;

    private GameObject currentButton;
    private int lineIndex;

    public LocalizationManager localization;

    //Setting rows of lines when i need action button to be changed or replaced by pick button
    private int[] pickButtonIndexes = { 3, 8, 10, 14, 16, 20, 25, 27 };
    private int[] actionChangeIndexes = { 4, 5, 6, 12, 18, 23, 28, 33, 36, 37 };
    private int actionLine;

    //Setting row of sprite indexes in image list for correct displaying
    private int[] spriteIndexes = { 0, 1, 2, 3, 4, 1, 1, 5, 3, 4, 5, 4, 5, 5, 5, 6, 8, 6, 4, 4, 4, 6, 1, 6, 6, 6, 8, 3, 7, 7, 7, 8, 8, 7, 7, 7, 7, 7, 1, 1, 1, 1, 1 };

    public SoundManager sound;

    void Start()
    {
        sound.PlaySound(0);
        pickButton.SetActive(false);
        theEndButton.SetActive(false);
        actionButton.SetActive(true);
        currentButton = actionButton;
    }

    public void RestartNarration()
    {
        lineIndex = 0;
        actionLine = 1;
        actionButtonText.text = localization.gameLines[0][0];
        DisplayNarrative();
        ChangeButton(actionButton);
    }

    public void Action() //Changing text in content window 
    {
        UpdateText();
    }

    public void UpdateText()
    {        
        lineIndex++;
        DisplayNarrative();
    }

    private void DisplayNarrative()
    {
        narrativeText.text = localization.gameLines[4][lineIndex];
        narrativeImage.sprite = sprites[spriteIndexes[lineIndex]];
        if (lineIndex == 38)
        {
            sound.PlaySound(3);
        }

        foreach (int number in pickButtonIndexes)
        {

            if (lineIndex == number)
            {
                ChangeButton(pickButton);
                break;
            }
        }

        foreach (int number in actionChangeIndexes)
        {
            if (lineIndex == number)
            {
                actionButtonText.text = localization.gameLines[0][actionLine];
                actionLine++;
                break;
            }
            else
            {
                actionButtonText.text = localization.gameLines[0][0];
            }
        }

        if (lineIndex == localization.gameLines[4].Count - 1)
        {
            ChangeButton(theEndButton);
        }
    }

    public void ChangeButton(GameObject newButton)
    {
        if (currentButton != null)
        {
            currentButton.SetActive(false);
            newButton.SetActive(true);
            currentButton = newButton;
        }
    }
}
