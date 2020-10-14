using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TowerDefence.Towers;
using TowerDefence.Managers;
using System.Runtime.InteropServices.ComTypes;

namespace TowerDefence.Enemies
{
    public class Enemy : MonoBehaviour
    {
        [Header("General Stats")]
        [SerializeField, Tooltip("How fast the enemy will move within the game.")]
        float speed = 1;
        [SerializeField, Tooltip("How much damage the enemy can take before dying.")]
        float health = 1;
        [SerializeField, Tooltip("How much damage the enemy will do to the player's health.")]
        float damage = 1;
        [SerializeField, Tooltip("How big is the enemy visually?")]
        float size = 1;
        // RESISTANCE HERE

        [Header("Rewards")]
        [SerializeField, Tooltip("The amount of experience the killing tower will gain from killing the enemy.")]
        float xp = 1;
        [SerializeField, Tooltip("The amount of money the player will gain upon killing the enemy.")]
        int money = 1;

        Player player; // The reference to the player gameObject within the scene.

        /// <summary>
        /// Handles damage of the enemy and if below or equal to 0, calls Die
        /// </summary>
        /// <param name="_tower">The tower doing the damage to the enemy.</param>
        public void Damage(Tower _tower)
        {
            health -= _tower.Damage;
            if (health <= 0)
            {
                Die(_tower);
            }
        }

        /// <summary>
        /// Handles the visual, and technical features of dying, such as giving the tower experience.
        /// </summary>
        /// <param name="_tower">The tower that killed the enemy.</param>
        void Die(Tower _tower)
        {
            _tower.AddExperience(xp);
            Player.instance.AddMoney(money);
            //Destroy(gameObject);
            EnemyManager.instance.KillEnemy(this);
        }

        void Start()
        {
            //player = Player.instance;
        }

        void Update()
        {

        }
    }
}