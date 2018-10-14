using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MainUI : MonoBehaviour {
 
	// Use this for initialization
	void Awake() 
	{
 
	}

	void Start() {
	
			GameMgr.inst().setGameState(gameState.mainUI);
	}

	public void playClick()
	{
		gameObject.SetActive(false);
		
		UIcontroller.getInst().gameReplay();
	}

	void OnEnable()
	{
		
	}
 
}
