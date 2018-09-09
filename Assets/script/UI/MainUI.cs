using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MainUI : MonoBehaviour {
 
	// Use this for initialization
	void Awake() 
	{

		//endlessBtn.GetComponent<Button>().onClick.AddListener(()=>{
		//		if(0 == curIndex)return;
		//		switchUI(0,true);	
		//	});

		//ballsBtn.GetComponent<Button>().onClick.AddListener(()=>{
		//			// reSetBtnState(); 
		//			// ballsBtn.Find("selectImg").gameObject.SetActive(true);
		//		switchUI(1,true);
		//	});
 
 
	}

	void Start() {
	
		
	}

	void OnEnable()
	{
		StartCoroutine(checkTip()); 
	}

	IEnumerator checkTip()
	{
		while (true)
		{
			yield return new WaitForSeconds(1);
			setTip();
		}
	}

	
	public void setTip()
	{
		// if(!GameMgr.inst().havaDoubleSign() || !GameMgr.inst().havaSign())
		// {
		// 	signTip.gameObject.SetActive(true);
		// }else{
		// 	signTip.gameObject.SetActive(false);
		// }
		// if(GameMgr.inst().havaDoubleSign() && GameMgr.inst().havaSign())
		// {
		// 	signFinishBg.gameObject.SetActive(true);
		// }

		// if(PlayerPrefs.GetInt("shopAdsCount",5) > 0 && PlatformHelper.inst().isReadUnityOrAdmobAwardAds())
		// {
		// 	shopTip.gameObject.SetActive(true);
		// }else
		// {
		// 	shopTip.gameObject.SetActive(false);
		// }
		
	}


	public void switchUI(int index, bool ation = false)
	{
 
		//reSetUI();
		//if(ation)
		//	preUI = curUI;
		//switch (index)
		//{
		//	case 0:
		//	endlessBtn.Find("selectImg").gameObject.SetActive(true);
		//	endlessUI.gameObject.SetActive(true);
		//	//endlessUI.localPosition = new Vector3(0,endlessUI.localPosition.y,endlessUI.localPosition.z);
		//	curUI = endlessUI;
		//	break;
		//	case 1:
		//	ballsBtn.Find("selectImg").gameObject.SetActive(true);
		//	ballsUI.gameObject.SetActive(true);
		//	ballsUI.GetComponent<BallsUI>().init();
		//	//ballsUI.localPosition = new Vector3(0,ballsUI.localPosition.y,ballsUI.localPosition.z);
		//	curUI = ballsUI;
		//	break;
		//	case 2:
		//	normalBtn.Find("selectImg").gameObject.SetActive(true);
		//	normalUI.gameObject.SetActive(true);
		//	//normalUI.localPosition = new Vector3(0,normalUI.localPosition.y,normalUI.localPosition.z);
		//	curUI = normalUI;
		//	GameMgr.inst().uiControl.levelUI.updateBtn();
		//	break;
		//	case 3:
		//	shopBtn.Find("selectImg").gameObject.SetActive(true);
		//	shopUI.gameObject.SetActive(true);
		//	//shopUI.localPosition = new Vector3(0,shopUI.localPosition.y,shopUI.localPosition.z);
		//	curUI = shopUI;
		//	break;
		//	case 4:
		//	signBtn.Find("selectImg").gameObject.SetActive(true);
		//	signUI.gameObject.SetActive(true);
		// 	curUI = signUI;
		//	break;
		//	default:
		//	break;
		//}
		//if(ation)
		//{
		//	curUI.gameObject.SetActive(true);

		//	float fromX = -1080.0f;
		//	float toX = 1080.0f;
		//	if(index > curIndex)
		//	{
		//		fromX = 1080.0f;
		//		toX = -1080.0f;
		//	}else
		//	{
		//		fromX = -1080.0f;
		//		toX = 1080.0f;
		//	}

		//	curUI.localPosition = new Vector3(fromX,curUI.localPosition.y,curUI.localPosition.z);
		//	preUI.DOLocalMoveX(toX,0.4f).SetEase(Ease.InOutSine).OnComplete(()=>{
		//		preUI.gameObject.SetActive(false);
		//	});
		//	curUI.DOLocalMoveX(0,0.4f).SetEase(Ease.InOutSine);
		//}else
		//{
		//	curUI.localPosition = new Vector3(0,curUI.localPosition.y,curUI.localPosition.z);
		////	preUI.gameObject.SetActive(false);
		//}

		//curIndex = index;
		
	}

	
	void reSetUI()
	{
		// ballsBtn.Find("selectImg").gameObject.SetActive(false);
		// normalBtn.Find("selectImg").gameObject.SetActive(false);
		// shopBtn.Find("selectImg").gameObject.SetActive(false);
		// endlessBtn.Find("selectImg").gameObject.SetActive(false);
		// signBtn.Find("selectImg").gameObject.SetActive(false);
		
 
	}
}
