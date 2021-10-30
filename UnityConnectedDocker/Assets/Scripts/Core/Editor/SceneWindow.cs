// ref https://tmls.hatenablog.com/entry/2020/07/11/154742

using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneWindow : EditorWindow
{
    // ボタンの大きさ
    private readonly Vector2 _buttonMinSize = new Vector2(100, 20);
    private readonly Vector2 _buttonMaxSize = new Vector2(1000, 100);

    //! MenuItem("メニュー名/項目名") のフォーマットで記載
    [MenuItem("MyEditor/SceneWindow")]
    static void ShowWindow()
    {
        // ウィンドウを表示！
        GetWindow<SceneWindow>();
    }

    void OnGUI()
    {
        // レイアウトを整える
        GUIStyle buttonStyle = new GUIStyle("button") { fontSize = 30 };
        var layoutOptions = new GUILayoutOption[]
        {
            GUILayout.MinWidth(_buttonMinSize.x),
            GUILayout.MinHeight(_buttonMinSize.y),
            GUILayout.MaxWidth(_buttonMaxSize.x),
            GUILayout.MaxHeight(_buttonMaxSize.y)
        };

        // 各シーンのファイルパスを取得
        string path = Application.dataPath + "/" + "Scenes";
        string[] files = Directory.GetFiles(path, "*.unity", SearchOption.AllDirectories);
        foreach(var file in files)
        {
            // Titleボタンを作る
            var name = file.Split('/').Last().Split('.')[0];
            if (GUILayout.Button(name, buttonStyle, layoutOptions))
            {
                if (!EditorSceneManager.SaveModifiedScenesIfUserWantsTo(new Scene[] { SceneManager.GetActiveScene() }))
                    return;
                EditorSceneManager.OpenScene(file);
            }
        }
    }
}