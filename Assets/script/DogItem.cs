using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DogItem : MonoBehaviour {

	void Start()
	{
		
	}

	// Use this for initialization
	public LayerMask enterLayer;
 
 
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.layer == 9)
		{
		 
		}
	}


	public void play(Vector2 pos,int index)
	{
		 
		DOTween.Sequence()
			.Append(transform.DOMoveX(pos.x + 7,3.8f))
			.AppendCallback(()=>{
				if(index == 2)
					GameMgr.inst().setGameState(GameMgr.inst().GetPreGameState());
				Destroy(gameObject);
			});

	}
 
}
