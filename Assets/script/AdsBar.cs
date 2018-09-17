using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AdsBar : MonoBehaviour {
	public Image img;
	// Use this for initialization 

	float totalNum = 800.0f;
	float curNum;
	System.Action<bool> thisAction;
	public void barRun(System.Action<bool> action)
	{
		img.fillAmount = 1;
		curNum = totalNum;
		thisAction = action;
		StartCoroutine(run());
	}

	IEnumerator run()
	{
		while(curNum > 0)
		{
			curNum -= Time.fixedDeltaTime*100.0f;
			img.fillAmount = curNum/totalNum;
			yield return new WaitForEndOfFrame();
		}

		thisAction(false);
		
	}


	public void playUnityAds()
	{
		if(PlayerPrefs.GetInt("noAds") == 1)
		{
			thisAction(true);
		}else
		{
			PlatformHelper.inst().unityShowRewardedAd((int state)=>{
				if(state == 1)
				{
					thisAction(true);
				}else
				{
					thisAction(false);
				}
			});
		}

		// UIcontroller.getInst().showBlockPanel(true,18);
		// PlatformHelper.inst().Pay(5,(int state)=>{
		// 	UIcontroller.getInst().showBlockPanel(false);
		// 	if(state == 1)
		// 	{
		// 		thisAction(true);
				
				
		// 	}else
		// 	{
		// 		thisAction(false);
				
		// 	}

		// });


	}
 
}
