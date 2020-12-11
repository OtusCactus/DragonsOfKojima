using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace DragonsOfKojima
{
	public class TurnToTarget : Action
	{
		public SharedObject bestWayPoint;
		public SharedVector2 SecondaryPoint;
		public SharedVector2 mine;
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

			float velocityOrientation = 0;
			float diffVelocityToTarget = 0;
			float diffIncreased = 0;
			
			if (Blackboard.instance.isAsteroidInTheWay)
			{
				velocityOrientation = Vector2.SignedAngle(Vector2.right, Blackboard.instance.ownerSpaceship.Velocity);
				diffVelocityToTarget = Vector2.SignedAngle(Blackboard.instance.ownerSpaceship.Velocity, SecondaryPoint.Value - Blackboard.instance.ownerSpaceship.Position);
				diffIncreased = diffVelocityToTarget * 1.5f;
				diffIncreased = Mathf.Clamp(diffIncreased, -179, 179);
				Blackboard.instance.angleToTarget = velocityOrientation + diffIncreased;	
			}
			else if (Blackboard.instance.isMineInTheWay)
            {
				velocityOrientation = Vector2.SignedAngle(Vector2.right, Blackboard.instance.ownerSpaceship.Velocity);
				diffVelocityToTarget = Vector2.SignedAngle(Blackboard.instance.ownerSpaceship.Velocity, mine.Value - Blackboard.instance.ownerSpaceship.Position);
				diffIncreased = diffVelocityToTarget * 1.5f;
				diffIncreased = Mathf.Clamp(diffIncreased, -179, 179);
				Blackboard.instance.angleToTarget = velocityOrientation + diffIncreased;
			}
			else
			{
				velocityOrientation = Vector2.SignedAngle(Vector2.right, Blackboard.instance.ownerSpaceship.Velocity);
				diffVelocityToTarget = Vector2.SignedAngle(Blackboard.instance.ownerSpaceship.Velocity, targetPoint.Position - Blackboard.instance.ownerSpaceship.Position);
				diffIncreased = diffVelocityToTarget * 1.5f;
				diffIncreased = Mathf.Clamp(diffIncreased, -179, 179);
				Blackboard.instance.angleToTarget = velocityOrientation + diffIncreased;
			}
			
			return TaskStatus.Success;
		}
	}
}