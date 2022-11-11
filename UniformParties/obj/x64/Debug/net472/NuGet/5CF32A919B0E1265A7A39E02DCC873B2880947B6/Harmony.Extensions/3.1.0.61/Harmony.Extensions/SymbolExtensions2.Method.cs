// <auto-generated>
//   This code file has automatically been added by the "Harmony.Extensions" NuGet package (https://www.nuget.org/packages/Harmony.Extensions).
//   Please see https://github.com/BUTR/Harmony.Extensions for more information.
//
//   IMPORTANT:
//   DO NOT DELETE THIS FILE if you are using a "packages.config" file to manage your NuGet references.
//   Consider migrating to PackageReferences instead:
//   https://docs.microsoft.com/en-us/nuget/consume-packages/migrate-packages-config-to-package-reference
//   Migrating brings the following benefits:
//   * The "Harmony.Extensions" folder and the "SymbolExtensions2.Method.cs" file don't appear in your project.
//   * The added file is immutable and can therefore not be modified by coincidence.
//   * Updating/Uninstalling the package will work flawlessly.
// </auto-generated>

#region License
// MIT License
//
// Copyright (c) Bannerlord's Unofficial Tools & Resources, Andreas Pardeike
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
#endregion

#if !HARMONYEXTENSIONS_DISABLE
#nullable enable
#if !HARMONYEXTENSIONS_ENABLEWARNINGS
#pragma warning disable
#endif

namespace HarmonyLib.BUTR.Extensions
{
    using global::System;
    using global::System.Linq.Expressions;
    using global::System.Reflection;

#if !HARMONYEXTENSIONS_PUBLIC
    internal
#else
    public
#endif
        static partial class SymbolExtensions2
    {
        /// <summary>Given a lambda expression that calls a method, returns the method info</summary>
        /// <param name="expression">The lambda expression using the method</param>
        /// <returns>The method in the lambda expression</returns>
        public static MethodInfo? GetMethodInfo(Expression<Action> expression)
        {
            if (expression is LambdaExpression lambdaExpression)
                return GetMethodInfo(lambdaExpression);

            return null;
        }

        /// <summary>Given a lambda expression that calls a method, returns the method info</summary>
        /// <typeparam name="T">The generic type</typeparam>
        /// <param name="expression">The lambda expression using the method</param>
        /// <returns>The method in the lambda expression</returns>
        public static MethodInfo? GetMethodInfo<T1>(Expression<Action<T1>> expression)
        {
            if (expression is LambdaExpression lambdaExpression)
                return GetMethodInfo(lambdaExpression);

            return null;
        }
        public static MethodInfo? GetMethodInfo<T1, T2>(Expression<Action<T1, T2>> expression)
        {
            if (expression is LambdaExpression lambdaExpression)
                return GetMethodInfo(lambdaExpression);

            return null;
        }
        public static MethodInfo? GetMethodInfo<T1, T2, T3>(Expression<Action<T1, T2, T3>> expression)
        {
            if (expression is LambdaExpression lambdaExpression)
                return GetMethodInfo(lambdaExpression);

            return null;
        }
        public static MethodInfo? GetMethodInfo<T1, T2, T3, T4>(Expression<Action<T1, T2, T3, T4>> expression)
        {
            if (expression is LambdaExpression lambdaExpression)
                return GetMethodInfo(lambdaExpression);

            return null;
        }
        public static MethodInfo? GetMethodInfo<T1, T2, T3, T4, T5>(Expression<Action<T1, T2, T3, T4, T5>> expression)
        {
            if (expression is LambdaExpression lambdaExpression)
                return GetMethodInfo(lambdaExpression);

            return null;
        }
        public static MethodInfo? GetMethodInfo<T1, T2, T3, T4, T5, T6>(Expression<Action<T1, T2, T3, T4, T5, T6>> expression)
        {
            if (expression is LambdaExpression lambdaExpression)
                return GetMethodInfo(lambdaExpression);

            return null;
        }
        public static MethodInfo? GetMethodInfo<T1, T2, T3, T4, T5, T6, T7>(Expression<Action<T1, T2, T3, T4, T5, T6, T7>> expression)
        {
            if (expression is LambdaExpression lambdaExpression)
                return GetMethodInfo(lambdaExpression);

            return null;
        }
        public static MethodInfo? GetMethodInfo<T1, T2, T3, T4, T5, T6, T7, T8>(Expression<Action<T1, T2, T3, T4, T5, T6, T7, T8>> expression)
        {
            if (expression is LambdaExpression lambdaExpression)
                return GetMethodInfo(lambdaExpression);

            return null;
        }
        public static MethodInfo? GetMethodInfo<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Expression<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9>> expression)
        {
            if (expression is LambdaExpression lambdaExpression)
                return GetMethodInfo(lambdaExpression);

            return null;
        }

        /// <summary>Given a lambda expression that calls a method, returns the method info</summary>
        /// <typeparam name="T">The generic type</typeparam>
        /// <typeparam name="TResult">The generic result type</typeparam>
        /// <param name="expression">The lambda expression using the method</param>
        /// <returns>The method in the lambda expression</returns>
        public static MethodInfo? GetMethodInfo<TResult>(Expression<Func<TResult>> expression)
        {
            if (expression is LambdaExpression lambdaExpression)
                return GetMethodInfo(lambdaExpression);

            return null;
        }
        public static MethodInfo? GetMethodInfo<T1, TResult>(Expression<Func<T1, TResult>> expression)
        {
            if (expression is LambdaExpression lambdaExpression)
                return GetMethodInfo(lambdaExpression);

            return null;
        }
        public static MethodInfo? GetMethodInfo<T1, T2, TResult>(Expression<Func<T1, T2, TResult>> expression)
        {
            if (expression is LambdaExpression lambdaExpression)
                return GetMethodInfo(lambdaExpression);

            return null;
        }
        public static MethodInfo? GetMethodInfo<T1, T2, T3, TResult>(Expression<Func<T1, T2, T3, TResult>> expression)
        {
            if (expression is LambdaExpression lambdaExpression)
                return GetMethodInfo(lambdaExpression);

            return null;
        }
        public static MethodInfo? GetMethodInfo<T1, T2, T3, T4, TResult>(Expression<Func<T1, T2, T3, T4, TResult>> expression)
        {
            if (expression is LambdaExpression lambdaExpression)
                return GetMethodInfo(lambdaExpression);

            return null;
        }
        public static MethodInfo? GetMethodInfo<T1, T2, T3, T4, T5, TResult>(Expression<Func<T1, T2, T3, T4, T5, TResult>> expression)
        {
            if (expression is LambdaExpression lambdaExpression)
                return GetMethodInfo(lambdaExpression);

            return null;
        }
        public static MethodInfo? GetMethodInfo<T1, T2, T3, T4, T5, T6, TResult>(Expression<Func<T1, T2, T3, T4, T5, T6, TResult>> expression)
        {
            if (expression is LambdaExpression lambdaExpression)
                return GetMethodInfo(lambdaExpression);

            return null;
        }
        public static MethodInfo? GetMethodInfo<T1, T2, T3, T4, T5, T6, T7, TResult>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, TResult>> expression)
        {
            if (expression is LambdaExpression lambdaExpression)
                return GetMethodInfo(lambdaExpression);

            return null;
        }
        public static MethodInfo? GetMethodInfo<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult>> expression)
        {
            if (expression is LambdaExpression lambdaExpression)
                return GetMethodInfo(lambdaExpression);

            return null;
        }
        public static MethodInfo? GetMethodInfo<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(Expression<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>> expression)
        {
            if (expression is LambdaExpression lambdaExpression)
                return GetMethodInfo(lambdaExpression);

            return null;
        }

        /// <summary>Given a lambda expression that calls a method, returns the method info</summary>
        /// <param name="expression">The lambda expression using the method</param>
        /// <returns>The method in the lambda expression</returns>
        public static MethodInfo? GetMethodInfo(LambdaExpression expression)
        {
            if (expression?.Body is MethodCallExpression { Method: MethodInfo methodInfo })
                return methodInfo;

            return null;
        }
    }
}

#pragma warning restore
#nullable restore
#endif // HARMONYEXTENSIONS_DISABLE