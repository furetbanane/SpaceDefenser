using Sirenix.OdinInspector;
using UnityEngine;

namespace StudioXP.Scripts.Components.Sprites
{
    public class SpringSprite : MonoBehaviour
    {
        [FoldoutGroup("Sprites", false)]
        [LabelText("Neutre")]
        [Required("Ajoute le sprite du ressort neutre")]
        [AssetSelector(Paths = "Assets/Etudiant/Sprites|Assets/StudioXP/Sprites", ExpandAllMenuItems = false)]
        [SerializeField] private Sprite neutral;
        [FoldoutGroup("Sprites")]
        [LabelText("Compressé")]
        [Required("Ajoute le sprite du ressort compressé")]
        [AssetSelector(Paths = "Assets/Etudiant/Sprites|Assets/StudioXP/Sprites", ExpandAllMenuItems = false)]
        [SerializeField] private Sprite compressed;
        [FoldoutGroup("Sprites")]
        [LabelText("Étendu en bas")]
        [Required("Ajoute le sprite du ressort étendu en bas")]
        [AssetSelector(Paths = "Assets/Etudiant/Sprites|Assets/StudioXP/Sprites", ExpandAllMenuItems = false)]
        [SerializeField] private Sprite extendedBottom;
        [FoldoutGroup("Sprites")]
        [LabelText("Étendu en haut")]
        [Required("Ajoute le sprite du ressort étendu en haut")]
        [AssetSelector(Paths = "Assets/Etudiant/Sprites|Assets/StudioXP/Sprites", ExpandAllMenuItems = false)]
        [SerializeField] private Sprite extendedTop;
    }
}
