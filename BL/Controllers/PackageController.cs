using BL.Helpers;
using BL.ServerClasses;
using DAL.Repository;
using MODELS;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Controllers
{
    class PackageController : HttpController
    {
        private PackageRepository m_packageRepository;
        private Auth m_authUsers;
        public PackageController(PackageRepository packageRepository, Auth auth)
        {
            this.m_packageRepository = packageRepository;
            this.m_authUsers = auth;
        }

        public override MyHttpResponse Post(MyHttpRequest request)
        {
            var response = new MyHttpResponse();

            if (!m_authUsers.IsAdmin(request.GetHeaderValue("Authorization")))
            {
                response.SetStatus(HttpStatus.Forbidden, "Not Authorized");
                return response;
            }

            Package package = JsonConvert.DeserializeObject<Package>(request.Body);

            if(package.Cards.Count() < 0)
            {
                response.SetStatus(HttpStatus.BadRequest, "Bad Request");
                return response;
            }


            //Console.WriteLine("Deck Contents: ");
            //package.Cards.ForEach(card => Console.WriteLine(card.Uid, card.Owner));

            if (m_packageRepository.Create(package))
            {
                response.SetStatus(HttpStatus.OK, "OK");
                return response;
            }

            response.SetStatus(HttpStatus.BadRequest, "Bad Request");
            return response;
        }
    }
}
