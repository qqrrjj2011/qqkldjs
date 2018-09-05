using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Data;

public enum MyLanguage
{
	CN,
	EN,
	ES,
	PT,
	JP,
	KO,
	RU,
	FR,
	DE
}

/// <summary>
/// 关卡配置数据采集类
/// </summary>
[System.Serializable]
public class LanguageData : ScriptableObject {

	public List<LanguageInfo> languageAllData = new List<LanguageInfo>();

    // 读取数据 //
    public void ReadData(DataTable i_dtData)
    {
        for (int i = 0; i < i_dtData.Rows.Count; i++)
        {
            DataRow row = i_dtData.Rows[i];
            LanguageInfo lc = new LanguageInfo();
            lc.getData(row.ItemArray);
            languageAllData.Add(lc);
        }
    }

    // 根据id 获取关卡的配置数据 //
    public LanguageInfo getConfigByID(int id)
    {
        for (int i = 0; i < languageAllData.Count; i++)
        {
            LanguageInfo sc = languageAllData[i];
            if (sc.Id == id)
            {
                return sc;
            }
        }
        return null;
    }


}


[System.Serializable]
public class LanguageInfo
{
 
    public int Id;

	 /// <summary>
    /// 语言
    /// </summary>
    public List<string> languageLst = new List<string>();
 
    public void getData(object[] data)
    {
        int i=0;
        Id = GameDataUtils.ReadInt(data[i++].ToString());
		for(int h = 1; h < 10; h++)
		{
			string str = data[h].ToString();
			languageLst.Add(str);
		}
	}
   
}
