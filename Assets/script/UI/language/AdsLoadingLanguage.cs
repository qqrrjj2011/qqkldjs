using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdsLoadingLanguage : LanguageTxt {

	 public override void updateTxt(string data)
	 {
		 GameDataMgr.inst().setLanguageTxt(transform.Find("Text").GetComponent<Text>(),21);
		  
	 }
}
