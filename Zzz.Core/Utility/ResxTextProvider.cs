using System.Globalization;
using System.Resources;
using MvvmCross.Localization;

namespace Zzz.Core.Utility
{
    public class ResxTextProvider : IMvxTextProvider
    {
        private readonly ResourceManager _resourceManager;

        public CultureInfo CurrentLanguage { get; set; }

        public ResxTextProvider(ResourceManager resourceManager)
        {
            _resourceManager = resourceManager;
            CurrentLanguage = new CultureInfo("en-US");
        }

        public string GetText(string namespaceKey, string typeKey, string name)
        {
            string resolvedKey = name;

            if (!string.IsNullOrEmpty(typeKey))
            {
                resolvedKey = $"{typeKey}.{resolvedKey}";
            }

            if (!string.IsNullOrEmpty(namespaceKey))
            {
                resolvedKey = $"{namespaceKey}.{resolvedKey}";
            }

            return _resourceManager.GetString(resolvedKey, CurrentLanguage);
        }

        public string GetText(string namespaceKey, string typeKey, string name, params object[] formatArgs)
        {
            string baseText = GetText(namespaceKey, typeKey, name);

            if (string.IsNullOrEmpty(baseText))
            {
                return baseText;
            }

            return string.Format(baseText, formatArgs);
        }
    }
}
