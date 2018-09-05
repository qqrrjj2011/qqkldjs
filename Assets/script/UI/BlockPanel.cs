using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockPanel : MonoBehaviour {
	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	/// 
	Text txt;
	void Awake()
	{
		txt = transform.Find("Text").GetComponent<Text>();
	}
	// Use this for initialization
	public void init(int languageID)
	{
		GameDataMgr.inst().setLanguageTxt(txt,languageID);
	}
}
