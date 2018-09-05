using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Umeng;
using UnityEngine.Advertisements;
using admob;
using System.Runtime.InteropServices;

public class PlatformHelper : MonoBehaviour {
	public string umeng_key_fy = "5ae41430f29d983bb0000055";
	public string umeng_key_fy_ios = "5aec48c3f43e481bfa0000e2";
	public string umeng_key_fy_mj = "5af032a4a40fa35f3d0001f0";
	public string umeng_key_yc = "5aeffb96a40fa3296e000057";

	public string unity_adsID_fy = "1796948";
	public string unity_adsID_fy_mj = "1796948";

	public string unity_adsID_yc = "1799252";

	public string admob_interstitialID_fy = "ca-app-pub-1640039600835010/9887807623";
	public string admob_interstitialID_fy_mj = "";

	public string admob_interstitialID_yc = "ca-app-pub-3621670171383768/5800598236";

	public string admob_rewardID_fy = "ca-app-pub-1640039600835010/8714326755";
	public string admob_rewardID_fy_mj = "";

	public string admob_rewardID_yc = "ca-app-pub-3621670171383768/8043618196";

	public string admob_bannerID = "ca-app-pub-3621670171383768/5307727929";

	// 游戏包名
	public string packageName = "com.fy.ProductName";




	System.Action<int> unityAction;
	System.Action<int> admobAction;

	System.Action<int> buyAction;

	static PlatformHelper instance = null;

	public Admob ad;

	[HideInInspector]
	public Purchaser purchaser;

// 视屏广告播放完
	bool admobRewardFinish = false;
	public static PlatformHelper inst()
	{
		return instance;
	}

	// [DllImport("__Internal")]  
    // // ios手机的当前语言 "en"、“zh"、“zh-Hans"、"zh-Hant"  
    // private static extern string CurIOSLang(); 

	// Use this for initialization
	void Start () {
		Input.multiTouchEnabled = false;

		instance = this;

		purchaser = transform.GetComponent<Purchaser>();

	//	Application.targetFrameRate = 20;

		// unity
		
		

		//Advertisement.debugMode();
	 
		packageName = getPackageName();

		#if UNITY_ANDROID && !UNITY_EDITOR && !TEST
			AndroidJavaClass ym = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject yo = ym.GetStatic<AndroidJavaObject>("currentActivity");
			string umID = yo.Call<string>("getUmengID");
		 
			if(umID  != "0")
			{
				 GA.StartWithAppKeyAndChannelId(umID, "Google Store");
			}
		#endif

  
		#if UNITY_ANDROID && !UNITY_EDITOR && !TEST
			AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
			string serverID = jo.Call<string>("getAdsID");
		 
			if(serverID  != "0")
			{
				string[] idArr = serverID.Split('|');
				Debug.Log("server Ads ID unity_adsID_fy:"+idArr[0]+"  unity_adsID_fy_mj:"+idArr[1]+"  unity_adsID_yc:"+idArr[2] + " admob_bannerID:"+idArr[3]);
				unity_adsID_fy = unity_adsID_fy_mj = unity_adsID_yc = idArr[0];
				admob_interstitialID_fy = admob_interstitialID_fy_mj = admob_interstitialID_yc = idArr[1];
				admob_rewardID_fy = admob_rewardID_fy_mj = admob_rewardID_yc = idArr[2];
				admob_bannerID = idArr[3];
			}
		#elif UNITY_IOS && !UNITY_EDITOR && !TEST
			// unity_adsID_fy  = "2640601";
			// admob_interstitialID_fy  = "ca-app-pub-1640039600835010/8144139597";
			// admob_rewardID_fy  = "ca-app-pub-1640039600835010/3322632722";
			// admob_bannerID = "ca-app-pub-1640039600835010/1223508028";
			//  GA.StartWithAppKeyAndChannelId("5b2db7cdf29d981738000013", "App Store");

			Application.targetFrameRate = 120;


			unity_adsID_fy  = "2675141";
			admob_interstitialID_fy  = "ca-app-pub-1640039600835010/3240138565";
			admob_rewardID_fy  = "ca-app-pub-1640039600835010/7738211804";
			admob_bannerID = "ca-app-pub-1640039600835010/3240138565";
			GA.StartWithAppKeyAndChannelId("5b472690f29d9839e2000035", "App Store");
		#elif TEST
				unity_adsID_fy = unity_adsID_fy_mj = unity_adsID_yc = "1796948";
				admob_interstitialID_fy = admob_interstitialID_fy_mj = admob_interstitialID_yc = "ca-app-pub-1640039600835010/9887807623";
				admob_rewardID_fy = admob_rewardID_fy_mj = admob_rewardID_yc = "ca-app-pub-1640039600835010/8714326755";
		#endif
		 


		// unity ads init
		Advertisement.Initialize(unity_adsID_fy);
		
		

		// 谷歌ads
		initAdmob();


	//	GetSystemLanguage();

 
	}

	// void OnGUI() {
	// 	GUILayout.Space(200);
	// 	GUILayout.Label(">>>>>>>>>>curLanguage:"+Application.systemLanguage.ToString());	
	// }

	public static void GetSystemLanguage()  
    { 
        // if (Application.platform == RuntimePlatform.IPhonePlayer)  
        // {  
        //     string name = CurIOSLang(); 
        //    Debug.Log(">>>>>>>>>>>>>>ios language name:"+name);
        // }  
   
    }  


	// 欧盟用户
	public bool isEU()
	{
		#if UNITY_ANDROID && !UNITY_EDITOR && !TEST
			AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
			string str = jo.Call<string>("isEU");
		 
			if(str != "0")
			{
				return true;
			}
		#endif
		#if TEST
		return true;
		#else
		return false;
		#endif
	} 

	// 获取游戏包名
	public string getPackageName()
	{
		string pgName = "com.fy.ProductName";
		#if UNITY_ANDROID && !UNITY_EDITOR && !TEST
			// AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			// AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
			// pgName = jo.Call<string>("getPackageName");
		#endif
		return pgName;
	} 



		public bool isReadUnityOrAdmobAwardAds()
		{
			if(PlatformHelper.inst().isReadUnityAds() || PlatformHelper.inst().isReadyadmobReward() )
			{
				return true;
			}
			return false;
		}

/***************************** unityads **********************************/
		public void unityShowAd(System.Action<int> action)
        {
			GameMgr.inst().adsPlaying = true;
			admobRewardFinish = false;
			unityAction = action;
            print(Advertisement.IsReady());
            if (Advertisement.IsReady())
            {
                Advertisement.Show();
            }else 
			{
				unityAction(0);
			}
		
        }

		public bool isReadUnityAds()
		{
			if (Advertisement.IsReady())
            {
                return true;
            }else 
			{
				return false;
			}
		}

       
        public void unityShowRewardedAd(System.Action<int> action)
        {
			unityAction = action;
            if (Advertisement.IsReady("rewardedVideo"))
            { 
              var option=new ShowOptions{resultCallback=HandleShowResult};
              Advertisement.Show("rewardedVideo", option);
            }else 
			{
				unityAction(0);
			}
        }
        private void HandleShowResult(ShowResult result)
        {
            switch (result)
            { 
                case ShowResult.Finished:
					unityAction(1);
					GameMgr.inst().umengEventByName("unityad_show");
					GameMgr.inst().adsPlayBackEvent();
                    print("the ad was successfully shown");
                    break;
                case ShowResult.Skipped:
					unityAction(0);
                    print("the ad was skipped before reaching the end");
                    break;
                case ShowResult.Failed:
					unityAction(0);
                    print("the ad failed to be shown");
                    break;
            }
        }


		/*******************admob ads *******************/

		void initAdmob()
		{
			
				ad = Admob.Instance();
				ad.bannerEventHandler += onBannerEvent;
				ad.interstitialEventHandler += onInterstitialEvent;
				ad.rewardedVideoEventHandler += onRewardedVideoEvent;
				ad.nativeBannerEventHandler += onNativeBannerEvent;
				//ad.setTesting(true);
			
			 
				ad.initAdmob(admob_bannerID, admob_interstitialID_fy);//all id are admob test id,change those to your
				 
				ad.loadInterstitial();
				
			//ad.setTesting(true);//show test ad
				ad.setGender(AdmobGender.MALE);
				string[] keywords = { "game","crash","male game"};
			//  ad.setKeywords(keywords);//set keywords for ad
				Debug.Log("admob inited -------------");

			//	Admob.Instance().showBannerAbsolute(AdSize.Banner, 20, 300);
			//1111 if(PlayerPrefs.GetInt("noAds",0) != 1)
			//  		ad.showBannerRelative(new AdSize(220,50), AdPosition.TOP_CENTER,0);
				


				loadRewardAds();
			
		}
		void loadRewardAds()
		{
		 
			ad.loadRewardedVideo(admob_rewardID_fy);//all id are admob test id,change those to your
			 
		}

		public void removeBanner()
		{
			 ad.removeAllBanner();
		}


		public bool isReadyAdmobInterstitial()
		{
			if(ad.isInterstitialReady())
			{
				return true;
			}
			return false;
		}

		public void showInterstitialAds(System.Action<int> action)
		{
			if(PlayerPrefs.GetInt("noAds",0) == 1) return;
			
			admobAction = action;
			if(ad == null)
			{
				admobAction(0);
				return;
			}
			if(ad.isInterstitialReady())
			{
				GameMgr.inst().adsPlaying = true;
				ad.showInterstitial(); 
			}else{
				admobAction(0);
			 
			}
		}

		public bool isReadyadmobReward()
		{
		//	return true;
			if(ad.isRewardedVideoReady())
			{
				return true;
			}else
			{
				loadRewardAds();
			}
			return false;
		}

		public void showRewardAds(System.Action<int> ation)
		{
			admobAction = ation;
			if(ad.isRewardedVideoReady())
			{
				GameMgr.inst().adsPlaying = true;
				admobRewardFinish = false;
				ad.showRewardedVideo();
			}else{
				admobAction(0);
			}

		//	admobAction(1);
		}

		public void showAdmobOrUnityRewardAds(System.Action<int> ation)
		{
			admobAction = ation;
			if(ad.isRewardedVideoReady())
			{
				GameMgr.inst().adsPlaying = true;
				admobRewardFinish = false;
				ad.showRewardedVideo();
			}else if(Advertisement.IsReady())
			{
				 
				unityShowRewardedAd(ation);
			}
			else{
				 
				admobAction(0);
			}

		//	admobAction(1);
		}


		void onInterstitialEvent(string eventName, string msg)
		{
			Debug.Log("handler onAdmobEvent---" + eventName + "   " + msg);
			if (eventName == AdmobEvent.onAdLoaded)
			{
				//Admob.Instance().showInterstitial();
			}
			else if(eventName == AdmobEvent.onAdClosed)
			{
				ad.loadInterstitial();
				admobAction(1);
			}
		}


		void onBannerEvent(string eventName, string msg)
		{
			Debug.Log("admob handler onAdmobBannerEvent---" + eventName + "   " + msg);
			if(eventName == AdmobEvent.onAdFailedToLoad)
			{
				 
			}
		}
		void onRewardedVideoEvent(string eventName, string msg)
		{
			Debug.Log("admob handler onRewardedVideoEvent---" + eventName + "  rewarded: " + msg);
			if (eventName == AdmobEvent.onAdLoaded)
			{
				//Admob.Instance().showInterstitial();
			}// 播放完获取奖励
			else if(eventName == AdmobEvent.onRewarded)
			{
				admobRewardFinish = true;
			}
			// 关闭广告
			else if(eventName == AdmobEvent.onAdClosed)
			{
			//	loadRewardAds();

				if(admobRewardFinish)
				{
					GameMgr.inst().umengEventByName("admob_show");
					GameMgr.inst().adsPlayBackEvent();
					admobAction(1);
				}else{
					admobAction(0);
				}
			}else if(eventName == AdmobEvent.onAdLeftApplication)
			{
				GameMgr.inst().umengEventByName("admob_click");
			}
		}
		void onNativeBannerEvent(string eventName, string msg)
		{
			Debug.Log("admob handler onAdmobNativeBannerEvent---" + eventName + "   " + msg);
		}


		public void Pay(int id,System.Action<int> buyAction)
		{
			this.buyAction = buyAction;

			
			#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
			// ads
			 	if(id == 1)
				{
					if(isReadUnityOrAdmobAwardAds())
					{
						showAdmobOrUnityRewardAds(buyAction);
					}else
					{
						GameMgr.inst().uiControl.gameTip.showAtion(42);
						buyAction(0);
					}
					
				}
				// 100 diamond
				else 
				{
					purchaser.DoIapPurchase(id,buyAction);
				}
			#else
		 		buyAction(1);
			#endif
 
		}
	
	 
}
