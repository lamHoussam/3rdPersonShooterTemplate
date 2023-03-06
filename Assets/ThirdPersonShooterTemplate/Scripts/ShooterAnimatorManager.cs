using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ThirdPersonTemplate;

namespace ThirdPersonShooterTemplate
{
    public class ShooterAnimatorManager : AnimatorManager
    {
        protected WeaponManager m_WeaponManager;

        private void Start()
        {
            m_WeaponManager = GetComponentInParent<WeaponManager>();
        }

        public void OnStopReload() => m_WeaponManager.OnStopReload();
    }
}