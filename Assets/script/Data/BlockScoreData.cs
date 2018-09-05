using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Data;
 

/// <summary>
/// 关卡配置数据采集类
/// </summary>
[System.Serializable]
public class BlockScoreData : ScriptableObject {

	public List<BlockScoreInfo> blockScoreAllData = new List<BlockScoreInfo>();

    // 读取数据 //
    public void ReadData(DataTable i_dtData)
    {
        for (int i = 0; i < i_dtData.Rows.Count; i++)
        {
            DataRow row = i_dtData.Rows[i];
            BlockScoreInfo lc = new BlockScoreInfo();
            lc.getData(row.ItemArray);
            blockScoreAllData.Add(lc);
        }
    }

    // 根据id 获取关卡的配置数据 //
    public BlockScoreInfo getConfigByScore(int score)
    {
        int len = blockScoreAllData.Count;
        BlockScoreInfo preSc = blockScoreAllData[0];
        if(score < preSc.score)
        {
            return null;
        } 
        for (int i = 0; i < len; i++)
        {
            BlockScoreInfo sc = blockScoreAllData[i];
            if (sc.score > score && score >= preSc.score)
            {
                return preSc;
            }else
            {
                preSc = sc;
            }
        }
        return blockScoreAllData[len - 1];
    }


}


[System.Serializable]
public class BlockScoreInfo
{
    public int score;

    public float less;
    public float more;

    public float numPower;
 
    public void getData(object[] data)
    {
        int i=0;
        score = GameDataUtils.ReadInt(data[i++].ToString());
        less = GameDataUtils.ReadFloat(data[i++].ToString());
        more = GameDataUtils.ReadFloat(data[i++].ToString());
        numPower = GameDataUtils.ReadFloat(data[i++].ToString());
	}
 
}
