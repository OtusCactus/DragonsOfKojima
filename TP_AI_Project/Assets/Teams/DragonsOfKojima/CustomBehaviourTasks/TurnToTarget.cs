using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace DragonsOfKojima
{
	public class TurnToTarget : Action
	{
		public SharedObject bestWayPoint;
		DoNotModify.WayPoint targetPoint;

		public override void OnStart()
		{
			targetPoint = bestWayPoint.Value as DoNotModify.WayPoint;
		}

		public override TaskStatus OnUpdate()
		{
			Vector2 vectorToDestinationWithInertia = targetPoint.Position - Blackboard.instance.ownerSpaceship.Position;

			Vector2 dir = vectorToDestinationWithInertia - new Vector2(Blackboard.instance.ownerSpaceship.transform.right.x, Blackboard.instance.ownerSpaceship.transform.right.y);
			Blackboard.instance.angleToTarget = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
			return TaskStatus.Success;
		}
	}
}