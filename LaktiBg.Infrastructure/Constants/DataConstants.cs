namespace LaktiBg.Infrastructure.Constants
{
    public static class DataConstants
    {
        public const string DateTimeFormat = "dd/MM/yyyy HH:mm";

        public static class EventConstants
        {
            public const int NameMaxLenght = 100;
            public const int NameMinLenght = 2;

            public const int DescriptionMaxLenght = 700;
            public const int DescriptionMinLenght = 5;
        }

        public static class PlaceConstants
        {
            public const int NameMaxLenght = 100;
            public const int NameMinlenght = 2;

            public const int RatingMaxValue = 7;
            public const int RatingMinValue = 1;
        }

        public static class UserConstants 
        {
            public const int FirstNameMaxLenght = 100;
            public const int FirstNameMinLenght = 2;

            public const int LastNameMaxLenght = 100;
            public const int LastNameMinLenght = 2;

            public const int RatingMaxValue = 7;
            public const int RatingMinValue = 1;

            public const int DescriptionMaxLenght = 500;
            public const int DescriptionMinLenght = 5;
        } 

        public static class EventTypeConstants
        {
            public const int NameMaxLenght = 100;
            public const int NameMinLenght = 3;
        }

        public static class CommentConstants
        {
            public const int TextMaxLenght = 200;
            public const int TextMinLenght = 2;
        }
    }
}
