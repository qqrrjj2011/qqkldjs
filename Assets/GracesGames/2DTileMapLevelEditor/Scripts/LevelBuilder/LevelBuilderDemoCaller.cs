using SimpleJson;
using UnityEngine;

namespace GracesGames._2DTileMapLevelEditor.Scripts.LevelBuilder
{

    // Demo script demonstrating the usage of the LevelBuilder script
    // Contains a public String method which can be used to define the path to the level to load relative to the asset folder
    // Loads and builds the level found using the path on start using the LevelBuilder script
    public class LevelBuilderDemoCaller : MonoBehaviour
    {

        public string RelativePath = "/Resources/LevelData/Level1.bytes";

        private LevelBuilder _levelBuilder;

        public void OnBuilding()
        {
            _levelBuilder.LoadLevelUsingPath(Application.dataPath + RelativePath);
        }
        void Start()
        {
           
            _levelBuilder = GetComponent<LevelBuilder>();
             OnBuilding();
            //_levelBuilder.LoadLevelUsingPath(Application.dataPath + RelativePath);

            // byte[] Ttbytes = Resources.Load<TextAsset>("config/level/Level_1").bytes;

            // string str = System.Text.Encoding.Default.GetString(Ttbytes);

            // if (str.Equals(""))
            // {
            //     Debug.LogError("tes");
            // }
            // string[] strss = str.Split('%');
            // Debug.LogError("aabb  " + strss[2] + "  sddd");
            // JsonObject jsonObject = SimpleJson.SimpleJson.DeserializeObject<JsonObject>(strss[1]);
            // Debug.Log(jsonObject);
        }


    }
}