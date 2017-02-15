using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace UniqueBlog.Controllers.MEF
{
    public class MEFControllerFactory : DefaultControllerFactory
    {
        private readonly CompositionContainer _container;

        public MEFControllerFactory(CompositionContainer container)
        {
            this._container = container;
        }

        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
            try
            {
                if (controllerType != null)
                {
                    Lazy<object, object> export = _container.GetExports(controllerType, null, null).FirstOrDefault();

                    return null == export ? base.GetControllerInstance(requestContext, controllerType) : (IController)export.Value;
                }

                else
                {
                   throw new HttpException(404, "Action is not found");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override void ReleaseController(IController controller)
        {
            ((IDisposable)controller).Dispose();
        }
    }
}
