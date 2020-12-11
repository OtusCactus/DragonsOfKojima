using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace DragonsOfKojima {
	public class Defend : Action
	{

		public float energyToKeep;
		public SharedVector2 minePosition;

		public override void OnStart()
		{

		}

		public override TaskStatus OnUpdate()
		{

			//for (int i = 0; i < Blackboard.instance.Mines.Count; i++)
			//         {
			//	if(Blackboard.instance.Mines[i] == null)
			//	{
			//		return TaskStatus.Success;
			//	}
			//             bool shouldContinue = ShouldShoot(i);
			//	if (shouldContinue)
			//             {
			//		return TaskStatus.Success;
			//	}
			//         }
			if (Blackboard.instance.isMineInTheWay)
			{
				Vector2 bridge = minePosition.Value - Blackboard.instance.ownerSpaceship.Position;
				Vector2 dir = new Vector2(Mathf.Cos(Blackboard.instance.ownerSpaceship.Orientation * Mathf.Deg2Rad), Mathf.Sin(Blackboard.instance.ownerSpaceship.Orientation * Mathf.Deg2Rad));
				float angle = Vector2.Angle(bridge, dir);
				if (angle <= 5)
				{
					if (Blackboard.instance.ownerSpaceship.Energy >= energyToKeep)
					{
						Blackboard.instance.isMineInTheWay = false;
						Blackboard.instance.ownerSpaceship.Shoot();
					}
				}
			}
			return TaskStatus.Success;
		}
	}
}