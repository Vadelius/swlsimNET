presets = {
    hammer:
    {
        'input-PrimaryWeapon': 'Hammer',
        'input-PrimaryWeaponAffix': 'Destruction',
        'input-PrimaryWeaponProc': 'null',
        'input-SecondaryWeapon': 'Fist',
        'input-Passive1': 'Outrage',
        'input-Passive2': 'UnbridledWrath',
        'input-Passive3': 'Annihilate',
        'input-Passive4': 'Berserker',
        'input-Passive5': 'FastAndFurious',
        'input-Apl':
            "Fist.Savagery, Buff.UnstoppableForce.Active\r\n" +
                "Hammer.Seethe, Buff.UnstoppableForce.Active\r\n" +
                "Hammer.UnstoppableForce, Rage > 50 || Hammer.Energy > 8\r\n" +
                "Hammer.Demolish, Buff.UnstoppableForce.Active\r\n" +
                "Hammer.Demolish, Rage > 60 || Hammer.Energy > 13\r\n" +
                "Hammer.Smash"
    },
    blood:
    {
        'input-PrimaryWeapon': 'Blood',
        'input-PrimaryWeaponAffix': 'Destruction',
        'input-PrimaryWeaponProc': 'null',
        'input-SecondaryWeapon': 'Fist',
        'input-Passive1': 'CrimsonPulse',
        'input-Passive2': 'Desolate',
        'input-Passive3': 'Defilement',
        'input-Passive4': 'Flay',
        'input-Passive5': 'Contaminate',
        'input-Apl':
            "Fist.Savagery, Corruption >= 10 && EldritchScourge.Cooldown <= 0 && Desecrate.Cooldown <= 0\r\n" +
                "Blood.EldritchScourge, Corruption > 0 && Buff.Savagery.Active\r\n" +
                "Blood.Desecrate, Buff.Savagery.Active\r\n" +
                "Blood.Maleficium, Blood.Energy > 10\r\n" +
                "Blood.Torment"
    },
    assaultrifle:
    {
        'input-PrimaryWeapon': 'Rifle',
        'input-PrimaryWeaponAffix': 'Destruction',
        'input-PrimaryWeaponProc': 'null',
        'input-SecondaryWeapon': 'Fist',
        'input-Passive1': 'null',
        'input-Passive2': 'null',
        'input-Passive3': 'null',
        'input-Passive4': 'null',
        'input-Passive5': 'null',
        'input-Apl':
            "Fist.Savagery, HighExplosiveGrenade.Cooldown <= 0\r\n" +
                "Rifle.HighExplosiveGrenade, Buff.Savagery\r\n" +
                "Rifle.IncendiaryGrenade, HighExplosiveGrenade.Cooldown >6\r\n" +
                "Rifle.BurstFire\r\n" +
                "Rifle.PlacedShot"
    }
};

module.exports = presets;