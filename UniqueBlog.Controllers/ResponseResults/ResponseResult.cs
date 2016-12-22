using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniqueBlog.Controllers.ResponseResults
{
    public class ResponseJsonResult
    {
        public bool Result { get; private set; }

        public object Data { get; set; }

        public string Message { get; set; }

        public ResponseJsonResult(bool isSucess)
        {
            this.Result = isSucess;
        }
    }
}
