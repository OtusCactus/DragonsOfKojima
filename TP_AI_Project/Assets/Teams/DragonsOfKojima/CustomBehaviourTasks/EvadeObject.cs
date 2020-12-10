using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using DoNotModify;

namespace DragonsOfKojima
{
	public class EvadeObject : Action
	{
		public SharedObject CurrentSharedTarget;
		private WayPoint currentTarget;
		public SharedGameObject objectToEvade;
		public SharedVector2 TemporaryTarget;
		
		private CircleCollider2D asteroidCollider;
		private bool mustGoRight = false;
		private bool mustGoLeft = false;
		public float offset = 2;
		public override void OnStart()
		{
			GameObject asteroid = objectToEvade.Value;
			asteroidCollider = asteroid.transform.GetChild(1).GetComponent<CircleCollider2D>();
			
			currentTarget = CurrentSharedTarget.Value as WayPoint;

			
			Vector2 directionToCurrentTarget = currentTarget.Position - Blackboard.instance.ownerSpaceship.Position;

			float scalarVector = Vector2.Dot(directionToCurrentTarget, Blackboard.instance.ownerSpaceship.Velocity);
			if (scalarVector < 0)
			{
				mustGoRight = true;
				mustGoLeft = false;
			}
			else
			{
				mustGoLeft = true;
				mustGoRight = false;
			}

			float colliderRadius = asteroidCollider.radius;
			Vector2 newTarget = Vector2.zero;
			if (mustGoLeft)
			{
				newTarget = new Vector2(asteroid.transform.position.x, asteroid.transform.position.y) +
				                    new Vector2(-Blackboard.instance.ownerSpaceship.Velocity.y, Blackboard.instance.ownerSpaceship.Velocity.x).normalized * (colliderRadius + offset);
			}
			else if (mustGoRight)
			{
				newTarget = new Vector2(asteroid.transform.position.x, asteroid.transform.position.y) +
				                    new Vector2(Blackboard.instance.ownerSpaceship.Velocity.y, -Blackboard.instance.ownerSpaceship.Velocity.x).normalized * (colliderRadius + offset);
			}

			TemporaryTarget.Value = newTarget;
		}

		public override TaskStatus OnUpdate()
		{
			return TaskStatus.Success;
		}
		
	}
}
