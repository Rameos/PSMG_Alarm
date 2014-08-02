using UnityEngine;
using System.Collections;

public class SubLightController : MonoBehaviour
{

    private GameObject camera3D;
    private GameObject player;

    private float offsetX = 5.7f;
    private float offsetY = -6.11f;

    void Start()
    {
        camera3D = GameObject.FindGameObjectWithTag("3DCam");
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (player != null)
        {
            float ratioX = 1 + Mathf.Pow(Mathf.Abs(player.transform.position.x), 1f / 4f);
            float ratioY = 1 + Mathf.Pow(Mathf.Abs(player.transform.position.y), 1f / 10f);

            Vector3 screenPos = Camera.main.WorldToScreenPoint(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.y - 10));

            transform.position = camera3D.camera.ScreenToWorldPoint(screenPos);
            transform.position += new Vector3(offsetY, 0, offsetY);
            transform.rotation = Quaternion.Euler(new Vector3(transform.eulerAngles.x, -1 * player.transform.eulerAngles.z + 90, 0));
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
