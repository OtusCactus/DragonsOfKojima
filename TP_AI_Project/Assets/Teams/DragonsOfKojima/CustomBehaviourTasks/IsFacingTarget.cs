using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace DragonsOfKojima {
	public class IsFacingTarget : Action
	{
		public float shipOriantation;
		public SharedBool canThrust;

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
			if (shipOriantation >= Blackboard.instance.angleToTarget - 5 && shipOriantation <= Blackboard.instance.angleToTarget + 5)
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