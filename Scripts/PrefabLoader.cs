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
    
    //[HideInInspector]
    public bool loaded;
    
    //[HideInInspector]
    public string[] assetNames;
    
    //[HideInInspector]
    public int assetIndex;

    //[HideInInspector] 
    public bool bundleLoaded;

    //[HideInInspector]
    public AssetBundle bundle;

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
        
        AssetBundle bundle = AssetBundle.LoadFromFile(assetBundlePath);
        Object obj = bundle.LoadAsset(prefabToLoad);
        Instantiate(obj, this.transform.position, this.transform.rotation);
        bundle.Unload(false);
    }
}