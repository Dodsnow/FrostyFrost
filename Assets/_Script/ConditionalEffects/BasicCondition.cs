

namespace _Script.ConditionalEffects
{
    public interface BasicCondition
    {
        string Name { get; set; }
        string Description { get; set; }
        string IconPath { get; set; }
        int Priority { get; set; }
        bool isPositive { get; set; }
        
    }
}