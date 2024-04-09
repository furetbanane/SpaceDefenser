using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using StudioXP.Scripts.Components.Common;
using StudioXP.Scripts.Events;
using StudioXP.Scripts.Game;
using StudioXP.Scripts.Utils;
using UnityEngine;

namespace StudioXP.Scripts.Components.Handlers
{
    /// <summary>
    /// Fonctionnalités supplémentaires pour un component de type Collider
    ///
    /// Exécute des UnityEvent selon des filtres de collision.
    ///
    /// Le filtre de type Collisions permet de filtre les collisions selon:
    ///     - Trigger ou Collision
    ///     - Enter, Stay ou Exit
    ///     - Provenance de la collision : Dessus, Gauche, Droite, Dessous
    ///
    /// Le filtre de type Groupes permet de limiter la détection à un groupe spécifique défini par <see cref="GroupList"/>
    ///
    /// On peut utiliser <see cref="SetHorizontalFacing"/> et <see cref="SetVerticalFacing"/> pour changer
    /// l'offset du collider de sens. Il faut d'abord appeler <see cref="SetDefaultFacing"/> pour définir la direction
    /// par défaut de l'objet au début du jeu.
    ///
    /// Pour fonctionner, le collider doit être placer sur un objet enfant de l'objet principal. Si la position en
    /// x est 2.5 par défaut, lorsqu'on change de direction, la position en x deviendra -2.5.
    ///
    /// </summary>
    public class ColliderHandler : MonoBehaviour
    {
        private Collider2D _collider;
        private Rigidbody2D _rigidbody;

        [LabelText("Tolérance")]
        [SerializeField] private float tolerance = 0.02f;
        
        [LabelText("Évènements")]
        [SerializeField] private List<CollisionFilterEvent> collisionEvents;

        private DirectionHorizontal _defaultHorizontalFacing = DirectionHorizontal.Left;
        private DirectionVertical _defaultVerticalFacing = DirectionVertical.Up;
        
        private DirectionHorizontal _currentHorizontalFacing = DirectionHorizontal.Left;
        private DirectionVertical _currentVerticalFacing = DirectionVertical.Up;

        private Vector2 _defaultOffset;

        private static readonly Dictionary<GameObject, GroupFilter>
            GroupFilterCache = new Dictionary<GameObject, GroupFilter>();

        /// <summary>
        /// Défini la direction par défaut de l'objet. Voir les explication dans <see cref="ColliderHandler"/>
        /// </summary>
        /// <param name="horizontal">Direction horizontale par défaut</param>
        /// <param name="vertical">Direction verticale par défaut</param>
        public void SetDefaultFacing(DirectionHorizontal horizontal, DirectionVertical vertical)
        {
            _defaultHorizontalFacing = horizontal;
            _defaultVerticalFacing = vertical;
            _currentHorizontalFacing = horizontal;
            _currentVerticalFacing = vertical;
        }

        /// <summary>
        /// Change la direction horizontale du collider
        /// </summary>
        /// <param name="horizontal"></param>
        public void SetHorizontalFacing(DirectionHorizontal horizontal)
        {
            if (_currentHorizontalFacing == horizontal) return;
            
            _currentHorizontalFacing = horizontal;
            var offset = _defaultOffset;
            if (horizontal != _defaultHorizontalFacing)
                offset.x = -_defaultOffset.x;

            var goTransform = transform;
            Vector2 position = goTransform.position;
            position.x -= offset.x * 2;
            goTransform.position = position;
            _collider.offset = offset;
        }

        /// <summary>
        /// Change la direction verticale du collider
        /// </summary>
        /// <param name="vertical"></param>
        public void SetVerticalFacing(DirectionVertical vertical)
        {
            if (_currentVerticalFacing == vertical) return;
            
            _currentVerticalFacing = vertical;
            var offset = _defaultOffset;
            if (vertical != _defaultVerticalFacing)
                offset.y = -_defaultOffset.y;

            var goTransform = transform;
            Vector2 position = goTransform.position;
            position.y -= offset.y * 2;
            goTransform.position = position;
            _collider.offset = offset;
        }

        /// <summary>
        /// Vérifie si l'objet touche une des couches suivantes:
        /// <see cref="GameConfiguration.Instance.WalkableLayers"/>
        /// <see cref="GameConfiguration.Instance.WallLayers"/>
        ///
        /// Si la direction est vers le bas, la vérification sera faite sur la couche
        /// <see cref="GameConfiguration.Instance.WalkableLayers"/>
        /// Sinon la vérification sera faite sur la couche <see cref="GameConfiguration.Instance.WallLayers"/>
        /// </summary>
        /// <param name="direction">Direction vers laquelle on vérifie la collision</param>
        /// <returns>Vrai si l'objet touche le terrain dans la direction spécifiée</returns>
        public bool IsTouchingTerrain(Direction direction)
        {
            return IsTouching(direction, direction == Direction.Down ? GameConfiguration.Instance.WalkableLayers : 
                                                                       GameConfiguration.Instance.WallLayers);
        }

        /// <summary>
        /// Vérifie si l'objet touche une couche spécifique dans la direction spécifiée.
        /// </summary>
        /// <param name="direction">Direction vers laquelle on vérifie la collision</param>
        /// <param name="layer">La couche à tester</param>
        /// <returns>Vrai si l'objet touche la couche dans la direction spécifiée</returns>
        public bool IsTouching(Direction direction, LayerMask layer)
        {
            return !ReferenceEquals(GetTouching(direction, layer), null);
        }

        /// <summary>
        /// Récupère le premier objet touché dans la direction spécifié. Filtre les collision selon le masque de couche
        /// envoyé en paramètre.
        /// </summary>
        /// <param name="direction">Direction à tester</param>
        /// <param name="layer">Couches à tester</param>
        /// <returns>Le premier objet touché, null si aucun objet n'est touché</returns>
        public GameObject GetTouching(Direction direction, LayerMask layer)
        {
            var dirVector = GetDirection(direction);
            var isHorizontal = direction == Direction.Left || direction == Direction.Right;
            
            var bounds = _collider.bounds;
            var result = Physics2DUtil.GetBoxCast(bounds.center,
                new Vector2(isHorizontal ? tolerance : (bounds.size.x - tolerance),
                    isHorizontal ? (bounds.size.y - tolerance) : tolerance),
                dirVector, isHorizontal ? bounds.extents.x : bounds.extents.y, layer);

            if (ReferenceEquals(result, null)) return null;
            if (ReferenceEquals(_rigidbody, null)) return result.gameObject;

            while (!ReferenceEquals(result, null) && result.attachedRigidbody == _rigidbody)
                result = Physics2DUtil.GetNextResult();

            return ReferenceEquals(result, null) ? null : result.gameObject;
        }

        /// <summary>
        /// Récupère tous les objets touchés dans la direction spécifié. Filtre les collision selon le masque de couche
        /// envoyé en paramètre.
        /// </summary>
        /// <param name="direction">Direction à tester</param>
        /// <param name="layer">Couches à tester</param>
        /// <returns>Tous les objets touchés</returns>
        public GameObject[] GetTouchingAll(LayerMask layer)
        {
            var goList = new List<GameObject>();
            var bounds = _collider.bounds;
            var result = Physics2DUtil.GetBoxCast(bounds.center,
                new Vector2(bounds.size.x - tolerance, tolerance),
                Vector2.up, bounds.extents.y, layer);

            while (result != null)
            {
                goList.Add(result.gameObject);
                result = Physics2DUtil.GetNextResult();
            }

            return goList.ToArray();
        }

        private static GroupFilter GetGroupFilter(GameObject go)
        {
            if (GroupFilterCache.ContainsKey(go)) return GroupFilterCache[go];
            
            var groupFilter = go.GetComponent<GroupList>()?.GroupFilter ?? GroupFilter.None;
            GroupFilterCache.Add(go, groupFilter);

            return GroupFilterCache[go];
        }

        private bool MatchGroupFilter(GameObject other, CollisionFilterEvent filterEvent)
        {
            return filterEvent.FilterGroup.Match(GetGroupFilter(other));
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (collisionEvents.Count == 0) return;

            foreach (var e in collisionEvents)
            {
                if (e.Event.GetPersistentEventCount() == 0) return;
                OnCollisionEnter2D(other, e);
            }
        }

        private void OnCollisionEnter2D(Collision2D other, CollisionFilterEvent filterEvent)
        {
            //var filterTag = filterEvent.FilterTag;
            //if (!filterTag.Equals(GroupName.Any) && !filterTag.Equals(other.gameObject.tag)) return;
            if (!MatchGroupFilter(other.gameObject, filterEvent)) return;
            
            var filter = filterEvent.Filter;
            
            if (other.otherCollider.gameObject != gameObject) return;
            if ((filter & CollisionInfoFilter.Collision) == 0) return;
            if ((filter & CollisionInfoFilter.Enter) == 0) return;
            
            Invoke(other.collider, CollisionInfoFilter.Collision | CollisionInfoFilter.Enter, filter, filterEvent.Event);
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            if (collisionEvents.Count == 0) return;

            foreach (var e in collisionEvents)
            {
                if (e.Event.GetPersistentEventCount() == 0) return;
                OnCollisionStay2D(other, e);
            }
        }
        
        private void OnCollisionStay2D(Collision2D other, CollisionFilterEvent filterEvent)
        {
            //var filterTag = filterEvent.FilterTag;
            //if (!filterTag.Equals(GroupName.Any) && !filterTag.Equals(other.gameObject.tag)) return;
            if (!MatchGroupFilter(other.gameObject, filterEvent)) return;
            
            var filter = filterEvent.Filter;
            
            if (other.otherCollider.gameObject != gameObject) return;
            if ((filter & CollisionInfoFilter.Collision) == 0) return;
            if ((filter & CollisionInfoFilter.Stay) == 0) return;
            
            Invoke(other.collider, CollisionInfoFilter.Collision | CollisionInfoFilter.Stay, filter, filterEvent.Event);
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (collisionEvents.Count == 0) return;

            foreach (var e in collisionEvents)
            {
                if (e.Event.GetPersistentEventCount() == 0) return;
                OnCollisionExit2D(other, e);
            }
        }
        
        private void OnCollisionExit2D(Collision2D other, CollisionFilterEvent filterEvent)
        {
            //var filterTag = filterEvent.FilterTag;
            //if (!filterTag.Equals(GroupName.Any) && !filterTag.Equals(other.gameObject.tag)) return;
            if (!MatchGroupFilter(other.gameObject, filterEvent)) return;
            
            var filter = filterEvent.Filter;
            
            if (other.otherCollider.gameObject != gameObject) return;
            if ((filter & CollisionInfoFilter.Collision) == 0) return;
            if ((filter & CollisionInfoFilter.Exit) == 0) return;
            
            Invoke(other.collider, CollisionInfoFilter.Collision | CollisionInfoFilter.Exit, filter, filterEvent.Event);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (collisionEvents.Count == 0) return;

            foreach (var e in collisionEvents)
            {
                if (e.Event.GetPersistentEventCount() == 0) return;
                OnTriggerEnter2D(other, e);
            }
        }
        
        private void OnTriggerEnter2D(Collider2D other, CollisionFilterEvent filterEvent)
        {
            //var filterTag = filterEvent.FilterTag;
            //if (!filterTag.Equals(GroupName.Any) && !filterTag.Equals(other.gameObject.tag)) return;
            if (!MatchGroupFilter(other.gameObject, filterEvent)) return;
            
            var filter = filterEvent.Filter;
            
            if (!other.IsTouching(_collider)) return;
            if ((filter & CollisionInfoFilter.Trigger) == 0) return;
            if ((filter & CollisionInfoFilter.Enter) == 0) return;
            
            Invoke(other, CollisionInfoFilter.Trigger | CollisionInfoFilter.Enter, filter, filterEvent.Event);
        }
        
        private void OnTriggerStay2D(Collider2D other)
        {
            if (collisionEvents.Count == 0) return;

            foreach (var e in collisionEvents)
            {
                if (e.Event.GetPersistentEventCount() == 0) return;
                OnTriggerStay2D(other, e);
            }
        }
        
        private void OnTriggerStay2D(Collider2D other, CollisionFilterEvent filterEvent)
        {
            //var filterTag = filterEvent.FilterTag;
            //if (!filterTag.Equals(GroupName.Any) && !filterTag.Equals(other.gameObject.tag)) return;
            if (!MatchGroupFilter(other.gameObject, filterEvent)) return;
            
            var filter = filterEvent.Filter;
            
            if (!other.IsTouching(_collider)) return;
            if ((filter & CollisionInfoFilter.Trigger) == 0) return;
            if ((filter & CollisionInfoFilter.Stay) == 0) return;
            
            Invoke(other, CollisionInfoFilter.Trigger | CollisionInfoFilter.Stay, filter, filterEvent.Event);
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if (collisionEvents.Count == 0) return;

            foreach (var e in collisionEvents)
            {
                if (e.Event.GetPersistentEventCount() == 0) return;
                OnTriggerExit2D(other, e);
            }
        }

        private void OnTriggerExit2D(Collider2D other, CollisionFilterEvent filterEvent)
        {
            //var filterTag = filterEvent.FilterTag;
            //if (!filterTag.Equals(GroupName.Any) && !filterTag.Equals(other.gameObject.tag)) return;
            if (!MatchGroupFilter(other.gameObject, filterEvent)) return;
            
            var filter = filterEvent.Filter;
            
            if ((filter & CollisionInfoFilter.Trigger) == 0) return;
            if ((filter & CollisionInfoFilter.Exit) == 0) return;
            
            Invoke(other, CollisionInfoFilter.Trigger | CollisionInfoFilter.Exit, filter, filterEvent.Event);
        }

        private void Invoke(Collider2D other, CollisionInfoFilter infoInfoFilter, CollisionInfoFilter filter, ColliderEventInfoEvent collisionEvent)
        {
            infoInfoFilter = infoInfoFilter.SetDirection(GetDirection(other));
            
            if ((filter.GetDirection() & infoInfoFilter.GetDirection()) == 0) return;
            if ((filter.GetColliderType() & infoInfoFilter.GetColliderType()) == 0) return;
            if ((filter.GetCollisionType() & infoInfoFilter.GetCollisionType()) == 0) return;

            collisionEvent.Invoke(new CollisionInfo(other.gameObject, infoInfoFilter));
        }

        private Direction GetDirection(Collider2D other)
        {
            var selfBounds = _collider.bounds;
            var otherBounds = other.bounds;

            Vector2 selfCenter = selfBounds.center;
            Vector2 otherCenter = otherBounds.center;

            var centerDistance = otherCenter - selfCenter;
            var percentDistance = centerDistance;
            percentDistance.x = Mathf.Abs(percentDistance.x);
            percentDistance.y = Mathf.Abs(percentDistance.y);
            percentDistance /= (otherBounds.extents + selfBounds.extents);

            if (percentDistance.x > percentDistance.y)
                return otherCenter.x < selfCenter.x ? Direction.Left : Direction.Right;

            return otherCenter.y < selfCenter.y ? Direction.Down : Direction.Up;
        }
        
        private Vector2 GetDirection(Direction direction)
        {
            return direction switch
            {
                Direction.Up => Vector2.up,
                Direction.Down => Vector2.down,
                Direction.Left => Vector2.left,
                Direction.Right => Vector2.right,
                _ => throw new ArgumentException("Invalid direction value")
            };
        }

        private void SetCollider()
        {
            if (_collider) return;
            _collider = GetComponent<Collider2D>();
            if (!_collider) return;
            _rigidbody = _collider.attachedRigidbody;
            Debug.Assert(_collider != null);
            _defaultOffset = _collider.offset;
        }

        private void Awake()
        {
            SetCollider();
        }

        private void Start()
        {
            SetCollider();
        }
    }
}
