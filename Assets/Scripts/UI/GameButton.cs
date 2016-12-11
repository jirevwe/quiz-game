using UnityEngine.UI;
using DG.Tweening;
using UnityEngine;

public class GameButton : MonoBehaviour {

    void Start()
    {
        var smallerScale = new Vector3(0.9f, 0.9f, 0.9f);

        //add tweening effect for all the buttons
        GetComponent<Button>().onClick.AddListener(() =>
        {
            DOTween.To(() => transform.localScale, a => transform.localScale = a, smallerScale, 0.1f).OnComplete(()=> 
            {
                transform.localScale = Vector3.one;
            });
        });
    }
}
