using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using System.IO;

[InitializeOnLoad]
public static class DefaultSceneLoader
{
    private const string SCENE_PATH = "Assets/StarterAssets/FirstPersonController/Scenes/Playground.unity";

    // SessionState prevents this from running endlessly when recompiling code inside Unity
    private const string HAS_RUN_KEY = "DefaultSceneLoader_HasRun";

    static DefaultSceneLoader()
    {
        EditorApplication.delayCall += ForceLoadDefaultScene;
    }

    private static void ForceLoadDefaultScene()
    {
        // Only run once when Unity first launches, not every time C# recompiles
        if (SessionState.GetBool(HAS_RUN_KEY, false)) return;
        SessionState.SetBool(HAS_RUN_KEY, true);

        // Check if Playground is already open so we don't reload it unnecessarily
        var activeScene = EditorSceneManager.GetActiveScene();
        if (activeScene.path == SCENE_PATH) return;

        // Force open Playground
        if (File.Exists(SCENE_PATH))
        {
            EditorSceneManager.OpenScene(SCENE_PATH);
            Debug.Log($"[DefaultSceneLoader] Successfully loaded launch scene: {SCENE_PATH}");
        }
        else
        {
            Debug.LogError($"[DefaultSceneLoader] File not found at: {SCENE_PATH}");
        }
    }
}