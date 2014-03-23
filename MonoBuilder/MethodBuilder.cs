using Mono.Cecil;
using System;
using System.Collections.Generic;

namespace MonoBuilder
{
    public class MethodBuilder
    {
        static MethodBuilder()
        {
            module = ModuleDefinition.CreateModule("MonoBuilder", ModuleKind.Dll);
        }

        public MethodBuilder(string name, Type type, MethodAttributes attributes)
        {
            this.name = name;
            this.type = module.Import(type);
            this.attributes = attributes;
        }

        public static implicit operator MethodDefinition(MethodBuilder builder)
        {
            MethodDefinition method = new MethodDefinition(builder.name, builder.attributes, builder.type);
            return method;
        }

        public MethodBuilder With(CustomAttribute customAttribute)
        {
            this.customAttributes.Add(customAttribute);
            return this;
        }

        public static MethodBuilder Create(string name, Type type, MethodAttributes attributes)
        {
            return new MethodBuilder(name, type, attributes);
        }

        public static MethodBuilder CreatePublicVoidMethod(string name)
        {
            return MethodBuilder.Create(name, typeof(void), MethodAttributes.Public);
        }

        private static ModuleDefinition module;
        private string name;
        private TypeReference type;
        private MethodAttributes attributes;
        private List<CustomAttribute> customAttributes = new List<CustomAttribute>();
    }
}
