using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity {
 
    // Use this for initialization
    void Start () {
    
	}
	
	// Update is called once per frame
	void Update () {
        speedDt = speed * Time.deltaTime;
        transform.position += new Vector3(0,-speedDt,0);

        if (transform.localPosition.y < -6)
        {
            Destroy(gameObject);
        }
	}
}
