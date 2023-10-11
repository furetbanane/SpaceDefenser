using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace StudioXP.Scripts.Components.Handlers
{
    /// <summary>
    /// Inutilisé
    /// </summary>
    public class InputStateHandler : MonoBehaviour
    {
        [LabelText("Etat de départ")]
        [SerializeField] private int startingState = 0;
        [LabelText("Etats")]
        [SerializeField] private List<InputState> states;

        private readonly Dictionary<string, int> _stateIdByName = new Dictionary<string, int>();

        private int _currentState;

        public void SetState(int stateId)
        {
            if (stateId < 0 || stateId >= states.Count)
                return;

            if (_currentState == stateId)
                return;

            states[_currentState].OnStateExit();
            _currentState = stateId;
            
            states[_currentState].OnStateEnter();
        }
        
        public void SetState(string stateName)
        {
            if (!_stateIdByName.ContainsKey(stateName))
                return;
            
            SetState(_stateIdByName[stateName]);
        }

        private void Update()
        {
            states[_currentState].Update();
        }

        private void Awake()
        {
            for (var i = 0; i < states.Count; i++)
            {
                _stateIdByName.Add(states[i].Identifier, i);
            }
        }

        private void Start()
        {
            _currentState = startingState;
            states[_currentState].OnStateEnter();
        }
    }
}
