using Lean.Localization;
using TMPro;
using UnityEngine;

public class ChangeLanguage : MonoBehaviour
{
    public TMP_Dropdown dropdown;

    private void Awake()
    {
        if (!dropdown)
            dropdown = GetComponent<TMP_Dropdown>();
        
        dropdown.onValueChanged.AddListener(Change);

        var currentLanguageInt = 0;
        if (LeanLocalization.GetFirstCurrentLanguage() == "Russian")
            currentLanguageInt = 1;
        
        Change(currentLanguageInt);
        dropdown.value = currentLanguageInt;
    }

    private void Change(int value)
    {
        switch (value)
        {
            case 0:
                LeanLocalization.SetCurrentLanguageAll("English");
                break;
            case 1:
                LeanLocalization.SetCurrentLanguageAll("Russian");
                break;
        }
    }

}
