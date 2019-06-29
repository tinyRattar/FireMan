using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireBehavior : MonoBehaviour {
	public float health;
	public float maxHealth;
	public float scale;
	[SerializeField]GameObject fog;
	[SerializeField]GameObject flame;
	public float fogGeneTime = 0.2f;
	float fogGeneTimer = 0f;
	public float flameGeneTime = 0.1f;
	float flameGeneTimer = 0f;
	[SerializeField]float recoverTime = 2f;
	[SerializeField]float refireTime = 30f;
	float recoverTimer = 0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (health < 0) {
			health = 0;
			recoverTimer = refireTime;
		}
		float curScale = scale * health / maxHealth;
		this.transform.localScale = scale * new Vector3 (health / maxHealth, health / maxHealth, 1);
		fogGeneTimer += Time.deltaTime;
		flameGeneTimer += Time.deltaTime;
		if (fogGeneTimer >= fogGeneTime) {
			fog.transform.localScale = new Vector3 (1f, 1f, 0) * curScale;
			fogGeneTimer = 0f;
			fog.transform.position = this.transform.position;
			fog.GetComponentInChildren<fogBehavior> ().direction = new Vector3 (0, 1, 0);
			fog.GetComponentInChildren<fogBehavior> ().speed = curScale / 7f;
			fog.transform.up = Vector3.zero;
			fog.transform.Rotate (0, 0, Random.Range (-180f, 180f));
			GameObject.Instantiate (fog);
		}
		if (flameGeneTimer >= flameGeneTime) {
			flame.transform.localScale = new Vector3 (1, 1, 0) * curScale / 3f;
			flameGeneTimer = 0f;
			flame.transform.position = this.transform.position;
			flame.GetComponentInChildren<flameBehavior> ().direction = new Vector3 (0, 1, 0);
			flame.GetComponentInChildren<flameBehavior> ().speed = curScale / 3f;
			flame.transform.up = Vector3.zero;
			flame.transform.Rotate (0, 0, Random.Range (-180f, 180f));
			GameObject.Instantiate (flame);
		}
		if (recoverTimer <= 0) {
			health += Time.deltaTime * 5f;
			if (health >= maxHealth)
				health = maxHealth;
		} else {
			recoverTimer -= Time.deltaTime;
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "water") {
			health -= 0.5f;
			Destroy (other.gameObject, 1f);
			recoverTimer = recoverTime;
		}
	}

	void OnTriggerStay2D(Collider2D other){
		if (other.tag == "Player") {
			Debug.Log ("on fire");
			other.GetComponent<firemanCTRL> ().maxO2 -= 2f * Time.deltaTime;
		}
	}
}
