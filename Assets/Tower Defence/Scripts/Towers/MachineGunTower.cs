using System.Collections;
using System.Collections.Generic;
using TowerDefence.Towers;
using TowerDefence.Utilities;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;

namespace TowerDefence.Towers
{
    public class MachineGunTower : Tower
    {
        [Header("Properties")]
        [SerializeField] Transform turret;
        [SerializeField] Transform gunPivot;
        [SerializeField] LineRenderer bulletLine;
        [SerializeField] Transform leftFirePoint;
        [SerializeField] Transform rightFirePoint;

        bool fireLeft;

        bool hasResetVisuals;

        protected override void Update()
        {
            base.Update();

            //Detect if we have no enemy AND we have not reset visuals
            if (Target == null && !hasResetVisuals)
            {
                //Check if the attack timer is ready
                if (shootTimer < shootCD)
                {
                    //Add to the current time
                    shootTimer += Time.deltaTime;
                }
                else
                {
                    //Disable line renderer
                    //Reset timer to 0
                    //Set reset visuals flag to true
                    bulletLine.positionCount = 0;
                    shootTimer = 0;
                    hasResetVisuals = true;
                }
            }
        }

        protected override void RenderAttackVisuals()
        {
            Vector3 direction = Target.transform.position - gunPivot.position;

            gunPivot.rotation = Quaternion.LookRotation(direction);
            if (fireLeft)
                RenderBulletLine(leftFirePoint);
            else
                RenderBulletLine(rightFirePoint);
            fireLeft = !fireLeft;
            hasResetVisuals = false;
        }

        protected override void RenderLevelUpVisuals()
        {
            Debug.Log("Leveld up");
        }

        void RenderBulletLine(Transform firePoint)
        {
            Debug.DrawRay(firePoint.position, Vector3.up * 20f, Color.red, 10f);
            //Debug.Log("firePoint.localPositio:" + firePoint.position + ", Target.transform.localPosition: " + Target.transform.localPosition);
            bulletLine.positionCount = 2;
            bulletLine.SetPosition(0, firePoint.position);
            bulletLine.SetPosition(1, Target.transform.position);
        }
    }
}
