using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FailLanguage : LanguageTxt {

 	public override void updateTxt(string data)
	 {
		 GameDataMgr.inst().setLanguageTxt(transform.Find("window").Find("Text").GetComponent<Text>(),23);
		  GameDataMgr.inst().setLanguageTxt(transform.Find("window").Find("adsImage").Find("Text (1)").GetComponent<Text>(),10);
	 }
}
