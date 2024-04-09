using Sirenix.OdinInspector;
using StudioXP.Scripts.Components.Handlers;
using StudioXP.Scripts.Events;
using StudioXP.Scripts.Game;
using UnityEngine;
using UnityEngine.Events;

namespace StudioXP.Scripts.Components.AI
{
    /// <summary>
    /// Fait marcher un ennemi vers une certaine direction. La direction change lorsque l'ennemi entre en collision
    /// avec une couche bloquante.
    /// </summary>
    [RequireComponent(typeof(ColliderHandler))]
    public class PassiveWalkAI : MonoBehaviour
    {
        [LabelText("Direction de départ")]
        [SerializeField] protected DirectionHorizontal startingDirection;

        [LabelText("Layers bloquants")]
        [SerializeField] protected LayerMask blockingLayers;
        
        [FoldoutGroup("Évènements", false)] 
        [LabelText("Début de la marche")]
        [SerializeField] protected DirectionEvent walkEvent;
        
        [FoldoutGroup("Évènements")] 
        [LabelText("Arrêt de la marche")]
        [SerializeField] protected UnityEvent stopEvent;
        
        protected ColliderHandler ColliderHandler;
        
        protected bool Stopped;
        protected Direction Direction;

        /// <summary>
        /// L'ennemi arrête de marcher
        /// </summary>
        public void Stop()
        {
            if (Stopped) return;
            Stopped = true;
            stopEvent.Invoke();
        }

        /// <summary>
        /// L'ennemi marche vers la direction passé en paramètre
        /// </summary>
        /// <param name="direction"></param>
        public void Walk(Direction direction)
        {
            Stopped = false;
            Direction = direction;
            walkEvent.Invoke(Direction);
        }

        void Awake()
        {
            ColliderHandler = GetComponent<ColliderHandler>();
            Direction = startingDirection == DirectionHorizontal.Left ? Direction.Left : Direction.Right;
            Stopped = false;
        }

        void Update()
        {
            // Écris le comportement de l'Intelligence Artificielle de l'ennemi ici
        }
    }
}


