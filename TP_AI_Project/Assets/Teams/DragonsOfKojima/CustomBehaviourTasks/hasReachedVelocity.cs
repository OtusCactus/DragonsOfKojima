using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace DragonsOfKojima
{
	public class hasReachedVelocity : Action
	{

		public SharedFloat distanceWithWaypoint;
		public float maxVelocityWithDistance;
		public SharedBool canThrust;

		public override void OnStart()
		{
			//maxVelocityWithDistance = Mathf.Pow(Mathf.Clamp((distanceWithWaypoint.Value / 4) + Blackboard.instance.ownerSpaceship.Velocity.sqrMagnitude, 0, Blackboard.instance.ownerSpaceship.SpeedMax),2);
			maxVelocityWithDistance = Mathf.Pow(Mathf.Clamp((distanceWithWaypoint.Value / 4), 0, Blackboard.instance.ownerSpaceship.SpeedMax),2);
		}

		public override TaskStatus OnUpdate()
		{
			//check if velocity reached depending distance
			if (Blackboard.instance.ownerSpaceship.Velocity.sqrMagnitude >= maxVelocityWithDistance)
			{
				canThrust.Value = true;
				Blackboard.instance.ChangeThrusterValue(0f);
				return TaskStatus.Success;
			}
			else
			{
				return TaskStatus.Failure;
			}
		}
	}
}