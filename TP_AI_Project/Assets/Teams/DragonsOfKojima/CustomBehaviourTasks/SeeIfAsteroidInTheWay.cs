using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityDebug;
using DragonsOfKojima;

namespace DragonsOfKojima
{
	public class SeeIfAsteroidInTheWay : Action
	{

		public SharedGameObject asteroidToEvade;

		public float distanceMinimumToTheAsteroid;

		private GameObject asteroidGameObject;

		public override void OnStart()
		{
			Physics2D.queriesHitTriggers = false;
		}

		public override TaskStatus OnUpdate()
		{
			if (IsAsteroidInTheWay())
			{
				Physics2D.queriesHitTriggers = true;
				return TaskStatus.Success;
			}
			else
			{
				Physics2D.queriesHitTriggers = true;
				return TaskStatus.Failure;
			}
		}


		public bool IsAsteroidInTheWay()
		{
			Debug.DrawLine(Blackboard.instance.ownerSpaceship.Position + Blackboard.instance.ownerSpaceship.Velocity.normalized * 0.2f,
				Blackboard.instance.ownerSpaceship.Position + Blackboard.instance.ownerSpaceship.Velocity.normalized * 2.5f, Color.red);
			

			
			RaycastHit2D hit = Physics2D.Linecast(Blackboard.instance.ownerSpaceship.Position + Blackboard.instance.ownerSpaceship.Velocity.normalized * 0.2f,
				Blackboard.instance.ownerSpaceship.Position + Blackboard.instance.ownerSpaceship.Velocity.normalized * 2.5f);
			
			
			if (hit)
			{
				if (hit.transform.CompareTag("Asteroid"))
				{
					asteroidToEvade.Value = hit.transform.gameObject;
					Blackboard.instance.isAsteroidInTheWay = true;
					return true;
				}
			}

			
			return false;
		}
	}
}
