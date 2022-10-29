using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    public int number;
    public Text label;
    public GameObject lockimage;

    // Use this for initialization
    void Start()
    {
        if (PlayerPrefs.GetInt("Score" + (number - 1)) > 0 || number == 1 || number == 16)
        {
            lockimage.gameObject.SetActive(false);
            if (number != 16)
                label.text = "" + number;
            else
            {
                label.text = "Random";
            }
            
        }

        int stars = PlayerPrefs.GetInt($"Level.{number:000}.StarsCount", 0);

        if (stars > 0)
        {
            for (int i = 1; i <= stars; i++)
            {
                transform.Find("Star" + i).gameObject.SetActive(true);
            }

        }

    }

    public void StartLevel()
    {
        InitScriptName.InitScript.Instance.OnLevelClicked(number);
    }
}
