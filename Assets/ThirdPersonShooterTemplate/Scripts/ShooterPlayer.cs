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
            base.SwitchShoulders();
        }
    }
}