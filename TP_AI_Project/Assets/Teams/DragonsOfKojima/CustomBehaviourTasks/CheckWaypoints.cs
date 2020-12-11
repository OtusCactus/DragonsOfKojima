using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using DoNotModify;


namespace DragonsOfKojima
{
	public class CheckWaypoints : Conditional
	{
		List<WayPoint> waypoint = new List<WayPoint>();
		private int numberOfWayPointForUs = 0;
		public float distanceMaxToSwitch = 5;

		public override void OnStart()
		{
			waypoint = Blackboard.instance.latestGameData.WayPoints;
			for (int i = 0; i < waypoint.Count; i++)
			{
				if (waypoint[i].Owner == Blackboard.instance.ownerSpaceship.Owner)
				{
					numberOfWayPointForUs++;
				}
			}
		}

		public override TaskStatus OnUpdate()
		{
			float distance =
				Vector2.Distance(
					Blackboard.instance.latestGameData.SpaceShips[1 - Blackboard.instance.ownerSpaceship.Owner]
						.Position, Blackboard.instance.ownerSpaceship.Position);

			if (numberOfWayPointForUs < waypoint.Count * 0.5f || (distance > distanceMaxToSwitch && numberOfWayPointForUs != waypoint.Count - 1))
			{
				numberOfWayPointForUs = 0;
				return TaskStatus.Success;
			}
			else
			{
				numberOfWayPointForUs = 0;
				return TaskStatus.Failure;
			}
		}
	}
}
