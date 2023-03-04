using ThirdPersonTemplate;
using UnityEngine;
using UnityEngine.AI;

namespace ThirdPersonShooterTemplate
{
    public class WeaponManager : MonoBehaviour
    {
        private Weapon m_CurrentWeapon;
        public Weapon CurrentWeapon => m_CurrentWeapon;

        private ShooterInputAsset m_Input;

        [SerializeField] private Transform m_WeaponParent;

        private Animator m_Animator;

        private void Awake()
        {
            m_Input = GetComponent<ShooterInputAsset>();
            m_Animator = GetComponentInChildren<Animator>();
            Debug.LogWarning(m_Input);
        }

        private void Update()
        {
            if (m_Input.fire && CurrentWeapon)
                CurrentWeapon.Shoot(transform.forward);

            if(m_Input.reload && CurrentWeapon)
                CurrentWeapon.Reload();
        }

        public void PickWeapon(Weapon weapon)
        {
            weapon.transform.SetParent(m_WeaponParent);

            weapon.transform.localPosition = Vector3.zero;
            weapon.transform.localRotation = Quaternion.identity;

            m_Animator.SetLayerWeight(1, 1);
        }

        public void DropWeapon()
        {

        }
    }
}