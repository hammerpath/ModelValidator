namespace ModelValidation.Constants
{
    public static class RegexConstants
    {
        public const string MaxThreeChars = @"^.{0,3}$";
        public const string OnlyPositiveNumbers = @"^[1-9][0-9]*$";
        public const string OnlyNumbers = @"^[0-9]*$";
        public const string OnlyPositiveNumbersWithNoMoreThanNineValues = @"^[1-9][0-9]{0,8}$";
    }
}
