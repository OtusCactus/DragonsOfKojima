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
			if (targetPoint == null)
			{
				return TaskStatus.Failure;
			}
			
			
			float velocityOrientation = Vector2.SignedAngle(Vector2.right, Blackboard.instance.ownerSpaceship.Velocity);
			float diffVelocityToTarget = Vector2.SignedAngle(Blackboard.instance.ownerSpaceship.Velocity, targetPoint.Position - Blackboard.instance.ownerSpaceship.Position);
			float diffIncreased = diffVelocityToTarget * 1.5f;
			diffIncreased = Mathf.Clamp(diffIncreased, -179, 179);
			Blackboard.instance.angleToTarget = velocityOrientation + diffIncreased;
			// Vector2 vectorToDestinationWithInertia = targetPoint.Position - Blackboard.instance.ownerSpaceship.Position;
			//
			// Vector2 dir = vectorToDestinationWithInertia - new Vector2(Blackboard.instance.ownerSpaceship.transform.right.x, Blackboard.instance.ownerSpaceship.transform.right.y);
			// Blackboard.instance.angleToTarget = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
			return TaskStatus.Success;
			

		}
	}
}