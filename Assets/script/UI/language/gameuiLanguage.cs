using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameuiLanguage : LanguageTxt {

	 public override void updateTxt(string data)
	 {
		 GameDataMgr.inst().setLanguageTxt(transform.Find("addItemBtn").Find("adsImg").Find("txt").GetComponent<Text>(),5);
		 GameDataMgr.inst().setLanguageTxt(transform.Find("bigItemBtn").Find("adsImg").Find("txt").GetComponent<Text>(),5);
		 GameDataMgr.inst().setLanguageTxt(transform.Find("lightItemBtn").Find("adsImg").Find("txt").GetComponent<Text>(),5);
		 GameDataMgr.inst().setLanguageTxt(transform.Find("dogItemBtn").Find("adsImg").Find("txt").GetComponent<Text>(),5);
		 GameDataMgr.inst().setLanguageTxt(transform.parent.transform.Find("btn_addSpeed").Find("Image").Find("Text").GetComponent<Text>(),6);
		 
	 }
}
