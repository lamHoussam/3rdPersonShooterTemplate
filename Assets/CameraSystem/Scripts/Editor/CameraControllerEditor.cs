using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using UnityEditor;
using UnityEditor.Build.Content;
using UnityEngine;

namespace CameraSystem
{
    [CustomEditor(typeof(CameraController))]
    public class CameraControllerEditor : Editor
    {
        private SerializedProperty spTarget;
        private SerializedProperty spDistance;


        // Values
        private SerializedProperty spCameraSettingsToLoad;
        private SerializedProperty spOffset;
        private SerializedProperty spCameraLerpTime;

        private SerializedProperty spAssetName, spAssetPath;

        private SerializedProperty spActive;

        // Yaw, Pitch extremes
        private SerializedProperty spMinPitchValue, spMaxPitchValue;
        private SerializedProperty spMinYawValue, spMaxYawValue;
        private SerializedProperty spUseYawLimit;

        // Collision
        private SerializedProperty spEnableCameraCollision;
        private SerializedProperty spCameraCollisionLayer;

        // Blend
        private SerializedProperty spTransitionLerpTime;
        private SerializedProperty spTransitionCurve;

        private CameraController m_CameraController;

        private void OnEnable()
        {
            spTarget = serializedObject.FindProperty("m_Target");
            spDistance = serializedObject.FindProperty("m_distance");
            spOffset = serializedObject.FindProperty("m_offset");
            spCameraLerpTime = serializedObject.FindProperty("m_cameraLerpTime");

            spCameraSettingsToLoad = serializedObject.FindProperty("m_CameraSettingsToLoad");

            spAssetName = serializedObject.FindProperty("m_assetName");
            spAssetPath = serializedObject.FindProperty("m_assetPath");

            spActive = serializedObject.FindProperty("m_active");

            spMinPitchValue = serializedObject.FindProperty("m_minPitchValue");
            spMaxPitchValue = serializedObject.FindProperty("m_maxPitchValue");
            spUseYawLimit = serializedObject.FindProperty("m_useYawLimit");

            spMinYawValue = serializedObject.FindProperty("m_yawMinValue");
            spMaxYawValue = serializedObject.FindProperty("m_yawMaxValue");

            spEnableCameraCollision = serializedObject.FindProperty("m_enableCameraCollision");
            spCameraCollisionLayer = serializedObject.FindProperty("m_cameraCollisionLayer");

            spEnableCameraCollision = serializedObject.FindProperty("m_enableCameraCollision");
            spCameraCollisionLayer = serializedObject.FindProperty("m_cameraCollisionLayer");


            spTransitionLerpTime = serializedObject.FindProperty("m_transitionLerpTime");
            spTransitionCurve = serializedObject.FindProperty("m_TransitionCurve");

            m_CameraController = target as CameraController;
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(spActive);

            EditorGUILayout.PropertyField(spTarget);

            // Values
            //EditorGUILayout.Space();
            EditorGUILayout.BeginVertical(GUI.skin.box);
            EditorGUILayout.LabelField("Values", EditorStyles.boldLabel);

            using (new EditorGUI.IndentLevelScope())
            {
                EditorGUILayout.PropertyField(spCameraSettingsToLoad);
                if(GUILayout.Button("Load Camera Settings"))
                    LoadCameraSettings();

                EditorGUILayout.PropertyField(spDistance);
                EditorGUILayout.PropertyField(spOffset);
                EditorGUILayout.PropertyField(spCameraLerpTime);

                EditorGUILayout.LabelField("Camera Settings", EditorStyles.boldLabel);
                using (new EditorGUI.IndentLevelScope())
                {
                    EditorGUILayout.PropertyField(spAssetName);
                    EditorGUILayout.PropertyField(spAssetPath);
                    if(GUILayout.Button("Generate Camera Settings"))
                        GenerateCameraSettingsAsset();
                }
            }

            EditorGUILayout.EndVertical();


            // Angles
            //EditorGUILayout.Space();
            EditorGUILayout.BeginVertical(GUI.skin.box);
            EditorGUILayout.LabelField("Angles", EditorStyles.boldLabel);

            using (new EditorGUI.IndentLevelScope())
            {

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PropertyField(spMinYawValue);
                EditorGUILayout.PropertyField(spMaxYawValue);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.PropertyField(spUseYawLimit);
                if (spUseYawLimit.boolValue)
                {
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PropertyField(spMinPitchValue);
                    EditorGUILayout.PropertyField(spMaxPitchValue);
                    EditorGUILayout.EndHorizontal();
                }
            }

            EditorGUILayout.EndVertical();


            // Collisions
            //EditorGUILayout.Space();
            EditorGUILayout.BeginVertical(GUI.skin.box);

            EditorGUILayout.LabelField("Collision", EditorStyles.boldLabel);

            using (new EditorGUI.IndentLevelScope())
            {
                EditorGUILayout.PropertyField(spEnableCameraCollision);
                if (spEnableCameraCollision.boolValue)
                    EditorGUILayout.PropertyField(spCameraCollisionLayer);
            }

            EditorGUILayout.EndVertical();

            //EditorGUILayout.Space();

            // Blend
            EditorGUILayout.BeginVertical(GUI.skin.box);

            EditorGUILayout.LabelField("Blend", EditorStyles.boldLabel);
            using (new EditorGUI.IndentLevelScope())
            {
                EditorGUILayout.PropertyField(spTransitionLerpTime);
                EditorGUILayout.PropertyField(spTransitionCurve);
            }

            EditorGUILayout.EndVertical();

            if (!Application.isPlaying)
                m_CameraController.ThirdPersonCamera();

            serializedObject.ApplyModifiedProperties();
        }

        public void GenerateCameraSettingsAsset()
        {
            CameraSettings asset = ScriptableObject.CreateInstance<CameraSettings>();

            asset.Distance = spDistance.floatValue;
            asset.Offset = spOffset.vector2Value;
            asset.CameraLerpTime = spCameraLerpTime.floatValue;

            AssetDatabase.CreateAsset(asset, spAssetPath.stringValue + "/" + spAssetName.stringValue + ".asset");
            AssetDatabase.SaveAssets();

            EditorUtility.FocusProjectWindow();

            Selection.activeObject = asset;

        }

        public void LoadCameraSettings()
        {
            CameraSettings settings = spCameraSettingsToLoad.objectReferenceValue as CameraSettings;

            if (!settings)
                return;

            spDistance.floatValue = settings.Distance;
            spOffset.vector2Value = settings.Offset;
            spCameraLerpTime.floatValue = settings.CameraLerpTime;
        }
    }
}