using UnityEngine;

namespace StudioXP.Scripts.Components.Handlers
{
    /// <summary>
    /// Ajoute des fonctionnalités au component Rigidbody.
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    public class RigidbodyHandler : MonoBehaviour
    {
        private Rigidbody2D _rigidbody;
        
        /// <summary>
        /// Gèle ou dégèle la position en X
        /// </summary>
        /// <param name="freeze">Si vrai, la position est gelé, sinon elle est dégelé</param>
        public void FreezePositionX(bool freeze)
        {
            if (freeze)
                _rigidbody.constraints |= RigidbodyConstraints2D.FreezePositionX;
            else
                _rigidbody.constraints &= ~RigidbodyConstraints2D.FreezePositionX;
        }
        
        /// <summary>
        /// Gèle ou dégèle la position en Y
        /// </summary>
        /// <param name="freeze">Si vrai, la position est gelé, sinon elle est dégelé</param>
        public void FreezePositionY(bool freeze)
        {
            if(freeze)
                _rigidbody.constraints |= RigidbodyConstraints2D.FreezePositionY;
            else
                _rigidbody.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
        }
        
        /// <summary>
        /// Gèle ou dégèle la position en Z
        /// </summary>
        /// <param name="freeze">Si vrai, la position est gelé, sinon elle est dégelé</param>
        public void FreezePosition(bool freeze)
        {
            if(freeze)
                _rigidbody.constraints |= RigidbodyConstraints2D.FreezePosition;
            else
                _rigidbody.constraints &= ~RigidbodyConstraints2D.FreezePosition;
        }
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }
    }
}
