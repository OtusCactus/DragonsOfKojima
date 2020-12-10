using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace DragonsOfKojima
{
	public class TurnToEnemy : Action
	{
		public override void OnStart()
		{

		}

		public override TaskStatus OnUpdate()
		{
			Vector2 vectorToDestinationWithInertia = DoNotModify.GameManager.Instance.spaceShips[1-Blackboard.instance.ownerSpaceship.Owner].Position - Blackboard.instance.ownerSpaceship.Position;

			Vector2 dir = vectorToDestinationWithInertia - new Vector2(Blackboard.instance.ownerSpaceship.transform.right.x, Blackboard.instance.ownerSpaceship.transform.right.y);
			Blackboard.instance.angleToTarget = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
			return TaskStatus.Success;
		}
	}
}