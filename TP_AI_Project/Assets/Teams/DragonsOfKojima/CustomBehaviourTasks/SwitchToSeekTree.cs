using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace DragonsOfKojima
{
	public class SwitchToSeekTree : Action
	{
		public override void OnStart()
		{
			Blackboard.instance.stateMachine.SetTrigger("SeekPoints");
		}

		public override TaskStatus OnUpdate()
		{
			return TaskStatus.Success;
		}
	}
}
