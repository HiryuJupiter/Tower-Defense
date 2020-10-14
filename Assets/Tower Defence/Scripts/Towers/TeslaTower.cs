using System.Collections;
using System.Collections.Generic;
using TowerDefence.Towers;
using TowerDefence.Utilities;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;

namespace TowerDefence.Towers
{
    public class TeslaTower : Tower
    {
        [Header("Components")]
        [SerializeField] Transform ball;
        [SerializeField] LineRenderer lightningLine;

        [Header("Settings")]
        [SerializeField, Range(1, 10), Tooltip("The amount of segment within a line to the enemy.")]
        int maxSegments = 2;
        [SerializeField, Range(0.01f, 0.25f), Tooltip("The maximum amount of offset a segment can have.")]
        float maxVariance = 0.25f;


        protected override void Update()
        {
            base.Update();
        }

        protected override void RenderLevelUpVisuals()
        {

        }

        protected override void RenderAttackVisuals()
        {
            if (Target == null)
                return;

            Vector3 direction = Target.transform.position - ball.position;
            float distance = direction.magnitude;

            //Generate line renderer points
            List<Vector3> linePositions = new List<Vector3>();

            linePositions.Add(ball.position);
            for (int i = 0; i < maxSegments; i++)
            {
                linePositions.Add(ball.position +  (1 + i) * direction / maxSegments);
            }
            linePositions.Add(Target.transform.position);

            lightningLine.positionCount = linePositions.Count;
            RenderLightning(linePositions, direction, 0);

            //
            ////lightningLine.SetPosition(0, segmentPoints[0]);
            //lightningLine.SetPosition(maxSegments, segmentPoints[segmentPoints.Count - 1]);

        }

        protected void RenderLightning(List<Vector3> linePositions, Vector3 directionToTarget, int positionIndex)
        {
            if (positionIndex == 0)
            {
                lightningLine.SetPosition(positionIndex, linePositions[positionIndex]);

                RenderLightning(linePositions, directionToTarget, ++positionIndex);
            }
            else if(positionIndex < linePositions.Count - 1)
            {
                Vector3 newDir = Vector3.Cross(directionToTarget, linePositions[positionIndex]);
                linePositions[positionIndex] += newDir * Random.Range(-maxVariance, maxVariance);

                lightningLine.SetPosition(positionIndex, linePositions[positionIndex]);

                RenderLightning(linePositions, directionToTarget, ++positionIndex);
            }
            else 
            {
                
                lightningLine.SetPosition(positionIndex, linePositions[positionIndex]);
            }
        }
    }
}
