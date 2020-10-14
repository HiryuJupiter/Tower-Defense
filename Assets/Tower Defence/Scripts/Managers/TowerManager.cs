using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence.Towers
{
    public class TowerManager : MonoBehaviour
    {
        public static TowerManager instance;

        [SerializeField] List<Tower> towerPrefabs = new List<Tower>();
        List<Tower> aliveTowers = new List<Tower>();
        Tower towerToPurchase;
        

        private void Awake()
        {
            //Singleton
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }

            DontDestroyOnLoad(gameObject);

            //Initialize
            towerToPurchase = towerPrefabs[1];
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                towerToPurchase = towerPrefabs[0];
            }
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                towerToPurchase = towerPrefabs[1];
            }
        }

        public void PurchaseTower (TowerPlatform platform)
        {
            Player.instance.PurchaseTower(towerToPurchase);
            Tower newTower = Instantiate(towerToPurchase);
            platform.AddTower(newTower);
            aliveTowers.Add(newTower);
        }

        void OnGUI()
        {
            GUI.Label(new Rect(20,  20, 500, 20), towerToPurchase.name);
        }
    }

}
