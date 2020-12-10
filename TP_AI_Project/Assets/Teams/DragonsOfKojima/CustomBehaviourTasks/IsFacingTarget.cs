using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace DragonsOfKojima {
	public class IsFacingTarget : Action
	{
		public float shipOriantation;

		public override void OnStart()
		{

		}

		public override TaskStatus OnUpdate()
		{
			shipOriantation = Blackboard.instance.ownerSpaceship.Orientation;
			if (shipOriantation > 180)
			{
				shipOriantation -= 360;
			}
			if (shipOriantation == Blackboard.instance.angleToTarget)
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