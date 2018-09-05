using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessDropBall : MonoBehaviour {
	public int sortingOrder = 0;
	// Use this for initialization
	void Awake() {
		transform.GetComponent<TrailRenderer>().sortingOrder = sortingOrder;
	}
	void Start() {
		
		transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(0,1000) + new Vector2(Random.Range(-200,200),0); 
		//int dir = Random.Range(0,100)%2 == 0?-1:1;
		//transform.GetComponent<Rigidbody2D>().AddForce(new Vector2((Random.Range(0,1) + 0.5f) * dir,0),ForceMode2D.Impulse);
	}
	
	 
}
