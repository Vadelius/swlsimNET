using swlsimNET.ServerApp.Models;

namespace swlsimNET.ServerApp.Spells
{
    public sealed class ValiMetabolicAccelerator : Spell
    {
        public ValiMetabolicAccelerator(IPlayer player, string args = null)
        {
            AbilityType = AbilityType.Gadget;
            SpellType = SpellType.Instant;
            PrimaryGain = 3;
            SecondaryGain = 2;
            MaxCooldown = 30;
            Args = args;
        }
    }
    public sealed class MnemonicGuardianWerewolf : Spell
    {
        public MnemonicGuardianWerewolf(IPlayer player, string args = null)
        {
            AbilityType = AbilityType.Gadget;
            SpellType = SpellType.Instant;
            MaxCooldown = 30;
            Args = args;
            // Summon a friendly Werewolf that will cast Bloodthirst. 
            // Bloodthirst: An effect that causes your attacks to deal an additional (CP*0.225) physical damage for 10 seconds. 
            // (Only for channels & casts)
        }
    }
    public sealed class ShardOfSesshoSeki : Spell
    {
        public ShardOfSesshoSeki(IPlayer player, string args = null)
        {
            AbilityType = AbilityType.Gadget;
            SpellType = SpellType.Procc;
            BaseDamage = 0.068; // Passively does this for every single hit except dots.
            Args = args;
        }
    }
    public sealed class ElectrograviticAttractor : Spell
    {
        public ElectrograviticAttractor(IPlayer player, string args = null)
        {
            AbilityType = AbilityType.Gadget;
            SpellType = SpellType.Instant;
            BaseDamage = 1.8;
            MaxCooldown = 30;
            Args = args;
        }
    }
}
