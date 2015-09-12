using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

	private float moveSpeed = 3.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey ("a"))
			transform.Translate (-moveSpeed*Time.deltaTime, 0, 0);
		if (Input.GetKey ("d"))
			transform.Translate (moveSpeed*Time.deltaTime, 0, 0);
		if (Input.GetKey ("s"))
			transform.Translate (0, -moveSpeed * Time.deltaTime, 0);
		if (Input.GetKey ("w"))
			transform.Translate (0, moveSpeed * Time.deltaTime, 0);

		if (Input.GetKey ("j"))
			transform.Rotate (0, 0, 60 * Time.deltaTime);
		if (Input.GetKey ("l"))
			transform.Rotate (0, 0, -60 * Time.deltaTime);
	}
}
