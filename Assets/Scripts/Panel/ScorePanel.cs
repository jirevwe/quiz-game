using UnityEngine;
using UnityEngine.UI;

public class ScorePanel : PanelBase {
    public Text scoreText, highScoreText;

    public static ScorePanel Instance;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GetReplayClick()
    {
        PanelsManager.PanelsInstance.ShowLoadingToGame();
    }

    public void GetMenuClick()
    {
        PanelsManager.PanelsInstance.ShowLoadingToMenu();
    }
}
