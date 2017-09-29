using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using swlsimNET.ServerApp.Spells;

namespace swlsimNET.ServerApp.Models
{
    public class AplReader
    {
        private readonly string _aplString;
        private readonly IPlayer _player;

        public AplReader(IPlayer player, string apl)
        {
            _player = player;
            _aplString = apl;
        }

        public List<ISpell> GetApl()
        {
            var apl = new List<ISpell>();
            var array = _aplString.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            foreach (var item in array)
            {
                string name;
                string expr = null;

                // create new class from name
                var charLocation = item.IndexOf(",", StringComparison.Ordinal);

                if (charLocation > 0)
                {
                    name = item.Substring(0, charLocation).Trim();
                    expr = item.Substring(charLocation + 1, item.Length - name.Length - 1).Trim();
                }
                else
                {
                    name = item.Trim();
                }

                // Name can't contain any spaces at all
                name = name.Replace(" ", string.Empty);

                var myType = Type.GetType("swlsimNET.ServerApp.Spells." + name, false, true);
                var myTypeRage = Type.GetType("swlsimNET.ServerApp.Spells." + name + "Rage", false, true);

                // TODO: Show where user input is BAD
                if (myType == null) continue;

                // TODO: Handle errors here
                var spell = (ISpell)Activator.CreateInstance(myType, _player, expr);
                apl.Add(spell);

                if (myTypeRage == null) continue;
                spell = (ISpell)Activator.CreateInstance(myTypeRage, _player, expr);
                apl.Add(spell);
            }

            return apl;
        }
    }
}
