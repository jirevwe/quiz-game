
public class GameTitlePanel : PanelBase {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GetPlayClick()
    {
        PanelsManager.PanelsInstance.ShowLoadingToGame();
    }
}
