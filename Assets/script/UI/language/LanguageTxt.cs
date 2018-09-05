using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LanguageTxt : MonoBehaviour {
    int preLanguageId = 1;
    void OnEnable()
    {
        GameMgr.inst().myEvent += updateTxt;
        if(preLanguageId != GameDataMgr.inst().languageID)
        {
            updateTxt("");
            preLanguageId = GameDataMgr.inst().languageID;
        }
    }
    
    void OnDisable()
    {
         GameMgr.inst().myEvent -= updateTxt;
    }

    public virtual void updateTxt(string data)
    {

    }
	 
}
