using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace DragonsOfKojima {
	public class IsFacingTarget : Action
	{

		public float miaou;

		public override void OnStart()
		{

		}

		public override TaskStatus OnUpdate()
		{

			if (Blackboard.instance.ownerSpaceship.Orientation == Blackboard.instance.angleToTarget)
			{
				return TaskStatus.Success;
            }
            else
            {
				return TaskStatus.Failure;
            }
		}
	}
}