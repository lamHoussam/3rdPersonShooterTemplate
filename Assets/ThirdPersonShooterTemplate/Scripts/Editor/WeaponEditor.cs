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


        private SerializedProperty spVerticalRecoil;
        private SerializedProperty spHorizontalRecoil;

        private SerializedProperty spRecoilSpeed;
        private SerializedProperty spRecoilSmoothTime;

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


            spVerticalRecoil = serializedObject.FindProperty("m_verticalRecoil");
            spHorizontalRecoil = serializedObject.FindProperty("m_horizontalRecoil");

            spRecoilSpeed = serializedObject.FindProperty("m_recoilSpeed");
            spRecoilSmoothTime = serializedObject.FindProperty("m_recoilSmoothTime");
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

                EditorGUILayout.PropertyField(spVerticalRecoil);
                EditorGUILayout.PropertyField(spHorizontalRecoil);
                EditorGUILayout.PropertyField(spRecoilSpeed);
                EditorGUILayout.PropertyField(spRecoilSmoothTime);
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