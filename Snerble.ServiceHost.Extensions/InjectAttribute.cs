using System;

namespace Snerble.ServiceHost.Extensions
{
	/// <summary>
	/// Indicates that the associated property should have a value injected
	/// from the service provider during initialization.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	public sealed class InjectAttribute : Attribute
	{
		/// <summary>
		/// Gets or sets whether the service is required. True by default.
		/// </summary>
		public bool Required { get; set; } = true;
	}
}
