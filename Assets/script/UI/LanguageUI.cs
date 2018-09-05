using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LanguageUI : MonoBehaviour {
	 
	Dictionary<string,int> languageNameValue = new Dictionary<string,int>();
	// Use this for initialization
	
	void Awake()
	{
		Debug.Log(">>>>>>>>>>>>>:");
		
		languageNameValue.Add(SystemLanguage.ChineseSimplified.ToString(),0);
		languageNameValue.Add(SystemLanguage.Chinese.ToString(),0);
		languageNameValue.Add(SystemLanguage.ChineseTraditional.ToString(),0);

		languageNameValue.Add(SystemLanguage.English.ToString(),1);
		languageNameValue.Add(SystemLanguage.Spanish.ToString(),2);
		languageNameValue.Add(SystemLanguage.Portuguese.ToString(),3);
		languageNameValue.Add(SystemLanguage.Japanese.ToString(),4);
		languageNameValue.Add(SystemLanguage.Korean.ToString(),5);
		languageNameValue.Add(SystemLanguage.Russian.ToString(),6);
		languageNameValue.Add(SystemLanguage.French.ToString(),7);
		languageNameValue.Add(SystemLanguage.German.ToString(),8);

		for(int i = 0; i < 9; i++)
		{
			Transform sendBtn = transform.Find("Button"+i);
			sendBtn.Find("gou").gameObject.SetActive(false);
			sendBtn.GetComponent<Button>().onClick.AddListener(delegate{
				onClickBtn(sendBtn);
			});
		}

		

		int lgId = GameDataMgr.inst().languageID;
		transform.Find("Button"+lgId).Find("gou").gameObject.SetActive(true);

		transform.Find("closeBtn").GetComponent<Button>().onClick.AddListener(()=>{
			gameObject.SetActive(false);
		});
 

	}

	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start()
	{
		if(PlayerPrefs.GetInt("languageID",-1) == -1)
		{
			string curlanguageStr = Application.systemLanguage.ToString();
			//curlanguageStr = "ChineseSimplified";
			// 自动设置语言
			if(languageNameValue.ContainsKey(curlanguageStr))
			{
				GameDataMgr.inst().languageID = languageNameValue[curlanguageStr];
					GameMgr.inst().myEvent("");

				gameObject.SetActive(false);
			}else
			{
				// 默认英文
				GameDataMgr.inst().languageID = 1;
					GameMgr.inst().myEvent("");

				gameObject.SetActive(false);
			}
		
		}
	}

	void onClickBtn(Transform send)
	{
		for(int i = 0; i < 9; i++)
		{
			Transform btn = transform.Find("Button"+i);
			btn.Find("gou").gameObject.SetActive(false);
		}
		int languageID = int.Parse(send.name.Substring(6,1));
		//Debug.Log("languageBtnName:"+send.name.Substring(6,1));
		send.Find("gou").gameObject.SetActive(true);
		PlayerPrefs.SetInt("languageID",languageID);
		PlayerPrefs.SetInt("havaSetLanguage",1);

		GameDataMgr.inst().languageID = languageID;

		GameMgr.inst().myEvent("");

		gameObject.SetActive(false);
		
	}
	
 
}
