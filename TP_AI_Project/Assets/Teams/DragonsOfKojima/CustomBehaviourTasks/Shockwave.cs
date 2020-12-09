using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using DragonsOfKojima;


namespace DragonsOfKojima
{
	public class Shockwave : Action
	{
		public override void OnStart()
		{
			Blackboard.instance._ownerSpaceShip.FireShockwave();
		}

		public override TaskStatus OnUpdate()
		{
			return TaskStatus.Success;
		}
	}
}