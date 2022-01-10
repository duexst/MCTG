using BL.ServerClasses;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Controllers
{
    class HttpController
    {
        public virtual MyHttpResponse Get(MyHttpRequest request)
        {
            var response = new MyHttpResponse();
            response.Status = (int)HttpStatus.BadRequest;

            return response;
        }

        public virtual MyHttpResponse Post(MyHttpRequest request)
        {
            var response = new MyHttpResponse();
            response.Status = (int)HttpStatus.BadRequest;

            return response;
        }

        public virtual MyHttpResponse Delete(MyHttpRequest request)
        {
            var response = new MyHttpResponse();
            response.Status = (int)HttpStatus.BadRequest;

            return response;
        }

        public virtual MyHttpResponse Put(MyHttpRequest request)
        {
            var response = new MyHttpResponse();
            response.Status = (int)HttpStatus.BadRequest;

            return response;
        }
    }
}
