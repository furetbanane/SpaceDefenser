using StudioXP.Scripts.Game;
using Unity.VisualScripting;
using Direction = StudioXP.Scripts.Game.Direction;

namespace StudioXP.Scripts.Units
{
    public class ColliderHandlerUnit : Unit
    {
        [DoNotSerialize]
        [PortLabelHidden]
        public ControlOutput OutputTrigger;

        [DoNotSerialize] public ValueOutput IsTrigger;
        [DoNotSerialize] public ValueOutput Type;
        [DoNotSerialize] public ValueOutput Direction;
        
        protected override void Definition()
        {
            OutputTrigger = ControlOutput("OutputTrigger");

            IsTrigger = ValueOutput<bool>("IsTrigger");
            IsTrigger = ValueOutput<CollisionType>("Type");
            IsTrigger = ValueOutput<Direction>("Direction");
        }
    }
}
