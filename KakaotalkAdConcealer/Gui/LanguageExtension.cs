using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using KakaotalkAdConcealer.Properties;

namespace KakaotalkAdConcealer.Gui
{
    public class LanguageExtension
    {
        private static CultureInfo[] Cache { get; set; }

        public static CultureInfo[] GetAvailableCultures()
        {
            return Cache ??= GetAvailableCulturesWithoutCache().ToArray();
            
            static IEnumerable<CultureInfo> GetAvailableCulturesWithoutCache()
            {
                var manager = new ResourceManager(typeof(Resources));

                foreach (var culture in CultureInfo.GetCultures(CultureTypes.AllCultures))
                {
                    bool supported;
                    try
                    {
                        supported = manager.GetResourceSet(culture, true, false) is not null;
                    }
                    catch (CultureNotFoundException)
                    {
                        supported = false;
                    }

                    if (supported)
                        yield return culture;
                }
            }
        }
    }
}