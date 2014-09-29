using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LobbyTitleScript : MonoBehaviour {

	public Text title;

	public void SetTitleText(string text)
	{
		title.text = text;
	}

	public void ResetTitleText()
	{
		title.text = "Lobby";
	}
}
