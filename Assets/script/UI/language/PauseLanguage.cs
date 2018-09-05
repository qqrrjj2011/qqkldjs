using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseLanguage : LanguageTxt {

	 public override void updateTxt(string data)
	 {
		 GameDataMgr.inst().setLanguageTxt(transform.Find("btn_restart").Find("Text").GetComponent<Text>(),9);
		 GameDataMgr.inst().setLanguageTxt(transform.Find("btn_contine").Find("Text").GetComponent<Text>(),10);
		 GameDataMgr.inst().setLanguageTxt(transform.Find("btn_home").Find("Text").GetComponent<Text>(),8);
	//	 GameDataMgr.inst().setLanguageTxt(transform.Find("btn_language").Find("Text").GetComponent<Text>(),16);
	 }
}
