using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manBehavior : MonoBehaviour {
	public float distance = 0.2f;
	public GameObject follower;
	public bool saved = false;
	public bool exit = false;

	// Use this for initialization
	void Start () {
		follower = GameObject.Find ("fireman");
	}
	
	// Update is called once per frame
	void Update () {
		if (saved) {
			Vector3 forward = this.transform.position - follower.transform.position;
			if (forward.sqrMagnitude > distance) {
				this.transform.up = -forward.normalized;
				this.transform.Translate (1.3f * new Vector3 (0, 1, 0) * Time.deltaTime);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.name == "exitBox") {
			saved = true;
			exit = true;
			firemanCTRL fireman = GameObject.Find("fireman").GetComponent<firemanCTRL> ();
			follower = GameObject.Find("exitTarget");
			fireman.speed += 0.2f;
			fireman.lastOne = fireman.gameObject;
		}
		if (other.tag == "Player" && !exit && !saved) {
			saved = true;
			firemanCTRL fireman = other.GetComponent<firemanCTRL> ();
			follower = fireman.lastOne;
			fireman.speed -= 0.2f;
			fireman.lastOne = this.gameObject;
		}
	}
}
