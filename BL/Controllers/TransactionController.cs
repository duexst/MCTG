using BL.Helpers;
using BL.ServerClasses;
using DAL;
using DAL.Repository;
using MODELS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Controllers
{
    class TransactionController : HttpController
    {
        private UserRepository m_userRepository;
        private CardRepository m_cardRepository;
        private PackageRepository m_packageRepository;
        private Auth m_auth;

        public TransactionController(UserRepository userRepo, CardRepository cardRepo, PackageRepository packageRepo, Auth a)
        {
            m_userRepository = userRepo;
            m_cardRepository = cardRepo;
            m_packageRepository = packageRepo;
            m_auth = a;
        }

        public override MyHttpResponse Post(MyHttpRequest request)
        {
            var resource = request.Path.Split("/");

            if (resource[1] == "packages")
            {
                return GetPackages(request);
            }

            return base.Post(request);
        }

        private MyHttpResponse GetPackages(MyHttpRequest request)
        {
            var response = new MyHttpResponse();
            string token = request.GetHeaderValue("Authorization");
            if (!m_auth.IsAuthenticated(request.GetHeaderValue("Authorization")))
            {
                response.SetStatus(HttpStatus.Forbidden, "Not Authorized");
                return response;
            }

            if(m_userRepository.GetCoins(m_auth.GetId(token)) < 5)
            {
                response.SetStatus(HttpStatus.BadRequest, "Not enough money");
            }

            Package result;
            if ((result = m_packageRepository.Read(request.Body)) == null)
            {
                response.SetStatus(HttpStatus.Notfound, "Not Found");
                return response;
            }

            User user = m_userRepository.Read(m_auth.GetId(token));
            user.Coins -= 5;
            if (!m_userRepository.Update(user))
            {
                response.SetStatus(HttpStatus.BadRequest, "couldnt update coins");
                return response;
            }

            foreach (Card card in result.Cards)
            {
                card.Owner = user.Guid;
                card.Uid = Guid.NewGuid().ToString();
                if (!m_cardRepository.Create(card))
                {
                    response.SetStatus(HttpStatus.BadRequest, "Bad Request");
                    return response;
                }
            }



            response.SetStatus(HttpStatus.Created, "Created");
            return response;
        }
    }
}
