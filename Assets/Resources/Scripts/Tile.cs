using UnityEngine;
using System.Collections;

public class Tile {

	public GameObject go;

	// Use this for initialization
	public Tile () {
		go = new GameObject ("Tile");
		go.AddComponent<BoxCollider2D> ();
		go.AddComponent<Rigidbody2D> ();
		go.AddComponent<SpriteRenderer> ();

		BoxCollider2D bc = go.GetComponent<BoxCollider2D> ();
		bc.size = new Vector2 (1, 1);

		Rigidbody2D rb = go.GetComponent<Rigidbody2D> ();
		rb.isKinematic = true;

		SpriteRenderer sr = go.GetComponent<SpriteRenderer> ();
		sr.sprite = Resources.Load<Sprite> ("Sprites/wall");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
