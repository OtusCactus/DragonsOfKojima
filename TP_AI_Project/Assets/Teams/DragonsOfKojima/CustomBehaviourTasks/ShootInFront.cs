using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using DragonsOfKojima;

namespace DragonsOfKojima
{
	[TaskDescription("Shoot in front of ship")]

	
	public class ShootInFront : Action
	{
		public int numbersOfShots = 1;
		private int shotsFired = 1;
		
		public override void OnStart()
		{
			if (shotsFired >= numbersOfShots)
			{
				Blackboard.instance.ownerSpaceship.Shoot();
				shotsFired++;
			}
		}

		public override TaskStatus OnUpdate()
		{
			return TaskStatus.Success;
		}
	}
}
