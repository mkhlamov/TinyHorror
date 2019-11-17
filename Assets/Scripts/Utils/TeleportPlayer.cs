using UnityEngine;

namespace TinyHorror
{
    public class TeleportPlayer : MonoBehaviour
    {
        #region VariableDeclaration
        // Were player will teleported
        private Vector3 playerPosition;
        [SerializeField]
        private float playerHeight = 1.8f;
        #endregion

        #region Monobehaviour
        void OnEnable()
        {
            TeleportManager.DoTeleport += MoveTo;
        }

        void OnDisable()
        {
            TeleportManager.DoTeleport -= MoveTo;
        }
        #endregion

        void MoveTo(Transform destTransform)
        {
            playerPosition = destTransform.position;
            transform.position = playerPosition;
        }
    }
}