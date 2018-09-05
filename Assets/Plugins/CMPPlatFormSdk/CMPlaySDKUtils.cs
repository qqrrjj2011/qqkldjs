using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using System;
#if UNITY_IOS && UNITY_EDITOR
using UnityEditor;
using UnityEditor.Callbacks;
using System.IO;
using UnityEditor.iOS.Xcode;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml;
#endif
public class CMPlaySDKUtils : MonoBehaviour
{
    public static CMPlaySDKUtils Instance;
	public bool isLoadingViewShow = false;
#if UNITY_ANDROID
    private AndroidJavaObject cmp_promotionUtils_unity;
    private AndroidJavaObject infoc_unity;
    private AndroidJavaObject cloud_hepler_unity;
    private AndroidJavaObject reportUtils;

    public static class SingletonHolder
    {
        public static AndroidJavaObject instance_context;
        static SingletonHolder()
        {
            using (AndroidJavaClass cls_UnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            {
                instance_context = cls_UnityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            }
        }
    }
#endif

#if UNITY_IPHONE
	[DllImport("__Internal")]
	public static extern void CMPInitCMPlaySDK();

	[DllImport("__Internal")]
	public static extern void CMPOnGamePause();

	[DllImport("__Internal")]
	public static extern void CMPOnGameResume();

	[DllImport("__Internal")]
	public static extern void CMPOnGameStop();

	[DllImport("__Internal")]
	public static extern void CMPSetEnableDebug(bool enabled);

	[DllImport("__Internal")]
	public static extern void CMPReportData(string tableName, string data);

	[DllImport("__Internal")]
	public static extern void CMPReportEvent(int action);

	[DllImport("__Internal")]
	public static extern string CMPGetCloudStringValue(int function, string section, string key, string defValue);

	[DllImport("__Internal")]
	public static extern int CMPGetCloudIntValue(int function, string section, string key, int defValue);

	[DllImport("__Internal")]
	public static extern long CMPGetCloudLongValue(int function, string section, string key, long defValue);

	[DllImport("__Internal")]
	public static extern bool CMPGetCloudBoolValue(int function, string section, string key, bool defValue);

	[DllImport("__Internal")]
	public static extern double CMPGetCloudDoubleValue(int function, string section, string key, double defValue);

	[DllImport("__Internal")]
	public static extern void CMPPullCloudConfigDataWithLanguage(string language);

    [DllImport("__Internal")]
    static extern bool CMPCanShowOpenScreen(bool isNewUser, int scene);

    [DllImport("__Internal")]
    static extern void CMPShowOpenScreen();

    [DllImport("__Internal")]
    static extern bool CMPCanShowResultCard();

    [DllImport("__Internal")]
    static extern string CMPGetResultCardData();

    [DllImport("__Internal")]
    static extern void CMPClickResultCard(string data);

    [DllImport("__Internal")]
    static extern bool CMPCanShowSettingCard();

    [DllImport("__Internal")]
	static extern bool CMPCanShowSettingCardRedDot();

    [DllImport("__Internal")]
    static extern string CMPGetSettingCardData();

    [DllImport("__Internal")]
    static extern void CMPClickSettingCard(string data);

    [DllImport("__Internal")]
    static extern bool CMPCanShowFamilyGames();

    [DllImport("__Internal")]
    static extern bool CMPCanShowFamilyGamesRedDot();

    [DllImport("__Internal")]
    static extern string CMPGetFamilyGamesData();

    [DllImport("__Internal")]
    static extern void CMPClickFamilyGames(string data);

    [DllImport("__Internal")]
    static extern bool CMPCanShowRewardedVideo(int scene, bool isClick);

    [DllImport("__Internal")]
    static extern void CMPShowRewardedVideo(int scene);

	[DllImport("__Internal")]
	static extern bool CMPCanShowHitTopRewardedVideo(int scene, bool isClick);

	[DllImport("__Internal")]
	static extern void CMPShowHitTopRewardedVideo(int scene);
        
    [DllImport("__Internal")]
    static extern void CMPLoadActivity(string url);
            
    [DllImport("__Internal")]
    static extern void CMPLoadFeedback(string url,string appID);
    
    [DllImport("__Internal")]
    static extern void CMPInitInsertPage(string url,string appID);

    [DllImport("__Internal")]
    static extern bool CMPCanShowInsertPage(bool isNewUser);
        
    [DllImport("__Internal")]
    static extern void CMPShowInsertPage();

	[DllImport("__Internal")]
	public static extern int CMPGetCurrentNetStatue();

	[DllImport("__Internal")]
	public static extern void CMPShowLoadingView(float red, float green, float blue, float alpha);

	[DllImport("__Internal")]
	public static extern void CMPDismissLoadingView();

	[DllImport("__Internal")]
	public static extern string CMPGetCurrentTime();

	[DllImport("__Internal")]
	public static extern string CMPGetDeviceIdentifier();
#endif

#if UNITY_IOS && UNITY_EDITOR
	[PostProcessBuild]
	static void OnPostProcessBuild(BuildTarget target, string path)
	{
		string projPath = path + "/Unity-iPhone.xcodeproj/project.pbxproj";

		PBXProject pbxProj = new PBXProject();
		pbxProj.ReadFromFile(projPath);
		string targetGuid = pbxProj.TargetGuidByName("Unity-iPhone");

		CopyAndAddFile(pbxProj, targetGuid, path, "Plugins/iOS/CMPlaySDK/kfmt.dat", "Libraries/Plugins/iOS/CMPlaySDK");
		pbxProj.WriteToFile(projPath);
	}

	static void CopyAndAddFile(PBXProject pbxProj, string targetGuid, string pathToBuiltProject, string srcFilePath, string dstPath)
	{
		string absSrcFilePath = Application.dataPath + "/" + srcFilePath;
		string srcFileName = System.IO.Path.GetFileName(absSrcFilePath);
		string fullDstPath = pathToBuiltProject + "/" + dstPath;
		string absDestFileName = fullDstPath + "/" + srcFileName;
		string relDestFileName = dstPath + "/" + srcFileName;

		if (!Directory.Exists(fullDstPath))
			Directory.CreateDirectory(fullDstPath);

		File.Copy(absSrcFilePath, absDestFileName, true);
		pbxProj.AddFileToBuild(targetGuid
			, pbxProj.AddFile(relDestFileName, relDestFileName, PBXSourceTree.Source)
		);
	}
#endif

	void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Init();
			DontDestroyOnLoad(gameObject);
        }
    }
		
	public static void Initialize()
	{
		if (Instance == null) 
		{
			Debug.Log ("Initialize CMPlaySDK");
			GameObject gameobject = GameObject.Find ("Nativeutil");
			if (gameobject == null) 
			{
				gameobject = new GameObject ("Nativeutil");
			}
			if (gameobject != null) 
			{
				gameobject.AddComponent<CMPlaySDKUtils> ();
			}
		}
	}

    void Init()
    {
        Debug.Log("CMPlaySDKUtils.Init()");
#if (UNITY_EDITOR || DISBLE_PLATFORM)
#elif UNITY_ANDROID
        Debug.Log("CMPlaySDKUtils Init()");

        //init android java class instance
        cmp_promotionUtils_unity = new AndroidJavaClass("com.cmplay.internalpush.CMPPromotionUtils");
        infoc_unity = new AndroidJavaClass("com.cmplay.kinfoc.report.UnityInfocUtil");
        cloud_hepler_unity = new AndroidJavaClass("com.cmplay.internalpush.CloudHeplerProxy");
        reportUtils = new AndroidJavaClass("com.cmplay.internalpush.CMPReportUtils");

        //Init CMPlay SDK
        InitCMPlaySDK();

        // Note:  after init your appsflyer sdk, set your appsflyerDeviceId that get from appsflyer sdk API
        string appsflyerDeviceId = "1519823815330-5705721926266935318";
        InfocEvent("setAppsflyerDeviceId", appsflyerDeviceId);


        //set init finish
        SetUnityInit(true);

        //call when init finish
        InfocEvent("onResume");

        //example of pull cloud config data, should call when game lauch up or on switch language 
        PullCloudConfigData("your_lan");

        //example of report data
        ReportData("tablename", "key=value&key=value");

		////example of get cloude value
		//int func_type = 2;
		//string section_name = "example_section_name";
		//string key_name = "example_key";
		//int defaultValue = 0;
		//int cloud_value = getIntValue(func_type, section_name, key_name, defaultValue);
  //      long lV = getLongValue(func_type, section_name, key_name, defaultValue);
  //      bool bV = getBooleanValue(func_type, section_name, key_name, false);
  //      double def = 0;
  //      double dV = getDoubleValue(func_type, section_name, key_name, def);
  //      string strV = getStringValue(func_type, section_name, key_name, "");
  //      Debug.Log("getIntValue:" + cloud_value);

  //      //example of cross promotion
  //      if (CanShowOpenScreen(1, false))
  //      {
  //          ShowOpenScreen();
  //      }
        
  //      if (CanShowHitTopRewardedVideo(1, true))
  //      {
  //          ShowHitTopRewardedVideo(1);
  //      }

  //      if (CanShowRewardedVideo(1, true))
  //      {
  //          ShowRewardedVideo(1);
  //      }

#elif UNITY_IPHONE
        InitCMPlaySDK();
		CMPOnGameResume();
		//example of get cloude value
		//int func_type = 2;
		//string section_name = "example_section_name";
		//string key_name = "example_key";
		//int defaultValue = 0;
		//int cloud_value = CMPGetCloudIntValue(func_type, section_name, key_name, defaultValue);
#endif
    }

    void OnApplicationPause(bool isPause)
    {
        Debug.Log("infoc______ OnApplicationPause " + isPause);
        if (isPause)
        {
#if (UNITY_EDITOR || DISBLE_PLATFORM)

#elif UNITY_ANDROID
            InfocEvent("onPause");
#elif UNITY_IOS || UNITY_IPHONE
			CMPOnGamePause();
#endif
        }
        else
        {
#if (UNITY_EDITOR || DISBLE_PLATFORM)

#elif UNITY_ANDROID
            InfocEvent("onResume");
#elif UNITY_IOS || UNITY_IPHONE
			CMPOnGameResume();
#endif
        }
    }

    void OnApplicationQuit()
    {
        Debug.Log("infoc______ OnApplicationQuit ");
#if (UNITY_EDITOR || DISBLE_PLATFORM)

#elif UNITY_ANDROID
        InfocEvent("onGameExit");
        unInitPromotionSdk();
#elif UNITY_IOS || UNITY_IPHONE
		CMPOnGameStop();
#endif
    }

    public void showLoadingView(float red, float green, float blue, float alpha) {
		if (!isLoadingViewShow) {
			isLoadingViewShow = true;
#if (UNITY_EDITOR || DISBLE_PLATFORM)

#elif UNITY_IOS || UNITY_IPHONE
			CMPShowLoadingView(red, green, blue, alpha);
#elif UNITY_ANDROID
		// TODO
#endif
		}
	}

	public void dismissLoadingView(){
		if (isLoadingViewShow) { 
			isLoadingViewShow = false;
#if (UNITY_EDITOR || DISBLE_PLATFORM)

#elif UNITY_IOS || UNITY_IPHONE
			CMPDismissLoadingView();
#elif UNITY_ANDROID
		// TODO
#endif
		}
	}
	
	public string getCurrentTime()
	{
#if (UNITY_EDITOR || DISBLE_PLATFORM)
		return "";
#elif UNITY_ANDROID
	// TODO
		return "";
#elif UNITY_IPHONE || UNITY_IOS
		return CMPGetCurrentTime();
#else
		return "";
#endif
	}

	public int getNetworkStatus() {
#if (UNITY_EDITOR || DISBLE_PLATFORM)
		return 0;
#elif UNITY_ANDROID
	// TODO
		return 0;
#elif UNITY_IPHONE || UNITY_IOS
	return CMPGetCurrentNetStatue();;
#else
		return 0;
#endif
	}

	public string getDeviceId() {
#if (UNITY_EDITOR || DISBLE_PLATFORM)
		return "";
#elif UNITY_ANDROID
	// TODO
		return cmp_promotionUtils_unity.CallStatic<string>("getUniquePsuedoID", SingletonHolder.instance_context);
#elif UNITY_IPHONE || UNITY_IOS
		return CMPGetDeviceIdentifier();
#else
		return "";
#endif
	}

    public void InfocEvent(string eventName, params object[] args)
    {
#if (UNITY_EDITOR || DISBLE_PLATFORM)
#elif UNITY_ANDROID
        Debug.Log("infoc______ " + eventName);
        infoc_unity.CallStatic(eventName, args);
#endif
    }

   


    /// <summary>
    /// Init CMPlay SDK
    /// </summary>
    public void InitCMPlaySDK()
    {
#if (UNITY_EDITOR || DISBLE_PLATFORM)
#elif UNITY_ANDROID
        cmp_promotionUtils_unity.CallStatic("initPromotionSdk", SingletonHolder.instance_context);
#elif UNITY_IPHONE
		CMPInitCMPlaySDK();
#endif
    }

    /// <summary>
    /// when exit game, uninit Promotion Sdk
    /// </summary>
    public void unInitPromotionSdk()
    {
#if (UNITY_EDITOR || DISBLE_PLATFORM)
#elif UNITY_ANDROID
        cmp_promotionUtils_unity.CallStatic("unInitPromotionSdk");
#elif UNITY_IPHONE
#endif
    }

    /// <summary>
    /// Game language settings are very important, 
    /// language is the set of current language
    /// We will pull data from cloud on open game or on switch language, 
    /// this method must be called, and use of asynchronous to pull data.
    /// </summary>
    /// <param name="language">language have set</param>
    public void PullCloudConfigData(string language)
	{
		string lang = "en";
		if (language == "English")
			lang = "en";
		else if (language == "French")
			lang = "fr";
		else if (language == "Spanish")
			lang = "es";
		else if (language == "Portuguese")
			lang = "pt";
		else if (language == "Italian")
			lang = "it";
		else if (language == "German")
			lang = "de";
		else if (language == "ChineseSimplified")
			lang = "zh_cn";
		else if (language == "ChineseTraditional")
			lang = "zh_tw";
		else if (language == "Japanese")
			lang = "ja";
		else if (language == "Korean")
			lang = "ko";
		else if (language == "Russian")
			lang = "ru";
		else if (language == "Arabic")
			lang = "ar";
		else if (language == "Indonesian")
			lang = "id";
		else if (language == "Turkish")
			lang = "tr";
		else if (language == "Norwegian")
			lang = "nb";
		else if (language == "Finnish")
			lang = "fi";
		else if (language == "Swedish")
			lang = "sv";
		else if (language == "Vietnamese")
			lang = "vi";
		else if (language == "Dutch")
			lang = "nl";
		else if (language == "Malay")
			lang = "ms";
		else if (language == "Thai")
			lang = "th";
		else if (language == "Hindi")
			lang = "hi";
		else if (language == "Polish")
			lang = "pl";
		else if (language == "Czech")
			lang = "cs";
		else if (language == "Slovak")
			lang = "sk";
		else if (language == "Ukrainian")
			lang = "uk";
		else if (language == "Croatian")
			lang = "hr";
		else if (language == "Romanian")
			lang = "ro";
		else if (language == "Hebrew")
			lang = "he";
		else
			lang = language;
        Debug.Log("PullCloudConfigData===:" + lang);
#if (UNITY_EDITOR || DISBLE_PLATFORM)
#elif UNITY_ANDROID
		cmp_promotionUtils_unity.CallStatic("pullCloudConfigData", lang);
#elif UNITY_IPHONE
		CMPPullCloudConfigDataWithLanguage(lang);
#endif
    }


    /// <summary>
    /// Call from Unity logic, tell us whether Unity engine have init.
    /// </summary>
    /// <note>must call it as early as possible when Unity engine have init finish.</note>
    /// <param name="isInit"></param>
    public void SetUnityInit(bool isInit)
    {
#if (UNITY_EDITOR || DISBLE_PLATFORM)
#elif UNITY_IPHONE
#elif UNITY_ANDROID
        cmp_promotionUtils_unity.CallStatic("setUnityInit", isInit);
#else
#endif
    }

    /// <summary>
    /// Whether can show open screen promotion
    /// </summary>
    /// <param name="scenes">value define: 1. app start up show the promotion; 2. come back main page again and then show the promotion</param>
    /// <param name="isNewPlayer">whether the first time app start up</param>
    /// <returns></returns>
    public bool CanShowOpenScreen(int scenes, bool isNewPlayer)
    {
#if (UNITY_EDITOR || DISBLE_PLATFORM)
        return false;
#elif UNITY_IPHONE
        return CMPCanShowOpenScreen(isNewPlayer,scenes);
#elif UNITY_ANDROID
        return cmp_promotionUtils_unity.CallStatic<bool>("canShowOpenScreen", scenes, isNewPlayer);
#else
        return false;
#endif
    }

    /// <summary>
    /// Show the open screen promotion.
    /// </summary>
    public void ShowOpenScreen()
    {
#if (UNITY_EDITOR || DISBLE_PLATFORM)
#elif UNITY_IPHONE
        CMPShowOpenScreen();
#elif UNITY_ANDROID
        cmp_promotionUtils_unity.CallStatic("showOpenScreen");
#endif
    }
    
    /// <summary>
    /// Whether can show insert screen promotion
    /// </summary>
    /// <returns></returns>
    public bool CanShowInsertScreen()
    {
#if (UNITY_EDITOR || DISBLE_PLATFORM)
        return false;
#elif UNITY_IPHONE
		return CMPCanShowInsertPage(false);
#elif UNITY_ANDROID
        return cmp_promotionUtils_unity.CallStatic<bool>("canShowInsertScreen");
#else
        return false;
#endif
    }

    /// <summary>
    /// Show the Insert screen promotion.
    /// </summary>
    public void ShowInsertScreen()
    {
#if (UNITY_EDITOR || DISBLE_PLATFORM)
#elif UNITY_ANDROID
        cmp_promotionUtils_unity.CallStatic("showInsertScreen");
#elif UNITY_IPHONE
        CMPShowInsertPage();
#endif
    }
    
    /// <summary>
    /// Whether can show result card.
    /// </summary>
    /// <returns></returns>
    public bool CanShowResultCard()
    {
#if (UNITY_EDITOR || DISBLE_PLATFORM)
        return false;
#elif UNITY_IPHONE
        return CMPCanShowResultCard();
#elif UNITY_ANDROID
        return cmp_promotionUtils_unity.CallStatic<bool>("canShowResultCard");
#else
        return false;
#endif
    }
    
    /// <summary>
    /// Get the result card data, json format
    /// </summary>
    /// <returns></returns>
    public string GetResultCardData()
    {
#if (UNITY_EDITOR || DISBLE_PLATFORM)
        return "";
#elif UNITY_IPHONE
        return CMPGetResultCardData();
#elif UNITY_ANDROID
        return cmp_promotionUtils_unity.CallStatic<string>("getResultCardData");
#else
        return "";
#endif
    }

    /// <summary>
    /// We should call this function on clicking the result card.
    /// </summary>
    /// <param name="jsonData">json format data</param>
    public void ClickResultCard(string jsonData)
    {
#if (UNITY_EDITOR || DISBLE_PLATFORM)
#elif UNITY_IPHONE
        CMPClickResultCard(jsonData);
#elif UNITY_ANDROID
        cmp_promotionUtils_unity.CallStatic("clickResultCard", jsonData);
#else
#endif
    }
    
    /// <summary>
    /// Determine whether can show red dot on setting page
    /// </summary>
    /// <returns></returns>
    public bool CanShowSettingCardRedDot()
    {
#if (UNITY_EDITOR || DISBLE_PLATFORM)
        return false;
#elif UNITY_IPHONE
		return CMPCanShowSettingCardRedDot();
#elif UNITY_ANDROID
        return cmp_promotionUtils_unity.CallStatic<bool>("canShowSettingCardRedDot");
#else
        return false;
#endif
    }
    
    /// <summary>
    /// Whether can show setting card.
    /// </summary>
    /// <returns></returns>
    public bool CanShowSettingCard()
    {
#if (UNITY_EDITOR || DISBLE_PLATFORM)
        return true;
#elif UNITY_IPHONE
        return CMPCanShowSettingCard();
#elif UNITY_ANDROID
        return cmp_promotionUtils_unity.CallStatic<bool>("canShowSettingCard");
#else
        return false;
#endif
    }

    /// <summary>
    /// Get the setting card data, json format
    /// </summary>
    /// <returns></returns>
    public string GetSettingCardData()
    {
#if (UNITY_EDITOR || DISBLE_PLATFORM)
        return "{\"jump_type\":1,\"pro_name\":\"\",\"title\":\"Cell Connect\",\"jump_url\":\"https://play.google.com/store/apps/details?id=com.cmplay.cellconnect&referrer=utm_source%3DPT2jiazuyouxi\",\"reward_counts\":0,\"show_red_dot_setting_card\":\"1\",\"pkg_name\":\"com.cmplay.cellconnect\",\"pro_type\":0,\"subtitle\":\"使人上瘾最佳数字消除游戏\",\"pro_id\":11104,\"icon_image_path\":\"/data/data/com.cmcm.arrowio/cache/d4f6feaa610a818be5b3f4f592c2aaab_ccnew112.png\"}";
       // return "";
#elif UNITY_IPHONE
        return CMPGetSettingCardData();
#elif UNITY_ANDROID
        return cmp_promotionUtils_unity.CallStatic<string>("getSettingCardData");
#else
        return "";
#endif
    }

    /// <summary>
    /// We should call this function on clicking the setting card 
    /// </summary>
    /// <param name="jsonData">json format data</param>
    public void ClickSettingCard(string jsonData)
    {
#if (UNITY_EDITOR || DISBLE_PLATFORM)
#elif UNITY_IPHONE
        CMPClickSettingCard(jsonData);
#elif UNITY_ANDROID
        cmp_promotionUtils_unity.CallStatic("clickSettingCard", jsonData);
#endif
    }
    
    /// <summary>
    /// Determine whether can show red dot on family games.
    /// </summary>
    /// <returns></returns>
    public bool CanShowFamilyGamesRedDot()
    {
#if (UNITY_EDITOR || DISBLE_PLATFORM)
        return false;
#elif UNITY_IPHONE
		return CMPCanShowFamilyGamesRedDot();
#elif UNITY_ANDROID
        return cmp_promotionUtils_unity.CallStatic<bool>("canShowFamilyGamesRedDot");
#else
        return false;
#endif
    }
    
    /// <summary>
    /// Whether can show family games card.
    /// </summary>
    /// <returns></returns>
    public bool CanShowFamilyGamesCard()
    {
#if (UNITY_EDITOR || DISBLE_PLATFORM)
        return false;
#elif UNITY_IPHONE
		return CMPCanShowFamilyGames();
#elif UNITY_ANDROID
        return cmp_promotionUtils_unity.CallStatic<bool>("canShowFamilyGamesCard");
#else
        return false;
#endif
    }
    
    /// <summary>
    /// Get the family games data, json format
    /// </summary>
    /// <returns>json format</returns>
    public string GetFamilyGamesData()
    {
#if (UNITY_EDITOR || DISBLE_PLATFORM)
        return "[{\"jump_type\":1,\"pro_name\":\"\",\"title\":\"Cell Connect\",\"jump_url\":\"https://play.google.com/store/apps/details?id=com.cmplay.cellconnect&referrer=utm_source%3DPT2jiazuyouxi\",\"reward_counts\":0,\"show_card_red_dot\":\"1\",\"pkg_name\":\"com.cmplay.cellconnect\",\"pro_type\":0,\"subtitle\":\"使人上瘾最佳数字消除游戏\",\"pro_id\":11104,\"icon_image_path\":\"/data/data/com.cmcm.arrowio/cache/d4f6feaa610a818be5b3f4f592c2aaab_ccnew112.png\"},{\"jump_type\":1,\"pro_name\":\"\",\"title\":\"点点冲刺\",\"jump_url\":\"https://play.google.com/store/apps/details?id=com.secondarm.taptapdash&referrer=utm_source%3Dpt2family\",\"reward_counts\":0,\"show_card_red_dot\":\"1\",\"pkg_name\":\"com.secondarm.taptapdash\",\"pro_type\":0,\"subtitle\":\"世界上最难的游戏\",\"pro_id\":11101,\"icon_image_path\":\"/data/data/com.cmcm.arrowio/cache/8dedce4117e62e415482c9fb9516ed44_112x112.png\"},{\"jump_type\":1,\"pro_name\":\"\",\"title\":\"Dancing Line\",\"jump_url\":\"https://play.google.com/store/apps/details?id=com.cmplay.dancingline&referrer=utm_source%3Dpt2family\",\"reward_counts\":0,\"show_card_red_dot\":\"1\",\"pkg_name\":\"com.cmplay.dancingline\",\"pro_type\":0,\"subtitle\":\"闭着眼睛也能玩\",\"pro_id\":12301,\"icon_image_path\":\"/data/data/com.cmcm.arrowio/cache/251f0e5b42e2be03314a26e3ac68db2f_170206112.png\"},{\"jump_type\":1,\"pro_name\":\"\",\"title\":\"Cell Connect\",\"jump_url\":\"https://play.google.com/store/apps/details?id=com.cmplay.cellconnect&referrer=utm_source%3DPT2jiazuyouxi\",\"reward_counts\":0,\"show_card_red_dot\":\"1\",\"pkg_name\":\"com.cmplay.cellconnect\",\"pro_type\":0,\"subtitle\":\"使人上瘾最佳数字消除游戏\",\"pro_id\":11104,\"icon_image_path\":\"/data/data/com.cmcm.arrowio/cache/d4f6feaa610a818be5b3f4f592c2aaab_ccnew112.png\"},{\"jump_type\":1,\"pro_name\":\"\",\"title\":\"点点冲刺\",\"jump_url\":\"https://play.google.com/store/apps/details?id=com.secondarm.taptapdash&referrer=utm_source%3Dpt2family\",\"reward_counts\":0,\"show_card_red_dot\":\"1\",\"pkg_name\":\"com.secondarm.taptapdash\",\"pro_type\":0,\"subtitle\":\"世界上最难的游戏\",\"pro_id\":11101,\"icon_image_path\":\"/data/data/com.cmcm.arrowio/cache/8dedce4117e62e415482c9fb9516ed44_112x112.png\"},{\"jump_type\":1,\"pro_name\":\"\",\"title\":\"Dancing Line\",\"jump_url\":\"https://play.google.com/store/apps/details?id=com.cmplay.dancingline&referrer=utm_source%3Dpt2family\",\"reward_counts\":0,\"show_card_red_dot\":\"1\",\"pkg_name\":\"com.cmplay.dancingline\",\"pro_type\":0,\"subtitle\":\"闭着眼睛也能玩\",\"pro_id\":12301,\"icon_image_path\":\"/data/data/com.cmcm.arrowio/cache/251f0e5b42e2be03314a26e3ac68db2f_170206112.png\"},{\"jump_type\":1,\"pro_name\":\"\",\"title\":\"Cell Connect\",\"jump_url\":\"https://play.google.com/store/apps/details?id=com.cmplay.cellconnect&referrer=utm_source%3DPT2jiazuyouxi\",\"reward_counts\":0,\"show_card_red_dot\":\"1\",\"pkg_name\":\"com.cmplay.cellconnect\",\"pro_type\":0,\"subtitle\":\"使人上瘾最佳数字消除游戏\",\"pro_id\":11104,\"icon_image_path\":\"/data/data/com.cmcm.arrowio/cache/d4f6feaa610a818be5b3f4f592c2aaab_ccnew112.png\"},{\"jump_type\":1,\"pro_name\":\"\",\"title\":\"点点冲刺\",\"jump_url\":\"https://play.google.com/store/apps/details?id=com.secondarm.taptapdash&referrer=utm_source%3Dpt2family\",\"reward_counts\":0,\"show_card_red_dot\":\"1\",\"pkg_name\":\"com.secondarm.taptapdash\",\"pro_type\":0,\"subtitle\":\"世界上最难的游戏\",\"pro_id\":11101,\"icon_image_path\":\"/data/data/com.cmcm.arrowio/cache/8dedce4117e62e415482c9fb9516ed44_112x112.png\"},{\"jump_type\":1,\"pro_name\":\"\",\"title\":\"Dancing Line\",\"jump_url\":\"https://play.google.com/store/apps/details?id=com.cmplay.dancingline&referrer=utm_source%3Dpt2family\",\"reward_counts\":0,\"show_card_red_dot\":\"1\",\"pkg_name\":\"com.cmplay.dancingline\",\"pro_type\":0,\"subtitle\":\"闭着眼睛也能玩\",\"pro_id\":12301,\"icon_image_path\":\"/data/data/com.cmcm.arrowio/cache/251f0e5b42e2be03314a26e3ac68db2f_170206112.png\"}]";
#elif UNITY_IPHONE
        return CMPGetFamilyGamesData();
#elif UNITY_ANDROID
        return cmp_promotionUtils_unity.CallStatic<string>("getFamilyGamesData");
#else
        return "";
#endif
    }

    /// <summary>
    /// We should call this function on clicking the family game card.
    /// </summary>
    /// <param name="jsonData">json format data</param>
    public void ClickFamilyGameCard(string jsonData)
    {
#if (UNITY_EDITOR || DISBLE_PLATFORM)
#elif UNITY_IPHONE
        CMPClickFamilyGames(jsonData);
#elif UNITY_ANDROID
        cmp_promotionUtils_unity.CallStatic("clickFamilyGameCard", jsonData);
#endif
    }

    /// <summary>
    /// Whether the inner push app is installed.  Note: only used at Android platform. 
    /// </summary>
    /// <param name="jsonDataForShow">The inner push app info, json format that get from getXXXData()</param>
    /// <returns></returns>
    public bool IsInnerPushAppInstalled(string jsonDataForShow)
    {
#if (UNITY_EDITOR || DISBLE_PLATFORM)
        return false;
#elif UNITY_ANDROID
        return cmp_promotionUtils_unity.CallStatic<bool>("isInnerPushAppInstalled", jsonDataForShow);
#else
        return false;
#endif
    }

    /// <summary>
    /// Determine whether there is any reward video.
    /// </summary>
    /// <param name="scene">This is used to distinguish between different scenes of the show.</param>
    /// <param name="isClicked">if user click to fire the event: true    others: false</param>
    /// <returns></returns>
    public bool CanShowRewardedVideo(int scene, bool isClicked)
    {
#if (UNITY_EDITOR || DISBLE_PLATFORM)
        return false;
#elif UNITY_IPHONE
        return CMPCanShowRewardedVideo(scene, isClicked);   
#elif UNITY_ANDROID
        return cmp_promotionUtils_unity.CallStatic<bool>("canShowRewardedVideo", scene, isClicked);
#else
        return false;
#endif
    }

	/// <summary>
	/// Show rewarded video.
	/// </summary>
	/// <returns></returns>
	public bool ShowRewardedVideo(int scene)
	{
#if (UNITY_EDITOR || DISBLE_PLATFORM)
		return false;
#elif UNITY_IPHONE
        CMPShowRewardedVideo(scene);    //TODO
		return true;
#elif UNITY_ANDROID
        return cmp_promotionUtils_unity.CallStatic<bool>("showRewardedVideo", scene);
#else
        return false;
#endif
	}

    /// <summary>
    /// determine whether there is any hit top reward video
    /// </summary>
    /// <param name="scene">This is used to distinguish between different scenes of the show.</param>
    /// <param name="isClicked">if user click to fire the event: true    others: false</param>
    /// <returns></returns>
    public bool CanShowHitTopRewardedVideo(int scene, bool isClicked)
    {
#if (UNITY_EDITOR || DISBLE_PLATFORM)
        return false;
#elif UNITY_IPHONE
        return CMPCanShowHitTopRewardedVideo(scene, isClicked);  
#elif UNITY_ANDROID
        return cmp_promotionUtils_unity.CallStatic<bool>("canShowHitTopRewardedVideo", scene, isClicked);
#else
        return false;
#endif
    }

    /// <summary>
    /// show hit top rewarded video
    /// </summary>
    /// <returns></returns>
    public bool ShowHitTopRewardedVideo(int scene)
    {
#if (UNITY_EDITOR || DISBLE_PLATFORM)
        return false;
#elif UNITY_IPHONE
        CMPShowHitTopRewardedVideo(scene);
		return true;
#elif UNITY_ANDROID
        return cmp_promotionUtils_unity.CallStatic<bool>("showHitTopRewardedVideo", scene);
#else
        return false;
#endif
    }

    /// <summary>
    ///  reported infoc data
    ///  NOTE: Must be set every field's value except network. In order to report a unified network type definition, the caller does not need to set network field, this Api appends the network field by default.
    ///  
    ///  For example:
    ///  suppose in our kfmt.dat file have difine an report format:
    ///  cmplaysdk_purchases:108 uptime:int64 network:byte action:byte source:byte orderid:string productid:string remark:string
    ///  "cmplaysdk_purchases" is infoc table name, 108 is version number of this table, "uptime:int64 network:byte action:byte source:byte orderid:string productid:string remark:string" is the field and type definition
    ///  then you can do an report by following example code:
    ///  
    ///  string tableName = "cmplaysdk_purchases";
    ///  string data = "uptime=1522308228&action=1&source=2&orderid=GPA.3306-9888-6774-74667&productid=diamonds_3280&remark=1";     //NOTE: must be set every field's value except network
    ///  ReportData(tableName, data);
    ///  
    /// </summary>
    /// <param name="tableName">table name</param>
    /// <param name="data">data, format：key=value&key=value，e.g.：uptime=1113213&action=3&remark=abc</param>
    public void ReportData(string tableName, string data)
	{
#if (UNITY_EDITOR || DISBLE_PLATFORM)
#elif UNITY_IOS || UNITY_IPHONE
		CMPReportData(tableName, data);
#elif UNITY_ANDROID
        reportUtils.CallStatic("reportData", tableName, data);
#else
#endif
    }

    public void ReportEvent(int action)
    {

#if (UNITY_EDITOR || DISBLE_PLATFORM)

#elif UNITY_IPHONE || UNITY_IOS
        CMPReportEvent(action);
#elif UNITY_ANDROID
        reportUtils.CallStatic("reportEvent", action);
#else
        
#endif
    }

    /// <summary>
    /// get key value from cloud
    /// </summary>
    /// <param name="func_type">cloud key's func_type</param>
    /// <param name="section">cloud key's section name</param>
    /// <param name="key">cloud key's key name</param>
    /// <param name="defValue">cloud key's default value</param>
    /// <returns></returns>
    public int getIntValue(int func_type, string section, string key, int defValue)
    {
#if (UNITY_EDITOR || DISBLE_PLATFORM)
        return 0;
#elif UNITY_IOS || UNITY_IPHONE
        return CMPGetCloudIntValue(func_type, section, key, defValue);
#elif UNITY_ANDROID
		return cloud_hepler_unity.CallStatic<int>("getIntValue", func_type, section, key, defValue);
#else
        return 0;
#endif
    }

    /// <summary>
    /// get key value from cloud
    /// </summary>
    /// <param name="func_type">cloud key's func_type</param>
    /// <param name="section">cloud key's section name</param>
    /// <param name="key">cloud key's key name</param>
    /// <param name="defValue">cloud key's default value</param>
    /// <returns></returns>
    public long getLongValue(int func_type, string section, string key, long defValue)
    {
#if (UNITY_EDITOR || DISBLE_PLATFORM)
        return 0;
#elif UNITY_IOS || UNITY_IPHONE
        return CMPGetCloudLongValue(func_type, section, key, defValue);
#elif UNITY_ANDROID
		return cloud_hepler_unity.CallStatic<long>("getLongValue", func_type, section, key, defValue);
#else
        return 0;
#endif
    }

    /// <summary>
    /// get key value from cloud
    /// </summary>
    /// <param name="func_type">cloud key's func_type</param>
    /// <param name="section">cloud key's section name</param>
    /// <param name="key">cloud key's key name</param>
    /// <param name="defValue">cloud key's default value</param>
    /// <returns></returns>
    public bool getBooleanValue(int func_type, string section, string key, bool defValue)
    {
#if (UNITY_EDITOR || DISBLE_PLATFORM)
        return false;
#elif UNITY_IOS || UNITY_IPHONE
        return CMPGetCloudBoolValue(func_type, section, key, defValue);
#elif UNITY_ANDROID
		return cloud_hepler_unity.CallStatic<bool>("getBooleanValue", func_type, section, key, defValue);
#else
        return false;
#endif
    }

    /// <summary>
    /// get key value from cloud
    /// </summary>
    /// <param name="func_type">cloud key's func_type</param>
    /// <param name="section">cloud key's section name</param>
    /// <param name="key">cloud key's key name</param>
    /// <param name="defValue">cloud key's default value</param>
    /// <returns></returns>
    public double getDoubleValue(int func_type, string section, string key, double defValue)
    {
#if (UNITY_EDITOR || DISBLE_PLATFORM)
        return 0;
#elif UNITY_IOS || UNITY_IPHONE
        return CMPGetCloudDoubleValue(func_type, section, key, defValue);
#elif UNITY_ANDROID
		return cloud_hepler_unity.CallStatic<double>("getDoubleValue", func_type, section, key, defValue);
#else
        return 0;
#endif
    }

    /// <summary>
    /// get key value from cloud
    /// </summary>
    /// <param name="func_type">cloud key's func_type</param>
    /// <param name="section">cloud key's section name</param>
    /// <param name="key">cloud key's key name</param>
    /// <param name="defValue">cloud key's default value</param>
    /// <returns></returns>
    public string getStringValue(int func_type, string section, string key, string defValue)
    {
#if (UNITY_EDITOR || DISBLE_PLATFORM)
        return "";
#elif UNITY_IOS || UNITY_IPHONE
        return CMPGetCloudStringValue(func_type, section, key, defValue);
#elif UNITY_ANDROID
		return cloud_hepler_unity.CallStatic<string>("getStringValue", func_type, section, key, defValue);
#else
        return "";
#endif
    }

	public void LoadFeedback(string url, string appID) {
#if (UNITY_EDITOR || DISBLE_PLATFORM)
        ;
#elif UNITY_IOS || UNITY_IPHONE
        CMPLoadFeedback(url, appID);
#elif UNITY_ANDROID
        cmp_promotionUtils_unity.CallStatic("startWebView", url);       //for android platform, need to set your FEEDBACK_APP_ID=your_app_id in file cmplaysdkcfg.dat
#else
        // TODO
#endif
    }
    /**
     * family games card push update notification
     */
    public void onFamilyGamesPushUpdate(string str)
    {
        Debug.Log("internal_push onFamilyGamesPushUpdate  msg received@@@@@@");
        //TODO add your code here
    }

    /**
     * setting card push update notification
     */
    public void onSettingsPushUpdate(string str)
    {
        Debug.Log("internal_push onSettingsPushUpdate  msg received@@@@@@");
        //TODO add your code here
    }

    /**
     * reward video closed notification
     */
    public void onVideoClosed(string str)
    {
        Debug.Log("internal_push onVideoClosed  msg received@@@@@@      isCompleteView:" + bool.Parse(str));
        //TODO add your code here
    }

    /**
     * callback of reward video load success
     */
    public void onLoadSuccess(string str)
    {
        Debug.Log("internal_push onLoadSuccess  msg received@@@@@@");
        //TODO add your code here
    }

    /**
     * callback of reward video load error
     */
    public void onLoadError(string str)
    {
        Debug.Log("internal_push onLoadError  msg received@@@@@@   errorMsg:" + str);
        //TODO add your code here
    }

    /**
     * callback of reward video video show
     * */
    public void onVideoShow(string str)
    {
        Debug.Log("internal_push onVideoShow  msg received@@@@@@");
        //TODO add your code here
    }

    /**
     * callback of reward video video show fail
     * */
    public void onVideoShowFail(string str)
    {
        Debug.Log("internal_push onVideoShowFail  msg received@@@@@@  errorInfo:" + str);
        //TODO add your code here
    }

}
