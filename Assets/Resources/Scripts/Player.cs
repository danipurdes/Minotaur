using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {

	private float moveSpeed = 3.0f;

	Sprite[] playerSprite;
	SpriteRenderer sr;
	int idleFrame;
	int[] walkCycle;
	float currentFrame;
	bool isRotatingLeft;
	bool isRotatingRight;
	List<Vector3> yarn;

	// Use this for initialization
	void Start () {
		yarn = new List<Vector3> ();

		gameObject.AddComponent<CircleCollider2D> ();
		gameObject.AddComponent<Rigidbody2D> ();
		gameObject.AddComponent<SpriteRenderer> ();
		gameObject.AddComponent<LineRenderer> ();

		CircleCollider2D cc = gameObject.GetComponent<CircleCollider2D> ();
		cc.radius = .25f;

		Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D> ();
		rb.gravityScale = 0;

		sr = gameObject.GetComponent<SpriteRenderer> ();
		playerSprite = Resources.LoadAll<Sprite> ("Sprites/playerAnimated");
		idleFrame = 2;
		walkCycle = new int[]{0,1,2,3,4,2};
		currentFrame = idleFrame;
		sr.sprite = playerSprite [Mathf.RoundToInt(currentFrame)];

		LineRenderer lr = gameObject.GetComponent<LineRenderer> ();
		lr.SetWidth (0.01f, 0.01f);
		lr.SetColors (Color.yellow, Color.yellow);
		lr.material = new Material(Shader.Find("Particles/Additive"));

		transform.position = new Vector3 (transform.position.x, transform.position.y, -1);
	}
	
	// Update is called once per frame
	void Update () {

		yarn.Add(gameObject.transform.position); 

		GameObject door = GameObject.Find("Door");
		if (Mathf.RoundToInt(door.transform.position.x) == Mathf.RoundToInt(gameObject.transform.position.x) && 
		    Mathf.RoundToInt(gameObject.transform.position.y) == Mathf.RoundToInt(door.transform.position.y)) {

		}

		bool moving = false;

		bool isRotating = isRotatingLeft || isRotatingRight;

		if (Input.GetKey ("s") && !isRotating) {
			transform.Translate (0, -moveSpeed * Time.deltaTime, 0);
			moving = true;
		}
		if (Input.GetKey ("w") && !isRotating) {
			transform.Translate (0, moveSpeed * Time.deltaTime, 0);
			moving = true;
		}

		if (isRotatingLeft) {
			transform.Rotate (0, 0, 120 * Time.deltaTime);
			if (transform.eulerAngles.z % 90 < 1 || transform.eulerAngles.z % 90 > 89) {
				transform.Rotate (0, 0, Mathf.RoundToInt(transform.eulerAngles.z/90)*90 - transform.eulerAngles.z);
				isRotatingLeft = false;
			}
		}

		if (isRotatingRight) {
			transform.Rotate (0, 0, -120 * Time.deltaTime);
			if (transform.eulerAngles.z % 90 < 1 || transform.eulerAngles.z % 90 > 89) {
				transform.Rotate (0, 0, Mathf.RoundToInt(transform.eulerAngles.z/90)*90 - transform.eulerAngles.z);
				isRotatingRight = false;
			}
		}

		if (Input.GetKey ("a")) {
			isRotatingLeft = !isRotatingRight;
		}
		if (Input.GetKey ("d")) {
			isRotatingRight = !isRotatingLeft;
		}

		if (moving) {
			currentFrame += Time.deltaTime * 16;
			sr.sprite = playerSprite [walkCycle [Mathf.RoundToInt(currentFrame) % walkCycle.Length]];
		} else {
			sr.sprite = playerSprite[walkCycle[idleFrame]];
			currentFrame = idleFrame;
		}

		LineRenderer lr = gameObject.GetComponent<LineRenderer> ();
		lr.SetVertexCount (yarn.Count);
		for (int i = 0; i < yarn.Count; ++i) {
			lr.SetPosition(i, yarn[i]);
		}
	}
}
