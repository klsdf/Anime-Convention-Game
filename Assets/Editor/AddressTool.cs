using System.Collections;
using System.Collections.Generic;
using UnityEditor.AddressableAssets.Settings;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEngine;


namespace ACG
{
    public static class AddressTool
    {
        static AddressableAssetSettings addressSetting;

        
        [MenuItem("Tools/Addressables/Build Addressables")]
        public static void ClearAndCreateAddressableGroups()
        {
            ClearGroup();
            addressSetting = AddressableAssetSettingsDefaultObject.Settings;
            // Create new group
            var newGroup = addressSetting.CreateGroup("AdbGroup", false, false, false, null);

            // Search for all "Adb" folders and add their contents to the new group
            var adbFolders = System.IO.Directory.GetDirectories(Application.dataPath, "Adb", System.IO.SearchOption.AllDirectories);
            foreach (var folder in adbFolders)
            {
            var relativePath = "Assets" + folder.Replace(Application.dataPath, "").Replace('\\', '/');
            var guids = AssetDatabase.FindAssets("", new[] { relativePath });

            foreach (var guid in guids)
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                var entry = addressSetting.CreateOrMoveEntry(guid, newGroup);
                entry.address = path;
            }
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }


        private static void ClearGroup(){
            addressSetting = AddressableAssetSettingsDefaultObject.Settings;
            List<int> missingGroupsIndices = new List<int>();
		    for (int i = 0; i < addressSetting.groups.Count; i++)
		    {
		    	var g = addressSetting.groups[i];
		    	if (g == null)
		    		missingGroupsIndices.Add(i);
		    }
		    if (missingGroupsIndices.Count > 0)
		    {
		    	for (int i = missingGroupsIndices.Count - 1; i >= 0; i--)
		    	{
		    		addressSetting.groups.RemoveAt(missingGroupsIndices[i]);
		    	}
		    }

            addressSetting.SetDirty(AddressableAssetSettings.ModificationEvent.GroupRemoved, null, true, true);
		    for (int i = addressSetting.groups.Count - 1; i >= 0; i--)
		    {
		    	addressSetting.RemoveGroup(addressSetting.groups[i]);
		    }

		    for (int i = addressSetting.GetLabels().Count; i > 0; i--)
		    {
		    	addressSetting.RemoveLabel(addressSetting.GetLabels()[i - 1]);
		    }
		    addressSetting = null;
        }


    }



}