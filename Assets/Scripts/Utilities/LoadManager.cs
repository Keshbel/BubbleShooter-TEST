using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadManager : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void SceneLoad(int index)
    {
        var soundBase = SoundBase.GetInstance();
        
        //проигрывание звука клика
        soundBase.GetComponent<AudioSource>().PlayOneShot(soundBase.click);
        //загрузка сцены с определенным индексом
        SceneManager.LoadSceneAsync(index);
    }
}
