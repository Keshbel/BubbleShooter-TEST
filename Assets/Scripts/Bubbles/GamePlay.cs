using UnityEngine;
using System.Collections;

public enum GameState
{
    Playing,
    Highscore,
    GameOver,
    Pause,
    Win,
    WaitForPopup,
    WaitAfterClose,
    BlockedGame,
    Tutorial,
    PreTutorial,
    WaitForChicken
}


public class GamePlay : MonoBehaviour
{
    public static GamePlay Instance;
    private GameState gameStatus;
    bool winStarted;
    public GameState GameStatus
    {
        get { return GamePlay.Instance.gameStatus; }
        set
        {
            if (GamePlay.Instance.gameStatus != value)
            {
                if (value == GameState.Win)
                {
                    if (!winStarted)
                        StartCoroutine(WinAction());
                }
                else if (value == GameState.GameOver)
                {
                    StartCoroutine(LoseAction());
                }
                else if (value == GameState.Tutorial && gameStatus != GameState.Playing)
                {
                    value = GameState.Playing;
                    gameStatus = value;
                }
                else if (value == GameState.PreTutorial && gameStatus != GameState.Playing)
                {
                    ShowPreTutorial();
                }
            }
            if (value == GameState.WaitAfterClose)
                StartCoroutine(WaitAfterClose());

            if (value == GameState.Tutorial)
            {
                if (gameStatus != GameState.Playing)
                    GamePlay.Instance.gameStatus = value;
            }
            GamePlay.Instance.gameStatus = value;
        }
    }

    // Use this for initialization
    void Start()
    {
        Instance = this;
    }

    void Update()
    {
        if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor)
        {
            if (Input.GetKey(KeyCode.W)) GamePlay.Instance.GameStatus = GameState.Win;
            if (Input.GetKey(KeyCode.L)) { LevelData.LimitAmount = 0; GamePlay.Instance.GameStatus = GameState.GameOver; }
            if (Input.GetKey(KeyCode.D)) MainScript.Instance.destroyAllballs();
            if (Input.GetKey(KeyCode.M)) LevelData.LimitAmount = 1;
        }
    }

    // Update is called once per frame
    IEnumerator WinAction()
    {
        winStarted = true;
        GameObject.Find("Canvas").transform.Find("LevelCleared").gameObject.SetActive(true);
        SoundBase.GetInstance().GetComponent<AudioSource>().PlayOneShot(SoundBase.GetInstance().winSound);
        yield return new WaitForSeconds(1f);
        if (LevelData.mode == ModeGame.Vertical)
        {
            yield return new WaitForSeconds(1f);
            GameObject.Find("CanvasPots").transform.Find("Black").gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }

        foreach (GameObject item in GameObject.FindGameObjectsWithTag("Ball"))
        {
            item.GetComponent<ball>().StartFall();

        }
        Transform b = GameObject.Find("-Ball").transform;
        ball[] balls = GameObject.Find("-Ball").GetComponentsInChildren<ball>();
        foreach (ball item in balls)
        {
            item.StartFall();
        }

        while (LevelData.LimitAmount > 0)
        {
            if (MainScript.Instance.boxCatapult.GetComponent<Grid>().Busy != null)
            {
                LevelData.LimitAmount--;
                ball ball = MainScript.Instance.boxCatapult.GetComponent<Grid>().Busy.GetComponent<ball>();
                MainScript.Instance.boxCatapult.GetComponent<Grid>().Busy = null;
                ball.transform.parent = MainScript.Instance.Balls;
                ball.tag = "Ball";
                ball.PushBallAFterWin();
            }
            yield return new WaitForEndOfFrame();
        }
        foreach (ball item in balls)
        {
            if (item != null)
                item.StartFall();
        }
        yield return new WaitForSeconds(2f);
        while (GameObject.FindGameObjectsWithTag("Ball").Length > 0)
        {
            yield return new WaitForSeconds(0.1f);
        }
        SoundBase.GetInstance().GetComponent<AudioSource>().PlayOneShot(SoundBase.GetInstance().aplauds);
        if (PlayerPrefs.GetInt($"Level.{MainScript.Instance.currentLevel:000}.StarsCount", 0) < MainScript.Instance.stars)
            PlayerPrefs.SetInt($"Level.{MainScript.Instance.currentLevel:000}.StarsCount", MainScript.Instance.stars);


        if (PlayerPrefs.GetInt("Score" + MainScript.Instance.currentLevel) < MainScript.Score)
        {
            PlayerPrefs.SetInt("Score" + MainScript.Instance.currentLevel, MainScript.Score);

        }
        GameObject.Find("Canvas").transform.Find("LevelCleared").gameObject.SetActive(false);
        GameObject.Find("Canvas").transform.Find("MenuComplete").gameObject.SetActive(true);

    }
    
    void ShowPreTutorial()
    {
        GameObject.Find("Canvas").transform.Find("PreTutorial").gameObject.SetActive(true);
    }

    IEnumerator LoseAction()
    {
        SoundBase.GetInstance().GetComponent<AudioSource>().PlayOneShot(SoundBase.GetInstance().OutOfMoves);
        GameObject.Find("Canvas").transform.Find("OutOfMoves").gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        GameObject.Find("Canvas").transform.Find("OutOfMoves").gameObject.SetActive(false);
        if (LevelData.LimitAmount <= 0)
        {
            SoundBase.GetInstance().GetComponent<AudioSource>().PlayOneShot(SoundBase.GetInstance().gameOver);
            GameObject.Find("Canvas").transform.Find("MenuGameOver").gameObject.SetActive(true);
        }
        yield return new WaitForSeconds(0.1f);

    }

    IEnumerator WaitAfterClose()
    {
        yield return new WaitForSeconds(1);
        GameStatus = GameState.Playing;
    }
}
