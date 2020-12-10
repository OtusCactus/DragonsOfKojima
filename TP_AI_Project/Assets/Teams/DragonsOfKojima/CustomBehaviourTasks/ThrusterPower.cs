using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace DragonsOfKojima
{
	[TaskDescription("Calibrate thruster power")]


	public class ThrusterPower : Action
	{
		public float ThrusterValue;
		public SharedBool canThrust;
		public float distanceWithPoint;
		public SharedObject bestWayPoint;
		DoNotModify.WayPoint targetPoint;

		public override void OnStart()
		{
			targetPoint = bestWayPoint.Value as DoNotModify.WayPoint;
			//if (ThrusterValue > 1)
			//{
			//	ThrusterValue = 1;
			//}
			//else if (ThrusterValue < 0)
			//{
			//	ThrusterValue = 0;
			//}
		}

		public override TaskStatus OnUpdate()
		{
			distanceWithPoint = Vector2.Distance(targetPoint.Position, Blackboard.instance.ownerSpaceship.Position);
			if (canThrust.Value && (distanceWithPoint >= 1.5 || Blackboard.instance.ownerSpaceship.Velocity == Vector2.zero))
            {

				//ThrusterValue = ((distanceWithPoint * 0.1f) + (Blackboard.instance.angleToTarget * 0.1f));
				Blackboard.instance.ChangeThrusterValue(0.2f);
			}
            else
            {
				Blackboard.instance.ChangeThrusterValue(0f);
			}
			return TaskStatus.Success;
		}
	}
}