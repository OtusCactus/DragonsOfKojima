using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace DragonsOfKojima
{
	public class HasReachTarget : Action
	{
		public SharedBool canDropMine;
		public SharedObject bestPoint;
		public SharedVector2 SecondaryPosition;
		DoNotModify.WayPoint targetPoint;

		public override void OnStart()
		{
			targetPoint = bestPoint.Value as DoNotModify.WayPoint;
		}

		public override TaskStatus OnUpdate()
		{
			if (Blackboard.instance.isAsteroidInTheWay)
			{
				float distance = Vector2.Distance(SecondaryPosition.Value, Blackboard.instance.ownerSpaceship.Position);
				if (distance < 1)
				{
					Blackboard.instance.isAsteroidInTheWay = false;
					return TaskStatus.Failure;
				}
				else
				{
					return TaskStatus.Success;
				}
			}
            else if (targetPoint.Owner == Blackboard.instance.ownerSpaceship.Owner)
			{
				Blackboard.instance.ChangeThrusterValue(0f);
				canDropMine.Value = true;
				return TaskStatus.Failure;
			}
            else
			{
				canDropMine.Value = false;
				return TaskStatus.Success;
			}
		}
	}
}