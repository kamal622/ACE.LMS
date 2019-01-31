using ACE.LMS.Core.Infrastructure.DependencyManagement;
using System;

namespace ACE.LMS.Core.Infrastructure
{
    /// <summary>
    /// Classes implementing this interface can serve as a portal for the 
    /// various services composing the INSOL engine. Edit functionality, modules
    /// and implementations access most INSOL functionality through this 
    /// interface.
    /// </summary>
    public interface IEngine
    {
        ContainerManager ContainerManager { get; }

        /// <summary>
        /// Initialize components and plugins in the insol environment.
        /// </summary>
        /// <param name="config">Config</param>
        void Initialize();

        T Resolve<T>() where T : class;

        object Resolve(Type type);

        T[] ResolveAll<T>();
    }
}
