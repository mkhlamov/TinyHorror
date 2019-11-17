using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace TinyHorror
{
    public class GunSelectionManager : MonoBehaviour
    {
        [SerializeField]
        private List<GunSelector> _selectors;

        private void Awake()
        {
            _selectors = GetComponentsInChildren<GunSelector>().ToList();
        }

        private void OnEnable()
        {
            foreach (var s in _selectors)
            {
                s.GunSelecled += OnGunSelected;
            }
        }

        private void OnDisable()
        {
            foreach (var s in _selectors)
            {
                s.GunSelecled -= OnGunSelected;
            }
        }

        private void OnGunSelected(GameObject gun)
        {
            foreach (var s in _selectors)
            {
                s.gameObject.transform.GetChild(0).localScale = Vector3.one * (s.gameObject == gun ? 1.5f : 1f);
            }
        }
    }
}