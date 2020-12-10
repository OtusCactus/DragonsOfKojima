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

					Vector2 dir = new Vector2(point.Position.x, point.Position.y) - Blackboard.instance.ownerSpaceship.Position;
					float angle = Vector2.Angle(Blackboard.instance.ownerSpaceship.Velocity, dir);


					thisPointScore += Mathf.Abs((angle * 2)) * angleRatio;


					float dist = Vector3.Distance(point.Position, Blackboard.instance.ownerSpaceship.Position);

					thisPointScore += (dist * distanceRatio) + smallDistanceMalus;

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
				}
			}

			bestWayPoint.Value = bestNeutral.thisPoint;
			if ((bestNeutral.thisPoint != null && bestNeutral.score < bestEnemy.score) || bestEnemy.thisPoint == null)
            {
				bestWayPoint.Value = bestNeutral.thisPoint;
			}
            else
            {
				bestWayPoint.Value = bestEnemy.thisPoint;

			}
			if (bestWayPoint.Value != null)
			{
				return TaskStatus.Success;
			}
            else
            {
				return TaskStatus.Failure;
			}
		}
	}
}