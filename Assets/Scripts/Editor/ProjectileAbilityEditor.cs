#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ProjectileAbility))]
public class ProjectileAbilityEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var ability = target as ProjectileAbility;

        var transform = Selection.activeTransform;
        if (transform && GUILayout.Button("Create Spawns From Children")) 
        {
            int count = transform.childCount;

            Vector2[] spawnPoses = new Vector2[count];
            for (int i = 0; i < count; i++)
            {
                spawnPoses[i] = transform.GetChild(i).localPosition;
            }

            ability.waveSpawnPoints = spawnPoses;
        }
    }
}
#endif
