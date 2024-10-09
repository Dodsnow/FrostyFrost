using System.Collections.Generic;

namespace _Script.ConditionalEffects
{
    public static class ConditionTag
    {
        public const string Bleed = "@Bleed#";
        public const string Stun = "@Stun#";
        public const string Poison = "@Poison#";
        public const string Weaken = "@Weaken#";
        public const string Disarm = "@Disarm#";
        public const string Immobilize = "@Immobilize#";
        public const string Curse = "@Curse#";
        public const string Bless = "@Bless#";
        public const string Empower = "@Empower#";
        public const string Invisible = "@Invisible#";
        public const string Shield = "@Shield#";
        public const string Retaliate = "@Retaliate#";
        public const string Pierce = "@Pierce#";
        public const string Shattered = "@Shattered#";
        public const string Recovery = "@Recovery#";
        public const string Immortal = "@Immortal#";
        public static List<string> ConditionTagList;
        
        static  ConditionTag()
        {
            ConditionTagList = new List<string>()
            {
                "Bleed",
                "Stun",
                "Poison",
                "Weaken",
                "Disarm",
                "Immobilize",
                "Curse",
                "Bless",
                "Empower",
                "Invisible",
                "Shield",
                "Retaliate",
                "Pierce",
                "Shattered",
                "Recovery",
                "Immortal"
            };
        }
        
        public static bool HasConditionTag(string tag)
        {
            return ConditionTagList.Contains(tag);
        }
    }
}