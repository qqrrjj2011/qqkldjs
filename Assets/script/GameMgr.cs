using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Umeng;

// 游戏状态
public enum gameState
{
	none,
	gameOver,
	mainUI,
	startRun,
	ballRuning,
	blockMove,
	canShoot,
	waitBallMove,
	pause,
	ation
}

struct gameInfo
{
  public	int score_0_1k;
public	int score_1_5k;
  public  int score_2_1w;
    public int score_3_2w;
   public int score_4_3w;
   public int score_5_5w;
   public int score_6_10w;

public	int score_7_10wMore;
public	int challenge_05round;

public	int  challenge_10round;
public	int challenge_20round;
public	int challenge_40round;
public	int challenge_40roundMore;

}


public enum GameMode
{
	none,
	level,                    // 关卡          
	endLess                   // 无尽
}



public class GameMgr : MonoBehaviour {
	public delegate void MyEvent(string data);
	public MyEvent myEvent;

    public Player player;
    public EnemyControl enemyControl;


	public Vector2 screenHW = new Vector2(1080,1920);
	public Vector2 screenMid = new Vector2(540,960);

	[HideInInspector]
	public int curLevelNum = 1;

	public int maxLevel = 50;
	static GameMgr instance = null;
	[HideInInspector]
	public ShootControl shootControl = null;
 

	[HideInInspector]
	public UIcontroller uiControl = null;

	[HideInInspector]
	public MusicControl musicControl = null;
	// 游戏局数
	[HideInInspector]
	public int playNum = 0;
	gameState curGameState = gameState.none;
	gameState preGameState = gameState.none;
	gameInfo curGameinfo;

	bool isFocus = false;
	bool isPause = false;

	public bool adsPlaying = false;

	[HideInInspector]
	public int diamondNum = 0;


	// 可加速
	[HideInInspector]
	public bool canAddSpeed = false;

	[HideInInspector]
	// 可存档
	public bool canSave = false;  

	// 玩家总共玩次数(包含存档)
	[HideInInspector]
	public int totalPlayNum = 0;

	// 玩家总共玩次数(不包含存档))
	[HideInInspector]
	public int totalPlayNumNoSave = 0;


	[HideInInspector]
	public LevelData levelData;


	// 玩家进入游戏时间戳
	double enterGameTimeStamp = 0;

	// 玩家游戏开局时间
	double gameStartTimeStamp = 0;

	public static GameMgr inst()
	{
		return instance;
	}

	public GameMode curGameMode = GameMode.level;

	int collisionNum = 0;

	bool havaHit = false;

   void Awake() {
	  // Application.targetFrameRate=60;
	   instance = this;

	   curGameState = gameState.mainUI;

	   curGameinfo = new gameInfo();

	    diamondNum = PlayerPrefs.GetInt("diamondNum",0);
		totalPlayNum = PlayerPrefs.GetInt("totalPlayNum",0);

		levelData = new LevelData();

		//   Resolution[] resolutions = Screen.resolutions;  
        // foreach (Resolution res in resolutions) {  
        //     print(res.width + "x" + res.height);  
        // } 


		AppsFlyer.setAppsFlyerKey ("JS4D7K6orQauKANLStD2nb");
		/* For detailed logging */
		/* AppsFlyer.setIsDebug (true); */
		#if UNITY_IOS
		/* Mandatory - set your apple app ID
			NOTE: You should enter the number only and not the "ID" prefix */
		AppsFlyer.setAppID ("1402938766");
		AppsFlyer.trackAppLaunch ();
		#elif UNITY_ANDROID
		/* Mandatory - set your Android package name */
		AppsFlyer.setAppID ("com.fy.ProductName");
		/* For getting the conversion data in Android, you need to add the "AppsFlyerTrackerCallbacks" listener.*/
		AppsFlyer.init ("JS4D7K6orQauKANLStD2nb","AppsFlyerTrackerCallbacks");
		#endif

		enterGameTimeStamp = getTimeStampUnix();
 
   }
 

	// Use this for initialization
	void Start () {
	 
	}

	public void addDiamond(int num)
	{
		diamondNum += num;
		PlayerPrefs.SetInt("diamondNum",diamondNum);
	}

	public void setGameState(gameState gstate)
	{
		preGameState = curGameState;
		curGameState = gstate;
  
	}

	public gameState GetGameState()
	{
		return curGameState;
	}

	public gameState GetPreGameState()
	{
		return preGameState;
	}

	public void umengEventByName(string evtName)
	{
		Umeng.GA.Event(evtName);
	}

	public void umengLevel(int state,int levelId)
	{
		AppsFlyer.trackRichEvent(AFInAppEvents.LEVEL_ACHIEVED, new Dictionary<string, string>(){
           		 {AFInAppEvents.CONTENT_ID, "GamesNum"},
       	 	});
		if(state == 2)
		{
			Umeng.GA.StartLevel("Level_"+levelId);
		}else if(state == 0)
		{
			Umeng.GA.FailLevel("Level_"+levelId);
		}else if(state == 1)
		{
			Umeng.GA.FinishLevel("Level_"+levelId);

			AppsFlyer.trackRichEvent(AFInAppEvents.LEVEL_ACHIEVED, new Dictionary<string, string>(){
           		 {AFInAppEvents.CONTENT_ID, "Level_Num"},
       	 	});
		}

		if(levelId > 35)
		{
			AppsFlyer.trackRichEvent(AFInAppEvents.LEVEL_ACHIEVED, new Dictionary<string, string>(){
           		 {AFInAppEvents.CONTENT_ID, "Player_ProNum"},
       	 	});
		}
	}

	string[] payIDs = {"shop_NoAds","","shop_diamond100","shop_diamond800","shop_diamond2000"};
	public void purchaserEvent(int id)
	{
		AppsFlyer.trackRichEvent(AFInAppEvents.PURCHASE, new Dictionary<string, string>(){
            {AFInAppEvents.CONTENT_ID, "" + payIDs[id]},
          
        });
	}

	public void adsPlayBackEvent()
	{
		AppsFlyer.trackRichEvent(AFInAppEvents.CONTENT_VIEW, new Dictionary<string, string>(){
            {AFInAppEvents.CONTENT_ID, "Ads_WatchNum"},
            
        });
	}

	public void umengEvent(int score, int round)
	{
		if(score <= 1000)
		{
			curGameinfo.score_0_1k++;
			Umeng.GA.Event("score_0_1k");
		}else if(score <= 5000)
		{
			curGameinfo.score_1_5k++;
			Umeng.GA.Event("score_1_5k");
		}else if(score <= 10000)
		{
			curGameinfo.score_2_1w++;
			Umeng.GA.Event("score_2_1w");
		}else if(score <= 20000)
		{
			curGameinfo.score_3_2w++;
			Umeng.GA.Event("score_3_2w");
		}else if(score <= 30000)
		{
			curGameinfo.score_4_3w++;
			Umeng.GA.Event("score_4_3w");
		}if(score <= 50000)
		{
			curGameinfo.score_5_5w++;
			Umeng.GA.Event("score_5_5w");
		}else if(score <= 100000)
		{
			curGameinfo.score_6_10w++;
			Umeng.GA.Event("score_6_10w");
		}else 
		{
			curGameinfo.score_7_10wMore++;
			Umeng.GA.Event("score_7_10wMore");
		}

 
		if(round <= 5)
		{
			curGameinfo.challenge_05round++;
			Umeng.GA.Event("challenge_05round");
		}else if(round <= 10)
		{
			curGameinfo.challenge_10round++;
			Umeng.GA.Event("challenge_10round");
		}else if(round <= 20)
		{
			curGameinfo.challenge_20round++;
			Umeng.GA.Event("challenge_20round");
		}else if(round <= 40)
		{
			curGameinfo.challenge_40round++;
			Umeng.GA.Event("challenge_40round");
		}else {
			curGameinfo.challenge_40roundMore++;
			Umeng.GA.Event("challenge_40roundMore");
		}
	}
 
	public void umengPlayTotal(int num)
	{
		if(num < 10)
		{
			Umeng.GA.Event("PlayCount_0"+num);
		}else
		{
			Umeng.GA.Event("PlayCount_"+num);
		}
		
	}

	public void umengPlayTime(int num)
	{
		int m = num/60;
		string evtStr = "";
		if(m < 10)
		{
			evtStr = "PlayTime_0"+m+"min";
		}else
		{
			evtStr = "PlayTime_"+m+"min";
		}
		Umeng.GA.Event(evtStr);
	}

	public void setStart()
	{
		setGameStartTime();
		addTotalPlayNum();
 
	}
 
 
	void OnApplicationQuit()
	{
		Debug.Log(">>>>>>>>>>>>OnApplicationQuit");
		if(playNum > 10)
		{
			Umeng.GA.Event("times_perStart10more");

		}else{
			string str = playNum < 10? "times_perStart0"+playNum:"times_perStart"+playNum;
			Umeng.GA.Event(str);
		}
		
		if(canSave)
		{
			save();
			 Debug.Log(">>>>>>>>>OnApplicationQuit: save");
		}
		 
	}

	


	  void OnApplicationFocus(bool hasFocus)
	  {
		  Debug.Log(">>>>>>>>>OnApplicationFocus:"+hasFocus);
		  isFocus  = hasFocus;
	  }

	   void OnApplicationPause(bool pauseStatus)
	   {
		   isPause = pauseStatus;
		 //  Debug.Log(">>>>>>>>>OnApplicationPause: save");
		  // Debug.Log(">>>>>>>>>OnApplicationPause:"+pauseStatus +"  isFocus:"+isFocus + "   canSave:"+canSave);
		   if(pauseStatus && canSave)
		   {
			  save();
		   }
		   if(!pauseStatus && PlatformHelper.inst() && !adsPlaying)
		   {
			 //  Debug.Log(">>>>>>>>show back InterstitialAds");
			//   PlatformHelper.inst().showInterstitialAds((int state)=>{});
		   }

		   adsPlaying = false;
	   }

	   public bool useDiamonds(int num)
	   {
		   if(diamondNum - num >= 0)
		   {
			   diamondNum -= num;
			   PlayerPrefs.SetInt("diamondNum",diamondNum);
			   UIcontroller.getInst().updateDiamondTxt(diamondNum);
			   return true;
		   }
		   return false;
	   }

	   public void addTotalPlayNum()
	   {
		   totalPlayNum++;
		   totalPlayNumNoSave++;
		   PlayerPrefs.SetInt("totalPlayNum",totalPlayNum);
	   }

	   public bool havaSign()
	   {
		   System.DateTime day =System.DateTime.Now;
			string curDate = day.ToString("yyyyMMdd");
			string lastSignDate = PlayerPrefs.GetString("signDate","");
			if(curDate == lastSignDate)
			{
				return true;
			}
			return false;
	   }

	   public bool havaDoubleSign()
	   {
		   System.DateTime day =System.DateTime.Now;
			string curDate = day.ToString("yyyyMMdd");
		   return PlayerPrefs.GetInt("doubleState" + curDate,0) != 0;
	   }

			// 新的一天
		public bool isNewDay()
		{
			System.DateTime day =System.DateTime.Now;
			string curDate = day.ToString("yyyyMMdd");
			string lastLoginDate = PlayerPrefs.GetString("lastLoginDate","");
			if(lastLoginDate != curDate)
			{
				PlayerPrefs.SetString("lastLoginDate",curDate);
				return true;
			}
			return false;
		}

	   public void save()
	   {
		    PlayerPrefs.SetInt("diamondNum",diamondNum);
			if(curGameMode == GameMode.endLess)
			{
				PlayerPrefs.SetInt("isSave",1);
  
				shootControl.save();
			}
		
	   }

	   public void readSave()
	   {
		   totalPlayNum = PlayerPrefs.GetInt("totalPlayNum",0);
		   shootControl.loadSave();
		 
	   }
     
		// 时间 

    /// <summary>
    /// unix时间戳转换成日期
    /// </summary>
    /// <param name="unixTimeStamp">时间戳（秒）</param>
    /// <returns></returns>
    public double getTimeStampUnix()
    {
		System.DateTime time = System.DateTime.Now;
        double intResult = 0;
        System.DateTime startTime = System.TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
        intResult = (time - startTime).TotalSeconds;
        return intResult;
    }

	// 获取游戏时间长度（秒）
	public int getGameTotalTime()
	{
		int time = (int)(getTimeStampUnix() - enterGameTimeStamp);
		return time;
	}

	// 设置开始游戏时间
	public void setGameStartTime()
	{
		gameStartTimeStamp = getTimeStampUnix();
	}


	// 获取本局游戏时长
	public int getCurPlayTime()
	{
		return (int)(getTimeStampUnix() - gameStartTimeStamp);
	}
 
}
