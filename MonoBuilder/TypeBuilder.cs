using Mono.Cecil;
using System.Collections.Generic;

namespace MonoBuilder
{
    public class TypeBuilder
    {
        public TypeBuilder(string @namespace, string name, TypeAttributes attributes)
        {
            this.@namespace = @namespace;
            this.name = name;
            this.attributes = attributes;
        }

        public static implicit operator TypeDefinition(TypeBuilder builder)
        {
            TypeDefinition type = new TypeDefinition(builder.@namespace, builder.name, builder.attributes);
            builder.types.ForEach(t => type.NestedTypes.Add(t));
            builder.methods.ForEach(m => type.Methods.Add(m));
            return type;
        }

        public TypeBuilder With(TypeDefinition type)
        {
            this.types.Add(type);
            return this;
        }

        public TypeBuilder With(IEnumerable<TypeDefinition> types)
        {
            this.types.AddRange(types);
            return this;
        }

        public TypeBuilder With(MethodDefinition method)
        {
            this.methods.Add(method);
            return this;
        }

        public static TypeBuilder CreateClass(string @namespace, string name, TypeAttributes attributes)
        {
            return new TypeBuilder(@namespace, name, attributes);
        }

        public static TypeBuilder CreatePublicClass(string @namespace, string name)
        {
            return CreateClass(@namespace, name, TypeAttributes.Public | TypeAttributes.Class | TypeAttributes.BeforeFieldInit);
        }

        private string @namespace;
        private string name;
        private TypeAttributes attributes;
        private List<TypeDefinition> types = new List<TypeDefinition>();
        private List<MethodDefinition> methods = new List<MethodDefinition>();
    }
}
