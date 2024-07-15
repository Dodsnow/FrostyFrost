using _Script.ConditionalEffects.Enum;

namespace _Script.ConditionalEffects
{
    public class BleedCondition : BasicCondition
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string IconPath { get; set; }
        public int Priority { get; set; }
        public bool isPositive { get; set; }
        
        
        BleedCondition()
        {
            Name = "Bleed";
            Description = "Bleed";
            IconPath = "Bleed";
            Priority = - 1;
            isPositive = false;
            
        }
    }
}