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
    class CardController : HttpController
    {
        private CardRepository m_cardRepository;
        private Auth m_authUsers;
        public CardController(CardRepository cardRepository, Auth auth)
        {
            this.m_cardRepository = cardRepository;
            this.m_authUsers = auth;
        }

        public override MyHttpResponse Get(MyHttpRequest request)
        {
            var response = new MyHttpResponse();

            if (!m_authUsers.IsAuthenticated(request.GetHeaderValue("Authorization")))
            {
                response.SetStatus(HttpStatus.Forbidden, "Not Authorized");
                return response;
            }

            List<Card> result = m_cardRepository.GetStackById(m_authUsers.GetId(request.GetHeaderValue("Authorization")));
            string resJson = JsonConvert.SerializeObject(result, Formatting.Indented);

            response.SetStatus(HttpStatus.OK, "OK");
            if (result.Count() > 0) {
                response.AddHeader("Content-Type", "application/json");
                response.AddHeader("Content-Length", resJson.Length.ToString());
                response.Body = resJson;
            }

            return response;
        }

        public override MyHttpResponse Post(MyHttpRequest request)
        {
            var response = new MyHttpResponse();

            if (!m_authUsers.IsAuthenticated(request.GetHeaderValue("Authorization")))
            {
                response.SetStatus(HttpStatus.Forbidden, "Not Authorized");
                return response;
            }

            List<Card> cards = JsonConvert.DeserializeObject<List<Card>>(request.Body);

            foreach(Card card in cards)
            {
                if (!m_cardRepository.Create(card)){
                    response.SetStatus(HttpStatus.BadRequest, "Bad Request");
                    return response;
                }
            }

            response.SetStatus(HttpStatus.Created, "Created");
            return response;
        }
    }
}
