using System.Globalization;
using System.Resources;
using KakaotalkAdConcealer.Forms.Properties;

namespace KakaotalkAdConcealer.Forms.Gui
{
    /// <summary>
    /// Helper for localization
    /// </summary>
    public static class LanguageExtension
    {
        /// <summary>
        /// Cache for supported culture info list
        /// </summary>
        private static CultureInfo[] Cache { get; set; }

        /// <summary>
        /// Get supported culture info list
        /// </summary>
        /// <returns>Supported culture info list</returns>
        public static IEnumerable<CultureInfo> GetAvailableCultures()
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

        /// <summary>
        /// Get ISO 639-2 three-letter code for CultureInfo
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static string ToISOCode(this CultureInfo info)
        {
            return info.Equals(CultureInfo.InvariantCulture) 
                ? "default (eng)" 
                : info.ThreeLetterISOLanguageName;
        }
    }
}