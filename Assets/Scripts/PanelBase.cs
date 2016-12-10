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
        if (this.gameObject.activeInHierarchy)
        {
            if (delay == 0)
            {
                DOTween.To(() => this.myPanel.alpha, a => this.myPanel.alpha = a, 0.0f, time).OnComplete(this.HideInstantly);
            }
            else
            {
                StartCoroutine(this.HideSmoothlyIE(time, delay));
            }
        }
    } // HideSmoothly

    private IEnumerator HideSmoothlyIE(float time, float delay)
    {
        if (this.gameObject.activeInHierarchy)
        {
            yield return new WaitForSeconds(delay);
            DOTween.To(() => this.myPanel.alpha, a => this.myPanel.alpha = a, 0.0f, time).OnComplete(this.HideInstantly);
        }
    } // HideSmoothly

    public void ShowSmoothly(float time = 0.2f, bool setActive = true)
    {
        if (setActive)
        {
            this.gameObject.SetActive(true); 
        }
        if (this.gameObject.activeInHierarchy)
        {
            DOTween.To(() => this.myPanel.alpha, a => this.myPanel.alpha = a, 1.0f, time).OnComplete(this.ShowInstantly);
        }
    } // ShowSmoothly

    public void HideInstantly()
    {
        this.gameObject.SetActive(false);
        this.myPanel.interactable = false;
        this.myPanel.alpha = 0.0f;
        this.PanelClosed();
    } // HideInstantly

    public void ShowInstantly()
    {
        this.gameObject.SetActive(true);
        this.myPanel.alpha = 1.0f;
        this.myPanel.interactable = true;
        this.PanelOpened();
    } // ShowInstantly

    public virtual void PanelOpened()
    {
        Invoke("AllowClick", 0.1f);
    } // PanelOpened

    public virtual void PanelClosed()
    {
        this.panelClickAllowed = false;
        this.myPanel.interactable = false;
    } // PanelOpened

    void AllowClick()
    {
        this.panelClickAllowed = true;
        this.myPanel.interactable = true;

    } // AllowClick

} // PanelBase