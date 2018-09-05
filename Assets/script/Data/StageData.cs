using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Data;
/// <summary>
/// 关卡配置数据采集类
/// </summary>
[System.Serializable]
public class StageData : ScriptableObject
{
    public List<StageConfig> allStageData = new List<StageConfig>();

    // 读取数据 //
    public void ReadData(DataTable i_dtData)
    {
        for (int i = 0; i < i_dtData.Rows.Count; i++)
        {
            DataRow row = i_dtData.Rows[i];
            StageConfig lc = new StageConfig();
            lc.getData(row.ItemArray);
            allStageData.Add(lc);
        }
    }

    // 根据id 获取关卡的配置数据 //
    public StageConfig getConfigByID(int id)
    {
        for (int i = 0; i < allStageData.Count; i++)
        {
            StageConfig sc = allStageData[i];
            if (sc.StageID == id)
            {
                return sc;
            }
        }
        return null;
    }
}
[System.Serializable]
public class StageConfig
{
    #region
    /// <summary>
    /// 小关卡ID
    /// </summary>
    public int StageID;

    /// <summary>
    /// 小关卡与大关卡的关联ID
    /// </summary>
    public int StageLevelID;

    /// <summary>
    /// 小关卡名字
    /// </summary>
    public string StageName;

    /// <summary>
    /// 小关卡描述
    /// </summary>
    public string StageDes;

    /// <summary>
    /// 小关卡掉落固定掉落ID与Treasure表的ID关联
    /// </summary>
    public int StageRewordTreasureID;

    /// <summary>
    /// 小关卡固定金钱奖励
    /// </summary>
    public int StageRewordCoin;

    /// <summary>
    /// 小关任务ID与Misson表的ID关联
    /// </summary>
    public List<int> StageMissionID;

    /// <summary>
    /// 本关卡消耗的体力
    /// </summary>
    public int StagePhysical;

    /// <summary>
    /// 当前小关卡的场景
    /// </summary>
    public string StageScene;

    /// <summary>
    /// 守护ID
    /// </summary>
    public List<int> stageLeadID;

    /// <summary>
    ///  关卡最底通关分数
    /// </summary>
    public int StageMinScore;

    /// <summary>
    /// 关卡场景图BG(现在不使用)
    /// </summary>
    public string StageSceneBG;

    /// <summary>
    /// 关卡Combo加成,当连击相同的怪物时的加成
    /// </summary>
    public int StageCombo;

    /// <summary>
    /// QTE开启分数 例如：200｜300  第一次开启为得到200分，第二次开启为再得到300分
    /// </summary>
    public List<int> StageQTEScore;

    /// <summary>
    /// QTE池ID，对应QTE表
    /// </summary>
    public List<int> StageQTEID;

    /// <summary>
    /// 陷阱类型1水柱，2木桶
    /// </summary>
    public List<int> TrapType;

    /// <summary>
    /// 陷阱守护停留时间
    /// </summary>
    public int TrapLeadStayTime;

    /// <summary>
    /// 陷阱刷新时间
    /// </summary>
    public int TrapCDTime;

    #endregion


    public void getData(object[] data)
    {
        int i=0;
        
        StageID = GameDataUtils.ReadInt(data[i++].ToString());

        StageLevelID = GameDataUtils.ReadInt(data[i++].ToString());

        StageName = data[i++].ToString();

        StageDes = data[i++].ToString();

        StageRewordTreasureID = GameDataUtils.ReadInt(data[i++].ToString());

        StageRewordCoin = GameDataUtils.ReadInt(data[i++].ToString());

        StageMissionID = GameDataUtils.ReadIntList(data[i++].ToString());

        StagePhysical = GameDataUtils.ReadInt(data[i++].ToString());

        StageScene = data[i++].ToString();

        stageLeadID = GameDataUtils.ReadIntList(data[i++].ToString());

        StageMinScore = GameDataUtils.ReadInt(data[i++].ToString());

        StageSceneBG = data[i++].ToString();

        StageCombo = GameDataUtils.ReadInt(data[i++].ToString());

        StageQTEScore = GameDataUtils.ReadIntList(data[i++].ToString());

        StageQTEID = GameDataUtils.ReadIntList(data[i++].ToString());

        TrapType = GameDataUtils.ReadIntList(data[i++].ToString());

        TrapLeadStayTime = GameDataUtils.ReadInt(data[i++].ToString());

        TrapCDTime = GameDataUtils.ReadInt(data[i++].ToString());
    }


}
