using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverLanguage : LanguageTxt {

	 public override void updateTxt(string data)
	 {
		 GameDataMgr.inst().setLanguageTxt(transform.Find("Text").GetComponent<Text>(),17);
		 GameDataMgr.inst().setLanguageTxt(transform.Find("adsImage").Find("Text (1)").GetComponent<Text>(),10);
		 
	 }
}
