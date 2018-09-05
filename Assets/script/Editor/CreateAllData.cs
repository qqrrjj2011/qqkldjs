using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Data;
using System.Data.Odbc;
using System.IO;
using Excel;
using System.Text.RegularExpressions;
using UnityEditor.SceneManagement;


// 打包配置数据 //
public class CreateAllData
{
    // 全平台
    bool allPlatform = false;
    // 打包所有数据 //
    [MenuItem("ConfigData/All DATA")]
    public static void PackAllData()
    {
        // PackLevel();
        // PackStage();

        PackLanguage();
        PackBlockScore();
        PackBlockType();
        
    }

     // 打包所有平台数据 //
    [MenuItem("ConfigData/All Platform DATA")]
    public static void PackAllPlatformData()
    {
        // PackLevel();
        // PackStage();
        // EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Android,BuildTarget.Android);
        // PackAllData();
        // EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Standalone,BuildTarget.StandaloneWindows);

        //  PackAllData();

        // EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Android,BuildTarget.Android);
        // EditorSceneManager.SaveOpenScenes();
        // PackAllData();
        
        
        // EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.iOS,BuildTarget.iOS);
        // EditorSceneManager.SaveOpenScenes();
        // PackAllData();
        
 
        // EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Standalone,BuildTarget.StandaloneWindows64);
        // EditorSceneManager.SaveOpenScenes();
        // PackAllData();
        

        // EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Android,BuildTarget.Android);
        // EditorSceneManager.SaveOpenScenes();
         
    }

   // [MenuItem("ConfigData/Level")]
    public static void PackLevel()
    {
        // string szDataFile = "Level.xlsx";
        // string szTable = "Level";

        // LevelData data = ScriptableObject.CreateInstance<LevelData>() as LevelData;
        // DataTable dtData = ReadDataTable(szDataFile, szTable);
        // data.ReadData(dtData);
        // //Debug.LogError(dtData.Rows[0][1].ToString());
        // CreateBundle(data, szTable);


    }
    
    [MenuItem("ConfigData/Language")]
    public static void PackLanguage()
    {
        string szDataFile = "PinballVSBlock_Language.xlsx";
        string szTable = "PinballVSBlock_Language";

        LanguageData data = ScriptableObject.CreateInstance<LanguageData>() as LanguageData;
        DataTable dtData = ReadDataTable(szDataFile, szTable);
        data.ReadData(dtData);
        CreateBundle(data, szTable);
    }

    [MenuItem("ConfigData/BlockScore")]
    public static void PackBlockScore()
    {
        string szDataFile = "BlockScore.xlsx";
        string szTable = "BlockScore";

        BlockScoreData data = ScriptableObject.CreateInstance<BlockScoreData>() as BlockScoreData;
        DataTable dtData = ReadDataTable(szDataFile, szTable);
        data.ReadData(dtData);
        CreateBundle(data, szTable);
    }

     [MenuItem("ConfigData/BlockType")]
    public static void PackBlockType()
    {
        string szDataFile = "BlockType.xlsx";
        string szTable = "BlockType";

        BlockTypeData data = ScriptableObject.CreateInstance<BlockTypeData>() as BlockTypeData;
        DataTable dtData = ReadDataTable(szDataFile, szTable);
        data.ReadData(dtData);
        CreateBundle(data, szTable);
    }

    // [MenuItem("ConfigData/Stage")]
    public static void PackStage()
    {
        string szDataFile = "Stage.xlsx";
        string szTable = "Stage";

        StageData data = ScriptableObject.CreateInstance<StageData>() as StageData;
        DataTable dtData = ReadDataTable(szDataFile, szTable);
        data.ReadData(dtData);
        CreateBundle(data, szTable);
    }

    

    // 从表中读取数据集 //
    private static DataTable ReadDataTable(string szDataFile, string szTable)
    {
        FileStream stream = File.Open(Application.dataPath + "/ConfigFile/" + szDataFile, FileMode.Open, FileAccess.Read);
        //Debug.LogError(Application.dataPath + "/ConfigFile/" + szDataFile);
        IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
        DataSet result = excelReader.AsDataSet();
        DataTable dtData = result.Tables[szTable];
        dtData.Rows.RemoveAt(0);
        return dtData;
    }


    // 打包数据到bundle data //
    private static void CreateBundle(Object dataObj, string szBundleFile)
    {
        // 调试用，生成的asset数据用于检查打包数据是否成功 //
        AssetDatabase.CreateAsset(dataObj, "Assets/StreamingAssets/DataFolder/" + szBundleFile + ".asset");

        // 打包数据 //
#if UNITY_ANDROID
                BuildPipeline.BuildAssetBundle (dataObj, null, "Assets/StreamingAssets/Android/" + szBundleFile + ".assetbundle", BuildAssetBundleOptions.CollectDependencies, BuildTarget.Android);
#elif UNITY_IPHONE
				BuildPipeline.BuildAssetBundle(dataObj, null, "Assets/StreamingAssets/IOS/" + szBundleFile + ".assetbundle", BuildAssetBundleOptions.CollectDependencies, BuildTarget.iOS);
#elif UNITY_STANDALONE_WIN || UNITY_EDITOR  
                BuildPipeline.BuildAssetBundle(dataObj, null, "Assets/StreamingAssets/PC/" + szBundleFile + ".assetbundle", BuildAssetBundleOptions.CollectDependencies, BuildTarget.StandaloneWindows);

#endif
        AssetDatabase.Refresh();
    }

    

}
