using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BTManager))] // YourComponent�� ���� GameObject�� �����ϴ� ������Ʈ�Դϴ�.
public class GameObjectArrayPreviewEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        BTManager bt = (BTManager)target;

        // GameObject �迭�� �� ��ҿ� ���� �����並 �׸��ϴ�.
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