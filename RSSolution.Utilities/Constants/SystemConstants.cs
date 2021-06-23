using System;
using System.Collections.Generic;
using System.Text;

namespace RSSolution.Utilities.Constants
{
    public class SystemConstants
    {
        public const string MainConnectionString = "DefaultConnection";
        public const string CartSession = "CartSession";

        public class AppSettings
        {
            public const string DefaultLanguageId = "DefaultLanguageId";
            public const string Token = "Token";
            public const string BaseAddress = "BaseAddress";
        }

        public class MealSettings
        {
            public const int NumberOfFeaturedMeals = 4;
            public const int NumberOfLatestMeals = 6;
        }

        public class MealConstants
        {
            public const string NA = "N/A";
        }
    }
}