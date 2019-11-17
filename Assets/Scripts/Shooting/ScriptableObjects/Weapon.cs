using UnityEngine;

namespace TinyHorror
{
    [CreateAssetMenu(fileName = "New weapon", menuName = "Weapon")]
    public class Weapon : AttackSO
    {
        public GameObject weaponPrefab;
        public int lightRange;
        public string description = "";
    }
}