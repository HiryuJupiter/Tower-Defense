using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TowerDefence.Enemies;
using TowerDefence.Managers;

namespace TowerDefence.Towers
{
    public abstract class Tower : MonoBehaviour
    {
        #region Fields
        [Header("General Stats")]
        [SerializeField]                protected string towerName   = "";
        [SerializeField, TextArea]      protected string description = "";
        [SerializeField, Range(1, 10)]  protected int   cost        = 1;

        [Header("Attack Stats")]
        [SerializeField, Min(0.1f)]     protected float damage = 1;
        [SerializeField, Min(0.1f)]     protected float minimumRange = 1;
        [SerializeField]                protected float maximumRange = 5;
        [SerializeField, Min(0.1f)]     protected float shootCD = 0.1f;

        [Header("Experience Stats")]
        [SerializeField, Range(2, 5)]   protected int   maxLevel = 3;
        [SerializeField, Min(1)]        protected float baseRequiredXp = 5;
        [SerializeField, Min(1)]        protected float experienceScaler = 1;

        int level = 1;
        float xp = 0;

        protected float shootTimer = 0;
        #endregion

        #region Properties
        public string TowerName => towerName;
        public string Description => description;
        public int Cost => cost;
        public float fireRatePerSec => 1f / shootCD;
        public string UiDisplayText
        {
            get
            {
                string display = $"Name: {TowerName} Cost: {Cost.ToString()}\n";
                display += Description + "\n";
                display += string.Format("Min Range: {0}, Max Range: {1}, Damage: {2}", minimumRange.ToString(), maximumRange.ToString(), damage.ToString());
                return display;
            }
        }
        public float MaximumRange => maximumRange * (level * 0.5f + 0.5f);
        public float Damage => damage * (level * 0.5f + 0.5f);
        protected float RequiredXP => level == 1 ? baseRequiredXp : baseRequiredXp * (level * experienceScaler);
        protected Enemy Target { get; private set; }
        #endregion

        #region MonoBehavior
        protected virtual void Update()
        {
            UpdateClosestTarget();
            FireWhenReady();
        }
        #endregion

        #region Public

        #endregion
        public void AddExperience(float _xp)
        {
            xp += _xp;
            // Check that the level is not maxed out and that we have 
            // passed the required experience to level up
            if (level < maxLevel)
            {
                if (xp >= RequiredXP)
                {
                    LevelUp();
                }
            }
        }

        #region Level up

        void LevelUp()
        {
            level++;
            xp = 0;

        }
        protected abstract void RenderLevelUpVisuals();
        #endregion


        #region Attack
        void Fire()
        {
            if (Target != null)
            {
                //Target.Damage(this);

                RenderAttackVisuals();
            }
        }

        void FireWhenReady()
        {
            if (Target != null)
            {
                shootTimer -= Time.deltaTime;
                if (shootTimer < 0f)
                {
                    shootTimer = shootCD;
                    Fire();
                }
            }
        }

        protected abstract void RenderAttackVisuals();

        #endregion

        void UpdateClosestTarget()
        {
            Enemy[] closeEnemies = EnemyManager.instance.GetClosestEnemies(transform, MaximumRange, minimumRange);
            Target = GetClosestEnemy(closeEnemies);
        }

        // _enemies is the array of enemies within range
        Enemy GetClosestEnemy(Enemy[] _enemies)
        {
            float closestDist = float.MaxValue;
            Enemy closest = null;

            foreach (Enemy enemy in _enemies)
            {
                float distToEnemy = Vector3.Distance(enemy.transform.position, transform.position);

                if (distToEnemy < closestDist)
                {
                    closestDist = distToEnemy;
                    closest = enemy;
                }
            }

            return closest;
        }

#if UNITY_EDITOR
        // OnValidate runs whenever a variable is changed within the inspector of this class
        void OnValidate()
        {
            maximumRange = Mathf.Clamp(maximumRange, minimumRange + 1, float.MaxValue);
        }

        // OnDrawGizmosSelected draws helpful visuals only when the object is selected
        void OnDrawGizmosSelected()
        {
            // Draw a mostly transparent red sphere indicating the minimum range
            Gizmos.color = new Color(1, 0, 0, 0.25f);
            Gizmos.DrawSphere(transform.position, minimumRange);

            // Draw a mostly transparent blue sphere indicating the maximum range
            Gizmos.color = new Color(0, 0, 1, 0.25f);
            Gizmos.DrawSphere(transform.position, MaximumRange);
        }
#endif
    }


}