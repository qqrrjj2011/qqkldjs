﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class GameOverUI : MonoBehaviour {

 
	// Use this for initialization
	void Start () {
		
	}
	
	void OnEnable()
	{
		
	}


	public void initUI()
	{
		EnemyControl.getInst().clear();
 
	}


	public void replayClick()
	{
		UIcontroller.getInst().gameReplay();
	}

	public void homeClick()
	{
		gameObject.SetActive(false);
		UIcontroller.getInst().enterMain();
	}


	public void setEndData()
	{
	 
	}



	public void setReviveBtn(bool isrevive)
	{
		// if(isrevive)
		// {
		// 	adsBtn.SetActive(true);
			 
		// 	adsBtn.GetComponent<AdsBar>().barRun((bool finish)=>{
				
		// 		setReviveBtn(false);
		// 		if(finish)
		// 		{
		// 			gameObject.SetActive(false);
		// 			UIcontroller.getInst().Revive();
		// 		}else
		// 		{
		// 			setEndData();
		// 		}
		// 	});

		// 	overBtn.SetActive(false);
		// }else{
		// 	adsBtn.SetActive(false);

		// 	overBtn.SetActive(true);
		// }
	}
	
}
