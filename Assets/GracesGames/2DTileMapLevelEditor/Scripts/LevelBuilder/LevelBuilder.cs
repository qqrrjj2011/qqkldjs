/// <summary>
/// 2017 12-08
/// </summary>
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using SimpleJson;
using UnityEngine;

namespace GracesGames._2DTileMapLevelEditor.Scripts.LevelBuilder {

    // Enum used to define save type
    public enum TileIdentificationMethod {
        Index,
        Name
    }

    // LevelBuilder允许在不需要水平编辑器或脚本的情况下构建级别。
    public class LevelBuilder : MonoBehaviour {
        public List<Transform> _LevelTran;
        // The tileset used to build the level
        [SerializeField] private Tileset _tileset;

        // The load method to identify the tiles
        [SerializeField] private TileIdentificationMethod _loadMethod;

        // The interal representation of an empty tile
        [SerializeField] private int _empty = -1;

        // The tiles array from the tileset
        private List<Transform> _tiles;

        // GameObject as the parent for all the layers (to keep the Hierarchy window clean)
        private GameObject _tileLevelParent;
        public GameObject TileLevelParent { get { return _tileLevelParent; } }

        // Dictionary as the parent for all the GameObjects per layer
        private readonly Dictionary<int, GameObject> _layerParents = new Dictionary<int, GameObject> ();

        //关卡数
        public TextAsset[] levelCount;
        
        // Setup the TileLevel prefab and set the _tile variable
        private void Awake () {
            levelCount = Resources.LoadAll<TextAsset> ("config/level");
            _tileLevelParent = GameObject.Find ("TileLevel") ?? new GameObject ("TileLevel");
            _tiles = _tileset.Tiles;

            _LevelTran = new List<Transform> ();
        }
        void Start () {
            
            for (int i = 0; i < levelCount.Length; i++) {
                OnInitLevelData (i + 1);
            }
        }
        // Load from a file using a path
        public void LoadLevelUsingPath (string path) {
            if (path.Length != 0) {
                BinaryFormatter bFormatter = new BinaryFormatter ();
                // Reset the level
                FileStream file = File.OpenRead (path);
                // Convert the file from a byte array into a string
                string levelData = bFormatter.Deserialize (file) as string;
                // We're done working with the file so we can close it
                file.Close ();
                string[] temp = levelData.Split ('%');

                LoadLevelFromStringLayers (temp[2]);

                //is Was the JSON data load
                LoadLevelPropertiesJsonData (temp[1]);
                //Debug.LogError(temp[0]);
            } else {
                Debug.Log ("Invalid path given");
            }
        }
        // Load from a file using a path
        public void LoadLevelUsingStr (string path) {
            if (path!=null&&!path.Equals ("")) {

                _LevelTran.Clear ();

                LoadLevelFromStringLayers (path);
            } else {
                Debug.Log ("Invalid path given");
            }
        }

        public void OnInitLevelData (int i) {

            byte[] Ttbytes = Resources.Load<TextAsset> ("config/level/Level_" + i).bytes;
            string str = System.Text.Encoding.Default.GetString (Ttbytes);
            string[] info = str.Split ('%');

            // Debug.LogError("  sd " + info[2]);
            // mGameData.gameDataDic.Add ("level" + i, info[2]);
            // mGameData.gameBattleLevel.Add ("level" + i, info[1]);
        }
        public string GetLevelData (string id) {
            // if (mGameData.gameDataDic.ContainsKey (id)) {
            //     return mGameData.gameDataDic[id];
            // }
            return null;
        }
        public string GetBattleData (string id) {
            // if (mGameData.gameBattleLevel.ContainsKey (id)) {
            //     return mGameData.gameBattleLevel[id];
            // }
            return null;
        }

        //自定义的属性数据在这里读取
        public void LoadLevelPropertiesJsonData (String Jsondata) {
            JsonObject jsonObject = SimpleJson.SimpleJson.DeserializeObject<JsonObject> (Jsondata);
            // Debug.Log(jsonObject);
        }

        // Method that loads the layers
        private void LoadLevelFromStringLayers (string content) {
            // Split our level on layers by the new tabs (\t)
            List<string> layers = new List<string> (content.Split ('\t'));
            foreach (string layer in layers) {
                if (layer.Trim () != "") {
                    LoadLevelFromString (int.Parse (layer[0].ToString ()), layer.Substring (1));
                }
            }
        }

        // Method that loads one layer
        private void LoadLevelFromString (int layer, string content) {
            // Split our layer on rows by the new lines (\n)
            List<string> lines = new List<string> (content.Split ('\n'));
            // Place each block in order in the correct x and y position
            for (int i = 0; i < lines.Count; i++) {
                string[] blockIDs = lines[i].Split (',');
                for (int j = 0; j < blockIDs.Length - 1; j++) {
                    CreateBlock (TileStringRepresentationToInt (blockIDs[j]), j, lines.Count - i - 1, layer);
                }
            }
        }

        // Transforms the tile identification type read from the file to a integer used as internal representation in the level editor
        // For index, parse the string to int
        // For name, transverse the Tiles and try to match on game object name or EMPTY
        // Defaults to _empty (-1)
        //将从文件读取的tile标识类型转换为在级别编辑器中用作内部表示的整数。
        //对于索引，将字符串解析为int。
        //为名称，横向瓦片，试着匹配游戏对象名称或空。
        //默认为_empty (-1)
        private int TileStringRepresentationToInt (string tileString) {
            switch (_loadMethod) {
                case TileIdentificationMethod.Index:
                    try {
                        return int.Parse (tileString);
                    } catch (FormatException) {
                        Debug.LogError ("Error: Trying to load a Name based level using Index loading");
                        return _empty;
                    } catch (ArgumentNullException) {
                        Debug.LogError ("Error: Encountered a null in the file");
                        return _empty;
                    }
                case TileIdentificationMethod.Name:
                    if (tileString == "EMPTY") { return LevelEditor.GetEmpty (); }

                    string[] temp = tileString.Split ('|');

                    for (int i = 0; i < _tiles.Count; i++) {
                        if (_tiles[i].name == temp[0]) {
                            if (_tiles[i].Find("Sprite").Find("text") != null) {
                                 
                                _tiles[i].Find("Sprite").Find("text").GetComponent<TextMesh>().text = int.Parse (temp[1]) + "";
                            }
                            _tiles[i].eulerAngles = new Vector3(0,0,float.Parse(temp[2]));
                            return i;
                        }
                    }
                    return _empty;
                default:
                    return _empty;
            }
        }

        // Method that creates a GameObject for a given value and position
        // The value should be the index in the _tiles variable, resulting in the tile to build
        //方法，为给定的值和位置创建一个GameObject。
        //值应该是_tiles变量中的索引，从而生成tile。ba
        private void CreateBlock (int value, int xPos, int yPos, int zPos) {
            // If the value is not empty, set it to the correct tile
            if (value != _empty) {
                BuildBlock (_tiles[value], xPos, yPos, GetLayerParent (zPos).transform);
            }
        }

        // Builds the block by instantiating it and setting its parent
        private void BuildBlock (Transform toCreate, int xPos, int yPos, Transform parent) {
            //Create the object we want to create
            Transform newObject = Instantiate (toCreate, new Vector3 (xPos, yPos, toCreate.position.z), toCreate.transform.localRotation);
            //Give the new object the same name as our tile prefab
            newObject.name = toCreate.name;
            // Set the object's parent to the layer parent variable so it doesn't clutter our Hierarchy
            newObject.SetParent (parent, false);

            _LevelTran.Add (newObject);
        }

        // Method that returns the parent GameObject for a layer
        private GameObject GetLayerParent (int layer) {
            if (_layerParents.ContainsKey (layer))
                return _layerParents[layer];
            GameObject layerParent = new GameObject ("Layer " + layer);

            _tileLevelParent.transform.localScale = Vector3.one * 0.5f;
            layerParent.transform.SetParent (_tileLevelParent.transform, false);

            _layerParents.Add (layer, layerParent);
            return _layerParents[layer];
        }

    }
}