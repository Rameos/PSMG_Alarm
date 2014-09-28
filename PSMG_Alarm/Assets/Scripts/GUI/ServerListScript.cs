using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ServerListScript : MonoBehaviour {

	public GameObject listItemPrefab;
	public GameObject scrollbar;
	public int itemCount = 10;

	// Use this for initialization
	void Start()
	{
		scrollbar.SetActive (false);
	}

	public void InitializeServerList(HostData[] hostData){
		Debug.Log ("initialising server list!!!!!!!!");
		itemCount = hostData.Length;

		if (itemCount > 6)
		{
			scrollbar.SetActive(false);
		}



		RectTransform containerRectTransform = gameObject.GetComponent<RectTransform> ();
		RectTransform prefabRectTransform = listItemPrefab.GetComponent<RectTransform> ();
		
		float width = containerRectTransform.rect.width;
		float ratio = width / prefabRectTransform.rect.width;
		
		float height = prefabRectTransform.rect.height * ratio;
		int rowCount = itemCount;
		
		float scrollHeight = height * rowCount;
		
		containerRectTransform.offsetMin = new Vector2 (containerRectTransform.offsetMin.x, -scrollHeight / 2);
		containerRectTransform.offsetMax = new Vector2 (containerRectTransform.offsetMax.x, scrollHeight / 2);

		for (int i = 0; i < itemCount; i++) 
		{
			GameObject newItem = Instantiate(listItemPrefab) as GameObject;
			newItem.name = gameObject.name + i;
			newItem.transform.parent = gameObject.transform;
			newItem.GetComponent<ServlistitemScript>().SetGameName(hostData[i].gameName);
			newItem.GetComponent<ServlistitemScript>().SetGameDifficulty(hostData[i].comment);
			
			RectTransform rectTransform = newItem.GetComponent<RectTransform>();
			
			float x = containerRectTransform.rect.width / 2 - width;
			float y = containerRectTransform.rect.height / 2 - height * (i+1);
			rectTransform.offsetMin = new Vector2(x,y);
			
			x = rectTransform.offsetMin.x + width;
			y = rectTransform.offsetMin.y + height;
			rectTransform.offsetMax = new Vector2(x,y);
		}
	}

}
