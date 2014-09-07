using UnityEngine;
using System.Collections;

public class AttachNightLayer : MonoBehaviour {

    public float startToGetDark;
    public float finalDarkness;
    public GameObject nightLayer;

    private bool started;
    private GameObject player;
    private GameObject layer;

	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        started = false;
	}
	
	// Update is called once per frame
	void Update () {
        float currentTime = GetComponent<GameControlScript>().timeUntilLevelEnd;
	    if(currentTime < startToGetDark && !started)
        {
            layer = (GameObject)Instantiate(nightLayer, player.transform.position, player.transform.rotation);
            layer.transform.parent = player.transform;
            layer.renderer.material.color = new Color(layer.renderer.material.color.r, layer.renderer.material.color.g,
                layer.renderer.material.color.b, 0);
            started = true;
        }

        if(started && layer != null)
        {
            layer.renderer.material.color = new Color(layer.renderer.material.color.r, layer.renderer.material.color.g,
                layer.renderer.material.color.b, layer.renderer.material.color.a + Time.deltaTime * (1 / (startToGetDark - finalDarkness)));
        }
	}
}
