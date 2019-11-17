using System;
using UnityEngine;
using UnityEngine.UI;
using VRStandardAssets.Utils;

namespace TinyHorror {
    [RequireComponent(typeof(VRInteractiveItem))]
    public class GunSelector : MonoBehaviour
    {
        #region VariableDeclaration
        public event Action<GameObject> GunSelecled;

        private readonly string NO_DESC_TEXT = "No desription provided. Take on your risk.";
        private VRInteractiveItem _vRInteractiveItem;
        [SerializeField]
        private Weapon _weapon;
        [SerializeField]
        private Text _descriptionText;

        public Weapon Weapon {
            get { return _weapon; }
            private set { }
        }
        #endregion

        #region Monobehaviour
        private void Awake()
        {
            _vRInteractiveItem = GetComponent<VRInteractiveItem>();
        }

        private void OnEnable()
        {
            _vRInteractiveItem.OnClick += HandleClick;
        }

        private void OnDisable()
        {
            _vRInteractiveItem.OnClick -= HandleClick;
        }

        private void Start()
        {
            GameObject gun = Instantiate(Weapon.weaponPrefab, transform.GetChild(0).GetChild(0));
            gun.GetComponentInChildren<Light>().enabled = false;
            _descriptionText.text = String.IsNullOrEmpty(Weapon.description) ? NO_DESC_TEXT : Weapon.description;
        }
        #endregion

        private void HandleClick()
        {
            GameManager.Instance.SetNewWeapon(_weapon);
            GunSelecled?.Invoke(gameObject);
        }

    }
}