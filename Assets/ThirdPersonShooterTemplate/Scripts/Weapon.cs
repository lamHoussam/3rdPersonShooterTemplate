using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ThirdPersonTemplate;

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

        [SerializeField] private Transform m_BulletPosition;

        private bool b_canShoot;
        public bool CanShoot => b_canShoot;

        private int m_ammo;
        public int Ammo => m_ammo;

        [SerializeField] private GameObject m_bulletPrefab;
        //[SerializeField] private Transform m_bulletStartPosition;

        [SerializeField] private ParticleSystem m_MuzzleFlash;




        protected virtual void Awake()
        {
            m_ammo = m_maxAmmo;
            b_canShoot = true;
            m_timer = 0;

        }

        protected virtual void Update()
        {
            m_timer += Time.deltaTime;
            if (m_timer >= m_fireRate / 60.0f && !b_canShoot)
                b_canShoot = true;
        }

        public virtual void Shoot(Vector3 direction)
        {
            if (m_ammo == 0 || !b_canShoot) { return; }

            //Debug.Log("Shoot");
            //Debug.DrawRay(transform.position, Vector3.forward * 20, Color.red, 10);
            //Bullet bullet = Instantiate(m_bulletPrefab, m_BulletPosition.position, transform.rotation).GetComponent<Bullet>();
            //bullet.Damage = m_damage;
            //bullet.GetComponent<Rigidbody>().AddForce(direction * 40, ForceMode.Impulse);

            if (m_MuzzleFlash)
                m_MuzzleFlash.Play();

            //Ray shotRay = new Ray(transform.position, direction);
            //RaycastHit[] hits = Physics.RaycastAll(shotRay, 50);

            //foreach (RaycastHit hit in hits)
            //{
            //    if (hit.transform.gameObject.TryGetComponent(out HealthComponent health))
            //    {
            //        health.TakeDamage(Damage, m_Player);
            //    }
            //}

            m_ammo--;

            m_timer = 0;
            b_canShoot = false;
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
    }
}