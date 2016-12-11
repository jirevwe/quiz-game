using System.Collections;
using UnityEngine;

public class PanelsManager : MonoBehaviour {

    public static PanelsManager PanelsInstance;

    public LoadingPanel loadingPanel;
    public GamePlayPanel gamePlayPanel;
    public GameTitlePanel gameTitlePanel;
    public ScorePanel scorePanel;

    void Start()
    {
        PanelsInstance = this;

        StartCoroutine(ShowMenu(.1f));
    }

    public void ShowLoadingToMenu()
    {
        loadingPanel.ShowSmoothly();

        gameTitlePanel.HideSmoothly();
        gamePlayPanel.HideSmoothly();
        scorePanel.HideSmoothly();

        StartCoroutine(ShowMenu(1f));
    }

    public void ShowLoadingToGame()
    {
        loadingPanel.ShowSmoothly();

        gameTitlePanel.HideSmoothly();
        gamePlayPanel.HideSmoothly();
        scorePanel.HideSmoothly();

        StartCoroutine(StartGame());
    }

    public void GoToGame()
    {
        gamePlayPanel.ShowSmoothly();
        GameManager.Instance.ResetGameForNewSession();

        gameTitlePanel.HideSmoothly();
        scorePanel.HideSmoothly();
        loadingPanel.HideSmoothly();
    }

    public IEnumerator ShowMenu(float time)
    {
        yield return new WaitForSeconds(time);
        gameTitlePanel.ShowSmoothly();

        scorePanel.HideSmoothly();
        loadingPanel.HideSmoothly();
        gamePlayPanel.HideSmoothly();
    }

    public IEnumerator ShowGameScore()
    {
        yield return new WaitForSeconds(0.1f);
        scorePanel.ShowSmoothly();

        gamePlayPanel.HideSmoothly();
        loadingPanel.HideSmoothly();
        gamePlayPanel.HideSmoothly();
    }

    //gives the game 20 seconds max to retrieve data from the server and parse it.
    public IEnumerator StartGame()
    {
        yield return new WaitForSeconds(5f);

        if (!GameManager.Instance.init && GameManager.Instance.connectionTries >= 3) //no connection after 15 seconds
        {
            //go back to the main menu and reset the connection info
            StartCoroutine(ShowMenu(0f));
            StopCoroutine(StartGame());                          //stop retrying the connection
            loadingPanel.errorText.gameObject.SetActive(false);
            GameManager.Instance.connectionTries = 0;
        }
        else if(GameManager.Instance.init)                      //connection has been established
        {
            GoToGame();
            loadingPanel.errorText.gameObject.SetActive(false);
            StopCoroutine(StartGame());                        //stop retrying the connection
            GameManager.Instance.connectionTries = 0;
        }
        else if (!GameManager.Instance.init && GameManager.Instance.connectionTries <= 5)
        {
            ++GameManager.Instance.connectionTries;
            StartCoroutine(StartGame());                       //allow the game to keep retrying the connection
        }

        if (!GameManager.Instance.init && GameManager.Instance.connectionTries >= 2) //no connection after 10 seconds
        {
            loadingPanel.errorText.gameObject.SetActive(true);
        }
    }
} // Panels