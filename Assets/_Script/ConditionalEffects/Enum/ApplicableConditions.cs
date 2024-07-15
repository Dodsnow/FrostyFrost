namespace _Script.ConditionalEffects.Enum
{
    public enum ApplicableConditions
    {
        Bleed, // Effect that causes damage over time(1) damage in the begging of character turn each round until healed, can be applied multiple times, DOT
        Poison, // Effect that causes damage over time(1) damage in the begging of character turn each round until healed, can be applied multiple times, DOT
        Stun,   // Effect that causes character to lose his turn
        Weaken, // Gains Disadvantage , until the end of character next turn
        Disarm, // Loses attack ability
        Immobilize, // Loses movement ability
        Curse, // /2 damage for 1 turn, until the end of character next turn
        Bless, // *2 damage for 1 turn, until the end of character next turn
        Empower, // Gains advantage, until the end of character next turn
        Invisible, // Gains invisibility, until the end of character next turn, Mobs disapears from the board
        Shield, // Gains +X to incoming damage from characters,until the end of character next turn
        Retaliate, // Damage Retaliation X damage to attacker, until the end of character next turn
        Pierce, // Ignores X shield  basic is 0, until the end of character next turn
        Shattered, // Gains +X to incoming damage, until the end of character next turn
    }
}