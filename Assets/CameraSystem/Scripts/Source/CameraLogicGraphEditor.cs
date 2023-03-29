
using NodeEditorFramework;
using UnityEditor;
using UnityEngine;

namespace CameraSystem
{
    [CustomEditor(typeof(CameraLogicGraph))]
    public class CameraLogicGraphEditor : Editor
    {
        private NodeCanvas m_Cnv;
        private void OnEnable()
        {

            m_Cnv = serializedObject.FindProperty("m_LogicCanvas").objectReferenceValue as NodeCanvas;
            if (m_Cnv != null && m_Cnv.GetFirstOrNull() == null)
                m_Cnv.LoadCanvasParameterState();
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();


            if (!m_Cnv)
                return;
            EditorGUILayout.BeginVertical(GUI.skin.box);
            EditorGUILayout.LabelField("Graph parameters");

            for(int i = 0; i < m_Cnv.ParametersCount; i++)
            {
                NodeEditorParameter param = m_Cnv.GetParameter(i);
                EditorGUILayout.BeginHorizontal();

                EditorGUILayout.LabelField(param.Name);
                EditorGUILayout.Toggle(param.Value.BoolValue);

                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.EndVertical();
        }
    }
}