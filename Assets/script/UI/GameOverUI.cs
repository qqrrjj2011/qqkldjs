using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class GameOverUI : MonoBehaviour {

	public Text scoreTxt;
	public Text bestScoreTxt;

	public GameObject adsBtn;
	public GameObject overBtn;

    public GameObject dieText1;
    public GameObject dieText2;
	// Use this for initialization
	void Start () {
		
	}
	
	void OnEnable()
	{
 
	}


	public void initUI()
	{
        if (GameMgr.inst().uiControl.dieType == 1)
        {
            dieText1.SetActive(true);
            dieText2.SetActive(false);
        } else if (GameMgr.inst().uiControl.dieType == 2)
        {
            dieText1.SetActive(false);
            dieText2.SetActive(true);
        }
		//int bestScore = PlayerPrefs.GetInt("bestScore",0);
		//int score = 0;
		//int round = GameMgr.inst().shootControl.round;

		//scoreTxt.text = score +"";
		//if(score > bestScore)
		//{
		//	PlayerPrefs.SetInt("bestScore",score);
		//	PlayerPrefs.SetInt("bestRound",round);
		//	PlayerPrefs.SetInt("bestTotalPlayNum",GameMgr.inst().totalPlayNum);
		//	bestScoreTxt.text = "" + score;
		//}else{
		//	bestScoreTxt.text = "" + bestScore;
		//}

		 
		 
		//if(Advertisement.IsReady() && PlayerPrefs.GetInt("isCanRevive",0) == 1)
		//{
		
		//	setReviveBtn(true);
		//}else
		//{
			 
		//	if(GameMgr.inst().shootControl.round > 10)
		//	{
		//		//PlatformHelper.inst().showInterstitialAds((int state)=>{});
		//	}
		//	setReviveBtn(false);
		//	setEndData();
			
		//}
 
	}


	public void setEndData()
	{
		GameMgr.inst().playNum++;
 
		GameMgr.inst().umengPlayTotal(GameMgr.inst().totalPlayNumNoSave);
 
		GameMgr.inst().shootControl.clear();
	}



	public void setReviveBtn(bool isrevive)
	{
		if(isrevive)
		{
			adsBtn.SetActive(true);
			 
			adsBtn.GetComponent<AdsBar>().barRun((bool finish)=>{
				
				setReviveBtn(false);
				if(finish)
				{
					gameObject.SetActive(false);
					GameMgr.inst().uiControl.Revive();
				}else
				{
					setEndData();
				}
			});

			overBtn.SetActive(false);
		}else{
			adsBtn.SetActive(false);

			overBtn.SetActive(true);
		}
	}
	
}
