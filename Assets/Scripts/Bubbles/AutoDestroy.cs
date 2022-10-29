using UnityEngine;

public class AutoDestroy : MonoBehaviour 
{
	void OnEnable() 
	{
		Invoke(nameof(HideGO),3);
	}

	void HideGO()
	{
		gameObject.SetActive(false);
	}
}
