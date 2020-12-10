using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace DragonsOfKojima
{
	public class GetEnemy : Action
	{
		public SharedObject enemy;
		
		public override void OnStart()
		{
			enemy.Value = Blackboard.instance.latestGameData.SpaceShips[1-Blackboard.instance.ownerSpaceship.Owner];
		}

		public override TaskStatus OnUpdate()
		{
			return TaskStatus.Success;
		}
	}
}
