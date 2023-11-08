using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace StudioXP.Scripts.Components.Functions
{
    /// <summary>
    /// Une fonction permet d'exécuter une action spécifique à partir d'un évenement Unity.
    ///
    /// BounceFunction permet de faire rebondir un objet dans une direction spécifique. Un multiplicateur sera multiplié
    /// à la force donnée en paramètre à Bounce.
    ///
    /// Le multiplicateur défini dans l'inspecteur est le multiplicateur par défaut. Il est possible de modifier le
    /// multiplicateur via SetBounceMultiplier puis de le remettre à celui par défaut avec ResetBounceMultiplier.
    ///
    /// Si le rigidbody n'est pas défini dans l'inspecteur. Le rigidbody sera recherché dans l'objet ou un de ses enfants.
    /// </summary>
    public class BounceFunction : SXPMonobehaviour
    {
        [FormerlySerializedAs("rigidbody2D")]
        [Required("Ajoute le rigidbody2D qui contrôle cet élément")]
        [SerializeField, LabelText("Rigidbody 2D")] private Rigidbody2D myRigidbody;
        
        [LabelText("Multiplicateur")]
        [MinValue(0)]
        [SerializeField] private float multiplier = 1;
        
        private float _tempMultiplier; 

        /// <summary>
        /// Change le multiplicateur de rebond
        /// </summary>
        /// <param name="bounceMultiplier"></param>
        public void SetBounceMultiplier(float bounceMultiplier)
        {
            _tempMultiplier = bounceMultiplier;
        }

        /// <summary>
        /// Réinitialise le multiplicateur de rebond au multiplicateur par défaut
        /// </summary>
        public void ResetBounceMultiplier()
        {
            _tempMultiplier = multiplier;
        }
        
        /// <summary>
        /// Fait rebondir l'objet selon la force envoyé en paramètre. Celle-ci est multiplié par le multiplicateur
        /// avant d'être appliqué.
        /// </summary>
        /// <param name="force"></param>
        public void Bounce(Vector2 force)
        {
            myRigidbody.velocity = force * _tempMultiplier;
        }

        private void Awake()
        {
            _tempMultiplier = multiplier;
            if(!myRigidbody)
                myRigidbody = GetComponentInChildren<Rigidbody2D>();
        }
    }
}
