using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighScoreHandler : MonoBehaviour
{

    List<HighScoreEntry> highScoresList = new List<HighScoreEntry>();
    public int max_spots = 5;
    public string fileName;
    public int cur_score;



    private void Start()
    {
        loadHighScores();
        
    }
    private void loadHighScores()
    {
        highScoresList = File_Handler.readListFromJson<HighScoreEntry>(fileName);
        while(highScoresList.Count > max_spots)
        {
            highScoresList.RemoveAt(max_spots);
        }
    }

    private void saveHighScores()
    {
        File_Handler.saveListToJson<HighScoreEntry>(highScoresList,fileName);
    }

    public void addHighScoresIfPossible(HighScoreEntry entry)
    {
        for(int i = 0; i < max_spots; i++)
        {
            if(i >= highScoresList.Count||entry.points > highScoresList[i].points)
            {
                highScoresList.Insert(i,entry);

                while(highScoresList.Count > max_spots)
                {
                    highScoresList.RemoveAt(max_spots);
                }
                saveHighScores();
                break;
            }
            
        }
    }

    public void onClick(string initials, int score)
    {
        addHighScoresIfPossible(new HighScoreEntry(initials, score));
    }


}
