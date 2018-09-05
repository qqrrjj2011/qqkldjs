using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScollViewTest : MonoBehaviour {
	public Transform prefab;
	// Use this for initialization
	void Start () {
		for(int i = 0; i < 50; i++)
		{
			Transform btn = GameObject.Instantiate(prefab);
			btn.parent = transform;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
