using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinLanguage : LanguageTxt {

	public override void updateTxt(string data)
	 {
		 GameDataMgr.inst().setLanguageTxt(transform.Find("window").Find("Text").GetComponent<Text>(),22);
		 
	 }
}
