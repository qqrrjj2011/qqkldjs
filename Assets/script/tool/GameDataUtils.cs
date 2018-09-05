using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

// 和游戏数据相关的实用类 //
public class GameDataUtils
{
	// 将数据转换成bool类型 //
	public static bool ReadBool(object data)
	{
		string str = data.ToString();
		if(str == "1" || str == "true")
		{
			return true;
		}
		return false;
	}

	// 将数据转换成int类型 //
	public static int ReadInt(object data)
	{
		if(data == null)
		{
			return 0;
		}
		int nIntData = 0;
		try
		{
			nIntData = System.Convert.ToInt32(data);
		}
		catch(Exception e)
		{
			//Debug.Log(e.Message);
			return 0;
		}
		return nIntData;
	}
	
	// 将数据转换成float类型 //
	public static float ReadFloat(object data)
	{
		if(data == null)
		{
			return 0;
		}
		float fResult;
		if(float.TryParse(data.ToString(), out fResult))
		{
			return fResult;
		}
		return 0f;
	}

	// 将数据转换成string list //
	public static List<string> ReadStringList(object data)
	{
		List<string> result = new List<string>();
		string[] strData = data.ToString().Split('|');
		for(int i = 0; i < strData.Length; i ++)
		{
			if(strData[i].Length > 0)
			{
				result.Add(strData[i]);
			}
		}

		return result;
	}

	// 将数据转换成int list //
	public static List<int> ReadIntList(object data)
	{
		List<int> result = new List<int>();
		string[] strData = data.ToString().Split('|');
		for(int i = 0; i < strData.Length; i ++)
		{
			if(strData[i].Length > 0)
			{
				result.Add(ReadInt(strData[i]));
			}
		}
		
		return result;
	}

	//根据自定义字符 Y 转换 int list//
	public static List<int> ReadIntListY(object data, char[] Y)
	{
		List<int> result = new List<int>();
		string[] strData = data.ToString().Split(Y);
		for(int i = 0; i < strData.Length; i ++)
		{
			if(strData[i].Length > 0)
			{
				result.Add(ReadInt(strData[i]));
			}
		}
		
		return result;
	}

	// 将数据转换成float list //
	public static List<float> ReadFloatList(object data)
	{
		List<float> result = new List<float>();
		string[] strData = data.ToString().Split('|');
		for(int i = 0; i < strData.Length; i ++)
		{
			if(strData[i].Length > 0)
			{
				result.Add(ReadFloat(strData[i]));
			}
		}
		
		return result;
	}

    /// <summary>
    /// 转换成Vector2列
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public static List<Vector2> ReadVector2List(object data)
    {
        List<Vector2> result = new List<Vector2>();
        string[] strData = data.ToString().Split('|');
        for (int i = 0; i < strData.Length; i++)
        {
            Vector2 v2 = new Vector2();
            if (strData[i].Length > 0)
            {
                v2.x = float.Parse(strData[i].Split('-')[0].ToString());
                v2.y = float.Parse(strData[i].Split('-')[1].ToString());
                result.Add(v2);
            }
        }

        return result;
    }

    /// <summary>
    /// 转换成Vector2
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public static Vector2 ReadVector2(object data)
    {
        if (data == null)
        {
            return new Vector2(0,0);
        }
        Vector2 v2Result;

        if (data.ToString().Split('-').Length > 1)
        {
            v2Result.x =float.Parse( data.ToString().Split('-')[0]);
            v2Result.y = float.Parse(data.ToString().Split('-')[1]);
            return v2Result;
        }

        return new Vector2(0,0);
    }
}
