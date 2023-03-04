using ThirdPersonTemplate;
using UnityEngine.InputSystem;

namespace ThirdPersonShooterTemplate
{
    public class ShooterInputAsset : InputAsset
    {
        public bool reload;

        private void OnReload(InputValue value)
        {
            reload = value.isPressed;
        }
    }
}