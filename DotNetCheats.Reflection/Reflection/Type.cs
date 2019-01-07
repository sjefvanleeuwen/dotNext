﻿using System;
using static System.Linq.Expressions.Expression;

namespace Cheats.Reflection
{
    /// <summary>
    /// Provides typed access to class or value type metadata.
    /// </summary>
    /// <typeparam name="T">Reflected type.</typeparam>
    public static partial class Type<T>
    {
        /// <summary>
        /// Gets reflected type.
        /// </summary>
        public static Type RuntimeType => typeof(T);

        /// <summary>
        /// Returns default value for this type.
        /// </summary>
        public static T Default => default;

        private static readonly System.Linq.Expressions.DefaultExpression DefaultExpression = Default(RuntimeType);

        /// <summary>
        /// Checks whether the specified value is default value.
        /// </summary>
        public static readonly Predicate<T> IsDefault;

        static Type()
        {
            if(RuntimeType.IsValueType)
            {
                var isDefaultMethod = typeof(ValueType<int>).GetGenericTypeDefinition().MakeGenericType(RuntimeType).GetMethod(nameof(ValueType<int>.IsDefault));
                IsDefault = isDefaultMethod.CreateDelegate<Predicate<T>>();
            }
            else
                IsDefault = new Predicate<object>(input => input is null).ConvertDelegate<Predicate<T>>();
        }

        /// <summary>
        /// Determines whether an instance of a specified type can be assigned to an instance of the current type.
        /// </summary>
        /// <typeparam name="U">The type to compare with the current type.</typeparam>
        /// <returns>True, if instance of type <typeparamref name="U"/> can be assigned to type <typeparamref name="T"/>.</returns>
        public static bool IsAssignableFrom<U>() => RuntimeType.IsAssignableFrom(typeof(U));

        public static bool IsAssignableTo<U>() => Type<U>.IsAssignableFrom<T>();

        public static Optional<T> TryConvert<U>(U value)
        {
            Operator<U, T> converter = Type<U>.Operator.Get<T>(UnaryOperator.Convert);
            return converter is null ? Optional<T>.Empty : converter(value);
        }

        public static bool TryConvert<U>(U value, out T result) => TryConvert(value).TryGet(out result);

        /// <summary>
        /// Converts object into type <typeparamref name="T"/>.
        /// </summary>
        /// <remarks>
        /// Semantics of this method includes typecast as well as conversion between numeric types
        /// and implicit/explicit cast operators.
        /// </remarks>
        /// <param name="value">The value to convert.</param>
        /// <typeparam name="U">Type of value to convert.</typeparam>
        /// <returns>Converted value.</returns>
        /// <exception cref="InvalidCastException">Cannot convert values.</exception>
        public static T Convert<U>(U value) => TryConvert<U>(value).OrThrow<InvalidCastException>();
    }
}