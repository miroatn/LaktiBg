using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaktiBg.Core.Constants
{
    public static class MessageConstants
    {
        public const string RequiredMessage = "Полето \"{0}\" е задължително.";

        public const string LenghtMessage = "Полето \"{0}\" трябва да съдържа между {2} и {1} символа.";

        public const string RatingRange = "Рейтинга трябва да бъде между {0} и {1}";

        public const string NoRestrictionAdded = "Няма зададено ограничение";

        public const string PasswordNotMatch = "Въведената парола не съвпада";
    }
}
