using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fogBehavior : MonoBehaviour {
	public float speed;
	public Vector3 direction;
	public float lifeTime;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		lifeTime -= Time.deltaTime;
		if (lifeTime <= 0)
			GameObject.Destroy (this.transform.parent.gameObject);
		this.transform.Translate (direction * speed * Time.deltaTime);
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "water") {
			this.GetComponentInChildren<Animation> ().Play ("fogOut");
			Destroy (this.transform.parent.gameObject,0.5f);
		}
	}

	void OnTriggerStay2D(Collider2D other){
		if (other.tag == "Player") {
			other.GetComponent<firemanCTRL> ().fogCount++;
		}
	}
}
