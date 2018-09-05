using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour {
	
	

	public void homeClick()
	{
		if(GameMgr.inst().curGameMode == GameMode.level)
		{
			GameMgr.inst().uiControl.enterMain(2);
		}else
		{
			GameMgr.inst().uiControl.enterMain();
		}
		
	}

	public void newClick()
	{
		GameMgr.inst().uiControl.gameReplay();
	}

	public void continueClick()
	{
		GameMgr.inst().uiControl.gameContinue();
	}

	public void languageClick()
	{
		GameMgr.inst().uiControl.languageBtn();
	}

}
