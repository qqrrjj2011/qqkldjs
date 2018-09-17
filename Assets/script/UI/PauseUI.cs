using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour {
	
	

	public void homeClick()
	{
	
			UIcontroller.getInst().enterMain();
		
		
	}

	public void newClick()
	{
		UIcontroller.getInst().gameReplay();
	}

	public void continueClick()
	{
		gameObject.SetActive(false);
		GameMgr.inst().setGameState(gameState.ballRuning);
		Time.timeScale = 1;
	}

	public void languageClick()
	{
		UIcontroller.getInst().languageBtn();
	}

}
