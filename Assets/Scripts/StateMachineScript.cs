using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateMachineScript : MonoBehaviour
{
    private GameObject currentScreen;
    [SerializeField] private GameObject firstScreen;

    private void Start()
    {
        firstScreen.SetActive(true);
        currentScreen = firstScreen;
    }

    public void ChangeState(GameObject screen)
    {
        if (currentScreen != null)
        {
            currentScreen.SetActive(false);
            screen.SetActive(true);
            currentScreen = screen;
        }
    }
}
