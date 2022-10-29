using UnityEngine;
using UnityEngine.UI;
using InitScriptName;
using Lean.Localization;
using UnityEngine.SceneManagement;

public class Counter : MonoBehaviour
{
    Text label;
    
    // Use this for initialization
    void Start()
    {
        label = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (name == "Moves")
        {
            label.text = "" + LevelData.LimitAmount;
            if (LevelData.LimitAmount <= 5 && GamePlay.Instance.GameStatus == GameState.Playing)
            {
                label.color = Color.red;
                if (!GetComponent<Animation>().isPlaying)
                {
                    GetComponent<Animation>().Play();
                    SoundBase.GetInstance().GetComponent<AudioSource>().PlayOneShot(SoundBase.GetInstance().alert);
                }
            }
        }
        if (name == "Scores" || name == "Score")
        {
            label.text = "" + MainScript.Score;
        }
        if (name == "Level")
        {
            label.text = "" + PlayerPrefs.GetInt("OpenLevel");
        }
        if (name == "Target")
        {
            if (LevelData.mode == ModeGame.Vertical)
                label.text = "" + Mathf.Clamp(MainScript.Instance.TargetCounter1, 0, 6) + "/6";
            else if (LevelData.mode == ModeGame.Rounded)
                label.text = "" + Mathf.Clamp(MainScript.Instance.TargetCounter1, 0, 1) + "/1";
            else if (LevelData.mode == ModeGame.Animals)
                label.text = "" + MainScript.Instance.TargetCounter1 + "/" + MainScript.Instance.TotalTargets;
        }
        
        if (name == "TargetDescription")
        {
            label.text = "" + GetTarget();
        }
    }

    string GetTarget()
    {
        if (SceneManager.GetActiveScene().name == "Map")
        {
            if (LeanLocalization.GetFirstCurrentLanguage() == "English")
            {
                switch (InitScript.Instance.currentTarget)
                {
                    case Target.Top:
                        return "Pop the bubbles and harvest the sun";
                    case Target.Chicken:
                        return "Rescue cute chicks";
                }
            }
            else
            {
                switch (InitScript.Instance.currentTarget)
                {
                    case Target.Top:
                        return "Лопайте пузыри и собирайте очки";
                    case Target.Chicken:
                        return "Спасите милых цыплят";
                }
            }
        }
        else
        {
            return LeanLocalization.GetFirstCurrentLanguage() == "English" ? "Game over!" : "Игра закончена!";
        }
        return "";
    }
}
