using System.Collections.Generic;

[System.Serializable]
public class Question {
    public string question;
    public int difficulty;
    public List<string> options = new List<string>();
    public int answerIndex;
}