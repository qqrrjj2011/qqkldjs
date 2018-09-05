using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class LoginGifts : MonoBehaviour {

	public int[] itemDiamond;
	string curDate;
	string lastSignDate;
	int signCount = 0; 

	List<Transform> itemLst = new List<Transform>();

	Transform bg;

	Action callBack;

	bool adsSuccessBack = false;

	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	Vector3 oriScale;
	void Awake()
	{
		 oriScale = transform.localScale;
	}
	
	void OnEnable()
	{
		callBack = null;
	//	transform.localScale = new Vector3(0.7f,0.7f,1);
	//	transform.DOScale(oriScale, 0.5f).SetEase(Ease.OutBack).SetUpdate(true);

		System.DateTime day =System.DateTime.Now;
		curDate = day.ToString("yyyyMMdd");
		lastSignDate = PlayerPrefs.GetString("signDate","");
		signCount = PlayerPrefs.GetInt("signCount",0);
		if(curDate != lastSignDate && signCount >= 7)
		{
			signCount = 0;                 
		}

		bg = transform.Find("bg");
		int itemNum = 0;
		foreach (Transform item in bg)
		{
			string itemName = item.name.Substring(0,4);
			if(itemName == "item")
			{
				itemLst.Add(item);
				updateItem(itemNum,item);
				itemNum++;
			}	
		}

		updateBtn();
	}

	public void setCallback(Action callBack)
	{
		this.callBack = callBack;
	}

	void updateBtn()
	{
		int doubleState = PlayerPrefs.GetInt("doubleState" + curDate,0);
		Transform doubleBtnTr = transform.Find("doubleBtn");
		Transform btnTr = transform.Find("checkBtn");

		
		Button doublebtn = doubleBtnTr.GetComponent<Button>();
		Button btn = btnTr.GetComponent<Button>();
		if(doubleState == 0)
		{
			doublebtn.interactable = true;
		}else
		{
			doublebtn.interactable = false;
			 
			GameMgr.inst().uiControl.mainUI.setTip();
 
			//doubleBtnTr.Find("Text").GetComponent<Text>().text = "finish";

			GameDataMgr.inst().setLanguageTxt(doubleBtnTr.Find("Text").GetComponent<Text>().GetComponent<Text>(),24);
		}
		
		if(lastSignDate != curDate)
			btn.interactable = true;
		else 
		{
			btn.interactable = false;
		
			GameMgr.inst().uiControl.mainUI.setTip();
			
			//btnTr.Find("Text").GetComponent<Text>().text = "finish";
			GameDataMgr.inst().setLanguageTxt(doubleBtnTr.Find("Text").GetComponent<Text>().GetComponent<Text>(),24);
		}

		if(lastSignDate == curDate && doubleState != 0)
		{
			doublebtn.gameObject.SetActive(false);
			btn.gameObject.SetActive(false);
		}
			
	}


	void updateItem(int num,Transform tr)
	{
		Transform gou = tr.Find("gou");
		Transform shadow = tr.Find("shadow");
 
		if(num < signCount)
		{
			gou.gameObject.SetActive(true);
			shadow.gameObject.SetActive(true);	
		}else
		{
			gou.gameObject.SetActive(false);
			shadow.gameObject.SetActive(false);	
		}
	}


	void showAward(int signNum)
	{
		// Transform tr = itemLst[signNum].Find("diamondIcon");
		// Transform flyTR = GameObject.Instantiate(tr);
		// flyTR.position = tr.position;
		// flyTR.SetParent(transform);

		// Vector3 startPos = flyTR.position;
		// flyTR.localScale = new Vector2(0.5f,0.5f);
		// DOTween.Sequence().Append(flyTR.DOMove(startPos + new Vector3(0,250,0),0.9f).SetEase(Ease.OutBounce))
		// .AppendInterval(1)
		// .Insert(0,flyTR.DOScale(new Vector3(1,1,1),0.9f))
		// .AppendCallback(()=>{
		// 	GameObject.Destroy(flyTR.gameObject);
		// });
		UnityEngine.Object ationObj = Resources.Load("prefab/awardLight", typeof(GameObject));
		GameObject ationTr =  GameObject.Instantiate(ationObj) as GameObject;
		//ationTr.transform.parent = transform;
		ationTr.transform.SetParent(transform,false);
		ationTr.transform.localScale = new Vector3(2.5f,2.5f,1);
		ationTr.transform.localPosition =new Vector2(0,0);
		ationTr.GetComponent<AwardLightAtion>().init(itemDiamond[signNum]);

		GameMgr.inst().addDiamond(itemDiamond[signNum]);
	}

	// Use this for initialization 

	public void btnClick()
	{
		lastSignDate = curDate;
		showAward(signCount);
		updateItem(-1,itemLst[signCount]);
		signCount++;

		PlayerPrefs.SetString("signDate",curDate);
		PlayerPrefs.SetInt("signCount",signCount);
 
		updateBtn();
	}

	public void doubleClick()
	{
		if(PlatformHelper.inst().isReadUnityOrAdmobAwardAds())
		{
			PlatformHelper.inst().showAdmobOrUnityRewardAds((int state)=>{
			if(state == 1)
			{
				adsSuccessBack = true;
			}
			
		});
		}else
		{
			GameMgr.inst().uiControl.gameTip.showAtion(42);
		}
	}

	void Update()
	{
		if(adsSuccessBack)
		{
			adsSuccessBack = false;
			PlayerPrefs.SetInt("doubleState" + curDate,1);
			updateBtn();
			if(curDate == lastSignDate)
			{
				showAward(signCount-1);
			}else 
			{
				showAward(signCount);
			}
		}
	}

	public void closeBtn()
	{
		if(callBack != null)
			callBack();
		gameObject.SetActive(false);
	}
	
	// Update is called once per frame
 
}
