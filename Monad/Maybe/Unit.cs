using System;

namespace Monades.Maybe
{
    /// <summary>
    /// Unit type (like void but can return anything)
    /// </summary>
    public struct Unit : IEquatable<Unit>
    {
        /// <summary>
        /// Default unit value
        /// </summary>
        public static readonly Unit Default
            = new Unit();

        /// <summary>
        /// Provide an alternative value to unit
        /// </summary>
        public T Return<T>(T anything) => 
            anything;

        /// <summary>
        /// Provide an alternative value to unit
        /// </summary>
        public T Return<T>(Func<T> anything) => 
            anything();

        #region Operators

        public static bool operator ==(Unit lhs, Unit rhs) =>
            true;

        public static bool operator !=(Unit lhs, Unit rhs) =>
            false;

        #endregion

        #region Overrides 

        public override int GetHashCode() =>
            0;

        public override bool Equals(object obj) =>
            obj is Unit;

        public override string ToString() =>
            "[Unit]";

        public bool Equals(Unit other) =>
            true;

        #endregion
    }
}