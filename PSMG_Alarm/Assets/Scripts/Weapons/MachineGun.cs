using UnityEngine;
using System.Collections;

public class MachineGun : MonoBehaviour {

	void Start () {
        //if (NetworkManagerScript.networkActive && networkView.isMine)
        //{
        //    GameObject laserInstance = (GameObject)Network.Instantiate(mg, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)), 3);
        //    laserInstance.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90));
        //    laserInstance.rigidbody2D.AddForce(laserInstance.transform.right * 1000);
        //    Destroy(laserInstance, 2);
        //}
        //if (NetworkManagerScript.networkActive == false)
        //{
        //    GameObject laserInstance = (GameObject)Instantiate(mg, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
        //    LineRenderer lr = laserInstance.GetComponent<LineRenderer>();

        //    Vector3 startPosition = Camera.main.ScreenToWorldPoint(ubootposition);
        //    startPosition.z = 9;

        //    lr.SetVertexCount(2);
        //    lr.SetPosition(0, startPosition);

        //    Vector3 shootVector = crosshair.transform.position - Camera.main.ScreenToWorldPoint(ubootposition);
        //    RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(ubootposition), shootVector);

        //    Debug.DrawLine(Camera.main.ScreenToWorldPoint(ubootposition), crosshair.transform.position);

        //    Vector3 endPosition = crosshair.transform.position;
        //    endPosition.z = 9;

        //    Debug.Log(Camera.main.ScreenToWorldPoint(ubootposition) + "  " + crosshair.transform.position);

        //    if (hit.collider != null && hit.collider.gameObject.tag == "Enemy")
        //    {
        //        Debug.Log("hit!");
        //        endPosition = hit.point;
        //        endPosition.z = 9;
        //    }

        //    lr.SetPosition(1, endPosition);

        //    Destroy(laserInstance, 2);
        //}
	}

    public void Fire(Vector3 aimPosition)
    {

    }

}
