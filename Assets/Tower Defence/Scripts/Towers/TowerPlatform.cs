using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TowerDefence.Towers;

namespace TowerDefence.Towers
{
    public class TowerPlatform : MonoBehaviour
    {
        public static TowerPlatform instance;

        [SerializeField] Transform towerHolder;
        Tower heldTower;

       

        void Awake()
        {

        }

        public void AddTower(Tower tower)
        {
            heldTower = tower;

            tower.transform.SetParent(towerHolder);
            tower.transform.localPosition = Vector3.zero;
        }

        void OnMouseUpAsButton()
        {
            if (heldTower == null)
            {
                TowerManager.instance.PurchaseTower(this);
            }
        }
    }
}