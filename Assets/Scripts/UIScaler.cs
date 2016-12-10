using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class UIScaler : MonoBehaviour {

    Text text;
    Vector2 screenSize;
    private ScreenOrientation screenOrientation;

    // Use this for initialization
    void Start () {
        text = GetComponent<Text>();
        screenSize = new Vector2(Screen.width, Screen.height);
    }
	
	// Update is called once per frame
	void Update () {
        screenSize.x = Screen.width;
        if (Input.deviceOrientation == DeviceOrientation.LandscapeLeft || Input.deviceOrientation == DeviceOrientation.LandscapeRight)
            text.fontSize = 44;
        else if (Input.deviceOrientation == DeviceOrientation.Portrait || Input.deviceOrientation == DeviceOrientation.PortraitUpsideDown)
            text.fontSize = 24;

        Canvas.ForceUpdateCanvases();
    }
}
