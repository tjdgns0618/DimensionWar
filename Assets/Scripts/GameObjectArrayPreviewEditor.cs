using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BTManager))] // YourComponent는 여러 GameObject를 포함하는 컴포넌트입니다.
public class GameObjectArrayPreviewEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        BTManager bt = (BTManager)target;

        // GameObject 배열의 각 요소에 대한 프리뷰를 그립니다.
        for (int i = 0; i < bt.Tower.Length; i++)
        {
            GameObject go = bt.Tower[i];
            Texture2D preview = AssetPreview.GetAssetPreview(go);
            if (preview != null)
            {
                GUILayout.Label("Preview " + i + ":");
                Rect rect = GUILayoutUtility.GetRect(100, 100);
                EditorGUI.DrawTextureTransparent(rect, preview, ScaleMode.ScaleToFit);
            }
        }
    }
}