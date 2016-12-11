using UnityEngine;
using System.Collections;
using DG.Tweening;

[RequireComponent(typeof(CanvasGroup))]
public abstract class PanelBase : MonoBehaviour {

    public CanvasGroup myPanel;
    [HideInInspector]
    public bool panelClickAllowed = false;

    public void HideSmoothly(float time = 0.2f, float delay = 0)
    {
        if (delay == 0)
            DOTween.To(() => myPanel.alpha, a => myPanel.alpha = a, 0.0f, time).OnComplete(HideInstantly);
        else
            StartCoroutine(HideSmoothlyIE(time, delay));
    } // HideSmoothly

    private IEnumerator HideSmoothlyIE(float time, float delay)
    {
        yield return new WaitForSeconds(delay);
        DOTween.To(() => myPanel.alpha, a => myPanel.alpha = a, 0.0f, time).OnComplete(HideInstantly);
    } // HideSmoothly

    public void ShowSmoothly(float time = 0.2f)
    {
        DOTween.To(() => myPanel.alpha, a => myPanel.alpha = a, 1.0f, time).OnComplete(ShowInstantly);
    } // ShowSmoothly

    public void HideInstantly()
    {
        myPanel.interactable = false;
        myPanel.alpha = 0.0f;
        myPanel.blocksRaycasts = false;
        PanelClosed();
    } // HideInstantly

    public void ShowInstantly()
    {
        myPanel.alpha = 1.0f;
        myPanel.blocksRaycasts = true;
        myPanel.interactable = true;
        PanelOpened();
    } // ShowInstantly

    public virtual void PanelOpened()
    {
        Invoke("AllowClick", 0.1f);
        //Logger.d(myPanel.gameObject.name + " opened");
    } // PanelOpened

    public virtual void PanelClosed()
    {
        panelClickAllowed = false;
        myPanel.interactable = false;
        //Logger.d(myPanel.gameObject.name + " closed");
    } // PanelOpened

    void AllowClick()
    {
        panelClickAllowed = true;
        myPanel.interactable = true;
    } // AllowClick
} // PanelBase