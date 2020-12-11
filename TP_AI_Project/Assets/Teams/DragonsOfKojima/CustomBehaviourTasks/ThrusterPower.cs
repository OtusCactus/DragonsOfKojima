using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using DoNotModify;

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
		public SharedVector2 SecondaryPosition;
		Vector2 targetPoint;
		DoNotModify.SpaceShip enemy;

		public override void OnStart()
		{
			WayPoint temp = bestWayPoint.Value as DoNotModify.WayPoint;
			if (temp == null)
			{
				SpaceShip temp2 = bestWayPoint.Value as DoNotModify.SpaceShip;
				targetPoint = temp2.Position;
			}
			else
			{
				targetPoint = temp.Position;
			}
		}

		public override TaskStatus OnUpdate()
		{
			Vector2 direction;
			float angle;
			if (Blackboard.instance.isAsteroidInTheWay)
			{
				targetPoint = SecondaryPosition.Value;
				distanceWithPoint = Vector2.Distance(targetPoint, Blackboard.instance.ownerSpaceship.Position);
				direction = new Vector2(Mathf.Cos(Blackboard.instance.ownerSpaceship.Orientation * Mathf.Deg2Rad), Mathf.Sin(Blackboard.instance.ownerSpaceship.Orientation * Mathf.Deg2Rad));
				angle = Mathf.Abs(Vector2.Angle(direction, Blackboard.instance.ownerSpaceship.Velocity));
			}
			else
			{
				distanceWithPoint = Vector2.Distance(targetPoint, Blackboard.instance.ownerSpaceship.Position);
				direction = new Vector2(Mathf.Cos(Blackboard.instance.ownerSpaceship.Orientation * Mathf.Deg2Rad), Mathf.Sin(Blackboard.instance.ownerSpaceship.Orientation * Mathf.Deg2Rad));
				angle = Mathf.Abs(Vector2.Angle(direction, Blackboard.instance.ownerSpaceship.Velocity));
			}
			
			if (canThrust.Value)
            {
                if (Blackboard.instance.isMineInTheWay)
                {
					Blackboard.instance.ChangeThrusterValue(0.1f);
					return TaskStatus.Success;
				}
                else if (angle <= angleForMaxThruster)
                {
					ThrusterValue = 1;
                }
				//else if (distanceWithPoint >= distanceForMaxThruster)
				//{
				//	ThrusterValue = 1;
				//}
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