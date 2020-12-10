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
			if (Blackboard.instance.NumberOfMinesInView >= 1)
			{
				if(Blackboard.instance.NumberOfMinesInView == 1)
                {
					Vector2 bridge = Blackboard.instance.Mines[0].GetComponent<DoNotModify.Mine>().Position - Blackboard.instance.ownerSpaceship.Position;
					Vector2 dir = new Vector2(Mathf.Cos(Blackboard.instance.ownerSpaceship.Orientation * Mathf.Deg2Rad), Mathf.Sin(Blackboard.instance.ownerSpaceship.Orientation * Mathf.Deg2Rad));
					float dist = Vector2.Distance(Blackboard.instance.Mines[0].GetComponent<DoNotModify.Mine>().Position, Blackboard.instance.ownerSpaceship.Position);
					if (Vector2.Angle(bridge, dir) == 0 && dist > 1)
                    {
						Debug.Log("miaou");
						Blackboard.instance.ownerSpaceship.Shoot();
					}
                }
                else
                {
                    //Blackboard.instance.ownerSpaceship.FireShockwave();
                }

            }
			return TaskStatus.Success;
		}
	}
}