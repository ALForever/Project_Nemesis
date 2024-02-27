using UnityEngine;
using static UnityEditor.Handles;
#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteAlways]
public class HandlesEnemySpawnerArea : MonoBehaviour
{
#if UNITY_EDITOR
    [SerializeField] private EnemySpawnerArea m_enemySpawnerArea;
    [SerializeField] private Color m_color;
        

    private void OnEnable()
    {        
        SceneView.duringSceneGui += OnSceneGUI;
    }

    private void OnDisable()
    {
        SceneView.duringSceneGui -= OnSceneGUI;
    }

    private void OnSceneGUI(SceneView sceneView)
    {        
        Vector3 pos = m_enemySpawnerArea.transform.position;
        Handles.color = m_color;
        Handles.DrawWireDisc(pos, Vector3.forward, m_enemySpawnerArea.AreaRadius);
    }
#endif
}