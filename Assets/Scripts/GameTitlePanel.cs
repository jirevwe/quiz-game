using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTitlePanel : PanelBase {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GetPlayClick()
    {
        Panels.PanelsInstance.ShowLoadingToGame();
    }
}
