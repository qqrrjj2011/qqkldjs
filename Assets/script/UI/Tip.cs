using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Tip : MonoBehaviour {

	// Use this for initialization
	Transform txtTr;
	Text txt;
	Vector3 oriPos;
	Vector3 oriScale;
	void Awake() {
		txtTr = transform.Find("Text");
		txt = txtTr.GetComponent<Text>();
		oriScale = transform.localScale;
		oriPos = transform.localPosition;
	}
	
	public void showAtion(Vector3 pos)
	{
		gameObject.SetActive(true);
		transform.localPosition = pos;
		startAtion();
	} 

	public void showAtion()
	{
		gameObject.SetActive(true);
		transform.localPosition = oriPos;
		startAtion();
	} 

	public void showAtion(int languageID)
	{
		gameObject.SetActive(true);
		GameDataMgr.inst().setLanguageTxt(txt,languageID);
		gameObject.SetActive(true);
		transform.localPosition = oriPos;
		startAtion();
	} 

	void startAtion()
	{
		txtTr.localScale = new Vector3(0.3f,1,1);
		DOTween.Sequence().Append(txtTr.DOScale(1,0.6f).SetEase(Ease.OutBack))
						.AppendInterval(0.85f)
						.AppendCallback(()=>{
							gameObject.SetActive(false);
						}).SetUpdate(true);
		 
	}
}
