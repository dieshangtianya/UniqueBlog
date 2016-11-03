using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UniqueBlog.Controllers.MEF
{
	public class MefDependencySolver : IDependencyResolver
	{

		private CompositionContainer container;

		public MefDependencySolver(CompositionContainer container)
		{
			this.container = container;
		}

		#region IDependencyResolver Members

		public object GetService(Type serviceType)
		{
			string contractName = AttributedModelServices.GetContractName(serviceType);
			return this.container.GetExportedValueOrDefault<object>(contractName);
		}

		public IEnumerable<object> GetServices(Type serviceType)
		{
			return this.container.GetExportedValues<object>(serviceType.FullName);
		}

		#endregion
	}
}