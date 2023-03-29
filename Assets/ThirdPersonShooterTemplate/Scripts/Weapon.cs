using UnityEngine;
using ThirdPersonTemplate;
using Unity.VisualScripting;

namespace ThirdPersonShooterTemplate
{

    public class Weapon : MonoBehaviour, IInteractable
    {
        public enum WeaponType
        {
            None = 0,
            Rifle = 1,
            Pistol = 2,
            Shotgun = 3,
            Turret = 4,
        }

        [SerializeField] private WeaponType m_weaponType;
        public WeaponType Type => m_weaponType;

        [SerializeField] private float m_damage = 8;
        public float Damage => m_damage;

        [SerializeField] private int m_maxAmmo = 6;
        public int MaxAmmo => m_maxAmmo;

        [SerializeField][Tooltip("Shots per 1 minute")] private float m_fireRate;
        public float FireRate => m_fireRate;
        private float m_timer;


        [SerializeField] private float m_verticalRecoil, m_horizontalRecoil;
        [SerializeField] private float m_recoilSpeed = 20;
        [SerializeField] private float m_recoilSmoothTime = 1;
        private float m_currentRecoilAngle;
         
        [SerializeField] private Transform m_BulletPosition;
        public Vector3 ShotStartPosition => m_BulletPosition.position;

        private bool b_canShoot;
        public bool CanShoot => b_canShoot;

        private int m_ammo;
        public int Ammo => m_ammo;

        [SerializeField] private GameObject m_BulletPrefab;
        //[SerializeField] private Transform m_bulletStartPosition;

        [SerializeField] private ParticleSystem m_MuzzleFlash;


        protected ThirdPersonTemplate.Player m_Holder;

        private Quaternion m_initialRotation;

        protected virtual void Awake()
        {
            m_ammo = m_maxAmmo;
            b_canShoot = true;
            m_timer = 0;
            m_currentRecoilAngle = 0;

            m_Holder = null;

        }

        protected virtual void Update()
        {
            m_timer += Time.deltaTime;
            if (m_timer >= m_fireRate / 60.0f && !b_canShoot)
                b_canShoot = true;


            if(m_Holder == null)
                transform.Rotate(75 * Time.deltaTime * Vector3.up);

            Reposition();
        }

        public virtual void Shoot(Vector3 direction)
        {
            if (m_ammo == 0 || !b_canShoot) { return; }
            if (m_MuzzleFlash)
                m_MuzzleFlash.Play();

            direction = MathsUtility.RotateVector(direction, m_currentRecoilAngle, MathsUtility.Axis.X);

            Debug.DrawRay(ShotStartPosition, direction * 30, Color.red, 10);

            m_ammo--;

            m_timer = 0;
            b_canShoot = false;
            Recoil();
        }


        public virtual void Reload() => m_ammo = MaxAmmo;

        public void OnBeginOverlap(ThirdPersonTemplate.Player player)
        {

        }

        public void OnInteract(ThirdPersonTemplate.Player player)
        {
            player.GetComponent<WeaponManager>().PickWeapon(this);
        }

        public void OnEndOverlap(ThirdPersonTemplate.Player player)
        {
            //throw new System.NotImplementedException();
        }

        public void OnPickUp(ThirdPersonTemplate.Player player)
        {
            m_Holder = player;
            m_initialRotation = transform.localRotation;
        }

        public void OnDropDown(ThirdPersonTemplate.Player player)
        {
            m_Holder = null;
        }


        private void Recoil()
        {
            float horizontalValue = UnityEngine.Random.Range(-m_horizontalRecoil, m_horizontalRecoil);
            float verticalValue = UnityEngine.Random.Range(0, m_verticalRecoil);

            m_currentRecoilAngle += verticalValue * Mathf.Rad2Deg * Time.deltaTime;

            Quaternion targetRotation = Quaternion.Euler(transform.localEulerAngles + Vector3.right * (m_currentRecoilAngle));
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, .3f * Time.deltaTime);
        }

        public void Reposition()
        {
            m_currentRecoilAngle = Mathf.LerpAngle(m_currentRecoilAngle, 0, 2 * Time.deltaTime);
            Debug.LogError(m_currentRecoilAngle);
        }

        public void SetRotate(float ang)
        {
        }
    }
}