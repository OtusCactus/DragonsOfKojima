using UnityEngine;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace DragonsOfKojima
{
	[TaskDescription("Find the more optimal waypoint to convert")]
	public class GetBestWaypoint : Action
	{
		public SharedObject bestWayPoint;
		private List<DoNotModify.WayPoint> allWayPoints;
		public SharedFloat distanceWithWaypoint;

		public override void OnStart()
		{
			Blackboard.instance.ChangeThrusterValue(0);
			allWayPoints = DoNotModify.GameManager.Instance.GetGameData().WayPoints;
		}

		public override TaskStatus OnUpdate()
		{
			Vector2 closestPointTransform = Vector2.zero;
			Object closestPoint = null;
			Vector2 lessAnglePointTransform = Vector2.zero;
			Object lessAnglePoint = null;
			Vector2 bestPointTransform = Vector2.zero;
			Object bestPoint = null;
			float minDist = Mathf.Infinity;
			float minAngle = Mathf.Infinity;
			Vector3 currentPos = transform.position;

			foreach (DoNotModify.WayPoint point in allWayPoints)
			{
				if (point.Owner != Blackboard.instance.ownerSpaceship.Owner)
				{
					Vector2 dir = new Vector2(point.Position.x, point.Position.y) - Blackboard.instance.ownerSpaceship.Velocity;
					dir = point.transform.InverseTransformDirection(dir);
					float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
					if (angle < minAngle)
					{
						lessAnglePointTransform = point.Position;
						lessAnglePoint = point;
						minAngle = angle;
					}

					float dist = Vector3.Distance(point.transform.position, currentPos);
					if (dist < minDist)
					{
						closestPointTransform = point.Position;
						closestPoint = point;
						minDist = dist;
					}
				}
				if (lessAnglePoint == closestPoint)
				{
					bestPoint = closestPoint;
					bestPointTransform = closestPointTransform;
				}
				//check rapport angle/distance
				else if (Vector3.Distance(lessAnglePointTransform, currentPos) >= 5)
				{
					bestPoint = closestPoint;
					bestPointTransform = closestPointTransform;
				}
				else
				{
					bestPoint = lessAnglePoint;
					bestPointTransform = lessAnglePointTransform;
				}

			}
			bestWayPoint.Value = bestPoint;
			DoNotModify.WayPoint temp = bestPoint as DoNotModify.WayPoint;
			distanceWithWaypoint.Value = Vector2.Distance(temp.Position, Blackboard.instance.ownerSpaceship.Position);
			return TaskStatus.Success;
		}
	}
}