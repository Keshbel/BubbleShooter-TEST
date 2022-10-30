using UnityEngine;

public enum Target
{
    Top = 0,
    Chicken
}

namespace InitScriptName
{
    public class InitScript : MonoBehaviour
    {
        public static InitScript Instance;
        private int _levelNumber = 1;
        private int _starsCount = 1;
        private bool _isShow;
        public static int openLevel;

        public static bool sound = false;
        public static bool music = false;

        int messCount;

        public Target currentTarget;

        public void Awake()
        {
            Instance = this;
            if (Application.loadedLevelName == "Map")
            {
                if (GameObject.Find("Canvas").transform.Find("MenuPlay").gameObject.activeSelf)
                    GameObject.Find("Canvas").transform.Find("MenuPlay").gameObject.SetActive(false);

            }


            //When the game is first run
            if (PlayerPrefs.GetInt("Lauched") == 0)
            {
                //Store data with PlayerPrefs
                PlayerPrefs.SetInt("Lauched", 1);
                PlayerPrefs.SetInt("Music", 1);
                PlayerPrefs.SetInt("Sound", 1);
                PlayerPrefs.Save();
            }

            GameObject.Find("Music").GetComponent<AudioSource>().volume = PlayerPrefs.GetInt("Music");
            SoundBase.GetInstance().GetComponent<AudioSource>().volume = PlayerPrefs.GetInt("Sound");
        }

        void Start()
        {
            if (Application.platform == RuntimePlatform.WindowsEditor)
            {
                Application.targetFrameRate = 60;
            }
        }


        #region Level selection related functions

        public int LoadLevelStarsCount(int level)
        {
            return level > 10 ? 0 : (level % 3 + 1);
        }

        public void SaveLevelStarsCount(int level, int starsCount)
        {
            Debug.Log($"Stars count {starsCount} of level {level} saved.");
        }

        public void OnLevelClicked(int number)
        {
            if (!GameObject.Find("Canvas").transform.Find("MenuPlay").gameObject.activeSelf)
            {
                PlayerPrefs.SetInt("OpenLevel", number);
                PlayerPrefs.Save();
                openLevel = number;
                currentTarget = LevelData.GetTarget(number);
                GameObject.Find("Canvas").transform.Find("MenuPlay").gameObject.SetActive(true);
            }
        }
        #endregion
    }
}