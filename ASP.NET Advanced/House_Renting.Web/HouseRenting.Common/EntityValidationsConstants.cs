namespace HouseRenting.Common
{
    public static class EntityValidationsConstants
    {
        public static class Category
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 50;
        }

        public static class House
        {
            public const int TitleMinLength = 10;
            public const int TitleMaxLength = 50;

            public const int AddressMinLength = 30;
            public const int AddressMaxLength = 150;

            public const int DesctiptionMinLength = 50;
            public const int DesctiptionMaxLength = 500;

            public const int ImgMaxLength = 500;

            public const string PricePerMounthMinValue = "0";
            public const string PricePerMounthMaxValue = "2000";
        }

        public static class Agent
        {
            public const int PhoneNumberMinLength = 7;
            public const int PhoneNumberMaxLength = 15;
        }
    }
}
