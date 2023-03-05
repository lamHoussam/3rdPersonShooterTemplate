using CameraSystem;
using ThirdPersonTemplate;
using UnityEngine;
//using UnityEngine.AI;
using UnityEngine.Animations.Rigging;

namespace ThirdPersonShooterTemplate
{
    public class WeaponManager : MonoBehaviour
    {
        private Weapon m_CurrentWeapon;
        public Weapon CurrentWeapon => m_CurrentWeapon;

        private ShooterInputAsset m_Input;

        private static readonly int m_animIDAim = Animator.StringToHash("Aim");

        [SerializeField] private Transform m_WeaponParent;
        [SerializeField] private TwoBoneIKConstraint m_LeftHandConstraint;

        [Space]
        [SerializeField] private CameraSettings m_AimCamera;
        [SerializeField] private CameraSettings m_NormalCamera;

        private CameraController m_CameraController;

        private Animator m_Animator;

        private bool m_isAiming;
        public bool IsAiming => m_isAiming;

        private void Awake()
        {
            m_Input = GetComponent<ShooterInputAsset>();
            m_Animator = GetComponentInChildren<Animator>();
            m_CameraController = Camera.main.GetComponent<CameraController>();

            //DropWeapon();
            m_LeftHandConstraint.weight = 1;
            m_Animator.SetLayerWeight(1, 1);


        }

        private void Update()
        {
            if (m_Input.fire && CurrentWeapon)
            {
                CurrentWeapon.Shoot(transform.forward);
                m_Input.fire = false;
            }

            if (m_Input.reload && CurrentWeapon)
            {
                CurrentWeapon.Reload();
                m_Input.reload = false;
            }

            if (m_Input.aim)
            {
                AimLogic();
                m_Input.aim = false;
            }

        }

        public void PickWeapon(Weapon weapon)
        {
            weapon.transform.SetParent(m_WeaponParent);

            weapon.transform.localPosition = Vector3.zero;
            weapon.transform.localRotation = Quaternion.identity;

            m_LeftHandConstraint.weight = 1;
            m_Animator.SetLayerWeight(1, 1);
        }

        public void DropWeapon()
        {
            m_LeftHandConstraint.weight = 0;
            m_Animator.SetLayerWeight(1, 0);
            m_CurrentWeapon = null;
        }

        public void AimLogic()
        {
            m_isAiming = !m_isAiming;
            m_Animator.SetBool(m_animIDAim, m_isAiming);


            m_CameraController.BlendBetweenCameraSettings(m_isAiming ? m_AimCamera : m_NormalCamera);
        }

    }
}