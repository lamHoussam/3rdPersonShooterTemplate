using ThirdPersonTemplate;


#if UNITY_EDITOR
using UnityEditor;

namespace ThirdPersonShooterTemplate
{
    [CustomEditor(typeof(ShooterMovement))]
    public class ShooterMovementEditor : MovementEditor
    {
        private SerializedProperty spAimAngleOverride;

        public override void OnEnable()
        {
            base.OnEnable();
            spAimAngleOverride = serializedObject.FindProperty("m_aimAngleOverride");
        }

        public override void OnInspectorGUI()
        {

            base.OnInspectorGUI();

            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(spAimAngleOverride);

            serializedObject.ApplyModifiedProperties();

        }
    }
}

#endif