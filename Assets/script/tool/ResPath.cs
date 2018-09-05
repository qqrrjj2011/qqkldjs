using UnityEngine;
using System.Collections;

// 游戏中的资源路径常量定义 //
public class ResPath : MonoBehaviour
{
	// 打包文件的后缀 //
	public static readonly string bundleSuffix = ".YYdata";


	// 定义不同平台的资源读取路径 //
	public static readonly string ResBaseUrl =
        #if UNITY_STANDALONE_WIN || UNITY_EDITOR
            "file://" + Application.dataPath + "/StreamingAssets/";
		#elif UNITY_ANDROID
			"jar:file://" + Application.dataPath + "!/assets/";
		#elif UNITY_IPHONE
			"file://" +Application.dataPath + "/Raw/";
		#else
			string.Empty;
		#endif

	// 定义不同平台的资源读取路径 //
	public static readonly string PlatformUrl =
		#if UNITY_STANDALONE_WIN || UNITY_EDITOR
			"PC/";
        #elif UNITY_ANDROID
			"Android/";
		#elif UNITY_IPHONE
			"IOS/";
		#else
			string.Empty;
		#endif
	
	// 打包资源时的生成路径 //
	public static readonly string PackBaseUrl = 
		#if UNITY_ANDROID
			"Assets/StreamingAssets/Android/";
		#elif UNITY_IPHONE
			"Assets/StreamingAssets/IOS/";
		#elif UNITY_STANDALONE_WIN || UNITY_EDITOR
			"Assets/StreamingAssets/PC/";
		#else
			string.Empty;
		#endif

	// 角色 //
	public static readonly string MonsterAssetPath = "monster/";

	// 场景 //
	public static readonly string SceneAssetPath = "scene/";

	// UI //
	public static readonly string UIResPath = "UI/";

    //技能特效加载
    public static readonly string SkillEffectPath = "skilleffect/";

    //守护怪物
    public static readonly string LeadPath = "lead/";

    //场景图
    public static readonly string SceneTexturePath = "scenetexture/";
		
}
