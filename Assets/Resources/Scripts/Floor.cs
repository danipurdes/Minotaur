using UnityEngine;
using System.Collections;

public class Floor : Tile {
	
	// Use this for initialization
	public Floor () : base() {
		go.AddComponent<SpriteRenderer> ();

		Sprite s = Resources.Load<Sprite> ("Sprites/floor");
		go.GetComponent<SpriteRenderer>().sprite = s;
	}
}
