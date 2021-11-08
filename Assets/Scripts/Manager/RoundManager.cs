using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class RoundManager : MonoBehaviour
{
    public string filePath;
    public EnemyManager manager;
    public Text totalRounds;

    private StreamReader reader;
    private string[] roundData;
    private string[] bubbleData;
    [HideInInspector]
    public int roundIndex = 0;

    private void Awake()
    {
        reader = new StreamReader(filePath);
        roundData = reader.ReadToEnd().Split('\n');
        reader.Close();
    }

    private void Start()
    {
        totalRounds.text = roundData.Length.ToString();
    }

    public void NewRoundStart()
    {
        bubbleData = roundData[roundIndex].Split(' ');
        roundIndex++;
        CreateBubble();
    }

    private void CreateBubble()
    {
        for(int i = 1; i < bubbleData.Length; i += 2)
        {
            switch (bubbleData[i])
            {
                case "G":
                    manager.BubbleDataIn(0, int.Parse(bubbleData[i + 1]));
                    break;
                case "F":
                    manager.BubbleDataIn(1, int.Parse(bubbleData[i + 1]));
                    break;
                case "T":
                    manager.BubbleDataIn(2, int.Parse(bubbleData[i + 1]));
                    break;
                case "V":
                    manager.BubbleDataIn(3, int.Parse(bubbleData[i + 1]));
                    break;
                case "PG":
                    manager.BubbleDataIn(4, int.Parse(bubbleData[i + 1]));
                    break;
                case "PF":
                    manager.BubbleDataIn(5, int.Parse(bubbleData[i + 1]));
                    break;
                case "PT":
                    manager.BubbleDataIn(6, int.Parse(bubbleData[i + 1]));
                    break;
                case "PV":
                    manager.BubbleDataIn(7, int.Parse(bubbleData[i + 1]));
                    break;
                case "B":
                    manager.BubbleDataIn(8, int.Parse(bubbleData[i + 1]));
                    break;
            }
        }
    }
}
