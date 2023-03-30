using CameraSystem;
using ThirdPersonTemplate;
using TMPro;
//using UnityEditor.Animations;
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
        private static readonly int m_animIDReload = Animator.StringToHash("Reload");

        [SerializeField] private Transform m_WeaponParent;
        [SerializeField] private TwoBoneIKConstraint m_LeftHandConstraint;
        [SerializeField] private MultiAimConstraint m_AimConstraint;

        [Space]
        [SerializeField] private Transform m_AimSphere;
        [SerializeField] private float m_FireDistance;

        //private BlendTree m_BlendTree;

        [Space]
        [SerializeField] private TextMeshProUGUI m_AmmoText;

        private ThirdPersonTemplate.Player m_Player;

        private Animator m_Animator;

        private CameraLogicGraph m_CameraLogicGraph;

        private bool m_isAiming;
        public bool IsAiming => m_isAiming;

        private bool m_changeLeftHandWeight = false;

        private void Awake()
        {
            m_Input = GetComponent<ShooterInputAsset>();
            m_Animator = GetComponentInChildren<Animator>();
            m_Player = GetComponent<ThirdPersonTemplate.Player>();
            m_CameraLogicGraph = Camera.main.GetComponent<CameraLogicGraph>();

            DropWeapon();

            m_AimConstraint.weight = 0;
            SetAmmoText();
        }

        private void Update()
        {
            if (m_Input.fire && CurrentWeapon)
            {
                Shoot();
                SetAmmoText();
            }

            if (m_Input.reload && CurrentWeapon)
            {

                Reload();
                m_Input.reload = false;
            }

            if (m_Input.aim)
            {
                AimLogic();
                m_Input.aim = false;
            }

            if (IsAiming)
                Aim();

            if (m_changeLeftHandWeight)
            {
                m_LeftHandConstraint.weight = 1;
                Debug.Log("Called here to change target weight");
                m_changeLeftHandWeight = false;
            }

        }

        public virtual void Aim()
        {
            Vector2 centerPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
            Ray ray = m_CameraLogicGraph.GetComponent<Camera>().ScreenPointToRay(centerPoint);
            //Vector3 direction = ray.direction + (m_CurrentWeapon != null ? Vector3.zero : Vector3.zero);

            //Debug.LogWarning("Vector : " + ray.direction + "; Rotated : " + direction);

            //ray.direction = direction;
            Vector3 point = ray.origin + ray.direction * 30;

            if (Physics.Raycast(ray, out RaycastHit hit, 30))
            {
                point = hit.point;
            }

            m_AimSphere.position = point;

            if (m_CurrentWeapon)
            {
                m_CurrentWeapon.Reposition();
            }
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

            m_CurrentWeapon.OnPickUp(GetComponent<ThirdPersonTemplate.Player>());
        }

        public void DropWeapon()
        {
            m_CurrentWeapon?.OnDropDown(GetComponent<ThirdPersonTemplate.Player>());


            m_LeftHandConstraint.weight = 0;
            m_Animator.SetLayerWeight(1, 0);
            m_CurrentWeapon = null;

            SetAmmoText();
        }

        public void AimLogic()
        {
            m_isAiming = !m_isAiming;
            m_Animator.SetBool(m_animIDAim, m_isAiming);

            m_CameraLogicGraph.SetBool("aim", m_isAiming);

            m_AimConstraint.weight = IsAiming ? 1 : 0;
            if (m_isAiming && m_CurrentWeapon)
                m_CurrentWeapon.SetInitialRotation();

        }

        public void SetAmmoText()
        {
            if (!CurrentWeapon)
                m_AmmoText.text = "";
            else
                m_AmmoText.text = CurrentWeapon.Ammo.ToString() + "/" + CurrentWeapon.MaxAmmo.ToString();
        }

        public void Reload()
        {
            m_Animator.SetTrigger(m_animIDReload);
            m_LeftHandConstraint.weight = 0;
        }

        public void OnStopReload()
        {
            CurrentWeapon.Reload();
            SetAmmoText();
            m_changeLeftHandWeight = true;
            Debug.Log(m_LeftHandConstraint.weight);
        }
    }
}