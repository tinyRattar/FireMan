using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterBehavior : MonoBehaviour {
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
			GameObject.Destroy (this.gameObject);
		this.transform.Translate (direction * speed * Time.deltaTime);
	}
}
