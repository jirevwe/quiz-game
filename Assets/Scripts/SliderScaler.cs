using UnityEngine;

public class SliderScaler : MonoBehaviour {

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
    void Update () {
        screenSize.x = Screen.width;
        if (Input.deviceOrientation == DeviceOrientation.LandscapeLeft || Input.deviceOrientation == DeviceOrientation.LandscapeRight)
        {
            rect.offsetMin = new Vector2(500, -70);
            rect.offsetMax = new Vector2(-500, 0);
        }
        else if (Input.deviceOrientation == DeviceOrientation.Portrait || Input.deviceOrientation == DeviceOrientation.PortraitUpsideDown)
        {
            rect.offsetMin = new Vector2(100, -70);
            rect.offsetMax = new Vector2(-100, 0);
        }

        Canvas.ForceUpdateCanvases();
    }
}
