using System;
using System.Collections.Generic;
using StudioXP.Scripts.Events;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace StudioXP.Scripts.Components.Handlers
{
    /// <summary>
    /// Inutilisé
    /// </summary>
    [Serializable]
    public class InputState
    {
        [LabelText("Identifieur")]
        [SerializeField] private string identifier;
        [LabelText("A l'entrée de l'état")]
        [SerializeField] private UnityEvent onStateEnter;
        [LabelText("A la sortie de l'état")]
        [SerializeField] private UnityEvent onStateExit;
        [LabelText("Evenements d'input")]
        [SerializeField] private List<InputEvent> inputEvents = new List<InputEvent>();

        public string Identifier => identifier;

        public void Update()
        {
            foreach (var inputEvent in inputEvents)
            {
                inputEvent.Update();
            }
        }

        public void OnStateEnter()
        {
            onStateEnter.Invoke();
        }

        public void OnStateExit()
        {
            onStateExit.Invoke();
        }
    }
}
