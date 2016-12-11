using UnityEngine;

public class GamePlayButtonScaler : MonoBehaviour {

    RectTransform rect;
    private ScreenOrientation screenOrientation;
    Vector2 screenSize;

    // Use this for initialization
    void Start()
    {
        rect = GetComponent<RectTransform>();
        screenSize = new Vector2(Screen.width, Screen.height);
    }

    // Update is called once per frame
    void Update()
    {
        screenSize.x = Screen.width;
        screenSize.y = Screen.height;
        if (screenSize.y < screenSize.x || Input.deviceOrientation == DeviceOrientation.LandscapeLeft || Input.deviceOrientation == DeviceOrientation.LandscapeRight)
        {
            rect.offsetMin = new Vector2(300, rect.offsetMin.y);
            rect.offsetMax = new Vector2(-300, rect.offsetMax.y);
        }
        else if (screenSize.y > screenSize.x || Input.deviceOrientation == DeviceOrientation.Portrait || Input.deviceOrientation == DeviceOrientation.PortraitUpsideDown)
        {
            rect.offsetMin = new Vector2(50, rect.offsetMin.y);
            rect.offsetMax = new Vector2(-50, rect.offsetMax.y);
        }

        Canvas.ForceUpdateCanvases();
    }
}
