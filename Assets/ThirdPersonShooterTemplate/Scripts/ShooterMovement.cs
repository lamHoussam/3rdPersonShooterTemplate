using ThirdPersonTemplate;
using UnityEngine;

namespace ThirdPersonShooterTemplate
{
    public class ShooterMovement : Movement
    {

        [SerializeField] private float m_aimAngleOverride;
        private WeaponManager m_WeaponManager;

        private void Start()
        {
            m_WeaponManager = GetComponent<WeaponManager>();
        }

        public override void Move(Vector3 direction, bool isRunning = false, Transform camera = null)
        {
            base.Move(direction, isRunning, camera);
        }

        public override void Rotate(Vector3 inpDirection, out Vector3 finalDirection, Transform camera = null)
        {
            if (m_WeaponManager.IsAiming)
                AimRotate(inpDirection, out finalDirection, camera);
            else
                base.Rotate(inpDirection, out finalDirection, camera);
        }

        private void AimRotate(Vector3 inpDirection, out Vector3 finalDirection, Transform camera = null)
        {
            m_targetRotation = (camera.transform.eulerAngles.y);
            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y,
                m_targetRotation + m_aimAngleOverride, ref m_rotationVelocity,
                m_rotationSmoothTime);

            // rotate to face input direction relative to camera position
            transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
            finalDirection = Quaternion.Euler(0.0f, m_targetRotation, 0.0f) * inpDirection;
        }
    }
}