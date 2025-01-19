using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ACG
{
    public class PrefabsUtil :Singleton<PrefabsUtil>{
        private Dictionary<string, string> prefabDict = new Dictionary<string, string>()
        {
            {"cow","Assets/AppRoot/Prefebs/Adb/cow.prefab"},
            {"portal", "Assets/AppRoot/Prefebs/Adb/portal.prefab"},
        };

        public string GetPath(string prefabName)
        {
            if (prefabDict.TryGetValue(prefabName, out string path))
            {
                return path;
            }
            
            return null;
        }
        public GameObject GetPrefab(string prefabName)
        {
            if (prefabDict.TryGetValue(prefabName, out string path))
            {   
                var go =  AddressModel.LoadAsset<GameObject>(path);
                return go;
            }
            return null;
        }
    }

}
