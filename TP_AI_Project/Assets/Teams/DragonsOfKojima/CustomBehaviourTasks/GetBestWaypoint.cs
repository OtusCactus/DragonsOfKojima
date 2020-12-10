using UnityEngine;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace DragonsOfKojima
{
	[TaskDescription("Find the more optimal waypoint to convert")]
	public class GetBestWaypoint : Action
	{
		public float distanceRatio;
		public float smallDistanceMalus;
		public float angleRatio;
		public float mineMalus;
		public float asteroidMalus;
		public float coefficentEnemy;
		public float radiusPointForMineDetection;
		public SharedObject bestWayPoint;
		private List<DoNotModify.WayPoint> allWayPoints;

		struct WayPointAndScore
		{
			public DoNotModify.WayPoint thisPoint;
			public float score;

			public WayPointAndScore(DoNotModify.WayPoint point, float newScore)
			{
				thisPoint = point;
				score = newScore;
			}
		}

		public override void OnStart()
		{
			Blackboard.instance.ChangeThrusterValue(0);
			allWayPoints = DoNotModify.GameManager.Instance.GetGameData().WayPoints;
		}

		public override TaskStatus OnUpdate()
		{
			//Object closestPoint = null;
			//Vector2 lessAnglePointTransform = Vector2.zero;
			//Object lessAnglePoint = null;
			//Object bestPoint = null;


			//float minDist = Mathf.Infinity;
			//float minAngle = Mathf.Infinity;
			//Vector3 currentPos = transform.position;

			WayPointAndScore bestNeutral = new WayPointAndScore(null, 0);
			WayPointAndScore bestEnemy = new WayPointAndScore(null, 0);
			float bestScoreNeutral = Mathf.Infinity;
			float bestScoreEnemy = Mathf.Infinity;

			foreach (DoNotModify.WayPoint point in allWayPoints)
			{
				if (point.Owner != Blackboard.instance.ownerSpaceship.Owner)
				{
					float thisPointScore = 0;
					RaycastHit2D[] objectsHit = Physics2D.LinecastAll(Blackboard.instance.ownerSpaceship.Position, point.Position);
					foreach (RaycastHit2D hit in objectsHit)
					{
                        if (hit.collider.CompareTag("Asteroid")){
							thisPointScore += asteroidMalus;
                        }
					}

					Vector2 dir = new Vector2(point.Position.x, point.Position.y) - Blackboard.instance.ownerSpaceship.Velocity;
                    dir = point.transform.InverseTransformDirection(dir);
                    float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

					thisPointScore += Mathf.Abs((angle * 2)) * angleRatio;

					//if (angle < minAngle)
					//{
					//	lessAnglePointTransform = point.Position;
					//	lessAnglePoint = point;
					//	minAngle = angle;
					//}

					float dist = Vector3.Distance(point.Position, Blackboard.instance.ownerSpaceship.Position);

					thisPointScore += (dist * distanceRatio) + smallDistanceMalus;
					//TODO MALUS MINE
					foreach (DoNotModify.Mine mine in Blackboard.instance.latestGameData.Mines)
					{
						float distanceSqr = (point.Position - mine.Position).sqrMagnitude;
						if (distanceSqr < point.Radius + radiusPointForMineDetection)
						{
							thisPointScore += mineMalus;
						}
					}

					if(point.Owner == -1){
						if (thisPointScore < bestScoreNeutral)
						{
							bestScoreNeutral = thisPointScore;
							bestNeutral.thisPoint = point;
							bestNeutral.score = thisPointScore;
						}
                    }
                    else
                    {
						thisPointScore *= coefficentEnemy;
						if (thisPointScore < bestScoreEnemy)
						{
							bestScoreEnemy = thisPointScore;
							bestEnemy.thisPoint = point;
							bestEnemy.score = thisPointScore;
						}
					}

					//if (dist < minDist)
					//{
					//	closestPoint = point;
					//	minDist = dist;
					//}
				}
			}
			//if (lessAnglePoint == closestPoint)
			//{
			//	bestPoint = closestPoint;
			//}
			////check rapport angle/distance
			//else if (Vector3.Distance(lessAnglePointTransform, currentPos) >= 5)
			//{
			//	bestPoint = closestPoint;
			//}
			//else
			//{
			//	bestPoint = lessAnglePoint;
			//}
			bestWayPoint.Value = bestNeutral.thisPoint;
			if ((bestNeutral.thisPoint != null && bestNeutral.score < bestEnemy.score) || bestEnemy.thisPoint == null)
            {
				bestWayPoint.Value = bestNeutral.thisPoint;
			}
            else
            {
				bestWayPoint.Value = bestEnemy.thisPoint;

			}
			return TaskStatus.Success;
		}
	}
}