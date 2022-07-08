namespace NerdStore.Core.DomainObjects
{
    public static class Validations
    {
        public static void ExceptionIfEqual(this object object1, object object2, string message)
        {
            if (object1 == object2)
                throw new Exception(message);
        }
        public static void ExceptionIfNull(this object object1, string message)
        {
            if (object1 is null)
                throw new Exception(message);
        }


        public static void ExceptionIfEmpty(this string text, string message)
        {
            if (String.IsNullOrWhiteSpace(text))
                throw new Exception(message);
        }

        public static void ExceptionIfLessThan(this decimal value, decimal min, string message)
        {
            if (value < min)
                throw new Exception(message);
        }

        public static void ExceptionIfLessThan(this int value, int min, string message)
        {
            if (value < min)
                throw new Exception(message);
        }

        public static void ExceptionIfEqualOrLessThan(this int value, int min, string message)
        {
            if (value <= min)
                throw new Exception(message);
        }
    }
}
