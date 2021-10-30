using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;
using System.Linq;

/// <summary>
/// 起動時に呼ばれるエディタクラス
/// </summary>
public class RefreshPlay
{
    [InitializeOnLoadMethod]
    static void Run()
    {
        EditorApplication.playModeStateChanged += (PlayModeStateChange state) =>
        {
            if (state == PlayModeStateChange.ExitingEditMode)
            {
                EditorApplication.ExecuteMenuItem("Assets/Refresh");
            }
        };
    }

    [MenuItem("MyEditor/GameScene", priority = 0)]
    public static void OpenGameScene()
    {
        EditorSceneManager.OpenScene("Assets/Scenes/Login.unity", OpenSceneMode.Single);

        EditorApplication.isPlaying = true;
    }
}