using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace DragonsOfKojima
{
	public class HasReachTarget : Action
	{
		public SharedBool canDropMine;
		public SharedObject bestPoint;
		DoNotModify.WayPoint targetPoint;

		public override void OnStart()
		{
			targetPoint = bestPoint.Value as DoNotModify.WayPoint;
		}

		public override TaskStatus OnUpdate()
		{
            if (targetPoint.Owner == Blackboard.instance.ownerSpaceship.Owner)
			{
				Blackboard.instance.ChangeThrusterValue(0f);
				canDropMine.Value = true;
				return TaskStatus.Failure;
			}
            else
			{
				return TaskStatus.Success;
			}
		}
	}
}