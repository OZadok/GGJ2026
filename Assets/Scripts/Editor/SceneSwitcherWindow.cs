#if UNITY_EDITOR
using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SimpleSceneSwitcherWindow : EditorWindow
{
    private string[] _scenePaths = Array.Empty<string>();
    private string[] _sceneNames = Array.Empty<string>();
    private int _selectedIndex = 0;

    [MenuItem("Window/Scene Switcher")]
    public static void ShowWindow()
    {
        var window = GetWindow<SimpleSceneSwitcherWindow>("Scenes");
        window.minSize = new Vector2(150, 20);
    }

    private void OnEnable()
    {
        RefreshScenes();
        SelectCurrentScene();
    }

    // Called when project assets change (added/removed scenes etc.)
    private void OnProjectChange()
    {
        RefreshScenes();
    }

    // Called when active scene changes
    private void OnHierarchyChange()
    {
        SelectCurrentScene();
    }

    private void OnGUI()
    {
        if (_sceneNames == null || _sceneNames.Length == 0)
        {
            EditorGUILayout.HelpBox("No scenes found in Assets/Scenes", MessageType.Info);

            if (GUILayout.Button("Refresh"))
                RefreshScenes();

            return;
        }

        EditorGUILayout.BeginHorizontal();

        int newIndex = EditorGUILayout.Popup(_selectedIndex, _sceneNames);

        if (newIndex != _selectedIndex)
        {
            // Ask to save current scene if modified
            if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
            {
                _selectedIndex = newIndex;
                EditorSceneManager.OpenScene(_scenePaths[_selectedIndex]);
            }
        }

        if (GUILayout.Button("↻", GUILayout.Width(24)))
        {
            RefreshScenes();
            SelectCurrentScene();
        }

        EditorGUILayout.EndHorizontal();
    }

    private void RefreshScenes()
    {
        var guids = AssetDatabase.FindAssets("t:Scene", new[] { "Assets/Scenes" });
        _scenePaths = guids
            .Select(AssetDatabase.GUIDToAssetPath)
            .OrderBy(p => p)
            .ToArray();

        _sceneNames = _scenePaths
            .Select(Path.GetFileNameWithoutExtension)
            .ToArray();

        // Clamp index in case number of scenes changed
        if (_sceneNames.Length == 0)
        {
            _selectedIndex = 0;
        }
        else
        {
            _selectedIndex = Mathf.Clamp(_selectedIndex, 0, _sceneNames.Length - 1);
        }
    }

    private void SelectCurrentScene()
    {
        if (_scenePaths == null || _scenePaths.Length == 0)
            return;

        var activePath = SceneManager.GetActiveScene().path;
        if (string.IsNullOrEmpty(activePath))
            return;

        int idx = Array.IndexOf(_scenePaths, activePath);
        if (idx >= 0)
            _selectedIndex = idx;
    }
}
#endif