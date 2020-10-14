using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TowerDefence.Towers;

namespace TowerDefence
{
    public class Player : MonoBehaviour
    {

        public static Player instance = null;

        [SerializeField, Tooltip("This sets the initial amount of money the player has.")]
        int money = 100;

        public void AddMoney(int _money)
        {
            money += _money;
        }

        public void PurchaseTower(Tower _tower)
        {
            money -= _tower.Cost;
        }

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(gameObject);
                return;
            }

            DontDestroyOnLoad(gameObject);
        }
    }
}