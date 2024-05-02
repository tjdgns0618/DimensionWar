using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EnemySpawner))]
public class EnemySpawnerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EnemySpawner spawner = (EnemySpawner)target;

        // 각 웨이브에 대한 설정을 표시하고 수정할 수 있도록 함
        if (spawner.EnemyWaves != null)
        {
            for (int i = 0; i < spawner.EnemyWaves.Count; i++)
            {
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("Wave " + (i + 1) + " Settings", EditorStyles.boldLabel);

                EditorGUILayout.BeginHorizontal();
                spawner.EnemyWaves[i].increaseSpeed = EditorGUILayout.Toggle("Increase Speed", spawner.EnemyWaves[i].increaseSpeed);
                if (spawner.EnemyWaves[i].increaseSpeed)
                {
                    spawner.EnemyWaves[i].speedMultiplier = EditorGUILayout.FloatField("Speed Multiplier", spawner.EnemyWaves[i].speedMultiplier);
                }
                EditorGUILayout.EndHorizontal();
            }
        }
    }
}
