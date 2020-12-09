using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityPhysics;
using DoNotModify;

namespace DragonsOfKojima
{
	public class CheckRadiusForObject : Action
	{

		public GameObject objectToCheck;
		private SpaceShip AISpaceShip;
		public float radius;
		public float TimerMax;
		private float timer;

		public override void OnStart()
		{
			AISpaceShip = Blackboard.instance.ownerSpaceship;
			timer = TimerMax;
		}

		public override TaskStatus OnUpdate()
		{
			//maybe faire un sphere cast pour détecter le nombre d'objet/les mines?
			float distance =
				(AISpaceShip.Position -
				 new Vector2(objectToCheck.transform.position.x, objectToCheck.transform.position.y)).sqrMagnitude;
			if (distance < radius * radius)
			{
				timer -= Time.deltaTime;
				if (timer <= 0)
				{
					timer = TimerMax;
					return TaskStatus.Success;
				}

				return TaskStatus.Running;
			}
			else
			{
				timer = TimerMax;
				return TaskStatus.Failure;
			}
		}

		public override void OnDrawGizmos()
		{
			base.OnDrawGizmos();
			Gizmos.DrawWireSphere(AISpaceShip.Position, radius);
		}
	}
}