using UnityEngine;

public class GameTitlePanel : PanelBase {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.Escape) && myPanel.alpha == 1)
            OnBackClick();
    }

    public void GetPlayClick()
    {
        PanelsManager.PanelsInstance.ShowLoadingToGame();
    }

    public override void OnBackClick()
    {
        //exit game
        Application.Quit();
    }
}
