using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using DoNotModify;
using DragonsOfKojima;

namespace DragonsOfKojima
{
	
	[TaskDescription("Shoot enemy by predicting his position")]

	public class ShootEnemy : Action
	{

		private Blackboard blackboard;

		public override void OnStart()
		{
			blackboard = Blackboard.instance;
			if (blackboard.TriggerShoot)
			{
				blackboard.ownerSpaceship.Shoot();
			}
		}

		public override TaskStatus OnUpdate()
		{
			return TaskStatus.Success;
		}
	}
}