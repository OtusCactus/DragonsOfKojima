using UnityEngine;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace DragonsOfKojima
{
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
			bestWayPointPosition.Value = new Vector2(allWayPoints[11].transform.position.x, allWayPoints[11].transform.position.y);
			currentPosition.Value = new Vector2(transform.position.x, transform.position.y);
			return TaskStatus.Success;
		}
	}
}