using System.Collections.Generic;
using System;
using UnityEngine;
using VRStandardAssets.Utils;

namespace TinyHorror
{
    public class TeleportManager : MonoBehaviour
    {
        #region VariableDeclaration
        // Here I will always "just" teleport, but this action
        // can be used to implement different types of telepoting, e.g. with
        // camera fade or some other effects to reduce VR motion sickness
        public static event Action<Transform> DoTeleport;

        [SerializeField]
        private GameObject teleportLocationsParent;
        private List<VRInteractiveItem> teleportLocations = new List<VRInteractiveItem>();
        [SerializeField]
        private Transform reticleTransform;
        #endregion

        #region Monobehaviour
        private void Awake()
        {
            foreach (Transform t in teleportLocationsParent.transform)
            {
                if (t.gameObject.GetComponent<VRInteractiveItem>() != null)
                {
                    teleportLocations.Add(t.gameObject.GetComponent<VRInteractiveItem>());
                }
            }
        }

        private void OnEnable()
        {
            foreach (VRInteractiveItem t in teleportLocations)
            {
                t.OnDoubleClick += Teleport;
            }
        }

        private void OnDisable()
        {
            foreach (VRInteractiveItem t in teleportLocations)
            {
                t.OnDoubleClick -= Teleport;
            }
        }
        #endregion

        private void Teleport()
        {
            DoTeleport?.Invoke(reticleTransform);
        }
    }
}