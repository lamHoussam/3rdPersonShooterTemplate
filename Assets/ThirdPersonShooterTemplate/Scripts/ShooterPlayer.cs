using CameraSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThirdPersonShooterTemplate
{
    public class ShooterPlayer : ThirdPersonTemplate.Player
    {
        private WeaponManager m_WeaponManager;

        public override void Awake()
        {
            base.Awake();
            m_WeaponManager = GetComponent<WeaponManager>();
        }

        public override void SwitchShoulders()
        {
            if (m_Movement.InCover)
                return;

            m_rightShoulder = !m_rightShoulder;

            string cameraSettingKey = m_rightShoulder ? "right" : "left";
            if (m_Movement.IsCrouched)
            {
                m_CameraController.GetComponent<CameraLogic>().SwitchCameraSetting(cameraSettingKey + "Crouch");
                return;
            }
            cameraSettingKey += m_WeaponManager.IsAiming ? "Aim" : "Stand";

            m_CameraController.GetComponent<CameraLogic>().SwitchCameraSetting(cameraSettingKey);

        }
    }
}