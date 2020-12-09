using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using DragonsOfKojima;

namespace DragonsOfKojima
{
	[TaskDescription("Shoot in front of ship")]

	
	public class ShootInFront : Action
	{
		public override void OnStart()
		{
			Blackboard.instance.ownerSpaceship.Shoot();
		}

		public override TaskStatus OnUpdate()
		{
			return TaskStatus.Success;
		}
	}
}
