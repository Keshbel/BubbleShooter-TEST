using UnityEngine;
using System.Collections;
using Lean.Localization;
using UnityEngine.UI;

public class Perfect : MonoBehaviour
{
    public Sprite[] images;
    public Sprite[] imagesRu;
  
    // Use this for initialization
    void OnEnable()
    {
        if (LeanLocalization.GetFirstCurrentLanguage() == "English")
            GetComponent<Image>().sprite = images[Random.Range(0, images.Length)];
        else
        {
            GetComponent<Image>().sprite = imagesRu[Random.Range(0, imagesRu.Length)];
        }
        GetComponent<Image>().SetNativeSize();
        StartCoroutine(PerfectAction());
    }

    IEnumerator PerfectAction()
    {
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
    }
}
