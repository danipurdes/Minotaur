using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	private float moveSpeed = 3.0f;

	// Use this for initialization
	void Start () {
		gameObject.AddComponent<CircleCollider2D> ();
		gameObject.AddComponent<Rigidbody2D> ();
		gameObject.AddComponent<SpriteRenderer> ();

		CircleCollider2D cc = gameObject.GetComponent<CircleCollider2D> ();
		cc.radius = .25f;

		Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D> ();
		rb.gravityScale = 0;

		SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer> ();
		Sprite[] s = Resources.LoadAll<Sprite> ("Sprites/playerAnimated");
		sr.sprite = s [0];
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
