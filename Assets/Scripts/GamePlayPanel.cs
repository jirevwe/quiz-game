using System.Collections.Generic;
using UnityEngine.UI;

public class GamePlayPanel : PanelBase {

    public Text question;
    public List<Button> buttons;
    public Text score;

    public static GamePlayPanel Instance;

    void Awake () {
        if (Instance == null)
            Instance = this;
    }
	
	void Update () {
		
	}

    public void SetObjecive_1()
    {
        GameManager.Instance.answerIndex = 0;
        GameManager.Instance.GetClick();
    }

    public void SetObjecive_2()
    {
        GameManager.Instance.answerIndex = 1;
        GameManager.Instance.GetClick();
    }

    public void SetObjecive_3()
    {
        GameManager.Instance.answerIndex = 2;
        GameManager.Instance.GetClick();
    }

    public void SetObjecive_4()
    {
        GameManager.Instance.answerIndex = 3;
        GameManager.Instance.GetClick();
    }

    public void GetMenuClick()
    {
        Panels.PanelsInstance.ShowLoadingToMenu();
    }
}
