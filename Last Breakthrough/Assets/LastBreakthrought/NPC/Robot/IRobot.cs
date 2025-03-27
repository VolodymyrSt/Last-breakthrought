using LastBreakthrought.Logic.ChargingPlace;
using System.Collections.Generic;
using UnityEngine;

namespace LastBreakthrought.NPC.Robot
{
    public interface IRobot
    {
        void OnCreated(BoxCollider wanderingZone, List<RobotChargingPlace> chargingPlaces);
    }
}
