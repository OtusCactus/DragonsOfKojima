using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace DragonsOfKojima
{
	[TaskDescription("Drop mines")]

	public class DropMine : Action
	{
		public override void OnStart()
		{
			Blackboard.instance.ownerSpaceship.DropMine();
		}

		public override TaskStatus OnUpdate()
		{
			return TaskStatus.Success;
		}
	}
}