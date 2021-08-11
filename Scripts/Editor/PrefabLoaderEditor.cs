using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PrefabLoader))]
public class PrefabLoaderEditor : Editor
{
    private PrefabLoader _prefabLoader;

    private string _assetBundlePath;
    private string _prefabToLoad;

    private bool _bundleLoaded;
    private bool _prefabsLoaded;

    private string[] _prefabNames;
    private int _selectedObjIndex;
    private string _selectedObjName;

    private string[] GetAssetNames(AssetBundle assetBundle)
    {
        EditorUtility.DisplayProgressBar("Loading Bundle", "Path: " + _assetBundlePath, 0f);
        //Why is unity like this
        string[] names = assetBundle.LoadAllAssets().ToDictionary(objName => objName.name).Keys.ToArray();
        EditorUtility.DisplayProgressBar("Loading Bundle", "Path: " + _assetBundlePath, 1f);
        EditorUtility.ClearProgressBar();
        return names;
    }
    
    private void OnEnable()
    {
        _prefabLoader = target as PrefabLoader;
        _bundleLoaded = false;
        _prefabsLoaded = false;
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        if (GUILayout.Button("Select Bundle"))
        {
            _assetBundlePath = EditorUtility.OpenFilePanel("Select AssetBundle", String.Empty, String.Empty);
            _bundleLoaded = true;
        }

        if (!_bundleLoaded) return;

        EditorGUILayout.LabelField("Selected Bundle: " + _assetBundlePath);

        
        if (!_prefabsLoaded)
        {
            AssetBundle bundle = AssetBundle.LoadFromFile(_assetBundlePath);
            _prefabNames = GetAssetNames(bundle);
            bundle.Unload(false);
            _prefabsLoaded = true;
        }

        if (_prefabNames != null)
        {
            _selectedObjIndex = EditorGUILayout.Popup(_selectedObjIndex, _prefabNames);
            _prefabToLoad = _prefabNames[_selectedObjIndex];
        }

        if (_bundleLoaded && _prefabsLoaded)
        {
            _prefabLoader.assetBundlePath = _assetBundlePath;
            _prefabLoader.prefabToLoad = _prefabToLoad;
        }
    }
}