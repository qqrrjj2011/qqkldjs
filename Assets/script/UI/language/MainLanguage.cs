using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainLanguage : LanguageTxt {
	public override void updateTxt(string data)
	{
		GameDataMgr.inst().setLanguageTxt(transform.Find("endlessBtn").Find("Text").GetComponent<Text>(),2);
		GameDataMgr.inst().setLanguageTxt(transform.Find("shopBtn").Find("Text").GetComponent<Text>(),4);
		 
		GameDataMgr.inst().setLanguageTxt(transform.Find("signBtn").Find("Text").GetComponent<Text>(),3);

		GameDataMgr.inst().setLanguageTxt(transform.Find("normalBtn").Find("Text").GetComponent<Text>(),26);
		GameDataMgr.inst().setLanguageTxt(transform.Find("ballBtn").Find("Text").GetComponent<Text>(),25);
	}
	
 
}
