using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginLanguage : LanguageTxt {

	// Use this for initialization

	 public override void updateTxt(string data)
	 {
	//	 GameDataMgr.inst().setLanguageTxt(transform.Find("titletxt").GetComponent<Text>(),11);
		 GameDataMgr.inst().setLanguageTxt(transform.Find("checkBtn").Find("Text").GetComponent<Text>(),3);
		 GameDataMgr.inst().setLanguageTxt(transform.Find("doubleBtn").Find("Text").GetComponent<Text>(),14);
	 
	 }
}
