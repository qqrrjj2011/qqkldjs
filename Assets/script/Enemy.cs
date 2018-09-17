using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Enemy : Entity {

    Vector3 dir,dir2;


    float oriX = 0;
 
    // Use this for initialization
    void Start () {
        oriX = transform.position.x;
        int turn = Random.Range(1,3) == 1?1:-1;

        dir = (Quaternion.Euler(0,0,turn*70)*Vector3.down).normalized;
         dir2 = (Quaternion.Euler(0,0,80)*Vector3.down).normalized;
         Debug.Log("dir:"+dir+ ":"+dir2);
        StartCoroutine(moveStep());
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


    IEnumerator moveStep()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);
            Vector3 newPos = transform.position + dir;
           
            DOTween.Sequence()
            .Append(transform.DOMove(newPos,0.2f));

             yield return new WaitForSeconds(0.8f);
             newPos = transform.position + new Vector3(-1*dir.x*0.5f,Random.Range(-0.1f,-0.3f),0);
              DOTween.Sequence()
            .Append(transform.DOMove(newPos,0.2f));

            yield return new WaitForSeconds(0.3f);

             newPos = new Vector3(oriX,transform.position.y+0.2f,transform.position.z);
              DOTween.Sequence()
            .Append(transform.DOMove(newPos,0.2f));
            
        }
    }
 
}
