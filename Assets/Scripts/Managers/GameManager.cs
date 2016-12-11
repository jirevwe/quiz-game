using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    List<Question> questions = new List<Question>();
    [SerializeField]Question currentQuestion;
    [HideInInspector]
    public int answerIndex = -1, score = 0, highScore = 0, questionCount = 0, connectionTries = 0;

    [Range(-1, 2)]
    [HideInInspector]
    public int difficulty = 0;
    [Range(-3, 6)]
    //[SerializeField]
    int currentScoreCombo = 0;

    [HideInInspector]
    public bool done = false, init = false;

    string data = "";

    float currentQuestionTime = 0f;
    [SerializeField]
    float timePerQuestion = 5f;
    
    [SerializeField]
    Slider timeRemainingSlider;

    public AudioClip winSoundClip;

    DatabaseReference reference;

    void Start ()
    {
        if (Instance == null)
            Instance = this;
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://digital-air-test.firebaseio.com/");
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        done = true;
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        //keep trying to get data from the online server
        if (!init)
            InvokeRepeating("Init", 2, 2);
        else
            CancelInvoke("Init");
    }

    void Update ()
    {
        GetQuestionTime();
    }

    public void GetQuestionTime()
    {
        if(currentQuestionTime - Time.time >= 0 && PanelsManager.PanelsInstance.gamePlayPanel.myPanel.alpha == 1f)
            timeRemainingSlider.value = currentQuestionTime - Time.time;
    }

    public void Init()
    {
        if (GamePlayPanel.Instance != null && reference != null && !init)
        {
            reference.GetValueAsync().ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    Logger.e("An error occured");
                }
                else if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;
                    data = snapshot.GetRawJsonValue();

                    var json = JSONObject.Create(data);

                    //get the difficulty
                    difficulty = (int)json["game"]["difficulty"].f;

                    //get the highscore
                    highScore = (int)json["game"]["highscore"].f;

                    //get the questions
                    json = json["questions"];

                    for (int i = 0; i < json.list.Count; i++)
                    {
                        Question ques = new Question();

                        ques.question = json[i]["question"].str;
                        ques.answerIndex = (int)json[i]["answer"].f;
                        ques.difficulty = (int)json[i]["difficulty"].f;

                        for (int j = 0; j < json[i]["options"].list.Count; j++)
                        {
                            ques.options.Add(json[i]["options"].list[j].str);
                        }

                        questions.Add(ques);
                    }

                    GetNextQuextion(difficulty);
                    GamePlayPanel.Instance.question.text = currentQuestion.question;

                    for (int j = 0; j < currentQuestion.options.Count; j++)
                    {
                        GamePlayPanel.Instance.buttons[j].transform.GetChild(0).GetComponent<Text>().text = currentQuestion.options[j];
                    }

                    GamePlayPanel.Instance.score.text = score.ToString();
                    init = true;
                }
            });
        }
    }

    public void GetClick()
    {
        //check if the player got the question
        if(answerIndex == currentQuestion.answerIndex)
        {
            //reset the combo if the player failed the last question
            if (currentScoreCombo < 0)
                currentScoreCombo = 0;
            
            //increment the combo
            ++currentScoreCombo;

            //update the player score based oncthe difficulty and how fast they answer the question
            score += (2 + difficulty) * (int)timeRemainingSlider.value;

            //win condition
            if (currentScoreCombo >= 5)
            {
                //update the highscore
                if (score > highScore)
                {
                    highScore = score;
                    reference.Child("/game/highscore").SetValueAsync(score);
                }

                PlaySound(winSoundClip, 1f);
                ScorePanel.Instance.scoreText.text = "score: " + score.ToString();
                ScorePanel.Instance.highScoreText.text = "highscore: " + highScore.ToString();
                StartCoroutine(PanelsManager.PanelsInstance.ShowGameScore());
            }
        }
        else
        {
            //reset the combo if the player passed the last question
            if (currentScoreCombo > 0)
                currentScoreCombo = 0;

            //subtract the combo
            --currentScoreCombo;

            //penalize the player for missing a question based on the difficulty and how slow they answer the question
            if(score >= 1)
                score -= (2 - difficulty) * (int)timeRemainingSlider.value;
            
            //adjust score value -- cannot be negative
            if (score < 0)
                score = 0;

            //lose condition
            if (currentScoreCombo <= -3)
            {
                //update the highscore
                if (score > highScore)
                {
                    highScore = score;
                    reference.Child("/game/highscore").SetValueAsync(score);
                }

                ScorePanel.Instance.scoreText.text = "score: " + score.ToString();
                ScorePanel.Instance.highScoreText.text = "highscore: " + highScore.ToString();
                StartCoroutine(PanelsManager.PanelsInstance.ShowGameScore());
            }
        }

        difficulty = GetNextQuestionDifficulty(currentScoreCombo, currentQuestion.difficulty);

        //save the new difficulty
        reference.Child("/game/difficulty").SetValueAsync(difficulty);

        GetNextQuextion(difficulty);

        GamePlayPanel.Instance.score.text = score.ToString();
    }

    public int GetNextQuestionDifficulty(int combo, int _difficulty)
    {
        var newDifficulty = combo + _difficulty;

        if (newDifficulty == 0)     //decrease or increase the difficulty
            newDifficulty = 0;
        else if (newDifficulty < 0) //reduce the difficulty
            newDifficulty = -1;
        else if (newDifficulty > 0) //increase the difficulty
            newDifficulty = 1;

        return newDifficulty;
    }

    //get next question of the required difficulty
    public void GetNextQuextion(int difficulty)
    {
        //reset the time
        currentQuestionTime = Time.time + timePerQuestion;
        var questionsForDifficulty = questions.FindAll(n => n.difficulty == difficulty);
        currentQuestion =  questionsForDifficulty[Random.Range(0, questionsForDifficulty.Count)];

        //set ui elements to show new question stuff
        GamePlayPanel.Instance.question.text = currentQuestion.question;

        for (int i = 0; i < currentQuestion.options.Count; i++)
        {
            GamePlayPanel.Instance.buttons[i].transform.GetChild(0).GetComponent<Text>().text = currentQuestion.options[i];
        }
    }

    public void ResetGameForNewSession()
    {
        //get next question
        GetNextQuextion(difficulty);
        //reset game score
        score = 0;
        //set game score text
        GamePlayPanel.Instance.score.text = score.ToString();
        //reset the game combo count
        currentScoreCombo = 0;
    }

    public void PlaySound(AudioClip clip, float volume = .5f)
    {
        AudioSource.PlayClipAtPoint(clip, transform.position, volume);
    }
}
