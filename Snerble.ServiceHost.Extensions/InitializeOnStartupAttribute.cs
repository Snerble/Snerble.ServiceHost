using System;

namespace Snerble.ServiceHost.Extensions
{
	/// <summary>
	/// Specifies that the class constructor of the affected type will be invoked
	/// on startup.
	/// </summary>
	/// <remarks>
	/// The class constructors are invoked before any services are registered.
	/// </remarks>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Struct, AllowMultiple = false, Inherited = false)]
	public sealed class InitializeOnStartupAttribute : Attribute { }
}
