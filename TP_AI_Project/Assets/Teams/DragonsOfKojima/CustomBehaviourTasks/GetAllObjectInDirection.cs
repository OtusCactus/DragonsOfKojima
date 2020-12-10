using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using DoNotModify;
using UnityEngine;

namespace DragonsOfKojima
{
    public class GetAllObjectsInDirection : Action
    {
        private SpaceShip AiSpaceShip;

        public float radius = 10;
        public float DistanceToPlayer = 4;

        private GameObject currentObjettargetd;
        public override void OnStart()
        {
            Physics2D.queriesHitTriggers = false;
            AiSpaceShip = Blackboard.instance.ownerSpaceship;
        }

        /// <summary>
        /// Returns success if an object was found otherwise failure
        /// </summary>
        /// <returns></returns>
        public override TaskStatus OnUpdate()
        {
            List<GameObject> objectsHit = WithinSight();
            Blackboard.instance.ObjectInView = objectsHit;
            Blackboard.instance.RefreshObjectsInView();
            // Return success if an object was found
            return TaskStatus.Success;
        }

        /// <summary>
        /// Determines if the targetObject is within sight of the transform.
        /// </summary>
        private List<GameObject> WithinSight()
        {
            var direction = transform.right - transform.position;
            direction.y = 0;

            Vector2 sphereOrigin =
                AiSpaceShip.Position + new Vector2(AiSpaceShip.transform.right.x, AiSpaceShip.transform.right.y) * DistanceToPlayer;
            
            RaycastHit2D[] hitArray;
            hitArray = Physics2D.CircleCastAll(sphereOrigin, radius, new Vector2(AiSpaceShip.transform.right.x, AiSpaceShip.transform.right.y), 0);
            List<GameObject> objectHit = new List<GameObject>();
            for (int i = 0; i < hitArray.Length; i++)
            {
                if (hitArray[i].transform.CompareTag("Wall") || hitArray[i].transform.CompareTag("WayPoint"))
                {
                    continue;
                }
                objectHit.Add(hitArray[i].transform.gameObject);
            }
            // The hit agent needs to be within view of the current agent
            return LineOfSight(objectHit);
        }

        /// <summary>
        /// Returns true if the target object is within the line of sight.
        /// </summary>
        private List<GameObject> LineOfSight(List<GameObject> objectList)
        {
            List<GameObject> objetInSight = new List<GameObject>();
            for (int i = 0; i < objectList.Count; i++)
            {
                currentObjettargetd = objectList[i];
                
                RaycastHit2D hit;
                
                Vector2 direction =
                    (new Vector2(currentObjettargetd.transform.position.x, currentObjettargetd.transform.position.y) -
                     AiSpaceShip.Position).normalized;
                
                hit = Physics2D.Linecast(AiSpaceShip.Position + direction * 1.2f, objectList[i].transform.position);
                if (hit.transform != null) {
                    if (hit.transform.CompareTag("Wall"))
                    {
                        continue;
                    }
                    if (hit.transform.gameObject == objectList[i] && hit.transform != AiSpaceShip.transform) {
                        objetInSight.Add(hit.transform.gameObject);
                    }
                }
            }
            return objetInSight;
        }

        public override void OnEnd()
        {
            Physics2D.queriesHitTriggers = true;
        }

        public override void OnDrawGizmos()
        {
            base.OnDrawGizmos();
            Gizmos.DrawWireSphere(AiSpaceShip.Position + new Vector2(AiSpaceShip.transform.right.x, AiSpaceShip.transform.right.y) * DistanceToPlayer,radius);
            Gizmos.DrawLine(AiSpaceShip.Position + (new Vector2(currentObjettargetd.transform.position.x, currentObjettargetd.transform.position.y) - AiSpaceShip.Position).normalized, currentObjettargetd.transform.position);
        }
    }
}