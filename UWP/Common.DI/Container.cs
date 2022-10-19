using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Unity.Injection;
using Unity.Lifetime;
using Unity.RegistrationByConvention;
using Common.DI.Attributes;

namespace Common.DI
{
    public static class Container
    {
        static Container()
        {
            Instance = new UnityContainer();
        }

        public static UnityContainer Instance { get; }

        public static bool IsEmpty
        {
            get
            {
                return Instance.Registrations.All(registration => registration.RegisteredType == typeof(IUnityContainer));
            }
        }

        public static void RegisterByConvention(params string[] assemblyNames)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(a =>
            { 
                var assemblyName = a.GetName();

                return assemblyNames.Any(requested => string.Equals(requested, assemblyName.Name, StringComparison.InvariantCultureIgnoreCase));
            })
            .ToArray();

            Debug.Assert(assemblies.Length > 0, "Assemblies for registration in DI container were not found");

            Instance.RegisterTypes(new DefaultRegistrationConvention(Instance, assemblies));

            var singletonRegistrationGroups = Instance.Registrations.Where(r => r.LifetimeManager is ContainerControlledLifetimeManager).GroupBy(r => r.MappedToType);

            var singletonsToInstantiate = new List<Type>();

            foreach (var group in singletonRegistrationGroups)
            {
                var @class = group.Key;

                var singletonAttribute = (SingletonAttribute)@class.GetCustomAttributes(typeof(SingletonAttribute), false).FirstOrDefault();

                if (singletonAttribute != null)
                {
                    Instance.RegisterType(@class, new ContainerControlledLifetimeManager());

                    foreach (var registration in group)
                    {
                        var @interface = registration.RegisteredType;

                        Instance.RegisterFactory(@interface, c => c.Resolve(@class));
                    }

                    if (!singletonAttribute.LazyLoading) singletonsToInstantiate.Add(@class);
                }
            }

            foreach (var @class in singletonsToInstantiate) Instance.Resolve(@class);
        }
    }
}
