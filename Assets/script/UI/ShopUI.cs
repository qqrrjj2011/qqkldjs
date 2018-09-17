using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ShopUI : MonoBehaviour {
	public int[] itemDiamond;
	// Use this for initialization
	int curdiamondNum = 0;

	Text txt;

	Transform successTr;

	Vector2 successOripos;

	public string[] priceIOS;
	public string[] priceAndroid;

	bool adsSuccessBack = false;
	int curType;

	 
	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	void Awake()
	{
		txt = transform.Find("diamondKuang").Find("diamondTxt").GetComponent<Text>();
		txt.text = "0";
		successTr = transform.Find("success");
		successTr.gameObject.SetActive(false);
		successOripos = successTr.localPosition;

		if(GameMgr.inst().isNewDay())
		{
			PlayerPrefs.SetInt("shopAdsCount",5);
		}

		if(PlayerPrefs.GetInt("noAds",0) == 1)
		{
			transform.Find("list").Find("item0").Find("buyBtn").GetComponent<Button>().interactable = false;
		}

		Transform list = transform.Find("list");
		for (int i = 0; i < 5; i++)
		{
			if(i == 1)continue;
			#if UNITY_ANDROID
			list.Find("item"+i).Find("buyBtn").Find("txt").GetComponent<Text>().text = priceAndroid[i];
			#elif UNITY_IOS
			list.Find("item"+i).Find("buyBtn").Find("txt").GetComponent<Text>().text = priceIOS[i];
			#endif
		}

		 
		#if UNITY_ANDROID
			transform.Find("reBuyBtn").gameObject.SetActive(false);
		#endif
	}
	void Start () {
 
	}

	void OnEnable()
	{
		updateDiamondText();
	}

	public void updateDiamondText()
	{
		txt.text = GameMgr.inst().diamondNum + "";

		int count = PlayerPrefs.GetInt("shopAdsCount",5);

		Transform btn1 = transform.Find("list").Find("item1").Find("buyBtn");
		Text adsCountTxt = btn1.Find("dian").Find("Text").GetComponent<Text>();
		if(count <= 0)
		{
			btn1.GetComponent<Button>().interactable = false;

		 
			//	UIcontroller.getInst().mainUI.setTip();
		 
			
		}
		adsCountTxt.text = ""+count;
	}
	
	public void buyBtnClick(int type)
	{
		if(type != 1)
			UIcontroller.getInst().showBlockPanel(true,18);
		PlatformHelper.inst().Pay(type,(int state)=>{
			UIcontroller.getInst().showBlockPanel(false);
			if(state == 1)
			{
				adsSuccessBack = true;
				curType = type;
				
			}else
			{
				Debug.Log(">>>>>>>>>>>>>>>>pay fail");
			}

		});
	}

	public void reBuyClick()
	{
		UIcontroller.getInst().showBlockPanel(true,18);
		PlatformHelper.inst().purchaser.RestorePurchases((int state)=>{
			if(state == 1)
			{
				adsSuccessBack = true;
				curType = 0;
			}
			UIcontroller.getInst().showBlockPanel(false,18);
		});
	}


	void Update()
	{
		if(adsSuccessBack)
		{
			adsSuccessBack = false;
			showAward();
		}
	}


	void showAward()
	{
		if(curType == 1)
		{
				int count = PlayerPrefs.GetInt("shopAdsCount",5);
				PlayerPrefs.SetInt("shopAdsCount",count - 1);
		}else if(curType == 0)
		{
				PlayerPrefs.SetInt("noAds",1);
				transform.Find("list").Find("item0").Find("buyBtn").GetComponent<Button>().interactable = false;
				PlatformHelper.inst().removeBanner();
		}
		int awardNum = itemDiamond[curType];
		GameMgr.inst().addDiamond(awardNum);
		updateDiamondText();
		// successTr.localPosition = successOripos;
		// successTr.gameObject.SetActive(true);
		// DOTween.Sequence()
		// .Append(successTr.DOLocalMove(successOripos + new Vector2(-660,0),0.8f).SetEase(Ease.OutBack))
		// .AppendInterval(0.5f)
		// .AppendCallback(()=>{ 
		// 	successTr.gameObject.SetActive(false);
		// });

		if(awardNum > 0)
		{
			Object ationObj = Resources.Load("prefab/awardLight", typeof(GameObject));
			GameObject ationTr =  GameObject.Instantiate(ationObj) as GameObject;
			
			ationTr.transform.SetParent(transform,false);
			ationTr.transform.localScale = new Vector3(2.5f,2.5f,1);
			ationTr.transform.localPosition =new Vector2(0,0);
			ationTr.GetComponent<AwardLightAtion>().init(awardNum);
		}
	}


	public void closeBtn()
	{
		StartCoroutine(closeDelay());
	}

	// 防止误操作
	IEnumerator closeDelay()
	{
		
		yield return new WaitForEndOfFrame();
		 
		gameObject.SetActive(false);
	}
}
