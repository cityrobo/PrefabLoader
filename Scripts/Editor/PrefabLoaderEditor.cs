using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;

[CustomEditor(typeof(PrefabLoader))]
public class PrefabLoaderEditor : Editor
{
    private PrefabLoader _prefabLoader;

    private void OnEnable()
    {
        _prefabLoader = target as PrefabLoader;
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        if (GUILayout.Button("Select Asset Bundle"))
        {
            _prefabLoader.assetBundlePath = EditorUtility.OpenFilePanel("Select Asset Bundle", String.Empty, String.Empty);
            _prefabLoader.bundle = AssetBundle.LoadFromFile(_prefabLoader.assetBundlePath);
            _prefabLoader.bundleLoaded = true;
        }

        if (_prefabLoader.bundleLoaded)
        {
            _prefabLoader.assetNames = _prefabLoader.bundle.GetAllAssetNames();
            _prefabLoader.bundle.Unload(false);
            _prefabLoader.loaded = true;
            _prefabLoader.bundleLoaded = false;
        }

        if (_prefabLoader.loaded)
        {
            _prefabLoader.assetIndex = EditorGUILayout.Popup(_prefabLoader.assetIndex, _prefabLoader.assetNames);
            _prefabLoader.prefabToLoad = _prefabLoader.assetNames[_prefabLoader.assetIndex];
        }
        
        //base.OnInspectorGUI();
    }
}