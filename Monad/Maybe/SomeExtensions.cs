namespace Monades.Maybe
{
    public static class SomeExtensions
    {
        /// <summary>
        /// Converts value to an Some<T>.
        /// </summary>
        public static Some<T> ToSome<T>(this T self) =>
            Some.Of(self);
    }
}
