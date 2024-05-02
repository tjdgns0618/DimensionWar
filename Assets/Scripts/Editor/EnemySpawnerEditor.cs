using UnityEngine;
using UnityEditor;

// EnemySpawner Ŭ������ ���� Ŀ���� �����͸� �����ϴ� Ŭ����
[CustomEditor(typeof(EnemySpawner))]
public class EnemySpawnerEditor : Editor
{
    // Inspector�� Ŀ���͸���¡�ϴ� �Լ� ������
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        // Ÿ���� EnemySpawner�� ĳ����
        EnemySpawner spawner = (EnemySpawner)target;

        // EnemyWaves ����Ʈ�� �� ���̺꿡 ���� ���� ǥ�� �� ����
        if (spawner.EnemyWaves != null)
        {
            for (int i = 0; i < spawner.EnemyWaves.Count; i++)
            {
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("Wave " + (i + 1) + " Settings", EditorStyles.boldLabel);

                // Increase Speed ��� �� Speed Multiplier ����
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
