using System;

[Serializable]
public class HighScoreEntry 
{
    public string playerName;
    public int points;
    public HighScoreEntry(string name,int points)
    {
        playerName = name;
        this.points = points;
    }
}
