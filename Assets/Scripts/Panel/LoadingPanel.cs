using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class LoadingPanel : PanelBase {

    public Text loadingText;
    public Text errorText;

	// Use this for initialization
	void Start () {
        StartLoad();
    }

    IEnumerator Load()
    {
        yield return new WaitForSeconds(.5f);
        if (loadingText.text != "LOADING...")
        {
            loadingText.text += ".";
        }
        else
        {
            loadingText.text = "LOADING";
        }
        StartCoroutine(Load());
    }

    public void StartLoad()
    {
        StartCoroutine(Load());
    }

    public void StopLoad()
    {
        StopCoroutine(Load());
    }

    public override void OnBackClick()
    {
        //do nothing
    }
}
