using CameraSystem;
using TMPro;
using UnityEngine;
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
        [SerializeField] private MultiAimConstraint m_AimConstraint;

        [Space]
        [SerializeField] private CameraSettings m_AimCamera;
        [SerializeField] private CameraSettings m_NormalCamera;

        [Space]
        [SerializeField] private Transform m_AimSphere;
        [SerializeField] private float m_FireDistance;

        [Space]
        [SerializeField] private TextMeshProUGUI m_AmmoText;

        private CameraController m_CameraController;

        private Animator m_Animator;

        private bool m_isAiming;
        public bool IsAiming => m_isAiming;

        private void Awake()
        {
            m_Input = GetComponent<ShooterInputAsset>();
            m_Animator = GetComponentInChildren<Animator>();
            m_CameraController = Camera.main.GetComponent<CameraController>();

            DropWeapon();
            //m_LeftHandConstraint.weight = 1;
            //m_Animator.SetLayerWeight(1, 1);


            m_AimConstraint.weight = 0;
            SetAmmoText();
        }

        private void Update()
        {
            if (m_Input.fire && CurrentWeapon)
            {
                Shoot();
                //m_Input.fire = false;
                SetAmmoText();
            }

            if (m_Input.reload && CurrentWeapon)
            {
                CurrentWeapon.Reload();
                m_Input.reload = false;
                SetAmmoText();
            }

            if (m_Input.aim)
            {
                AimLogic();
                m_Input.aim = false;
            }

            if (IsAiming)
                Aim();

        }

        public virtual void Aim()
        {
            Vector2 centerPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
            Ray ray = Camera.main.ScreenPointToRay(centerPoint);
            Vector3 point = ray.origin + ray.direction * 30;

            if (Physics.Raycast(ray, out RaycastHit hit, 30))
            {
                point = hit.point;
            }

            m_AimSphere.position = point;
        }

        public virtual void Shoot()
        {
            Vector3 shotDirection = (m_AimSphere.position - CurrentWeapon.ShotStartPosition).normalized;

            CurrentWeapon.Shoot(shotDirection);
        }

        public void PickWeapon(Weapon weapon)
        {
            m_CurrentWeapon = weapon;

            m_CurrentWeapon.transform.SetParent(m_WeaponParent);

            m_CurrentWeapon.transform.localPosition = Vector3.zero;
            m_CurrentWeapon.transform.localRotation = Quaternion.identity;

            m_LeftHandConstraint.weight = 1;
            m_Animator.SetLayerWeight(1, 1);

            SetAmmoText();
        }

        public void DropWeapon()
        {
            m_LeftHandConstraint.weight = 0;
            m_Animator.SetLayerWeight(1, 0);
            m_CurrentWeapon = null;

            SetAmmoText();
        }

        public void AimLogic()
        {
            m_isAiming = !m_isAiming;
            m_Animator.SetBool(m_animIDAim, m_isAiming);

            m_AimConstraint.weight = IsAiming ? 1 : 0;

            m_CameraController.BlendBetweenCameraSettings(m_isAiming ? m_AimCamera : m_NormalCamera);
        }

        public void SetAmmoText()
        {
            if (!CurrentWeapon)
                m_AmmoText.text = "";
            else
                m_AmmoText.text = CurrentWeapon.Ammo.ToString() + "/" + CurrentWeapon.MaxAmmo.ToString();
        }

    }
}