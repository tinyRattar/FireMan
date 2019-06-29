using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class firemanCTRL : MonoBehaviour {
	[SerializeField]GameObject water;
	public float speed;
	public GameObject lastOne;
	GameObject newWater;
	public int fogCount = 0;
	public int fireCount = 0;
	public float o2 = 100;
	public float maxO2 = 100;
	public float co2 = 100;
	public float recoverTimer = 0.5f;
	public Image imgO2;
	public Image imgCO2;
	public Image imgMAXO2;
	bool gameStart = false;
	[SerializeField]int clickLeft = 2;


	// Use this for initialization
	void Start () {
		lastOne = this.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if (!gameStart) {
			if (Input.GetMouseButtonDown (0)) {
				clickLeft -= 1;
			}
			if (clickLeft <= 0)
				gameStart = true;
			return;
		}
		float x, y;
		x = Input.GetAxis ("Horizontal");
		y = Input.GetAxis ("Vertical");
		this.transform.Translate (new Vector3 (x, y) * speed * Time.deltaTime);
		Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,-Camera.main.transform.position.z)); 
		Vector3 direction = (worldPos - this.transform.position).normalized;
		if (Input.GetMouseButton (0)) {
			if (co2 > 0) {
				co2 -= Time.deltaTime * 1.5f;
				water.transform.position = this.transform.position;
				water.GetComponent<waterBehavior> ().direction = direction;
				water.transform.up = Vector3.zero;
				water.transform.Rotate (0, 0, Random.Range (-20f, 20f));
				GameObject.Instantiate (water);
			}
		}
		if (fogCount > 0) {
			if (recoverTimer <= 0) {
				recoverTimer = 0.5f;
				fogCount = 0;
			} else {
				recoverTimer -= Time.deltaTime;
			}
			o2 -= Time.deltaTime * 2f;
			if (maxO2 - o2 >= 20f) {
				o2 -= Time.deltaTime * 1f;
				maxO2 -= Time.deltaTime * 1f;
			}
			if (maxO2 - o2 >= 40f) {
				o2 -= Time.deltaTime * 1f;
				maxO2 -= Time.deltaTime * 1f;
			}
		} else {
			if (o2 >= maxO2)
				o2 = maxO2;
			else {
				o2 += Time.deltaTime * 10f;
				//maxO2 -= Time.deltaTime * 5f;
			}
		}

		if (co2 < 0) {
			co2 = 0;
		}
		imgCO2.fillAmount = co2 / 100;

		if (maxO2 < 0) {
			maxO2 = 0;
		}
		imgMAXO2.fillAmount = maxO2 / 100;

		if (o2 < 0) {
			Debug.Log ("you died");
			SceneManager.LoadScene ("gameover");
			o2 = 0;
		}
		imgO2.fillAmount = o2 / 100;
	}
}
