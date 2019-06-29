using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class helpFader : MonoBehaviour {
	float timer = 1f;
	[SerializeField]int clickLeft = 1;
	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			clickLeft -= 1;
		}
		if (clickLeft <= 0) {
			Destroy (this.gameObject, 1.5f);
			timer -= Time.deltaTime;
			this.GetComponent<Image> ().color = new Color (1, 1, 1, timer);
		}
	}
}
