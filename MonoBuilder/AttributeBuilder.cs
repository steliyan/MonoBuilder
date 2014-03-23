using Mono.Cecil;
using System;
using System.Linq;

namespace MonoBuilder
{
    public class AttributeBuilder
    {
        static AttributeBuilder()
        {
            module = ModuleDefinition.CreateModule("MonoBuilder", ModuleKind.Dll);
        }

        public AttributeBuilder(Type type)
        {
            this.type = module.Import(type).Resolve();
            this.ctor = this.type.Methods.First(m => m.IsConstructor && !m.Parameters.Any()).GetElementMethod();
        }

        public static implicit operator CustomAttribute(AttributeBuilder builder)
        {
            return new CustomAttribute(builder.ctor);
        }

        public static AttributeBuilder Create(Type type)
        {
            return new AttributeBuilder(type);
        }

        public AttributeBuilder ObsoleteAttribute()
        {
            return AttributeBuilder.Create(typeof(ObsoleteAttribute));
        }

        private static ModuleDefinition module;
        private TypeDefinition type;
        private MethodReference ctor;
    }
}
