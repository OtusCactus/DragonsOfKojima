using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace DragonsOfKojima {
	public class IsFacingTarget : Action
	{
		public SharedFloat shipOriantation;
		public SharedBool canThrust;

		public override void OnStart()
		{

		}

		public override TaskStatus OnUpdate()
		{
			shipOriantation.Value = Blackboard.instance.ownerSpaceship.Orientation;
			if (shipOriantation.Value > 180)
			{
				shipOriantation.Value -= 360;
			}
			if (shipOriantation.Value >= Blackboard.instance.angleToTarget - 5 && shipOriantation.Value <= Blackboard.instance.angleToTarget + 5)
			{
				//return TaskStatus.Success;
				canThrust.Value = true;
			}
            else
            {
				canThrust.Value = false;
			}
			//        else
			//        {
			//return TaskStatus.Failure;
			//        }
			return TaskStatus.Success;
		}
	}
}