using UnityEngine.UI;
using UnityEngine;

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
        if (Input.GetKeyUp(KeyCode.Escape) && myPanel.alpha == 1)
            OnBackClick();
    }

    public void GetReplayClick()
    {
        PanelsManager.PanelsInstance.ShowLoadingToGame();
    }

    public void GetMenuClick()
    {
        PanelsManager.PanelsInstance.ShowLoadingToMenu();
    }

    public override void OnBackClick()
    {
        //go back to main menu
        PanelsManager.PanelsInstance.ShowLoadingToMenu();
    }
}
