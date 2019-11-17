using UnityEngine;
using VRStandardAssets.Utils;

namespace TinyHorror
{
    public class Player : MonoBehaviour
    {
        #region VariableDeclaration
        public Weapon _attack;

        private ActorStats _stats;
        private VREyeRaycaster _VREyeRaycaster;
        private VRInput _VRInput;
        private AudioSource _gunShot;
        private OVRControllerHelper[] _controllerHelpers;
        #endregion

        #region Monobehaviour
        private void Awake()
        {
            _stats              = GetComponent<ActorStats>();
            _VREyeRaycaster     = GetComponentInChildren<VREyeRaycaster>();
            _VRInput            = FindObjectOfType<VRInput>();
            _gunShot            = GetComponent<AudioSource>();
            _controllerHelpers  = FindObjectsOfType<OVRControllerHelper>();
        }

        private void OnEnable()
        {
            _VRInput.OnClick += HandleClick;
        }


        private void OnDisable()
        {
            _VRInput.OnClick -= HandleClick;
        }
        #endregion

        public void EquipWeapon(Weapon w)
        {
            _attack = w;
            foreach (var ch in _controllerHelpers)
            {
                ch.m_modelGun = Instantiate(_attack.weaponPrefab, ch.transform);
                ch.m_modelGun.GetComponentInChildren<Light>().range = _attack.lightRange;
            }
        }

        #region PrivateMethods
        private void HandleClick()
        {
            if (_stats.GetHealth() <= 0)
            {
                return;
            }
            PlayFireSound();
            // Check if we are looking at attackable target and attack it
            if (_VREyeRaycaster.CurrentInteractible != null &&
                _VREyeRaycaster.CurrentInteractible.GetComponent<IAttackable>() != null) {
                GameObject target = _VREyeRaycaster.CurrentInteractible.gameObject;
                // we can have multiple IAttackables on one object responsible for different effects
                Attack attack = _attack.CreateAttack(_stats);
                var attackableComponents = target.GetComponentsInChildren<IAttackable>();
                foreach (IAttackable a in attackableComponents)
                {
                    a.OnAttack(gameObject, attack);
                }
            }
            
        }

        private void PlayFireSound()
        {
            if (_gunShot != null)
            {
                _gunShot.Play();
            }
        }
        #endregion
    }
}