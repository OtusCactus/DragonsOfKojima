using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using DoNotModify;
using DragonsOfKojima;

namespace DragonsOfKojima
{
	public class IsEnemyInRange : Conditional
	{
		public float radius = 0;


		public override TaskStatus OnUpdate()
		{
			SpaceShip enemyShip = GameManager.Instance.spaceShips[1 - Blackboard.instance.ownerSpaceship.Owner];

			float DistanceToEnemy = Vector2.Distance(enemyShip.Position, Blackboard.instance.ownerSpaceship.Position);

			if (DistanceToEnemy < radius)
			{
				return TaskStatus.Success;
			}
			else
			{
				return TaskStatus.Failure;
			}
		}

		public override void OnDrawGizmos()
		{
			Gizmos.DrawWireSphere(transform.position, radius);
		}
	}
}