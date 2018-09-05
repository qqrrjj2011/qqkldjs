using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using SimpleJson;


public struct blockInfo
{
	public int type;
	public int row;
	public int col;
	public float angle;
	public int score;
}

public class levelInfo
{
	public int maxRow;

	public int oneStarScore;
	public int twoStarScore;
	public int threeStarScore;
	public int rowStart;

	public int maxCol = 7;

	public float blockScale = 1.0f;

	public int ballStartNum = 1;
 

	public List<List<blockInfo>> dataLst;
 
}



public class LevelData  {
	private int _empty = -1;
	Dictionary<int,levelInfo> allData = new Dictionary<int, levelInfo>(); 

	public levelInfo curLevelData;
	// Use this for initialization
	void Start () {
		
	}

	int curLevel = 0;

	public void setLevel(int level)
	{
		if(!allData.ContainsKey(level))
		{
			curLevel = level;
			string path = "LevelData/Level"+level;
			LoadLevelUsingPath(path);

		} 
		 curLevelData = allData[level];
	}

	public List<blockInfo> getRowData(int row)
	{
		if(curLevelData.maxRow > row)
		{
			return curLevelData.dataLst[row];
		}
		return null;
	}

	public int getStartRow()
	{
	 
		return curLevelData.rowStart;
	}

	public bool isFinish(int row)
	{
		if(row >= curLevelData.maxRow)
		{
			return true;
		}
		return false;
	}

	void LoadLevelUsingPath (string path) 
	 {
            if (path.Length != 0) {

				byte[] Ttbytes = Resources.Load<TextAsset>(path).bytes;

				string str = System.Text.Encoding.Default.GetString(Ttbytes);

				if (str.Equals(""))
				{
					Debug.LogError("tes");
				}
				string[] temp = str.Split('%');
				//Debug.LogError("aabb  " + strss[2] + "  sddd");
				//JsonObject jsonObject = SimpleJson.SimpleJson.DeserializeObject<JsonObject>(strss[1]);



                // BinaryFormatter bFormatter = new BinaryFormatter ();
                // // Reset the level
                // FileStream file = File.OpenRead (path);
                // // Convert the file from a byte array into a string
                // string levelData = bFormatter.Deserialize (file) as string;
                // // We're done working with the file so we can close it
                // file.Close ();
                // string[] temp = levelData.Split ('%');

                LoadLevelFromStringLayers (temp[2]);

                //is Was the JSON data load
                LoadLevelPropertiesJsonData (temp[1]);
                //Debug.LogError(temp[0]);
            } else {
                Debug.Log ("Invalid path given");
            }
    }

	//自定义的属性数据在这里读取
	void LoadLevelPropertiesJsonData (String Jsondata) {
		JsonObject Jobj = SimpleJson.SimpleJson.DeserializeObject<JsonObject> (Jsondata);
		// Debug.Log(jsonObject);
		
		levelInfo info = allData[curLevel];
		info.oneStarScore = int.Parse(Jobj.GetString("OneStart"));
        info.twoStarScore = int.Parse(Jobj.GetString("TwoStart"));
        info.threeStarScore = int.Parse(Jobj.GetString("ThreeStart"));
        info.rowStart = int.Parse(Jobj.GetString("RowStart"));
	
		if(!int.TryParse(Jobj.GetString("ballStartNum"),out info.ballStartNum))
		{
			info.ballStartNum = 1;
		}

		if(!float.TryParse(Jobj.GetString("blockScale"),out info.blockScale))
		{
			info.blockScale = 1;
		}
	 
		if(!int.TryParse(Jobj.GetString("maxCol"),out info.maxCol))
		{
			info.maxCol = 7;
		}
	}

	// Method that loads the layers
	void LoadLevelFromStringLayers (string content) {
		// Split our level on layers by the new tabs (\t)
		List<string> layers = new List<string> (content.Split ('\t'));
		foreach (string layer in layers) {
			if (layer.Trim () != "") {
				LoadLevelFromString (int.Parse (layer[0].ToString ()), layer.Substring (1));
			}
		}
	}


	   // Method that loads one layer
	void LoadLevelFromString (int layer, string content) {
		// Split our layer on rows by the new lines (\n)
		List<string> lines = new List<string>(content.Split ('\n'));
		// Place each block in order in the correct x and y position
		levelInfo data = new levelInfo();
		data.dataLst  =new List<List<blockInfo>>();
		for (int i = 0; i < lines.Count; i++)
		 {
			string[] blockIDs = lines[i].Split (',');
		//	Debug.Log(">>>>>>>>>>>>level lines i:"+i+"  "+lines[i]);
			List<blockInfo> rowdata = new List<blockInfo>();
			data.dataLst.Add(rowdata);
			bool haveData = false;
			for (int j = 0; j < blockIDs.Length - 1; j++) {
				blockInfo colData = new blockInfo();
				if(blockIDs[j] != "EMPTY")
				{
					//Debug.Log(">>>>>>>>>>>>>blockIDs:"+j+"  "+blockIDs[j]);
					// 名字|分数|角度|类型
					string[] temp = blockIDs[j].Split ('|');
					colData.row = j;
					colData.col = lines.Count - i - 1;
					if(temp[1] != "")
						colData.score = int.Parse(temp[1]);
					colData.angle = float.Parse(temp[2]);
					colData.type = int.Parse(temp[3]);
					haveData = true;
				}else
				{
					colData.type = -1;
				}
				rowdata.Add(colData);
			}
			if(haveData)
			{
				data.maxRow = i + 1;
			}
		}
 
		allData[curLevel] = data;
	}
}
