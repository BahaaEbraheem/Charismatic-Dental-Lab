using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Localization.Sources.Resource;
using Abp.Reflection.Extensions;
using Charismatic.Localization.SourceFiles;

namespace Charismatic.Localization
{
    public static class CharismaticLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(CharismaticConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(CharismaticLocalizationConfigurer).GetAssembly(),
                        "Charismatic.Localization.SourceFiles"
                    )
                )
            );

          //  localizationConfiguration.Sources.Add(
          //    new ResourceFileLocalizationSource(
          //        "Messages",
          //        Messages.ResourceManager
          //        )
          //);

          //  localizationConfiguration.Sources.Add(
          //      new ResourceFileLocalizationSource(
          //          "DataAnnotations",
          //          DataAnnotations.ResourceManager
          //          )
          //  );

          //  localizationConfiguration.Sources.Add(
          //      new ResourceFileLocalizationSource(
          //          "Exceptions",
          //          Exceptions.ResourceManager
          //          )
          //  );

            localizationConfiguration.Sources.Add(
                new ResourceFileLocalizationSource(
                    "Tokens",
                    Tokens.ResourceManager
                    )
            );

        }
    }




}
