using LastBreakthrought.CrashedShip;
using LastBreakthrought.Infrustructure.Services.EventBus.Signals;
using LastBreakthrought.Logic.ChargingPlace;
using LastBreakthrought.NPC.Robot.States;
using System.Collections.Generic;
using UnityEngine;

namespace LastBreakthrought.NPC.Robot
{
    public class RobotMiner : RobotBase
    {
        private RobotMiningState _robotMiningState;

        public ICrashedShip CrashedShip { get; private set; } = null;

        public override void OnCreated(BoxCollider wanderingZone, List<RobotChargingPlace> chargingPlaces, string id)
        {
            base.OnCreated(wanderingZone, chargingPlaces, id);
            EventBus.SubscribeEvent<OnAllRobotsInformedAboutCrashedShipSignal>(SetCrashedShip);

            _robotMiningState = new RobotMiningState(this, CoroutineRunner, Agent, Animator, Battary, RobotData.GeneralSpeed);


            StateMachine.AddTransition(_robotMiningState, RobotWanderingState, () => CrashedShip == null && IsWanderingState);
            StateMachine.AddTransition(_robotMiningState, RobotFollowingPlayerState, () => CrashedShip == null && IsFollowingState);

            StateMachine.AddTransition(_robotMiningState, RobotRechargingState, () => Battary.NeedToBeRecharged);

            StateMachine.AddTransition(RobotRechargingState, _robotMiningState, () => !Battary.NeedToBeRecharged && CrashedShip != null);
            StateMachine.AddTransition(RobotRechargingState, RobotWanderingState, () => !Battary.NeedToBeRecharged && CrashedShip == null && IsWanderingState);
            StateMachine.AddTransition(RobotRechargingState, RobotFollowingPlayerState, () => !Battary.NeedToBeRecharged && CrashedShip == null && IsFollowingState);

            StateMachine.AddTransition(RobotFollowingPlayerState, RobotRechargingState, () => Battary.NeedToBeRecharged);
            StateMachine.AddTransition(RobotWanderingState, RobotRechargingState, () => Battary.NeedToBeRecharged);

            StateMachine.AddTransition(RobotWanderingState, RobotFollowingPlayerState, () => IsFollowingState);
            StateMachine.AddTransition(RobotFollowingPlayerState, RobotWanderingState, () => IsWanderingState);

            StateMachine.EnterInState(RobotWanderingState);
        }

        public void SetCrashedShip(OnAllRobotsInformedAboutCrashedShipSignal signal) => CrashedShip = signal.CrashedShip;
        public void ClearCrashedShip() => CrashedShip = null;

        public void SetMiningState()
        {
            if (Battary.NeedToBeRecharged || CrashedShip == null) return;

            StateMachine.EnterInState(_robotMiningState);
        }

        private void OnDestroy() => 
            EventBus.UnSubscribeEvent<OnAllRobotsInformedAboutCrashedShipSignal>(SetCrashedShip);
    }
}
