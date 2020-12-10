using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DoNotModify;
using BehaviorDesigner.Runtime;

namespace DragonsOfKojima
{
	public class Blackboard : MonoBehaviour
	{
		public float ShootTimeTolerance = 0.2f;

		public bool TriggerShoot { get; private set; }

		BehaviorTree _behaviorTree = null;
		Animator _stateMachine = null;

		public float ThrusterValue { get; private set; }

		int _owner;
		public SpaceShip ownerSpaceship { get; private set; }
		GameData _latestGameData = null;

		bool _debugCanShootIntersect = false;
		Vector2 _debugIntersection = Vector2.zero;
		float _debugTimeDiff = 0;

		/// <INPUT DATA PROPERTIES>
		public float angleToTarget = 0;

		public List<GameObject> ObjectInView;
		
		public int NumberOfMinesInView { get; private set; }
		public bool IsEnemyInSight { get; private set; }
		public int NumberOfAsteroidsInView { get; private set; }

		public List<GameObject> Mines { get; private set; }
		public List<GameObject> Asteroids{ get; private set; }


		public static Blackboard instance;
		public void Awake()
		{
			_behaviorTree = GetComponent<BehaviorTree>();
			_stateMachine = GetComponent<Animator>();
			Mines = new List<GameObject>();
			Asteroids = new List<GameObject>();
			if (instance == null)
			{
				instance = this;
			}
			else
			{
				Destroy(this);
			}
		}

		public void Initialize(SpaceShip aiShip, GameData gameData)
		{
			_latestGameData = gameData;
			_owner = aiShip.Owner;
			ownerSpaceship = aiShip;
		}

		public void UpdateData(GameData gameData)
		{
			_latestGameData = gameData;

			//_behaviorTree.SetVariableValue("SomeVariable", 1.5f);
			//_stateMachine.SetFloat("SomeVariable", 1.5f);

			TriggerShoot = CanHit(gameData, ShootTimeTolerance);
		}

		public bool CanHit(GameData gameData, float timeTolerance)
		{
			_debugCanShootIntersect = false;

			SpaceShip aiShip = gameData.SpaceShips[_owner];
			SpaceShip enemyShip = gameData.SpaceShips[1 - _owner];

			float shootAngle = Mathf.Deg2Rad * aiShip.Orientation;
			Vector2 shootDir = new Vector2(Mathf.Cos(shootAngle), Mathf.Sin(shootAngle));

			Vector2 intersection;
			bool canIntersect = MathUtils.ComputeIntersection(aiShip.Position, shootDir, enemyShip.Position, enemyShip.Velocity, out intersection);
			if (!canIntersect)
			{
				return false;
			}
			Vector2 aiToI = intersection - aiShip.Position;
			Vector2 enemyToI = intersection - enemyShip.Position;
			if (Vector2.Dot(aiToI, shootDir) <= 0)
				return false;

			float bulletTimeToI = aiToI.magnitude / Bullet.Speed;
			float enemyTimeToI = enemyToI.magnitude / enemyShip.Velocity.magnitude;
			enemyTimeToI *= Vector2.Dot(enemyToI, enemyShip.Velocity) > 0 ? 1 : -1;

			_debugCanShootIntersect = canIntersect;
			_debugIntersection = intersection;

			float timeDiff = bulletTimeToI - enemyTimeToI;
			_debugTimeDiff = timeDiff;
			return Mathf.Abs(timeDiff) < timeTolerance;
		}

		private void OnDrawGizmos()
		{
			if (_debugCanShootIntersect)
			{
				SpaceShip aiShip = _latestGameData.SpaceShips[_owner];
				SpaceShip enemyShip = _latestGameData.SpaceShips[1 - _owner];
				Gizmos.DrawLine(aiShip.Position, _debugIntersection);
				Gizmos.DrawLine(enemyShip.Position, _debugIntersection);
				Gizmos.DrawSphere(_debugIntersection, Mathf.Clamp(Mathf.Abs(_debugTimeDiff), 0.5f, 0));
			}
		}

		public void ChangeThrusterValue(float newValue)
		{
			ThrusterValue = newValue;
		}

		private void AddMine(GameObject mineToAdd)
		{
			if (Mines.Contains(mineToAdd)) return;
			NumberOfMinesInView++;
			Mines.Add(mineToAdd);
		}

		private void RemoveMine(GameObject mineToAdd)
		{
			if (!Mines.Contains(mineToAdd)) return;

			NumberOfMinesInView--;
			Mines.Remove(mineToAdd);
		}
		
		private void AddAsteroid(GameObject asteroidToAdd)
		{
			if (Asteroids.Contains(asteroidToAdd)) return;
			NumberOfAsteroidsInView++;
			Asteroids.Add(asteroidToAdd);
		}

		private void RemoveAsteroid(GameObject asteroidToAdd)
		{
			if (!Asteroids.Contains(asteroidToAdd)) return;

			NumberOfAsteroidsInView--;
			Asteroids.Remove(asteroidToAdd);
		}

		private void SeeEnemyShip(bool value)
		{
			IsEnemyInSight = value;
		}

		public void RefreshObjectsInView()
		{
			RemoveFromList(Asteroids);
			RemoveFromList(Mines);
			
			for (int i = 0; i < ObjectInView.Count; i++)
			{
				if (ObjectInView[i].CompareTag("Asteroid"))
				{
					if (!Asteroids.Contains(ObjectInView[i]));
					{
						AddAsteroid(ObjectInView[i]);
					}
				}
				else if (ObjectInView[i].CompareTag("Mine"))
				{
					if (!Mines.Contains(ObjectInView[i]));
					{
						AddMine(ObjectInView[i]);
					}
				}
			}

			int gameDataId = _latestGameData.SpaceShips.IndexOf(ownerSpaceship);
			
			if (ObjectInView.Contains(_latestGameData.SpaceShips[gameDataId].gameObject))
			{
				SeeEnemyShip(true);
			}
			else
			{
				SeeEnemyShip(false);
			}
		}

		private void RemoveFromList(List<GameObject> gameObjectList)
		{
			if (gameObjectList.Count == 0) return;
			List<GameObject> indexToRemove = new List<GameObject>();
			for (int i = 0; i < gameObjectList.Count; i++)
			{
				if (ObjectInView.Contains(gameObjectList[i])) return;
				indexToRemove.Add(gameObjectList[i]);
			}

			if (gameObjectList == Asteroids)
			{
				for (int i = 0; i < indexToRemove.Count; i++)
				{
					RemoveAsteroid(indexToRemove[i]);
				}
			}
			else if (gameObjectList == Mines)
			{
				for (int i = 0; i < indexToRemove.Count; i++)
				{
					RemoveMine(indexToRemove[i]);
				}
			}
		}
	}
}