using UnityEngine.UI;
using DG.Tweening;
using UnityEngine;

public class GameButton : MonoBehaviour {

    public AudioClip clickSound;

    void Start()
    {
        var smallerScale = new Vector3(0.9f, 0.9f, 0.9f);

        //add tweening effect for all the buttons
        GetComponent<Button>().onClick.AddListener(() =>
        {
            GameManager.Instance.PlaySound(clickSound, 1f);
            DOTween.To(() => transform.localScale, a => transform.localScale = a, smallerScale, 0.1f).OnComplete(()=> 
            {
                transform.localScale = Vector3.one;
            });
        });
    }
}
