using Charismatic.Debugging;

namespace Charismatic
{
    public class CharismaticConsts
    {
        public const string LocalizationSourceName = "Charismatic";

        public const string ConnectionStringName = "Default";

        public const bool MultiTenancyEnabled = false;


        public const string LocalizationTokens = "Tokens";

        public const string LocalizationDataAnnotations = "DataAnnotations";

        public const string LocalizationExceptions = "Exceptions";

        public const string LocalizationMessages = "Messages";

        public const string NameFormat = @"^[\u0600-\u06FFa-zA-Z0-9-, ]+([_\.]?[\u0600-\u06FFa-zA-Z0-9-, ])*$";



        /// <summary>
        /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
        /// </summary>
        public static readonly string DefaultPassPhrase =
            DebugHelper.IsDebug ? "gsKxGZ012HLL3MI5" : "8a86b5d02add4781a210acdb5767a9e1";
    }
}
