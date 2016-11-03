using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Web.Mvc;
using System.Reflection;

namespace UniqueBlog.Controllers.MEF
{
	public static class MefConfiguration
	{
		private static CompositionContainer _MEFContainer;

		public static CompositionContainer MEFContainer
		{
			get
			{
				if (_MEFContainer == null)
				{
					InitializeMEFContainer();
				}
				return _MEFContainer;
			}
		}

		private static void InitializeMEFContainer()
		{
			string path = string.Concat(AppDomain.CurrentDomain.BaseDirectory, "bin");

			//Create catalog
			DirectoryCatalog catalog = new DirectoryCatalog(path, "UniqueBlog.*.dll");
			_MEFContainer = new CompositionContainer(catalog);
		}

		public static void RegisterMef()
		{
			IControllerFactory mefControllerFactory = new MEFControllerFactory(MEFContainer);
			ControllerBuilder.Current.SetControllerFactory(mefControllerFactory);

			MefDependencySolver resolver = new MefDependencySolver(MEFContainer);
			DependencyResolver.SetResolver(resolver);
		}
	}
}