using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace StudioXP.Scripts.Components.AI
{
    /// <summary>
    /// Système d'états de contrôle du boss.
    ///
    /// Il y a 8 type d'états possibles. <see cref="State"/>
    /// Chaque type d'état peut active un état aléatoire dans une liste.
    /// Neutral : Le boss n'a pas encore été activé.
    /// Neutral Hurt : Le boss a été attaqué en mode Neutral
    /// Awaken : Le boss a été réveillé. (Est activé après l'état Neutral Hurt)
    /// Awaken Hurt : Le boss a été blessé en mode Idle, Walk ou Attack
    /// Idle : Le joueur est plus loin que la distance d'Idle
    /// Walk : Le boss n'est pas en train d'attaquer
    /// Attack : Le boss effectue une attaque si le joueur est à l'intérieur de la distance d'attaque.
    ///          Par défaut le boss est en mode Walk et attaquera avec une probabilité défini par la chance d'attaque.
    /// Die : Le boss meurt
    /// </summary>
    public class BossStateSystem : MonoBehaviour
    {
        [SerializeField] private float attackDistance = 10;
        [SerializeField] private float idleDistance = 30;
        [SerializeField] private float attackChance = 0.8f;

        [SerializeField] private Animator animator;

        [SerializeField] private List<State> neutralHurtStates;
        [SerializeField] private List<State> awakenHurtStates;
        [SerializeField] private List<State> neutralStates;
        [SerializeField] private List<State> awakenStates;
        [SerializeField] private List<State> idleStates;
        [SerializeField] private List<State> walkStates;
        [SerializeField] private List<State> attackStates;
        [SerializeField] private List<State> dieStates;
        
        
        private Dictionary<List<State>, List<State>> _statePouches;
        private GameObject _player;
        private State _currentState;
        private int _currentHitPoints;
        private bool _activateTimer;
        private float _currentTimer;
        private bool _isAwaken;
        private bool _isDead;
        private bool _isAttacking;
        private static readonly int Loop = Animator.StringToHash("loop");

        /// <summary>
        /// Défini l'état à NeutralHurt ou AwakenHurt
        /// </summary>
        public void Hurt()
        {
            SetRandomState(_isAwaken ? awakenHurtStates : neutralHurtStates);
        }

        /// <summary>
        /// Défini l'état à Awaken si le boss n'est pas déjà réveillé
        /// </summary>
        public void SetAwaken()
        {
            if (_isAwaken) return;
            
            SetRandomState(awakenStates);
            _isAwaken = true;
        }

        /// <summary>
        /// Défini l'état à Die si le boss n'est pas déja mort
        /// </summary>
        public void Kill()
        {
            if (_isDead) return;

            SetRandomState(dieStates);
            _isDead = true;
        }

        /// <summary>
        /// Défini l'état suivant selon l'état courant
        /// </summary>
        private void SetNextState()
        {
            if (!_isAwaken)
            {
                SetRandomState(neutralStates);
            }
            else if (_isDead)
            {
                _currentState.Deactivate();
                Destroy(gameObject);
            }
            else
            {
                var playerPosition = _player.transform.position;
                playerPosition.z = 0;
                var thisPosition = transform.position;
                thisPosition.z = 0;
            
                var playerDistance = (playerPosition - thisPosition).magnitude;
                if (playerDistance <= attackDistance)
                {
                    if (!_isAttacking && attackChance > Random.value)
                    {
                        _isAttacking = true;
                        SetRandomState(attackStates);
                    }
                    else
                    {
                        _isAttacking = false;
                        SetRandomState(walkStates);
                    }
                        
                }
                else if (playerDistance >= idleDistance)
                {
                    _isAttacking = false;
                    SetRandomState(idleStates);
                }
                else
                {
                    _isAttacking = false;
                    SetRandomState(walkStates);
                }
            }
        }

        /// <summary>
        /// Défini un état au hasard dans la liste donné en paramètre
        /// </summary>
        /// <param name="states"></param>
        private void SetRandomState(List<State> states)
        {
            if (states.Count == 0) return;

            var pouch = _statePouches[states];
            SetState(pouch[Random.Range(0, pouch.Count)]);
        }

        /// <summary>
        /// Défini l'état du boss à l'état passé en paramètre
        /// </summary>
        /// <param name="state"></param>
        private void SetState(State state)
        {
            if(_currentState != null)
                _currentState.Deactivate();

            _currentState = state;

            if (_currentState == null) return;
            
            _activateTimer = _currentState.IsLooping;
            _currentTimer = _currentState.Duration;
                
            SetAnimation(state.Clip);
            animator.SetBool(Loop, _currentState.IsLooping);
            animator.Play("Animation", 0, 0.0f);
                
            _currentState.Activate();
        }

        /// <summary>
        /// Change l'animation clip du boss pour celle passé en paramètre
        /// </summary>
        /// <param name="clip"></param>
        private void SetAnimation(AnimationClip clip)
        {
            if (!animator || !clip) return;

            var aoc = new AnimatorOverrideController(animator.runtimeAnimatorController);
            var anims = new List<KeyValuePair<AnimationClip, AnimationClip>> {
                new KeyValuePair<AnimationClip, AnimationClip>(aoc.animationClips[0], clip)
            };
            aoc.ApplyOverrides(anims);
            animator.runtimeAnimatorController = aoc;
        }

        /// <summary>
        /// Remplis une poche de pige avec les états passés en paramètre.
        /// Le paramètre chance de chaque état est utilisé pour décider la quantité à mettre dans la poche de pige.
        /// Un état avec une valeur chance de 10 aura 10 copies d'elle même placé dans la poche de pige.
        /// </summary>
        /// <param name="states"></param>
        private void FillUpPouch(List<State> states)
        {
            var pouch = new List<State>();
            _statePouches[states] = pouch;
            foreach (var state in states)
            {
                for(var i = 0; i < state.Chances; i++)
                    pouch.Add(state);
            }
        }
        
        private void Awake()
        {
            _statePouches = new Dictionary<List<State>, List<State>>();
            FillUpPouch(neutralHurtStates);
            FillUpPouch(awakenHurtStates);
            FillUpPouch(neutralStates);
            FillUpPouch(awakenStates);
            FillUpPouch(idleStates);
            FillUpPouch(walkStates);
            FillUpPouch(attackStates);
            FillUpPouch(dieStates);
            
            _player = GameObject.FindWithTag("Player");
            SetRandomState(neutralStates);
        }
        
        private void Update()
        {
            if (_activateTimer)
            {
                _currentTimer -= Time.deltaTime;
                if (_currentTimer <= 0)
                {
                    SetNextState();
                }
            } 
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Nothing"))
            {
                SetNextState();
            }
        }
    }
}
