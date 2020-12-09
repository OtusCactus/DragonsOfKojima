using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace DragonsOfKojima
{
	[TaskDescription("Calibrate thruster power")]


	public class ThrusterPower : Action
	{
		public float ThrusterValue;

		public override void OnStart()
		{
			if (ThrusterValue > 1)
			{
				ThrusterValue = 1;
			}
			else if (ThrusterValue < 0)
			{
				ThrusterValue = 0;
			}

			Blackboard.instance.ChangeThrusterValue(ThrusterValue);
		}

		public override TaskStatus OnUpdate()
		{
			return TaskStatus.Success;
		}
	}
}