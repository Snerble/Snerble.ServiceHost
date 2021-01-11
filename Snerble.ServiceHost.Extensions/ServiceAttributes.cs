using System;

namespace Snerble.ServiceHost.Extensions
{
	/// <summary>
	/// Specifies that the affected class should be added as a transient service.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	public sealed class TransientAttribute : Attribute
	{
		/// <summary>
		/// Gets or sets the service type on which the affected class
		/// is registered.
		/// </summary>
		public Type ServiceType { get; set; }
	}

	/// <summary>
	/// Specifies that the affected class should be added as a scoped service.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	public sealed class ScopedAttribute : Attribute
	{
		/// <summary>
		/// Gets or sets the service type on which the affected class
		/// is registered.
		/// </summary>
		public Type ServiceType { get; set; }
	}

	/// <summary>
	/// Specifies that the affected class should be added as a singleton service.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	public sealed class SingletonAttribute : Attribute
	{
		/// <summary>
		/// Gets or sets the service type on which the affected class
		/// is registered.
		/// </summary>
		public Type ServiceType { get; set; }
	}
}
