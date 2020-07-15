using Terraria.ModLoader;

namespace PersonalMod
{
	public class PersonalMod : Mod
    {
       internal static PersonalMod mod;

       public static PersonalMod Instance;
       public PersonalMod()
       {
           Properties = new ModProperties()
           {
               Autoload = true
           };
       }
    }
}