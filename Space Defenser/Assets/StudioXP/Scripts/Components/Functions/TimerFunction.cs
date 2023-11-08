using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace StudioXP.Scripts.Components.Functions
{
    /// <summary>
    /// Une fonction permet d'exécuter une action spécifique à partir d'un évenement Unity.
    ///
    /// Permet d'exécuter un évènement après un délai. Il est possible de configuré le timer pour qu'il s'exécute en boucle.
    ///
    /// On peut aussi définir si le timer démarre au début du jeu ou après un appel à la méthode StartTimer.
    /// </summary>
    public class TimerFunction : SXPMonobehaviour
    {
        [LabelText("Nombre de secondes")]
        [SerializeField] private float seconds = 5;
        [LabelText("En boucle")]
        [SerializeField] private bool loop = false;
        [LabelText("Démarrage au lancement du jeu")]
        [SerializeField] private bool startOnGameStart = false;

        [FoldoutGroup("Évènements", false)] 
        [LabelText("Action du Timer")]
        [SerializeField] private UnityEvent onTimerAction;
        [FoldoutGroup("Évènements")] 
        [LabelText("Lancement du Timer")]
        [SerializeField] private UnityEvent onTimerStart;
        [FoldoutGroup("Évènements")] 
        [LabelText("Pause du Timer")]
        [SerializeField] private UnityEvent onTimerPause;
        [FoldoutGroup("Évènements")] 
        [LabelText("Reprise du Timer")]
        [SerializeField] private UnityEvent onTimerResume;
        [FoldoutGroup("Évènements")] 
        [LabelText("Arrêt du Timer")]
        [SerializeField] private UnityEvent onTimerStop;

        private bool _timerRunning;
        private bool _timerIsPaused;
        private float _elapsedTime;
        private float _secondsOverride;

        private void Start()
        {
            if(startOnGameStart)
                StartTimer();
        }

        private void Update()
        {
            if (!_timerRunning || _timerIsPaused) return;

            _elapsedTime -= Time.deltaTime;
            if (!(_elapsedTime <= 0)) return;
            
            onTimerAction.Invoke();
            if (!loop)
            {
                StopTimer();
                return;
            }

            _elapsedTime = _secondsOverride;
        }

        /// <summary>
        /// Démarre le timer pour le nombre de secondes définies dans l'inspecteur.
        /// </summary>
        public void StartTimer()
        {
            StartTimer(seconds);
        }
        
        /// <summary>
        /// Démarre le timer pour le nombre de secondes définies en paramètre.
        /// </summary>
        /// <param name="secondsOverride"></param>
        public void StartTimer(float secondsOverride)
        {
            if (_timerRunning) return;

            _secondsOverride = secondsOverride;
            _elapsedTime = _secondsOverride;
            _timerRunning = true;
            _timerIsPaused = false;
            
            onTimerStart.Invoke();
        }

        /// <summary>
        /// Met le timer en pause.
        /// </summary>
        public void PauseTimer()
        {
            if (!_timerRunning || _timerIsPaused) return;

            _timerIsPaused = true;
            
            onTimerPause.Invoke();
        }

        /// <summary>
        /// Arrête la pause et continue le timer où il était rendu.
        /// </summary>
        public void ResumeTimer()
        {
            if (!_timerIsPaused) return;

            _timerIsPaused = false;
            onTimerResume.Invoke();
        }

        /// <summary>
        /// Arrête le timer. Il recommencera à zéro lorsque StartTimer sera appelé.
        /// </summary>
        public void StopTimer()
        {
            if (!_timerRunning) return;

            _timerRunning = false;
            _timerIsPaused = false;
            onTimerStop.Invoke();
        }
    }
}
