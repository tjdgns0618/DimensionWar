using UnityEngine;
using UnityEditor;

// EnemySpawner 클래스를 위한 커스텀 에디터를 정의하는 클래스
[CustomEditor(typeof(EnemySpawner))]
public class EnemySpawnerEditor : Editor
{
    // Inspector를 커스터마이징하는 함수 재정의
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        // 타겟을 EnemySpawner로 캐스팅
        EnemySpawner spawner = (EnemySpawner)target;

        // EnemyWaves 리스트의 각 웨이브에 대한 설정 표시 및 수정
        if (spawner.EnemyWaves != null)
        {
            for (int i = 0; i < spawner.EnemyWaves.Count; i++)
            {
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("Wave " + (i + 1) + " Settings", EditorStyles.boldLabel);

                // Increase Speed 토글 및 Speed Multiplier 조정
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
