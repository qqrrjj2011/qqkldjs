using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.EventSystems;

using UnityEngine.Advertisements;

using DG.Tweening;

public class UIcontroller : MonoBehaviour {
	
	public GameObject pauseUI;

	public GameObject gameUI;

	public GameObject gameOverObj;

 

	public GameObject dataMgrBtn;
 
	public GameObject shopUI;

	public GameObject loginUI;

 
	public MainUI mainUI;
 
	public LanguageUI languageUI;
 
 
	[HideInInspector]
	public GameOverUI gameOverUI;

	public BlockPanel blockPanel;
 
	public Text scoreTxt; 
	public Text diamondTxt;

 
	[HideInInspector]
	public Tip gameTip;

	int uiScore = 0; 

	bool openLoginUI = false;

	int havaSetLanguage = 0;

    public int dieType = 0;

	public Button pauseBtn;

	static UIcontroller instance;

	public static UIcontroller getInst()
	{
		return instance;
	}
	void Awake()
	{
		instance = this;
		havaSetLanguage = PlayerPrefs.GetInt("havaSetLanguage",0);
	  
	}
	// Use this for initialization
	void Start () {

		gameOverUI = gameOverObj.GetComponent<GameOverUI>();

 
	//	dataMgrBtn.SetActive(false);
 

		gameTip =  transform.Find("Tip").GetComponent<Tip>();
		
	 
		//gameStart();
			 
		 
		//GameMgr.inst().setGameState(gameState.mainUI);
		//GameMgr.inst().curGameMode = GameMode.none;

		//updateDiamondTxt(GameMgr.inst().diamondNum);
 
	}

	public void gameStart()
	{
 

		if(havaSetLanguage == 0)
		{
			languageUI.gameObject.SetActive(true);
		}
		
	}

	public void levelStart(int level)
	{
		
		GameMgr.inst().curGameMode = GameMode.level;
		GameMgr.inst().curLevelNum = level;
		GameMgr.inst().levelData.setLevel(level);
		Debug.Log("start level:"+GameMgr.inst().curLevelNum);
		StartCoroutine(gamePlay(0));
	}

	public void endLessStart()
	{
		GameMgr.inst().curGameMode = GameMode.endLess;
		gameReplay();
	}

	public void enterLevelMenu()
	{
 
		Time.timeScale = 1;
		pauseUI.gameObject.SetActive(false);
 
	}


	public void enterMain(int index = 0)
	{
		EnemyControl.getInst().clear();
		GameMgr.inst().setGameState(gameState.mainUI);
		Time.timeScale = 1;
		pauseUI.gameObject.SetActive(false);
		
		mainUI.gameObject.SetActive(true);
 
	}

	
	// Update is called once per frame
	// 复活
	public void Revive()
	{
		Time.timeScale = 1;
	 
		DOTween.Sequence().AppendInterval(4.0f)
		.AppendCallback(()=>{
			PlayerPrefs.SetInt("isCanRevive",0);
			StartCoroutine(gamePlay(3));
		});
	}

 
	IEnumerator gamePlay(int type = 0)
	{
		GameMgr.inst().musicControl.playBgMusic(soundType.bgShengming);
		pauseUI.SetActive(false);
		gameOverObj.SetActive(false);
		gameUI.SetActive(true);
		if(GameMgr.inst().curGameMode == GameMode.endLess)
			PlayerPrefs.SetInt("isSave",0);
	 
		//GameMgr.inst().setGameState(gameState.mainUI);
 
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();

		Time.timeScale = 1;
		
	 
		// 正常开始
		if(type == 0)
		{
			GameMgr.inst().setGameState(gameState.ballRuning);
            GameMgr.inst().player.reStart();
            EnemyControl.getInst().reStart();
 
		}
		// 暂停继续
		else if(type == 1) 
		{
			GameMgr.inst().setGameState(GameMgr.inst().GetPreGameState());
		}
 

		//updateDiamondTxt(GameMgr.inst().diamondNum);

		
	}


	public void gameReplay()
	{
	 
	//	GameMgr.inst().shootControl.clear();

		//GC.Collect();
		
		StartCoroutine(gamePlay(0));
	}

	public void gameHalfStart()
	{
	 
		GameMgr.inst().shootControl.clear();
		GameMgr.inst().curGameMode = GameMode.endLess;
		StartCoroutine(gamePlay(4));
	}

	
	public void playNextLevel()
	{
	 
		GameMgr.inst().shootControl.clear();
		
		// Debug.Log("curlevel:"+ GameMgr.inst().curLevelNum);
		// Debug.Log("maxlevel:"+ GameMgr.inst().maxLevel);
		if(GameMgr.inst().curLevelNum < GameMgr.inst().maxLevel)
		{
			GameMgr.inst().curLevelNum++;
			//GC.Collect();
			//Time.timeScale = 1;
			levelStart(GameMgr.inst().curLevelNum);
		}else
		{
			enterLevelMenu();
		}
	
	}

	public void showBlockPanel(bool show,int languageID = -1)
	{
		if(show)
		{
			blockPanel.gameObject.SetActive(true);
			blockPanel.init(languageID);
		}else
		{
			blockPanel.gameObject.SetActive(false);
		}
	}
 	public void gameOver(int state)
	{
		Time.timeScale = 0;
	 
			gameOverObj.SetActive(true);

			gameOverUI.initUI();
 
	}

	public void gamePause()
	{
		Time.timeScale = 0;
	 
		pauseUI.SetActive(true);
 

		GameMgr.inst().setGameState(gameState.pause);
	 
		GameMgr.inst().save();
 
	}

	public void showPauseBtn(bool b)
	{
		if(!b)
			pauseBtn.transform.Find("Image").GetComponent<Image>().color = new Vector4(1,1,1,0.8f);
		else 
			pauseBtn.transform.Find("Image").GetComponent<Image>().color = new Vector4(1,1,1,1);
		pauseBtn.interactable = b;
	}

	public void languageBtn()
	{
		languageUI.gameObject.SetActive(true);
	}

	public void shopBtn()
	{
		shopUI.SetActive(true);
	}

	public void loginBtn()
	{
		openLoginUI = true;
		loginUI.SetActive(true);
	}

 
	public void showAddSpeedBtn(bool b)
	{
 
	}


	public void updateScoreTxt(int num,bool ani = false)
	{
		uiScore+= num;
		scoreTxt.text = uiScore.ToString();
		if(ani)
		{
			scoreTxt.transform.DOComplete();
			scoreTxt.transform.DOPunchScale(new Vector3(1.14f,1.14f,1),0.5f,3);
		}
	}

	public void updateDiamondTxt(int num)
	{
		diamondTxt.text = num.ToString();
	}


	// 继续游戏
	public void gameContinue()
	{
		if(Time.timeScale != 1)
		{
			//Time.timeScale = 1;
			StartCoroutine(gamePlay(1));
		}
		// 读存档开始
		else if(PlayerPrefs.GetInt("isSave",0) == 1)
		{
			StartCoroutine(gamePlay(2));
		}else {
			StartCoroutine(gamePlay(0));
		}
 
	}

	// 无尽读档继续
	public void gameContinueBySave()
	{
 
		GameMgr.inst().shootControl.clear();

		GameMgr.inst().curGameMode = GameMode.endLess;
		if(PlayerPrefs.GetInt("isSave",0) == 1)
		{
			StartCoroutine(gamePlay(2));
		}else {
			StartCoroutine(gamePlay(0));
		}

	}
 



	public void exitGame()
	{

	}


	public bool IsPointerOverUIObject(Vector2 screenPosition)
    {
        //实例化点击事件
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        //将点击位置的屏幕坐标赋值给点击事件
        eventDataCurrentPosition.position = screenPosition;
        //获取画布上的 GraphicRaycaster 组件
        GraphicRaycaster uiRaycaster = GetComponent<Canvas>().gameObject.GetComponent<GraphicRaycaster>();

        List<RaycastResult> results = new List<RaycastResult>();
        // GraphicRaycaster 发射射线
        uiRaycaster.Raycast(eventDataCurrentPosition, results);

        return results.Count > 0;
    }
}
