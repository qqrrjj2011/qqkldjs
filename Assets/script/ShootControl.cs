using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShootControl : MonoBehaviour {

	// Use this for initialization

	List<Transform> shootBalls = new List<Transform>();

	float angle = 0;


	public Transform arrow;

	public Transform line;
 

	// 参照点
	Vector2 referenceScreenPos;

	public float scaleOffset = 800;


	 // 回合
	[HideInInspector]
	public int round  = 1;         



	// 原点
	Vector2 oriScreenPos;

	Vector2 touchPos;

	Vector2 preTouchPos;
	// 小球间隔
	float ballGap;
	float minBallGap; 
 
	int ballsLstLen = 10;

	float shootScale = 1;

	public float timeScale = 2.5f;

	bool indrag = false;

	void Awake()
	{
		GameMgr.inst().shootControl = this;
	}
	void Start () {
	 
		oriScreenPos = Camera.main.WorldToScreenPoint(transform.position);
		referenceScreenPos = oriScreenPos + new Vector2(0,-100);
	 
	}


	public void save()
	{
		PlayerPrefs.SetInt("round", round);
	}

	public void loadSave()
	{
		round = PlayerPrefs.GetInt("round", 1);
	}


	public void halfStart()
	{
		round = PlayerPrefs.GetInt("bestRound", 0);
	}

	
	// Update is called once per frame
	void Update () {
 
        if(!(GameMgr.inst().GetGameState() == gameState.canShoot || GameMgr.inst().GetGameState() == gameState.ballRuning))return;
  
		if (Input.GetMouseButton(0) || Input.GetMouseButtonUp(0))
		{
			if(UIcontroller.getInst().IsPointerOverUIObject(Input.mousePosition) && !indrag)
			{
			//	Debug.Log(">>>>>>>>>>>>>>>>>click ui3");
				return;
			}
		}
		
			
			
		if(GameMgr.inst().canAddSpeed && Input.GetMouseButtonUp(0))
		{
			Time.timeScale = timeScale;
		}
 
		if(GameMgr.inst().GetGameState() != gameState.canShoot)return;
		
		if(Input.GetMouseButton(0))
		{
			 indrag = true;
		  

		}else if(Input.GetMouseButtonUp(0))
		{
			indrag = false;
		 
		}
	}


	public void clear()
	{
		round = 1;  
	}


	public bool IsPointerOverUIObject(Vector2 screenPosition)
    {
        //实例化点击事件
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        //将点击位置的屏幕坐标赋值给点击事件
        eventDataCurrentPosition.position = new Vector2(screenPosition.x, screenPosition.y);

        List<RaycastResult> results = new List<RaycastResult>();
        //向点击处发射射线
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

        return results.Count > 0;
    }

}
