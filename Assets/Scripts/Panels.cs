using UnityEngine;

public class Panels : MonoBehaviour {
    public Canvas canvas;

    void Awake()
    {
        
    }

    void Start()
    {
        
    }

    public void ShowLoadingSmoothlyInDelay(float delay)
    {
        Invoke("ShowLoadingSmoothly", delay);
    }

    public void ShowLoadingSmoothly()
    {
        
    }
} // Panels