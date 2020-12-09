using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace DragonsOfKojima {
	public class IsFacingTarget : Action
	{

		public SharedVector2 targetOrientation;
		public Vector2 currentForward;

		public override void OnStart()
		{

		}

		public override TaskStatus OnUpdate()
		{
			currentForward = new Vector2(Blackboard.instance.ownerSpaceship.transform.right.x, Blackboard.instance.ownerSpaceship.transform.right.y);
			if (currentForward == targetOrientation.Value)
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