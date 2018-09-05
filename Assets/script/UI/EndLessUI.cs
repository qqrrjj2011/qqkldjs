using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EndLessUI : MonoBehaviour {
	public Transform rankBtn;
	public Transform restartBtn;
	public Transform contineBtn;

	public Transform halfBtn;

	public Transform dropBall;
	public Transform logo;

	List<Transform> balls = new List<Transform>();


	// Use this for initialization
	void Start () {
		contineBtn.GetComponent<Button>().onClick.AddListener(()=>{
			transform.parent.gameObject.SetActive(false);
				
			GameMgr.inst().uiControl.gameContinueBySave();
			//GameMgr.inst().uiControl.gameHalfStart();
		});
		rankBtn.GetComponent<Button>().onClick.AddListener(()=>{
				 
			});
		restartBtn.GetComponent<Button>().onClick.AddListener(()=>{
				transform.parent.gameObject.SetActive(false);
					
				GameMgr.inst().uiControl.endLessStart();
			});


	 
		
		// halfBtn.GetComponent<Button>().onClick.AddListener(()=>{
		// 		transform.parent.gameObject.SetActive(false);
					
		// 		GameMgr.inst().uiControl.gameHalfStart();
		// 	});

		doStartAni();
	}


	void OnEnable() {
		balls.Clear();
		StartCoroutine(runDropBall());
	}

	void OnDisable()
	{
		foreach (var item in balls)
		{
			DestroyObject(item.gameObject);
		}
		balls.Clear();
	}

	IEnumerator runDropBall()
	{
		while (true)
		{
			yield return new WaitForSeconds(4f);
			if(balls.Count < 150)
			{
				Transform ball = Transform.Instantiate(dropBall);
				ball.SetParent(transform);
				ball.localScale = Vector3.one;
				balls.Add(ball);
			}
			
		}

	}

	void doStartAni()
	{
		Vector3 logoOriPos = logo.localPosition;

		Vector2 pos = logo.GetComponent<RectTransform>().anchoredPosition;
	//	Debug.Log(">>>>>>>>>>>>Vector2:"+pos + " Vector3"+logoOriPos);

		 logo.localPosition = new Vector3(14,-377,0) + logoOriPos;
		
		 logo.DOLocalMoveY(logoOriPos.y,1.5f).SetEase(Ease.InOutBack);

		 	contineBtn.Find("Text").GetComponent<Text>().color = new Color(1,1,1,0);
			contineBtn.GetComponent<Image>().material.color = new Color(1,1,1,0);
			restartBtn.Find("Text").GetComponent<Text>().color = new Color(1,1,1,0);
			restartBtn.GetComponent<Image>().material.color = new Color(1,1,1,0);
			contineBtn.Find("effects_AN_tiaosaoguang").gameObject.SetActive(false);
		
		restartBtn.localScale = Vector2.one*0.5f;
		contineBtn.localScale = Vector2.one*0.5f;
		DOTween.Sequence().AppendInterval(1.5f).AppendCallback(()=>{
			restartBtn.GetComponent<Image>().material.DOFade(1,1);
			restartBtn.Find("Text").GetComponent<Text>().DOFade(1,0.6f);
			restartBtn.DOScale(1,0.7f).SetEase(Ease.OutBack);
		})
		.AppendInterval(0.45f)
		.AppendCallback(()=>{
			contineBtn.GetComponent<Image>().material.DOFade(1,1).OnComplete(()=>{
				contineBtn.Find("effects_AN_tiaosaoguang").gameObject.SetActive(true);
			});
			contineBtn.DOScale(1,0.7f).SetEase(Ease.OutBack);
			contineBtn.Find("Text").GetComponent<Text>().DOFade(1,0.6f);
		});
		 
		
	}
	
	
}
