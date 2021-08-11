using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;
using Object = UnityEngine.Object;

public class PrefabLoader : MonoBehaviour
{
    public string assetBundlePath;
    public string prefabToLoad;

    private AssetBundle _bundle;

    private void Awake()
    {
        if (String.IsNullOrEmpty(assetBundlePath) || !File.Exists(assetBundlePath))
        {
            EditorUtility.DisplayDialog("Error!", "Path to AssetBundle is empty, or the path in invalid", "☹");
            return;
        }
        if (String.IsNullOrEmpty(prefabToLoad))
        {
            EditorUtility.DisplayDialog("Error!", "No prefab selected to load", "☹");
            return;
        }
        
        _bundle = AssetBundle.LoadFromFile(assetBundlePath);
        Object obj = _bundle.LoadAsset(prefabToLoad);
        Instantiate(obj, this.transform.position, this.transform.rotation);
        _bundle.Unload(false);
    }
}