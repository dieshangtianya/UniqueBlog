using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniqueBlog.Infrastructure.MEF
{
    public static class MEFConfiguration
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
    }
}
