using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Unity.Injection;
using Unity.Lifetime;
using Unity.RegistrationByConvention;
using Common.DI.Attributes;

namespace Common.DI
{
    internal class DefaultRegistrationConvention : RegistrationConvention
    {
        private readonly IUnityContainer _container;

        private readonly IEnumerable<Type> _types;
     
        public DefaultRegistrationConvention(IUnityContainer unity, params Assembly[] assemblies) : this(unity, assemblies.SelectMany(a => a.GetExportedTypes()).ToArray())
        {
            _container = unity;
        }
    
        public DefaultRegistrationConvention(IUnityContainer unity, params Type[] types)
        {
            _container = unity;

            _types = types ?? Enumerable.Empty<Type>();
        }
    
        public override Func<Type, IEnumerable<Type>> GetFromTypes()
        {
            return (Type type) =>
            {
                var classRootNamespace = type.Namespace.Split('.')[0];

                var interfaces = type.GetInterfaces();

                return interfaces.Where(i =>
                {
                    var interfaceRootNamespace = i.Namespace.Split('.')[0];

                    if (interfaceRootNamespace != classRootNamespace && interfaceRootNamespace != "Common") return false;
                    
                    return i.GetCustomAttribute<NotForDIAttribute>() == null;
                });
            };
        }
  
        public override Func<Type, IEnumerable<InjectionMember>> GetInjectionMembers()
        {
            return type => Enumerable.Empty<InjectionMember>();
        }
  
        public override Func<Type, ITypeLifetimeManager> GetLifetimeManager()
        {
            return type =>
            {
                if (type.GetCustomAttribute(typeof(SingletonAttribute)) != null) return WithLifetime.ContainerControlled(type);
                
                return WithLifetime.Transient(type);
            };
        }
  
        public override Func<Type, String> GetName()
        {
            return type => (_container.Registrations.Select(r => r.RegisteredType).Any(r => type.GetInterfaces().Contains(r) == true) == true) ? WithName.TypeName(type) : WithName.Default(type);
        }
        
        public override IEnumerable<Type> GetTypes()
        {
            return _types.Where(type => (type.IsClass == true) && (type.IsAbstract == false) && (type.GetInterfaces().Any() == true));
        }
    }
}
