using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using StudioXP.Scripts.Components.Functions;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace StudioXP.Scripts.UI
{
    public class HealthUI : MonoBehaviour
    {
        public enum HeartType
        {
            Full = 1,
            Half = 2,
            Quarter = 4
        }
        
        [LabelText("Joueur (Tag)")]
        [ValueDropdown("GetTagsView")]
        [SerializeField] private string playerTag;
        
        [FoldoutGroup("Visuel")] [LabelText("Image (Type)")] 
        [SerializeField] private HeartType heartType = HeartType.Half;
        
        [FoldoutGroup("Visuel")] [LabelText("Image (Grandeur)")] 
        [SerializeField] private int heartSize = 16;
        
        [FoldoutGroup("Visuel")]
        [LabelText("Image Coeur (Vide)")]
        [SerializeField] private Sprite emptyHeart;
        
        [FoldoutGroup("Visuel")]
        [LabelText("Image Coeur (1/4)")]
        [ShowIf("heartType", HeartType.Quarter)]
        [SerializeField] private Sprite quarterHeart;
        
        [FoldoutGroup("Visuel")]
        [LabelText("Image Coeur (1/2)")]
        [HideIf("heartType", HeartType.Full)]
        [SerializeField] private Sprite halfHeart;
        
        [FoldoutGroup("Visuel")]
        [LabelText("Image Coeur (3/4)")]
        [ShowIf("heartType", HeartType.Quarter)]
        [SerializeField] private Sprite threeQuarterHeart;
        
        [FoldoutGroup("Visuel")]
        [LabelText("Image Coeur (Plein)")]
        [SerializeField] private Sprite fullHeart;

        private CounterFunction _healthCounter;
        private int _lastHealth;
        private int _heartDivision;
        private int _nbHeart;

        private readonly List<Image> _hearts = new List<Image>();

        private void Awake()
        {
            var textComponent = GetComponent<Text>();
            if(textComponent)
                textComponent.enabled = false;
        }

        void Start()
        {
            _healthCounter = GameObject.FindWithTag(playerTag).GetComponent<CounterFunction>();
            if (ReferenceEquals(_healthCounter, null))
                return;
            
            _lastHealth = _healthCounter.Value;
            _heartDivision = (int)heartType;
            _nbHeart = Mathf.CeilToInt((float) _lastHealth / (float) _heartDivision);

            for (int i = _nbHeart - 1; i >= 0; i--)
            {
                var goHeart = new GameObject();
                goHeart.transform.parent = transform;
                goHeart.name = $"Heart ({i})";
                var imageHeart = goHeart.AddComponent<Image>();
                imageHeart.sprite = fullHeart;
                imageHeart.rectTransform.sizeDelta = Vector2.one * heartSize;
                imageHeart.rectTransform.localPosition = Vector3.left * heartSize * 1.25f * i;
                imageHeart.rectTransform.localScale = Vector3.one;
                _hearts.Add(imageHeart);
            }
            
            UpdateSprites();
        }
        
        void Update()
        {
            if (ReferenceEquals(_healthCounter, null))
                return;
            
            if (_lastHealth == _healthCounter.Value) return;
            
            _lastHealth = _healthCounter.Value;
            UpdateSprites();
        }

        private void UpdateSprites()
        {
            if (ReferenceEquals(_healthCounter, null))
                return;
            
            int currentMaxDiv = 0;
            int currentHealth = _healthCounter.Value;
            
            for (int i = 0; i < _nbHeart; i++)
            {
                currentMaxDiv += _heartDivision;
                if (currentHealth >= currentMaxDiv)
                {
                    _hearts[i].sprite = fullHeart;
                    continue;
                }
                
                if (currentHealth < currentMaxDiv - _heartDivision)
                {
                    _hearts[i].sprite = emptyHeart;
                    continue;
                }

                int localDiv = currentHealth % _heartDivision;

                if (localDiv == 0)
                {
                    _hearts[i].sprite = emptyHeart;
                    continue;
                }

                switch (heartType)
                {
                    case HeartType.Half:
                    {
                        _hearts[i].sprite = halfHeart;
                        continue;
                    }
                    case HeartType.Quarter:
                        _hearts[i].sprite = localDiv switch
                        {
                            1 => quarterHeart,
                            2 => halfHeart,
                            _ => threeQuarterHeart
                        };
                        break;
                    case HeartType.Full:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
        
#if UNITY_EDITOR
        private IEnumerable GetTagsView()
        {
            return UnityEditorInternal.InternalEditorUtility.tags;
        }
#endif
    }
}
