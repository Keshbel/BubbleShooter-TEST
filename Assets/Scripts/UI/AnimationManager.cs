using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using InitScriptName;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class AnimationManager : MonoBehaviour
{
    public bool playOnEnable = true;
    Dictionary<string, string> parameters;

    void OnEnable()
    {
        //For Windows
        #if UNITY_STANDALONE_WIN
            Screen.SetResolution(360, 540, false);
        #endif

        if (playOnEnable)
        {
            SoundBase.GetInstance().GetComponent<AudioSource>().PlayOneShot(SoundBase.GetInstance().swish[0]);
        }
        
        if (name == "MenuPlay")
        {
            for (int i = 1; i <= 3; i++)
            {
                transform.Find("Image").Find("Star" + i).gameObject.SetActive(false);
            }
            
            int stars = PlayerPrefs.GetInt($"Level.{PlayerPrefs.GetInt("OpenLevel"):000}.StarsCount", 0);
            if (stars > 0)
            {
                for (int i = 1; i <= stars; i++)
                {
                    transform.Find("Image").Find("Star" + i).gameObject.SetActive(true);
                }

            }
            else
            {
                for (int i = 1; i <= 3; i++)
                {
                    transform.Find("Image").Find("Star" + i).gameObject.SetActive(false);
                }

            }

        }

        if (name == "Settings" || name == "MenuPause")
        {
            if (PlayerPrefs.GetInt("Sound") == 0)
                transform.Find("Image/Sound/SoundOff").gameObject.SetActive(true);
            else
                transform.Find("Image/Sound/SoundOff").gameObject.SetActive(false);

            if (PlayerPrefs.GetInt("Music") == 0)
                transform.Find("Image/Music/MusicOff").gameObject.SetActive(true);
            else
                transform.Find("Image/Music/MusicOff").gameObject.SetActive(false);

        }

    }

    public void OnFinished()
    {
        if (name == "MenuComplete")
        {
            StartCoroutine(MenuComplete());
            StartCoroutine(MenuCompleteScoring());
        }
        if (name == "MenuPlay")
        {
            InitScript.Instance.currentTarget = LevelData.GetTarget(PlayerPrefs.GetInt("OpenLevel"));
        }
    }



    IEnumerator MenuComplete()
    {
        for (int i = 1; i <= MainScript.Instance.stars; i++)
        {
            //  SoundBase.Instance.audio.PlayOneShot( SoundBase.Instance.scoringStar );
            transform.Find("Image").Find("Star" + i).gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            SoundBase.GetInstance().GetComponent<AudioSource>().PlayOneShot(SoundBase.GetInstance().hit);
        }
    }
    
    IEnumerator MenuCompleteScoring()
    {
        Text scores = transform.Find("Image").Find("Scores").GetComponent<Text>();
        for (int i = 0; i <= MainScript.Score; i += 500)
        {
            scores.text = "" + i;
            yield return new WaitForSeconds(0.00001f);
        }
        scores.text = "" + MainScript.Score;
    }


    public void PlaySoundButton()
    {
        SoundBase.GetInstance().GetComponent<AudioSource>().PlayOneShot(SoundBase.GetInstance().click);

    }

    public void CloseMenu()
    {
        SoundBase.GetInstance().GetComponent<AudioSource>().PlayOneShot(SoundBase.GetInstance().click);
        
        if (gameObject.name == "MenuPreGameOver")
        {
            ShowGameOver();
        }
        
        if (gameObject.name == "MenuComplete" || gameObject.name == "MenuGameOver")
        {
            SceneManager.LoadSceneAsync(1);
        }

        if (SceneManager.GetActiveScene().name == "Game" || SceneManager.GetActiveScene().name == "game")
        {
            if (GamePlay.Instance.GameStatus == GameState.Pause)
            {
                GamePlay.Instance.GameStatus = GameState.WaitAfterClose;

            }
        }
        SoundBase.GetInstance().GetComponent<AudioSource>().PlayOneShot(SoundBase.GetInstance().swish[1]);

        gameObject.SetActive(false);
    }

    /// <summary>
    /// The method called when the button related to Play on various interfaces is pressed
    /// </summary>
    public void Play()
    {
        SoundBase.GetInstance().GetComponent<AudioSource>().PlayOneShot(SoundBase.GetInstance().click);
        
        //Different GameObjects have different functions depending on the binding
        if (gameObject.name == "MenuGameOver")
        {
            SceneManager.LoadSceneAsync(1);
        }
        else if (gameObject.name == "MenuPlay")
        {
            SceneManager.LoadSceneAsync(2);
        }
    }

    public void PlayTutorial()
    {
        GamePlay.Instance.GameStatus = GameState.Playing;
    }

    public void Next()
    {
        SoundBase.GetInstance().GetComponent<AudioSource>().PlayOneShot(SoundBase.GetInstance().click);
        CloseMenu();
    }


    void ShowGameOver()
    {
        SoundBase.GetInstance().GetComponent<AudioSource>().PlayOneShot(SoundBase.GetInstance().gameOver);

        GameObject.Find("Canvas").transform.Find("MenuGameOver").gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    #region Settings
    
    public void ShowSettings(GameObject menuSettings)
    {
        SoundBase.GetInstance().GetComponent<AudioSource>().PlayOneShot(SoundBase.GetInstance().click);

        menuSettings.SetActive(!menuSettings.activeSelf);
    }

    public void SoundOff(GameObject go)
    {
        if (!go.activeSelf)
        {
            SoundBase.GetInstance().GetComponent<AudioSource>().volume = 0;
            InitScript.sound = false;

            go.SetActive(true);
        }
        else
        {
            SoundBase.GetInstance().GetComponent<AudioSource>().volume = 1;
            InitScript.sound = true;

            go.SetActive(false);

        }
        PlayerPrefs.SetInt("Sound", (int)SoundBase.GetInstance().GetComponent<AudioSource>().volume);
        PlayerPrefs.Save();

    }
    public void MusicOff(GameObject go)
    {
        if (!go.activeSelf)
        {
            GameObject.Find("Music").GetComponent<AudioSource>().volume = 0;
            InitScript.music = false;

            go.SetActive(true);
        }
        else
        {
            GameObject.Find("Music").GetComponent<AudioSource>().volume = 1;
            InitScript.music = true;

            go.SetActive(false);

        }
        PlayerPrefs.SetInt("Music", (int)GameObject.Find("Music").GetComponent<AudioSource>().volume);
        PlayerPrefs.Save();

    }

    public void Info()
    {
        if (SceneManager.GetActiveScene().name == "Map" || SceneManager.GetActiveScene().name == "menu")
            GameObject.Find("Canvas").transform.Find("Tutorial").gameObject.SetActive(true);
        else
            GameObject.Find("Canvas").transform.Find("PreTutorial").gameObject.SetActive(true);
    }

    public void Quit()
    {
        if (SceneManager.GetActiveScene().name == "Game" || SceneManager.GetActiveScene().name == "game")
            SceneManager.LoadSceneAsync(1);
        else if (SceneManager.GetActiveScene().name == "Map" || SceneManager.GetActiveScene().name == "map")
            SceneManager.LoadSceneAsync(0);
        else
            Application.Quit();
    }
    
    #endregion
}
