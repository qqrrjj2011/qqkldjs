﻿using UnityEngine;

using System.Collections.Generic;

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

using GracesGames.Common.Scripts;
using GracesGames.SimpleFileBrowser.Scripts;
using SimpleJson;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace GracesGames._2DTileMapLevelEditor.Scripts.Functionalities
{
    public class SaveFunctionality : MonoBehaviour
    {

        // ----- PRIVATE VARIABLES -----

        // The level editor
        private LevelEditor _levelEditor;

        // The file browser
        private GameObject _fileBrowserPrefab;

        // The file extension for the saved file
        private string[] _fileExtensions;

        // Method to identifiction the tiles when saving
        private TileIdentificationMethod _saveMethod;

        // Temporary variable to save level before getting the path using the FileBrowser
        private string _levelToSave;

        // Temporary variable to save state of level editor before opening file browser and restore it after save/load
        private bool _preFileBrowserState = true;

        // The tiles used to build the level
        private List<Transform> _tiles;

        // Starting path of the file browser
        private string _startPath;

        Transform[,,] _gameObjects;

        // ----- SETUP -----

        public void Setup(GameObject fileBrowserPrefab, string[] fileExtensions, TileIdentificationMethod saveMethod,
            List<Transform> tiles, string startPath, Transform[,,] gameObjects)
        {
            _levelEditor = LevelEditor.Instance;
            _fileBrowserPrefab = fileBrowserPrefab;
            _fileExtensions = fileExtensions;
            _saveMethod = saveMethod;
            _tiles = tiles;
            _startPath = startPath;
            _gameObjects = gameObjects;
            SetupClickListeners();
        }

        // Hook up Save/Load Level method to Save/Load button
        private void SetupClickListeners()
        {
            Utilities.FindButtonAndAddOnClickListener("SaveButton", SaveLevel);
        }

        // ----- PUBLIC METHODS -----

        // Save to a file using a path
        public void SaveLevelUsingPath(string path)
        {
            // Enable the LevelEditor when the fileBrowser is done
            _levelEditor.ToggleLevelEditor(_preFileBrowserState);
            if (path.Length != 0)
            {
                // Save the level to file
                BinaryFormatter bFormatter = new BinaryFormatter();
                FileStream file = File.Create(path);
                bFormatter.Serialize(file, _levelToSave);
                file.Close();
                // Reset the temporary variable
                _levelToSave = null;
#if UNITY_EDITOR
                 AssetDatabase.Refresh();
                 #endif
            }
            else
            {
                Debug.Log("Invalid path given");
            }
        }

        // ----- PRIVATE METHODS -----

        // Method to determine whether a layer is empty (empty layers are not saved)
        private bool EmptyLayer(int[,,] level, int width, int height, int layer, int empty)
        {
            bool result = true;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (level[x, y, layer] != empty)
                    {
                        result = false;
                    }
                }
            }

            return result;
        }

        // Converts the internal level represtation (integer) to the tile idenfication type
        // Tiles can be identified using their index in the Tileset array or the name of the prefab game object
        // Empty tiles will be saved using the name "EMPTY"
        // Default will be LevelEditor.GetEmpty() (-1 default value)
        //将内部级别的represtation (integer)转换为tile idenfication类型。
        //可以通过在Tileset数组中使用索引或预置游戏对象的名称来标识Tiles。
        //空的瓦片将使用“空”的名称保存
        //默认为LevelEditor.GetEmpty()(-1默认值)
        private string TileSaveRepresentationToString(int[,,] levelToSave, int x, int y, int layer)
        {
            switch (_saveMethod)
            {
                case TileIdentificationMethod.Index:
                    return "" + levelToSave[x, y, layer];
                case TileIdentificationMethod.Name:
                    string temp = "EMPTY";
                    if (levelToSave[x, y, layer] != LevelEditor.GetEmpty())
                    {
                        temp = _tiles[levelToSave[x, y, layer]].gameObject.name;
                        Transform txt = _gameObjects[x, y, 0].Find("Sprite").Find("text");
                        //暂时忽略layer
                        if (txt != null)
                        {
                            temp += "|" + txt.GetComponent<TextMesh>().text;
                        }else
                        {
                             temp += "|"+"";
                        }
                        // 名字|分数|角度|类型
                       //  temp +="|" + _gameObjects[x, y, 0].eulerAngles.z +"|"+ (int)_gameObjects[x, y, 0].GetComponent<Block>().btype;

                    }
                    return temp;
                default:
                    return "" + LevelEditor.GetEmpty();
            }
        }

        // Save the level to a variable and file using FileBrowser and SaveLevelUsingPath
        private void SaveLevel()
        {
            int[,,] levelToSave = _levelEditor.GetLevel();
            int width = _levelEditor.Width;
            int height = _levelEditor.Height;
            int layers = _levelEditor.Layers;
            float blockScale = _levelEditor.Layers;

            //可能需要更换成json存储格式
            List<string> newLevel = new List<string>();
            // Loop through the layers
            for (int layer = 0; layer < layers; layer++)
            {
                // If the layer is not empty, add it and add \t at the end"
                if (!EmptyLayer(levelToSave, width, height, layer, LevelEditor.GetEmpty()))
                {
                    // Loop through the rows and add \n at the end"
                    for (int y = 0; y < height; y++)
                    {
                        string newRow = "";
                        for (int x = 0; x < width; x++)
                        {
                            newRow += TileSaveRepresentationToString(levelToSave, x, y, layer) + ",";
                        }

                        if (y != 0)
                        {
                            newRow += "\n";
                        }

                        newLevel.Add(newRow);
                    }

                    newLevel.Add("\t" + layer);
                }
            }

            //额外数据 三星字段 json
            JsonObject obj = new JsonObject();
            obj.Add("OneStart", _levelEditor.UiScript._StartInput[0].text);
            obj.Add("TwoStart", _levelEditor.UiScript._StartInput[1].text);
            obj.Add("ThreeStart", _levelEditor.UiScript._StartInput[2].text);
            obj.Add("RowStart", _levelEditor.rowStart + "");
            obj.Add("blockScale", _levelEditor.blockScale+"");
            obj.Add("maxCol", _levelEditor.Width+"");
            obj.Add("ballStartNum", _levelEditor.ballStartNum+"");
  

            newLevel.Add("%" + obj + "%");

            // Reverse the rows to make the final version rightside up
            newLevel.Reverse();
            string levelComplete = "";
            foreach (string level in newLevel)
            {
                levelComplete += level;
            }

            // Temporarily save the level to save it using SaveLevelUsingPath
            _levelToSave = levelComplete;
            // Open file browser to get the path and file name
            OpenFileBrowser();

            
        }

        // Open a file browser to save files
        private void OpenFileBrowser()
        {
            _preFileBrowserState = _levelEditor.GetScriptEnabled();
            // Disable the LevelEditor while the fileBrowser is open
            _levelEditor.ToggleLevelEditor(false);
            // Create the file browser and name it
            GameObject fileBrowserObject = Instantiate(_fileBrowserPrefab, transform);
            fileBrowserObject.name = "FileBrowser";
            // Set the mode to save or load
            FileBrowser fileBrowserScript = fileBrowserObject.GetComponent<FileBrowser>();
            fileBrowserScript.SetupFileBrowser(ViewMode.Landscape, _startPath);
            fileBrowserScript.SaveFilePanel("Level", _fileExtensions);
            // Subscribe to OnFileSelect event (call SaveLevelUsingPath using path) 
            fileBrowserScript.OnFileSelect += SaveLevelUsingPath;
            // Subscribe to OnFileBrowserClose event (call ReopenLevelEditor) 
            fileBrowserScript.OnFileBrowserClose += ReopenLevelEditor;
        }

        // Reopens the level editor after closing the file browser
        private void ReopenLevelEditor()
        {
            _levelEditor.ToggleLevelEditor(true);
        }
    }
}