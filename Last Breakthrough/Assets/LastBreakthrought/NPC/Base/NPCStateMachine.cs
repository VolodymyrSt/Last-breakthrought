using System;
using System.Collections.Generic;

namespace LastBreakthrought.NPC.Base
{
    public class NPCStateMachine
    {
        private INPCState _currentState;
        private Dictionary<Type, List<Transition>> _transitions = new();

        private List<Transition> _currentTransitions = new();
        private List<Transition> _anyTransitions = new();

        private List<Transition> _emptyTransitions = new(0);

        public void Tick()
        {
            var transition = GetTransition();

            if (transition != null)
                EnterInState(transition.To);

            _currentState?.Update();
        }

        public void EnterInState(INPCState state)
        {
            if (state == _currentState) return;

            _currentState?.Exit();
            _currentState = state;

            _transitions.TryGetValue(_currentState.GetType(), out _currentTransitions);

            if (_currentTransitions == null)
                _currentTransitions = _emptyTransitions;

            _currentState.Enter();
        }

        public void AddTransition(INPCState from, INPCState to, Func<bool> predicate)
        {
            if (!_transitions.TryGetValue(from.GetType(), out List<Transition> transition))
            {
                _transitions[from.GetType()] = new List<Transition>();
            }

            _transitions[from.GetType()].Add(new Transition(to, predicate));
        }

        public void AddAnyTransition(INPCState to, Func<bool> predicate) => 
            _anyTransitions.Add(new Transition(to, predicate));

        private Transition GetTransition()
        {
            foreach (var transition in _anyTransitions)
                if (transition.Condition()) 
                    return transition;

            foreach (var transition in _currentTransitions)
                if (transition.Condition())
                    return transition;

            return null;
        }

        private class Transition 
        {
            public INPCState To { get; private set; }
            public Func<bool> Condition { get; private set; }

            public Transition(INPCState to, Func<bool> predicate)
            {
                To = to;
                Condition = predicate;
            }
        }
    }
}
