using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Data;
 

/// <summary>
/// 关卡配置数据采集类
/// </summary>
[System.Serializable]
public class BlockTypeData : ScriptableObject {

	public List<BlockTypeInfo> blockTypeAllData = new List<BlockTypeInfo>();

    // 读取数据 //
    public void ReadData(DataTable i_dtData)
    {
        for (int i = 0; i < i_dtData.Rows.Count; i++)
        {
            DataRow row = i_dtData.Rows[i];
            BlockTypeInfo lc = new BlockTypeInfo();
            lc.getData(row.ItemArray);
            blockTypeAllData.Add(lc);
        }
    }

    // 根据id 获取关卡的配置数据 //
    public BlockTypeInfo getConfigByScore(int score)
    {
        int len = blockTypeAllData.Count;
        BlockTypeInfo preSc = blockTypeAllData[0];
        if(score == 0)return blockTypeAllData[0];
        for (int i = 0; i < len; i++)
        {
            BlockTypeInfo sc = blockTypeAllData[i];
            if (score >= preSc.score && score < sc.score)
            {
                return preSc;
            }
            preSc = sc;
        }
        return blockTypeAllData[len - 1];
    }


}


[System.Serializable]
public class BlockTypeInfo
{
    public int score;

    public List<int> pblLst = new List<int>();
    
    public int rotationPbl;
    public void getData(object[] data)
    {
        int i=0;
        score = GameDataUtils.ReadInt(data[i].ToString());
		for(int h = 1; h < 14; h++)
		{
            int pbl = GameDataUtils.ReadInt(data[h].ToString());
			 
			pblLst.Add(pbl);
            i++;
		}

        rotationPbl = GameDataUtils.ReadInt(data[14].ToString());
	}

   
}
