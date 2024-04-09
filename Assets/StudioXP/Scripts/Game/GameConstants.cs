using System;
using Sirenix.OdinInspector;

namespace StudioXP.Scripts.Game
{
    [Serializable]
    [Flags]
    public enum DirectionHorizontal
    {
        [LabelText("Gauche")]
        Left = 0x0001,
        [LabelText("Droite")]
        Right = 0x0002
    }

    [Serializable]
    [Flags]
    public enum DirectionVertical
    {
        [LabelText("Haut")]
        Up = 0x0004,
        [LabelText("Bas")]
        Down = 0x0008
    }

    [Serializable]
    [Flags]
    public enum Direction
    {
        [LabelText("Gauche")]
        Left = 0x0001,
        [LabelText("Droite")]
        Right = 0x0002,
        [LabelText("Haut")]
        Up = 0x0004,
        [LabelText("Bas")]
        Down = 0x0008
    }

    public enum Orientation
    {
        Horizontal,
        Vertical
    }
    
    [Serializable]
    [Flags]
    public enum CollisionType
    {
        [LabelText("Entrer")]
        Enter = 0x0040,
        
        [LabelText("Sortir")]
        Exit = 0x0080,
        
        [LabelText("Rester")]
        Stay = 0x0100,
    }

    /// <summary>
    /// Enumération sous la forme de flag. Chaque bit représente un état spécifique.
    /// Contient de l'information sur le type de collision (Enter, Exit, Stay) et la direction à partir de laquelle
    /// a été détecté la collision (Left, Right, Up, Down).
    /// </summary>
    [Serializable]
    [Flags]
    public enum CollisionInfoFilter
    {
        [LabelText("Aucune")]
        None = 0x0,
        
        [LabelText("Entrer")]
        Enter = CollisionType.Enter,
        
        [LabelText("Sortir")]
        Exit = CollisionType.Exit,
        
        [LabelText("Rester")]
        Stay = CollisionType.Stay,
        
        Collision = 0x0010,
        Trigger = 0x0020,
        
        [LabelText("Gauche")]
        Left = Direction.Left,
        
        [LabelText("Droite")]
        Right = Direction.Right,
        
        [LabelText("Dessus")]
        Top = Direction.Up,
        
        [LabelText("Dessous")]
        Bottom = Direction.Down,
        
        [LabelText("Tous les types")]
        AllTypes = Enter | Exit | Stay,
        
        [LabelText("Tous les colliders")]
        AllColliders = Collision | Trigger,
        
        [LabelText("Toutes les directions")]
        AllDirections = Left | Right | Top | Bottom
    }

    [Serializable]
    [Flags]
    public enum CollisionTypeMask
    {
        CollisionTriggerMask = CollisionInfoFilter.Collision | CollisionInfoFilter.Trigger,
        CollisionTypeMask = CollisionInfoFilter.Enter | CollisionInfoFilter.Exit | CollisionInfoFilter.Stay,
        DirectionMask = CollisionInfoFilter.Left | CollisionInfoFilter.Right | CollisionInfoFilter.Top | CollisionInfoFilter.Bottom,
    }

    public static class CollisionTypeExtensions
    {
        public static CollisionInfoFilter SetDirection(this CollisionInfoFilter collisionInfoFilter, Direction direction)
        {
            return (CollisionInfoFilter)((int)collisionInfoFilter | (int)direction);
        }

        public static CollisionInfoFilter GetDirection(this CollisionInfoFilter collisionInfoFilter)
        {
            return collisionInfoFilter & CollisionInfoFilter.AllDirections;
        }
        
        public static CollisionInfoFilter GetCollisionType(this CollisionInfoFilter collisionInfoFilter)
        {
            return collisionInfoFilter & CollisionInfoFilter.AllTypes;
        }
        
        public static CollisionInfoFilter GetColliderType(this CollisionInfoFilter collisionInfoFilter)
        {
            return collisionInfoFilter & CollisionInfoFilter.AllColliders;
        }
    }
    
    public static class Paths
    {
        public const string Prefabs = "Assets/Etudiant/Prefabs|Assets/StudioXP/Prefabs";
        public const string Sprites = "Assets/Etudiant/Sprites|Assets/StudioXP/Sprites";
        public const string Sounds = "Assets/Etudiant/Sounds|Assets/StudioXP/Sounds";
		public const string StudentScenes = "Etudiant/Scenes";
    }
    
    public static class GroupName
    {
        public static string None = "Aucun";
        public static string Any = "Tous";
    }
}