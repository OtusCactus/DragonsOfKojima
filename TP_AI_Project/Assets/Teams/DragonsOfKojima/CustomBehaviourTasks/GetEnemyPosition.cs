using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using DoNotModify;
using DragonsOfKojima;

namespace DragonsOfKojima
{
	public class GetEnemyPosition : Action
	{

		public SharedVector2 enemyPosition;
		public SharedObject enemy;
		public SpaceShip enemyShip;
	
		public override void OnStart()
		{
			enemyShip = enemy.Value as SpaceShip;
			enemyPosition.Value = enemyShip.Position;
		}

		public override TaskStatus OnUpdate()
		{
			return TaskStatus.Success;
		}
	}
}
