using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndLessLanguage : LanguageTxt {
	public override void updateTxt(string data)
	{
		GameDataMgr.inst().setLanguageTxt(transform.Find("btn_rank").Find("Text").GetComponent<Text>(),27);
		GameDataMgr.inst().setLanguageTxt(transform.Find("btn_restart").Find("Text").GetComponent<Text>(),9);
		GameDataMgr.inst().setLanguageTxt(transform.Find("btn_contine").Find("Text").GetComponent<Text>(),10);
	}
	 
}
