using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DoNotModify;

namespace DragonsOfKojima
{
    public class AIController : BaseSpaceShipController
    {
        // Start is called before the first frame update
        public override void Initialize(SpaceShip spaceship, GameData data)
        {
        }

        //public override InputData UpdateInput(SpaceShip spaceship, GameData data)
        //{
        //    float thrust = 0.0f;
        //    float targetOrient = spaceship.Orientation/* + 90.0f*/;
        //    return new InputData(thrust, targetOrient, false, false, false);
        //}
    }
}
