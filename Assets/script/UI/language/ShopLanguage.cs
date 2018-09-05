using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopLanguage : LanguageTxt {

	 public override void updateTxt(string data)
	 {
		 GameDataMgr.inst().setLanguageTxt(transform.Find("reBuyBtn").Find("Text").GetComponent<Text>(),19);
		 GameDataMgr.inst().setLanguageTxt(transform.Find("list").Find("item0").Find("txt").GetComponent<Text>(),15);
		 GameDataMgr.inst().setLanguageTxt(transform.Find("Text").GetComponent<Text>(),4);
	 }
}
