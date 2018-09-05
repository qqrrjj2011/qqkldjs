using UnityEngine;
using System.Collections;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

// 小球加成类型
public enum ballAdditionType
{
    big,
    newBall,
    newTemp,
    ballNum,
    ballScale,
    ballScrits
}
/// <summary>
/// 游戏配置数据读取类
/// </summary>
public class GameDataMgr : MonoBehaviour {
    public LanguageData languageData;
    public BlockScoreData blockScoreData;
    public BlockTypeData blockTypeData;
    static GameDataMgr instance;

     int _bigLv;           // 变大
     int _newBallLv;       // 新球
     int _newTempLv;       // 临时小球
     int _ballNumLv;       // 初始数量
     int _ballScaleLv;     // 尺寸
     int _ballCritsLv;     // 暴击

    public int bigLv{
        get{ return _bigLv; }
        set{ _bigLv = value; 
             PlayerPrefs.SetInt("bigLv",_bigLv);
        }
    }        
    public int newBallLv
    {
        get{ return _newBallLv; }
        set{ _newBallLv = value; 
             PlayerPrefs.SetInt("newBallLv",_newBallLv);
        }
    }
    public int newTempLv
    {
        get{ return _newTempLv; }
        set{ _newTempLv = value; 
            PlayerPrefs.SetInt("newTempLv",_newTempLv);
        }
    }       
    
    public int ballNumLv
    {
        get{ return _ballNumLv; }
        set{ _ballNumLv = value; 
             PlayerPrefs.SetInt("ballNumLv",_ballNumLv);
        }
    }
    public int ballScaleLv
    {
        get{ return _ballScaleLv; }
        set{ 
            _ballScaleLv = value; 
            PlayerPrefs.SetInt("ballScaleLv",_ballScaleLv);
            }
    }
    public int ballCritsLv
    {
        get{ return _ballCritsLv; }
        set{ _ballCritsLv = value; 
             PlayerPrefs.SetInt("ballCritsLv",_ballCritsLv);
        }
    }

   // int userinfo

    int curLanguageId;
    public int languageID
    {
        set{curLanguageId = value;}
        get{return curLanguageId;}
    }
    public static GameDataMgr inst()
	{
		return instance;
	}

    
    void Awake()
    {
        // PlayerPrefs.DeleteAll();
        DontDestroyOnLoad(gameObject);

         _bigLv = PlayerPrefs.GetInt("bigLv",0);
         _newBallLv = PlayerPrefs.GetInt("newBallLv",0);       // 新球
         _newTempLv = PlayerPrefs.GetInt("newTempLv",0);       // 临时小球
         _ballNumLv = PlayerPrefs.GetInt("ballNumLv",0);       // 初始数量
         _ballScaleLv = PlayerPrefs.GetInt("ballScaleLv",0);     // 尺寸
         _ballCritsLv = PlayerPrefs.GetInt("ballCritsLv",0);     // 暴击

        languageID = PlayerPrefs.GetInt("languageID",1);
        instance = this;
        StartCoroutine(LoadGameData());

        CMPlaySDKUtils.Initialize();
    }

    public IEnumerator LoadGameData()
    {
        yield return new WaitForEndOfFrame();
        string szPath = ResPath.ResBaseUrl + ResPath.PlatformUrl;		// 数据文件存放的目录 //
        WWW language = new WWW(szPath + "PinballVSBlock_Language.assetbundle");
       // Debug.Log(szPath + "PinballVSBlock_Language.assetbundle");
        yield return language;
        languageData = language.assetBundle.mainAsset as LanguageData;

         
        WWW blkTypeData = new WWW(szPath + "BlockType.assetbundle");
        yield return blkTypeData;
        blockTypeData = blkTypeData.assetBundle.mainAsset as BlockTypeData;

        WWW blkScoreData = new WWW(szPath + "BlockScore.assetbundle");
        yield return blkScoreData;
        blockScoreData = blkScoreData.assetBundle.mainAsset as BlockScoreData;

       // Debug.Log("PinballVSBlock_Language " + languageData.languageAllData.Count);
        SceneManager.LoadScene("game");
    }


    public void setLanguageTxt(Text txt,int id)
    {
        LanguageInfo info = languageData.getConfigByID(id);
        if(info != null)
        {
            txt.text = info.languageLst[languageID];
        }
        
    }

    public float getBallAddition(ballAdditionType type)
    {
        float addNum = 0;
        switch (type)
        {
            case ballAdditionType.ballNum:
            addNum = ballNumLv + 1;
            break;
            case ballAdditionType.ballScale:
            addNum = ballScaleLv * 0.001f;
            break;
            case ballAdditionType.ballScrits:
            addNum = (0.02f + (ballCritsLv - 1)*0.005f) * 1000;
            break;
            case ballAdditionType.big:
            addNum = bigLv*0.05f + 1;
            break;
            case ballAdditionType.newBall:
            addNum = newBallLv * 0.05f + 1;
            break;
            case ballAdditionType.newTemp:
            addNum = newTempLv* 0.05f + 1;
            break;
            default:
            break;
        }
        return addNum;
    }

    public int getBallLevel(ballAdditionType type)
    {
        int level = 0;
        switch (type)
        {
            case ballAdditionType.ballNum:
            level = ballNumLv;
            break;
            case ballAdditionType.ballScale:
            level = ballScaleLv;
            break;
            case ballAdditionType.ballScrits:
            level = ballCritsLv;
            break;
            case ballAdditionType.big:
            level = bigLv;
            break;
            case ballAdditionType.newBall:
            level = newBallLv;
            break;
            case ballAdditionType.newTemp:
            level = newTempLv;
            break;
            default:
            break;
        }
        return level;
    }


    public void AddBallLevel(ballAdditionType type)
    {
        switch (type)
        {
            case ballAdditionType.ballNum:
            ballNumLv++;
            break;
            case ballAdditionType.ballScale:
            ballScaleLv++;
            break;
            case ballAdditionType.ballScrits:
            ballCritsLv++;
            break;
            case ballAdditionType.big:
            bigLv++;
            break;
            case ballAdditionType.newBall:
            newBallLv++;
            break;
            case ballAdditionType.newTemp:
            newTempLv++;
            break;
            default:
            break;
        }
       
    }
}
