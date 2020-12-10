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

		public float distanceForMaxThruster;
		public float distanceForMediumThruster;

		public float angleForMaxThruster;
		public float angleForMediumThruster;

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
			Vector2 direction = new Vector2(Mathf.Cos(Blackboard.instance.ownerSpaceship.Orientation * Mathf.Deg2Rad), Mathf.Sin(Blackboard.instance.ownerSpaceship.Orientation * Mathf.Deg2Rad));
			float angle = Mathf.Abs(Vector2.Angle(direction, Blackboard.instance.ownerSpaceship.Velocity));
			if (canThrust.Value && (distanceWithPoint >= 1.5 || Blackboard.instance.ownerSpaceship.Velocity == Vector2.zero))
            {
				if(distanceWithPoint >= distanceForMaxThruster)
                {
					ThrusterValue = 1;
                }
                else if (angle <= angleForMaxThruster)
                {
					ThrusterValue = 1;
                }
                else
                {
					ThrusterValue = (distanceWithPoint * (0.5f/distanceForMediumThruster)) + (0.5f - (angle * (0.5f/angleForMediumThruster)));
				}
				Blackboard.instance.ChangeThrusterValue(ThrusterValue);
			}
            else
            {
				Blackboard.instance.ChangeThrusterValue(0f);
			}
			return TaskStatus.Success;
		}
	}
}