namespace SeminarHub.Utilities
{
    public static class GlobalConstants
    {

        public static class SeminarConstants
        {
            public const int TopicMaxLength = 100;
            public const int TopicMinLength = 3;

            public const int LecturerMaxLength = 60;
            public const int LecturerMinLength = 5;

            public const int DetailsMaxLength = 500;
            public const int DetailsMinLength = 10;

            public const string dateFormat = "dd/MM/yyyy HH:mm";

            public const string RequireErrorMessage = "Date must be in this format dd/MM/yyyy HH:mm";

            public const int DurationMaxLength = 180;
            public const int DurationMinLength = 30;
        }

        public static class CategoryConstants
        {
            public const int NameMaxLength = 50;
            public const int NameMinLength = 3;
        }
    }
}