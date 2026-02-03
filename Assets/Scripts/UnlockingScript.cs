using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Linq;

public class UnlockingScript : MonoBehaviour
{
    //Need to check if all the objects are required in the code

    //game message object
    [SerializeField] private TMP_Text message;

    //Buttons
    [SerializeField] private GameObject startOverButton;
    [SerializeField] private Button nextButton;

    [SerializeField] private GameObject hintObject;
    [SerializeField] private GameObject hintHidingObject;
    [SerializeField] private TMP_Text hintText;

    [SerializeField] private Button wireHook;
    [SerializeField] private Button skeletonKey;
    [SerializeField] private Button bumpKey;

    private List<ToolButtonScript> buttonExamples = new List<ToolButtonScript>();
    private List<Button> buttons = new List<Button>();

    public ToolButtonScript wireHookTool = new ToolButtonScript(3, -2, 2);
    public ToolButtonScript skeletonKeyTool = new ToolButtonScript(-1, -3, 1);
    public ToolButtonScript bumpKeyTool = new ToolButtonScript(2, 2, 2);

    //Pins
    [SerializeField] private TMP_Text[] pins;

    [SerializeField] private TMP_Text[] pinGoals;

    public TimerScript timer;
    public LocalizationManager localization;

    private bool isLocked = true;

    [HideInInspector] public int lockNumber = 0; //For index in locks' collections, it's public for using in timer script (setting hint number in case of death)

    //Combinations are what the gamer needs to achieve, locks are what positions the pins have at the start, hints are first letters of tools in required order
    private int[,] combinations = { { 5, 5, 5 }, { 2, 2, 2 }, { 7, 7, 7 }, { 4, 4, 4 }, { 9, 9, 9 }, { 8, 8, 8 }, { 10, 10, 10 }, { 6, 6, 6 } };
    private int[,] locks = { { 2, 7, 3 }, { 4, 8, 0 }, { 4, 6, 2 }, { 4, 8, 0 }, { 6, 8, 4 }, { 0, 10, 2 }, { 2, 9, 1 }, { 3, 8, 4 } };
    private string[] hints = { "W", "SS", "BBS", "SSB", "BSB", "WBW", "SBBWB", "W" };

    private int[] currentCombination = new int[3];
    private int[] currentLock = new int[3];

    private string[] toolNames = new string[3];

    void Start()
    {
        SetButtonsList();
    }

    public void StartUnlocking()
    {
        hintObject.SetActive(false);
        hintHidingObject.SetActive(true);
        message.text = ""; //For clearing the field at each start

        if (lockNumber == 0)
        {
            message.text = localization.gameLines[3][0];
        }

        if (timer.GetHadDied() && timer.hintNum == lockNumber)
        {
            hintObject.SetActive(true);
            hintText.text = localization.gameLines[2][lockNumber];
        }

        SetLock(); //Applying the pins' values for current lock
        if (lockNumber > 0)
        {
            timer.StartTimer(); //Starting the timer from timer script (only after first tutorial lock)
        }
    }

    public void SetLock()
    {
        //Applying combination and lock settings to current game (depends on lock number)
        isLocked = true;

        for (int indx = 0; indx < currentLock.Length; indx++)
        {
            currentLock[indx] = locks[lockNumber, indx];
            currentCombination[indx] = combinations[lockNumber, indx];
        }

        UpdatePins(); //Changing texts on the pins

        for (int indx = 0; indx < currentCombination.Length; indx++)//setting text for pin goals
        {
            pinGoals[indx].text = (currentCombination[indx].ToString());
        }

        SetInteractable(true); //Activating tools' buttons
        nextButton.interactable = false;
        startOverButton.SetActive(false);
    }

    public void Picking(Button pressedButton) //Action when any tool button is pressed
    {
        UseTool(pressedButton); //Checking which tool is used and changing the pins

        if (Unlock())
        {
            message.text = localization.gameLines[3][3];
            lockNumber++; //For being able to pick the next lock in array
            SetInteractable(false); //Deactivating tools' buttons
            nextButton.interactable = true;//Activating next button
            timer.StopTimer();//Stoppong timer

        }
    }

    //Checking if it is possible to use a tool without moving pins out of range and displaying messages
    private void UseTool(Button pressedButton)
    {
        for (int indx = 0; indx < buttons.Count; indx++)
        {
            if (pressedButton == buttons[indx])
            {
                if (buttonExamples[indx].ChangePins(currentLock))
                {
                    UpdatePins();
                    message.text = $"{toolNames[indx]} ";
                }
                else
                {
                    message.text = "";
                    SetInteractable(false);
                    startOverButton.SetActive(true);
                }
                message.text += localization.gameLines[3][buttonExamples[indx].messageIndx];            
            }
        }
    }

    private bool Unlock()
    {
        for (int indx = 0; indx < pins.Length; indx++)
        {
            if (currentLock[indx] != currentCombination[indx])
            {
                isLocked = false;
                break;
            }
            else
            {
                isLocked = true;
            }
        }

        return isLocked;
    }

    private void UpdatePins()
    {
        for (int indx = 0; indx < currentLock.Length; indx++)
        {
            pins[indx].text = currentLock[indx].ToString();
        }
    }

    private void SetInteractable(bool isInteractable)
    {
        foreach (Button button in buttons)
        {
            button.interactable = isInteractable; //Setting each button in buttons list as interactable or non-interactable depends on bool var
        }
    }

    public void SetToolNames()
    {
        int indx = 2;

        for (int x = 0; x < toolNames.Length; x++)
        {
            toolNames[x] = localization.canvasLines[1][indx];
            indx++;
        }
    }

    private void SetButtonsList()
    {
        buttons.InsertRange(buttons.Count, new Button[] { wireHook, skeletonKey, bumpKey });
        buttonExamples.InsertRange(buttonExamples.Count, new ToolButtonScript[] { wireHookTool, skeletonKeyTool, bumpKeyTool });
    }

    public void ResetLockpicking()
    {
        lockNumber = 0;
    }
}
