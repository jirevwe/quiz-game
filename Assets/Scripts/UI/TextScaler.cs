using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TextScaler : MonoBehaviour {

    Text text;
    Vector2 screenSize;
    private ScreenOrientation screenOrientation;
    [SerializeField]
    int largerText, smallerText;

    // Use this for initialization
    void Start () {
        text = GetComponent<Text>();
        screenSize = new Vector2(Screen.width, Screen.height);
    }

    // Update is called once per frame
    void Update() {
        screenSize.x = Screen.width;
        screenSize.y = Screen.height;
        if (screenSize.y < screenSize.x || Input.deviceOrientation == DeviceOrientation.LandscapeLeft || Input.deviceOrientation == DeviceOrientation.LandscapeRight)
            text.fontSize = largerText;
        else if (screenSize.y > screenSize.x || Input.deviceOrientation == DeviceOrientation.Portrait || Input.deviceOrientation == DeviceOrientation.PortraitUpsideDown)
            text.fontSize = smallerText;

        Canvas.ForceUpdateCanvases();
    }
}
