using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using UniqueBlog.Infrastructure.MEF;

namespace UniqueBlog.Controllers.MEF
{
	public static class MVCMEFConfiguration
	{
        public static void RegisterMef()
        {
            IControllerFactory mefControllerFactory = new MEFControllerFactory(MEFConfiguration.MEFContainer);
            ControllerBuilder.Current.SetControllerFactory(mefControllerFactory);

            MefDependencySolver resolver = new MefDependencySolver(MEFConfiguration.MEFContainer);
            DependencyResolver.SetResolver(resolver);
        }
    }
}