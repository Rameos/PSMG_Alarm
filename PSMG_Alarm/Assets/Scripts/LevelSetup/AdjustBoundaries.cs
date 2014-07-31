using UnityEngine;
using System.Collections;

public class AdjustBoundaries : MonoBehaviour {
	
	private GameObject left, right, camera2d;
	
	void Start () {
		left = GameObject.Find("Left");
		right = GameObject.Find("Right");
		camera2d = GameObject.Find("2D Camera");
		
		Vector3 fieldSizeRight = camera2d.camera.ScreenToWorldPoint(new Vector3(camera2d.camera.pixelWidth, 0, 0));
		Vector3 fieldSizeLeft = camera2d.camera.ScreenToWorldPoint(new Vector3(0, 0, 0));
		
		left.transform.position = new Vector3(fieldSizeLeft.x, left.transform.position.y, left.transform.position.z);
		right.transform.position = new Vector3(fieldSizeRight.x, left.transform.position.y, left.transform.position.z);
	}
}