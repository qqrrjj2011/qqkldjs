using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DestroyTime : MonoBehaviour {

	// Use this for initialization 
	public float delaytime = 2.0f;
	public void StartRun()
	{
		GetComponent<ParticleSystem>().Play();
		//GameObject.Destroy(gameObject,2);
		DOTween.Sequence()  
			.AppendInterval(delaytime)
			.AppendCallback  
			(  
				() =>  
				{  
				///	GameMgr.inst().blockControl.blockPool.Despawn(transform);  
				}  
				);  
	}
	
	// Update is called once per frame 
}
