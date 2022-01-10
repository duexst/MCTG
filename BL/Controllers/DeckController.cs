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
    class DeckController : HttpController
    {
        private CardRepository m_cardRepository;
        private Auth m_authUsers;

        public DeckController(CardRepository cR, Auth a)
        {
            m_cardRepository = cR;
            m_authUsers = a;
        }

        public override MyHttpResponse Get(MyHttpRequest request)
        {
            var response = new MyHttpResponse();

            if (!m_authUsers.IsAuthenticated(request.GetHeaderValue("Authorization")))
            {
                response.SetStatus(HttpStatus.Forbidden, "Not Authorized");
                return response;
            }

            List<Card> result = m_cardRepository.GetDeckById(m_authUsers.GetId(request.GetHeaderValue("Authorization")));
            string resJson = JsonConvert.SerializeObject(result, Formatting.Indented);

            response.SetStatus(HttpStatus.OK, "OK");
            if (result.Count() > 0)
            {
                response.AddHeader("Content-Type", "application/json");
                response.AddHeader("Content-Length", resJson.Length.ToString());
                response.Body = resJson;
            }

            return response;
        }

        public override MyHttpResponse Put(MyHttpRequest request)
        {
            var response = new MyHttpResponse();

            if (!m_authUsers.IsAuthenticated(request.GetHeaderValue("Authorization")))
            {
                response.SetStatus(HttpStatus.Forbidden, "Not Authorized");
                return response;
            }

            List<string> cardIds = JsonConvert.DeserializeObject<List<string>>(request.Body);

            if(cardIds.Count < 4)
            {
                response.SetStatus(HttpStatus.BadRequest, "Bad Request");
                return response;
            }

            if (!m_cardRepository.ResetDeck(m_authUsers.GetId(request.GetHeaderValue("Authorization")))){
                response.SetStatus(HttpStatus.BadRequest, "Bad Request");
                return response;
            }

            if (!m_cardRepository.SetDeck(cardIds))
            {
                response.SetStatus(HttpStatus.BadRequest, "Bad Request");
                return response;
            }

            response.SetStatus(HttpStatus.OK, "OK");
            return response;
        }
    }
}
