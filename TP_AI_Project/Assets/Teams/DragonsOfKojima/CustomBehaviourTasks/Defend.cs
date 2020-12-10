using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace DragonsOfKojima {
	public class Defend : Action
	{
		public override void OnStart()
		{

		}

		public override TaskStatus OnUpdate()
		{
			
			for (int i = 0; i < Blackboard.instance.Mines.Count; i++)
            {
				if(Blackboard.instance.Mines[i] == null)
				{
					return TaskStatus.Success;
				}
                bool shouldContinue = ShouldShoot(i);
				if (shouldContinue)
                {
					return TaskStatus.Success;
				}
            }
            //if (Blackboard.instance.NumberOfMinesInView >= 1)
            //{
            //	if(Blackboard.instance.NumberOfMinesInView == 1)
            //             {
            //		ShouldShoot(0);

            //	}
            //             else
            //             {
            //		for(int i = 0; i < Blackboard.instance.Mines.Count; i++)
            //                 {
            //			ShouldShoot(i);
            //		}
            //                 //Blackboard.instance.ownerSpaceship.FireShockwave();
            //             }
            //         }
            return TaskStatus.Success;
		}
		private bool ShouldShoot(int index)
		{
			Vector2 bridge = Blackboard.instance.Mines[index].GetComponent<DoNotModify.Mine>().Position - Blackboard.instance.ownerSpaceship.Position;
			//Vector2 dir = new Vector2(Mathf.Cos(Blackboard.instance.ownerSpaceship.Orientation * Mathf.Deg2Rad), Mathf.Sin(Blackboard.instance.ownerSpaceship.Orientation * Mathf.Deg2Rad));
			float dist = Vector2.Distance(Blackboard.instance.Mines[index].GetComponent<DoNotModify.Mine>().Position, Blackboard.instance.ownerSpaceship.Position);
			float angle = Vector2.Angle(bridge, Blackboard.instance.ownerSpaceship.Velocity);

			if (angle >= -10 && angle <= 10 && dist > 1)
			{
				Blackboard.instance.ownerSpaceship.Shoot();
				return true;
			}
			return false;
		}
	}
}