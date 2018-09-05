using UnityEngine;
using System.Collections;
using UnityEditor;
/// <summary>
/// 打包资源类
/// </summary>
public class PackAsset : MonoBehaviour
{

    // 打包的目标平台 //
    public static readonly BuildTarget BuildPlatform =
    #if UNITY_ANDROID
            BuildTarget.Android;
    #elif UNITY_IPHONE
	        BuildTarget.iOS;
    #elif UNITY_STANDALONE_WIN || UNITY_EDITOR
            BuildTarget.StandaloneWindows;
    #endif

    /// <summary>
    /// 打包所有标记的资源，但不包括XLS
    /// </summary>
    [MenuItem("AssetBundle/Build AssetBundles")]
    static void BuildAllAssetBundles()
    {
        string path = ResPath.PackBaseUrl;
        Debug.LogError(path);
        BuildPipeline.BuildAssetBundles(path,BuildAssetBundleOptions.None,BuildPlatform);
        AssetDatabase.Refresh();
        Debug.Log("AssetBundle打包完毕");
    }

    //显示所有可以打包的资源的名字
    [MenuItem("AssetBundle/Get AssetBundle names")]
    static void GetNames()
    {
        var names = AssetDatabase.GetAllAssetBundleNames();
        foreach (var name in names)
            Debug.Log("AssetBundle: " + name);
    }


}
