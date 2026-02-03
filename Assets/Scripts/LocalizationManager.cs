using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System.Linq;

public class LocalizationManager : MonoBehaviour
{
    //Find fittable font for rus localization
    [SerializeField] private Toggle eng;
    [SerializeField] private Font fontENG;
    [SerializeField] private Font fontRUS;

    private TMP_FontAsset asset;

    static string readFromFilePath;

    [HideInInspector] public List<List<string>> canvasLines = new List<List<string>>();
    [HideInInspector] public List<List<string>> gameLines = new List<List<string>>();

    [System.Serializable] public class serializableClass
    {
        public List<TMP_Text> sampleList;

        public List<TMP_Text> ReturnList()
        {
            return sampleList;
        }
    }
    public List<serializableClass> texts = new List<serializableClass>();

    [SerializeField] private List<TMP_Text> gameTexts = new List<TMP_Text>();

    string[] fileNamesForCanvas = { "MainMenuCanvas", "GameCanvas", "NarrativeCanvas" };
    string[] fileNamesForGame = { "Actions", "GameOver", "Hints", "Messages", "Narrative", "Tutorial" };

    // Start is called before the first frame update
    void Start()
    {
        eng.isOn = true;
        ChangeLanguage();
    }

    public void ChangeLanguage()
    {
        if (eng.isOn)
        {
            CreateLineLists("ENG", fileNamesForCanvas, fileNamesForGame);
            SetLanguage(fontENG);
        }
        else
        {
            CreateLineLists("RUS", fileNamesForCanvas, fileNamesForGame);
            SetLanguage(fontRUS);
        }
    }

    private void SetLanguage(Font font)
    {
        asset = TMP_FontAsset.CreateFontAsset(font);
        
        for (int indx = 0; indx < texts.Count; indx++)
        {
            for (int indx2 = 0; indx2 < texts[indx].ReturnList().Count; indx2++)
            {
                texts[indx].ReturnList()[indx2].text = canvasLines[indx][indx2];
                texts[indx].ReturnList()[indx2].font = asset;
            }
            
        }

        for (int indx = 0; indx < gameTexts.Count; indx++)
        {
            gameTexts[indx].font = asset;
            gameTexts[indx].text = gameLines[indx][0];
        }
    }

    private void CreateLineLists(string path, string [] fileNames1, string [] fileNames2)
    {
        canvasLines.Clear();
        gameLines.Clear();
        for (int indx = 0; indx < fileNames1.Length; indx++)
        {
            readFromFilePath = Application.streamingAssetsPath + $"/Texts/{path}/" + fileNames1[indx] + ".txt";
            List<string> canvasFileLines = File.ReadAllLines(readFromFilePath).ToList();
            canvasLines.Add(canvasFileLines);
        }

        for (int indx = 0; indx < fileNames2.Length; indx++)
        {
            readFromFilePath = Application.streamingAssetsPath + $"/Texts/{path}/" + fileNames2[indx] + ".txt";
            List<string> gameFileLines = File.ReadAllLines(readFromFilePath).ToList();
            gameLines.Add(gameFileLines);
        }
    }
}
