using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DoNotModify;

namespace DragonsOfKojima
{
    public class AIController : BaseSpaceShipController
    {
        
        private Blackboard _blackboard;
        
        // Start is called before the first frame update
        public override void Initialize(SpaceShip spaceship, GameData data)
        {
            _blackboard = GetComponent<Blackboard>();
            _blackboard.Initialize(spaceship, data);
        }

        public override InputData UpdateInput(SpaceShip spaceship, GameData data)
        {
            float thrust = _blackboard.ThrusterValue;
            float targetOrient = spaceship.Orientation + Blackboard.instance.angleToTarget;

            _blackboard.UpdateData(data);

            return new InputData(thrust, Blackboard.instance.angleToTarget, false, false, false);
        }
    }
}
