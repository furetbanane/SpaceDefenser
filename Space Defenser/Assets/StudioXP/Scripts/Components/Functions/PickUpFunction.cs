using Sirenix.OdinInspector;
using StudioXP.Scripts.Components.Objects;
using StudioXP.Scripts.Events;
using UnityEngine;

namespace StudioXP.Scripts.Components.Functions
{
    /// <summary>
    /// Une fonction permet d'exécuter une action spécifique à partir d'un évenement Unity.
    ///
    /// PickUpFunction donne des capacité pour ramasser des objets qui sont au sol et de les lancer. La fonction doit
    /// être idéalement mise sur un objet enfant à un objet joueur et placé à l'avant du joueur.
    ///
    /// Le flip est pris en compte pour modifier l'offset du pickup. On peut y définir la force de lancer.
    /// </summary>
    public class PickUpFunction : SXPMonobehaviour
    {
        [LabelText("Vecteur lancé")]
        [SerializeField] private Vector2 throwVector = Vector2.right;
        [LabelText("Commence retourné")]
        [SerializeField] private bool startFlipped = false;
        
        private PickableObject _pickableObject = null;
        private bool _flip;
        private Vector2 _flippedThrowVector;

        private void Awake()
        {
            _flip = startFlipped;
            _flippedThrowVector = new Vector2(-throwVector.x, throwVector.y);
        }

        /// <summary>
        /// Flip l'offset du pickup
        /// </summary>
        /// <param name="flip"></param>
        public void Flip(bool flip)
        {
            _flip = flip;
        }

        /// <summary>
        /// Défini l'objet se trouvant dans la zone de pickup et le ramasse
        /// </summary>
        /// <param name="info"></param>
        public void ApplyCollision(CollisionInfo info)
        {
            if (_pickableObject) return;
            
            _pickableObject = info.GameObject.GetComponent<PickableObject>();
            if (_pickableObject)
                _pickableObject.Pickup(gameObject);
        }

        /// <summary>
        /// Lance un objet ramassé
        /// </summary>
        public void Throw()
        {
            if (_pickableObject) _pickableObject.Throw(_flip ? _flippedThrowVector : throwVector);
            _pickableObject = null;
            gameObject.SetActive(false);
        }

        /// <summary>
        /// Laisse tomber un objet ramassé
        /// </summary>
        public void Drop()
        {
            if (_pickableObject) _pickableObject.Drop();
            _pickableObject = null;
            gameObject.SetActive(false);
        }
    }
}
