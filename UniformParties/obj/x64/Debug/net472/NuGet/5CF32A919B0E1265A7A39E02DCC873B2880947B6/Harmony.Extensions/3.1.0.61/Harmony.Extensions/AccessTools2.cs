// <auto-generated>
//   This code file has automatically been added by the "Harmony.Extensions" NuGet package (https://www.nuget.org/packages/Harmony.Extensions).
//   Please see https://github.com/BUTR/Harmony.Extensions for more information.
//
//   IMPORTANT:
//   DO NOT DELETE THIS FILE if you are using a "packages.config" file to manage your NuGet references.
//   Consider migrating to PackageReferences instead:
//   https://docs.microsoft.com/en-us/nuget/consume-packages/migrate-packages-config-to-package-reference
//   Migrating brings the following benefits:
//   * The "Harmony.Extensions" folder and the "AccessTools2.cs" file don't appear in your project.
//   * The added file is immutable and can therefore not be modified by coincidence.
//   * Updating/Uninstalling the package will work flawlessly.
// </auto-generated>

#region License
// MIT License
//
// Copyright (c) Bannerlord's Unofficial Tools & Resources
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
#else
[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("Harmony.Extensions.Tests")]
#endif

namespace HarmonyLib.BUTR.Extensions
{
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Diagnostics;
    using global::System.Diagnostics.CodeAnalysis;
    using global::System.Linq;
    using global::System.Reflection;
    using global::System.Reflection.Emit;

    /// <summary>An extension of Harmony's helper class for reflection related functions</summary>
#if !HARMONYEXTENSIONS_PUBLIC
    internal
#else
    public
#endif
        static partial class AccessTools2
    {
        [ExcludeFromCodeCoverage]
        private readonly struct DynamicMethodDefinitionHandle
        {
            public static DynamicMethodDefinitionHandle? Create(string name, Type returnType, Type[] parameterTypes) =>
                Helper.DynamicMethodDefinitionCtor is null ? null : new(Helper.DynamicMethodDefinitionCtor(name, returnType, parameterTypes));

            private readonly object _dynamicMethodDefinition;

            public DynamicMethodDefinitionHandle(object dynamicMethodDefinition) => _dynamicMethodDefinition = dynamicMethodDefinition;

            public ILGeneratorHandle? GetILGenerator() => Helper.GetILGenerator is null ? null : new(Helper.GetILGenerator(_dynamicMethodDefinition));

            public MethodInfo? Generate() => Helper.Generate is null ? null : Helper.Generate(_dynamicMethodDefinition);
        }

        [ExcludeFromCodeCoverage]
        private readonly struct ILGeneratorHandle
        {
            private readonly object _ilGenerator;

            public ILGeneratorHandle(object ilGenerator) => _ilGenerator = ilGenerator;

            public void Emit(OpCode opcode) => Helper.Emit1?.Invoke(_ilGenerator, opcode);
            public void Emit(OpCode opcode, FieldInfo field) => Helper.Emit2?.Invoke(_ilGenerator, opcode, field);
            public void Emit(OpCode opcode, Type type) => Helper.Emit3?.Invoke(_ilGenerator, opcode, type);
        }

        [ExcludeFromCodeCoverage]
        private static class Helper
        {
            public delegate object DynamicMethodDefinitionCtorDelegate(string name, Type returnType, Type[] parameterTypes);
            public delegate object GetILGeneratorDelegate(object instance);
            public delegate void Emit1Delegate(object instance, OpCode opcode);
            public delegate void Emit2Delegate(object instance, OpCode opcode, FieldInfo field);
            public delegate void Emit3Delegate(object instance, OpCode opcode, Type type);
            public delegate MethodInfo GenerateDelegate(object instance);

            public static readonly DynamicMethodDefinitionCtorDelegate? DynamicMethodDefinitionCtor;
            public static readonly GetILGeneratorDelegate? GetILGenerator;
            public static readonly Emit1Delegate? Emit1;
            public static readonly Emit2Delegate? Emit2;
            public static readonly Emit3Delegate? Emit3;
            public static readonly GenerateDelegate? Generate;

            static Helper()
            {
                DynamicMethodDefinitionCtor = AccessTools2.GetDeclaredConstructorDelegate<DynamicMethodDefinitionCtorDelegate>("MonoMod.Utils.DynamicMethodDefinition", new[] { typeof(string), typeof(Type), typeof(Type[]) });
                GetILGenerator = AccessTools2.GetDelegateObjectInstance<GetILGeneratorDelegate>("MonoMod.Utils.DynamicMethodDefinition:GetILGenerator", Type.EmptyTypes);
                Emit1 = AccessTools2.GetDelegateObjectInstance<Emit1Delegate>("System.Reflection.Emit.ILGenerator:Emit", new[] { typeof(OpCode) });
                Emit2 = AccessTools2.GetDelegateObjectInstance<Emit2Delegate>("System.Reflection.Emit.ILGenerator:Emit", new[] { typeof(OpCode), typeof(FieldInfo) });
                Emit3 = AccessTools2.GetDelegateObjectInstance<Emit3Delegate>("System.Reflection.Emit.ILGenerator:Emit", new[] { typeof(OpCode), typeof(Type) });
                Generate = AccessTools2.GetDelegateObjectInstance<GenerateDelegate>("MonoMod.Utils.DynamicMethodDefinition:Generate", Type.EmptyTypes);
            }

            public static bool IsValid()
            {
                if (DynamicMethodDefinitionCtor is null)
                {
                    Trace.TraceError("AccessTools2.Helper.IsValid: DynamicMethodDefinitionCtor is null");
                    return false;
                }

                if (GetILGenerator is null)
                {
                    Trace.TraceError("AccessTools2.Helper.IsValid: GetILGenerator is null");
                    return false;
                }

                if (Emit1 is null)
                {
                    Trace.TraceError("AccessTools2.Helper.IsValid: Emit1 is null");
                    return false;
                }

                if (Emit2 is null)
                {
                    Trace.TraceError("AccessTools2.Helper.IsValid: Emit2 is null");
                    return false;
                }

                if (Emit3 is null)
                {
                    Trace.TraceError("AccessTools2.Helper.IsValid: Emit3 is null");
                    return false;
                }

                if (Generate is null)
                {
                    Trace.TraceError("AccessTools2.Helper.IsValid: Generate is null");
                    return false;
                }

                return true;
            }
        }


        /// <summary>Enumerates all assemblies in the current app domain, excluding visual studio assemblies</summary>
        /// <returns>An enumeration of <see cref="Assembly"/></returns>
        public static IEnumerable<Assembly> AllAssemblies() => 
            AppDomain.CurrentDomain.GetAssemblies().Where(a => !a.FullName.StartsWith("Microsoft.VisualStudio"));

        /// <summary>Enumerates all successfully loaded types in the current app domain, excluding visual studio assemblies</summary>
        /// <returns>An enumeration of all <see cref="Type"/> in all assemblies, excluding visual studio assemblies</returns>
        public static IEnumerable<Type> AllTypes() => AllAssemblies().SelectMany(a => GetTypesFromAssembly(a));

        /// <summary>Gets all successfully loaded types from a given assembly</summary>
        /// <param name="assembly">The assembly</param>
        /// <returns>An array of types</returns>
        /// <remarks>
        /// This calls and returns <see cref="Assembly.GetTypes"/>, while catching any thrown <see cref="ReflectionTypeLoadException"/>.
        /// If such an exception is thrown, returns the successfully loaded types (<see cref="ReflectionTypeLoadException.Types"/>,
        /// filtered for non-null values).
        /// </remarks>
        public static Type[] GetTypesFromAssembly(Assembly assembly)
        {
            if (assembly is null)
                return Type.EmptyTypes;

            try
            {
                return assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException ex)
            {
                Trace.TraceError($"AccessTools2.GetTypesFromAssembly: assembly {assembly} => {ex}");
                return ex.Types.Where(type => type is object).ToArray();
            }
        }

        /// <summary>Gets all types from a given assembly if it has not any type loading related exceptions</summary>
        /// <param name="assembly">The assembly</param>
        /// <returns>An array of types</returns>
        /// <remarks>
        /// This calls and returns <see cref="Assembly.GetTypes"/>, while catching any thrown <see cref="ReflectionTypeLoadException"/>.
        /// If such an exception is thrown, returns an empty array.
        /// </remarks>
        public static Type[] GetTypesFromAssemblyIfValid(Assembly assembly)
        {
            if (assembly is null)
                return Type.EmptyTypes;

            try
            {
                return assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException ex)
            {
                Trace.TraceError($"AccessTools2.GetTypesFromAssemblyIfValid: assembly {assembly} => {ex}");
                return Type.EmptyTypes;
            }
        }

        /// <summary>Gets a type by name. Prefers a full name with namespace but falls back to the first type matching the name otherwise</summary>
        /// <param name="name">The name</param>
        /// <returns>A type or null if not found</returns>
        public static Type? TypeByName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                Trace.TraceError($"AccessTools2.TypeByName: 'name' is null or empty");
                return null;
            }

            var type = Type.GetType(name, false);
            if (type is null)
                type = AllTypes().FirstOrDefault(t => t.FullName == name);
            if (type is null)
                type = AllTypes().FirstOrDefault(t => t.Name == name);
            if (type is null)
                Trace.TraceError($"AccessTools2.TypeByName: Could not find type named '{name}'");
            return type;
        }

        /// <summary>Applies a function going up the type hierarchy and stops at the first non-<c>null</c> result</summary>
        /// <typeparam name="T">Result type of func()</typeparam>
        /// <param name="type">The class/type to start with</param>
        /// <param name="func">The evaluation function returning T</param>
        /// <returns>The first non-<c>null</c> result, or <c>null</c> if no match</returns>
        /// <remarks>
        /// The type hierarchy of a class or value type (including struct) does NOT include implemented interfaces,
        /// and the type hierarchy of an interface is only itself (regardless of whether that interface implements other interfaces).
        /// The top-most type in the type hierarchy of all non-interface types (including value types) is <see cref="object"/>.
        /// </remarks>
        public static T? FindIncludingBaseTypes<T>(Type type, Func<Type, T> func) where T : class
        {
            if (type is null || func is null) return null;

            while (true)
            {
                var result = func(type);
                if (result is object) return result;
                type = type.BaseType;
                if (type is null) return null;
            }
        }

        private static FieldInfo? GetInstanceField(Type type, string fieldName)
        {
            var fieldInfo = Field(type, fieldName);
            if (fieldInfo is null)
                return null;

            if (fieldInfo.IsStatic)
            {
                Trace.TraceError($"AccessTools2.GetInstanceField: Field must not be static, type '{type}', fieldName '{fieldName}'");
                return null;
            }

            return fieldInfo;
        }

        private static bool ValidateFieldType<F>(FieldInfo? fieldInfo)
        {
            if (fieldInfo is null)
            {
                Trace.TraceError($"AccessTools2.ValidateFieldType<{typeof(F).FullName}>: 'fieldInfo' is null");
                return false;
            }

            var returnType = typeof(F);
            var fieldType = fieldInfo.FieldType;
            if (returnType == fieldType)
                return true;

            if (fieldType.IsEnum)
            {
                var underlyingType = Enum.GetUnderlyingType(fieldType);
                if (returnType != underlyingType)
                {
                    Trace.TraceError($"AccessTools2.ValidateFieldType<{typeof(F).FullName}>: FieldRefAccess return type must be the same as FieldType or FieldType's underlying integral type ({underlyingType}) for enum types, fieldInfo '{fieldInfo}'");
                    return false;
                }
            }
            else if (fieldType.IsValueType)
            {
                // Boxing/unboxing is not allowed for ref values of value types.
                Trace.TraceError($"AccessTools2.ValidateFieldType<{typeof(F).FullName}>: FieldRefAccess return type must be the same as FieldType for value types, fieldInfo '{fieldInfo}'");
                return false;
            }
            else
            {
                if (returnType.IsAssignableFrom(fieldType) is false)
                {
                    Trace.TraceError($"AccessTools2.ValidateFieldType<{typeof(F).FullName}>: FieldRefAccess return type must be assignable from FieldType for reference types");
                    return false;
                }
            }

            return true;
        }

        private static bool ValidateStructField<T, F>(FieldInfo? fieldInfo) where T : struct
        {
            if (fieldInfo is null)
                return false;

            if (fieldInfo.IsStatic)
            {
                Trace.TraceError($"AccessTools2.ValidateStructField<{typeof(T).FullName}, {typeof(F).FullName}>: Field must not be static");
                return false;
            }
            if (fieldInfo.DeclaringType != typeof(T))
            {
                Trace.TraceError($"AccessTools2.ValidateStructField<{typeof(T).FullName}, {typeof(F).FullName}>: FieldDeclaringType must be T (StructFieldRefAccess instance type)");
                return false;
            }

            return true;
        }

        private static bool TryGetComponents(string typeColonName, [NotNullWhen(true)] out Type? type, [NotNullWhen(true)] out string? name)
        {
            if (string.IsNullOrWhiteSpace(typeColonName))
            {
                Trace.TraceError("AccessTools2.TryGetComponents: 'typeColonName' is null or whitespace/empty");

                type = null;
                name = null;
                return false;
            }

            var parts = typeColonName!.Split(':');
            if (parts.Length != 2)
            {
                Trace.TraceError($"AccessTools2.TryGetComponents: typeColonName '{typeColonName}', name must be specified as 'Namespace.Type1.Type2:Name");

                type = null;
                name = null;
                return false;
            }

            type = TypeByName(parts[0]);
            name = parts[1];
            return type is not null;
        }
    }
}

#pragma warning restore
#nullable restore
#endif // HARMONYEXTENSIONS_DISABLE