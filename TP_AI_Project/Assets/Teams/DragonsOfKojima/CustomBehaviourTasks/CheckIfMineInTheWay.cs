using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace DragonsOfKojima
{
	public class CheckIfMineInTheWay : Action
	{
		public SharedVector2 mineInWay;
		public float distanceToMine;

		public override void OnStart()
		{

		}

		public override TaskStatus OnUpdate()
		{
			for (int i = 0; i < Blackboard.instance.Mines.Count; i++)
			{
				if (Blackboard.instance.Mines[i] == null)
				{
					return TaskStatus.Success;
				}
				bool inTheWay = ShouldShoot(i);
                if (inTheWay)
                {
					return TaskStatus.Success;
				}
			}
			Blackboard.instance.isMineInTheWay = false;
			return TaskStatus.Success;
		}
		private bool ShouldShoot(int index)
		{
			Vector2 bridge = Blackboard.instance.Mines[index].GetComponent<DoNotModify.Mine>().Position - Blackboard.instance.ownerSpaceship.Position;
			float dist = Vector2.Distance(Blackboard.instance.Mines[index].GetComponent<DoNotModify.Mine>().Position, Blackboard.instance.ownerSpaceship.Position);
			float angle = Vector2.Angle(bridge, Blackboard.instance.ownerSpaceship.Velocity);

			if (angle >= -15 && angle <= 15 && dist > 0.2f && dist < distanceToMine)
			{
				Blackboard.instance.isMineInTheWay = true;
				mineInWay.Value = Blackboard.instance.Mines[index].transform.position;
				return true;
			}
			return false;
		}
	}
}