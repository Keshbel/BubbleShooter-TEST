using UnityEngine;

public class ButtonX : MonoBehaviour 
{
    void OnMouseDown()
    {
        if (name == "Change" && GamePlay.Instance.GameStatus == GameState.Playing)
        {
            MainScript.Instance.ChangeBoost();
        }

    }
	
	// Update is called once per frame
	void OnPress (bool press) 
    {
        if (press) return;
 	}
}
