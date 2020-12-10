using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace DragonsOfKojima
{
	public class HasReachTarget : Action
	{

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
				return TaskStatus.Failure;
			}
            else
			{
				return TaskStatus.Success;
			}
		}
	}
}