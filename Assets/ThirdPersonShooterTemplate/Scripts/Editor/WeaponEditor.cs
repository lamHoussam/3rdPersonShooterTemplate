using UnityEngine;
using UnityEditor;
using UnityEditor.Build.Content;
using Unity.VisualScripting;

namespace ThirdPersonShooterTemplate
{
    [CustomEditor(typeof(Weapon))]
   public class WeaponEditor : Editor
    {
        private SerializedProperty spWeaponType;

        private SerializedProperty spDamage;
        private SerializedProperty spMaxAmmo;

        private SerializedProperty spFireRate;

        private SerializedProperty spBulletPosition;

        private SerializedProperty spBulletPrefab;
        private SerializedProperty spMuzzleFlash;

        private void OnEnable()
        {
            spWeaponType = serializedObject.FindProperty("m_weaponType");
            spDamage = serializedObject.FindProperty("m_damage");

            spMaxAmmo = serializedObject.FindProperty("m_maxAmmo");
            spFireRate = serializedObject.FindProperty("m_fireRate");

            spBulletPosition = serializedObject.FindProperty("m_BulletPosition");
            spBulletPrefab = serializedObject.FindProperty("m_BulletPrefab");

            spMuzzleFlash = serializedObject.FindProperty("m_MuzzleFlash");
        }


        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.BeginVertical(GUI.skin.box);
            EditorGUILayout.LabelField("Properties", EditorStyles.boldLabel);

            using (new EditorGUI.IndentLevelScope())
            {
                EditorGUILayout.PropertyField(spWeaponType);
                EditorGUILayout.Space();

                EditorGUILayout.PropertyField(spDamage);
                EditorGUILayout.PropertyField(spMaxAmmo);
                EditorGUILayout.PropertyField(spFireRate);
                EditorGUILayout.Space();

                EditorGUILayout.PropertyField(spBulletPosition);
                EditorGUILayout.PropertyField(spBulletPrefab);
                EditorGUILayout.PropertyField(spMuzzleFlash);
            }

            EditorGUILayout.EndVertical();

            serializedObject.ApplyModifiedProperties();
        }
    }
}