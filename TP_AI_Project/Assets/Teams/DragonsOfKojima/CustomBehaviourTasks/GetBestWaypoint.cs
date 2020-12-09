using UnityEngine;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace DragonsOfKojima
{
	[TaskDescription("Find the more optimal waypoint to convert")]
	public class GetBestWaypoint : Action
	{
		public SharedVector2 bestWayPointPosition;
		public SharedVector2 currentPosition;
		private List<DoNotModify.WayPoint> allWayPoints;

		public override void OnStart()
		{
			allWayPoints = DoNotModify.GameManager.Instance.GetGameData().WayPoints;
		}

		public override TaskStatus OnUpdate()
		{
			Transform closestPoint = null;
			Transform lessAnglePoint = null;
			Transform bestPoint = null;
			float minDist = Mathf.Infinity;
			float minAngle = Mathf.Infinity;
			Vector3 currentPos = transform.position;
			foreach (DoNotModify.WayPoint point in allWayPoints)
			{
				///TO DO : add owner verification   avec singleton blackboard ou mettre spaceship en variable du blackboard
				//if(point.Owner != spaceship)

				//should be velocity, how to get ??
				Vector3 dir = point.transform.position - transform.position;
				dir = point.transform.InverseTransformDirection(dir);
				float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
				if(angle < minAngle)
                {
					lessAnglePoint = point.transform;
					minAngle = angle;
                }

				float dist = Vector3.Distance(point.transform.position, currentPos);
				if (dist < minDist)
				{
					closestPoint = point.transform;
					minDist = dist;
				}

				if(lessAnglePoint == closestPoint)
                {
					bestPoint = closestPoint;
                }
				//check rapport angle/distance
				else if (Vector3.Distance(lessAnglePoint.transform.position, currentPos) >= 5)
                {
					bestPoint = closestPoint;
                }
                else
                {
					bestPoint = lessAnglePoint;
				}

			}
			bestWayPointPosition.Value = new Vector2(bestPoint.position.x, bestPoint.position.y);
			currentPosition.Value = new Vector2(transform.position.x, transform.position.y);
			return TaskStatus.Success;
		}
	}
}