using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;


namespace DragonsOfKojima
{
	[TaskDescription("Returns failure if not enough energy, success otherwise.")]
	public class CheckEnergyLevel : Action
	{
		public float energyRequired;

		public override TaskStatus OnUpdate()
		{
			if (Blackboard.instance.ownerSpaceship.Energy >= energyRequired)
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