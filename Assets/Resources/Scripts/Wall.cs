using UnityEngine;
using System.Collections;

public class Wall : Tile {
	
	// Use this for initialization
	public Wall () : base(){
		go = new GameObject ("Wall");
		go.AddComponent<BoxCollider2D> ();
		go.AddComponent<Rigidbody2D> ();
		go.AddComponent<SpriteRenderer> ();
		
		BoxCollider2D bc = go.GetComponent<BoxCollider2D> ();
		bc.size = new Vector2 (1, 1);
		bc.offset = new Vector2 (.5f, .5f);
		
		Rigidbody2D rb = go.GetComponent<Rigidbody2D> ();
		rb.isKinematic = true;
		
		SpriteRenderer sr = go.GetComponent<SpriteRenderer> ();
		sr.sprite = Resources.Load<Sprite> ("Sprites/wall");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
