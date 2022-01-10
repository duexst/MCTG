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
    class BattleController : HttpController
    {
        private CardRepository m_cardRepository;
        private UserRepository m_userRepository;
        private Auth m_auth;
        private BattleQueue m_battleQ;

        public BattleController(UserRepository u, CardRepository c, Auth a, BattleQueue b)
        {
            m_userRepository = u;
            m_cardRepository = c;
            m_auth = a;
            m_battleQ = b;
        }

        public override MyHttpResponse Post(MyHttpRequest request)
        {
            var response = new MyHttpResponse();
            string token = request.GetHeaderValue("Authorization");

            if (!m_auth.IsAuthenticated(token))
            {
                response.SetStatus(HttpStatus.Forbidden, "Not Authorized");
                return response;
            }

            string id = m_auth.GetId(token);
            Player player = new();

            player.AccInfo = m_userRepository.Read(id);
            player.Deck = m_cardRepository.GetDeckById(id);
            player.Stack = m_cardRepository.GetStackById(id);

            if(player.Deck.Count < 4)
            {
                response.SetStatus(HttpStatus.BadRequest, "Bad Request");
                return response;
            }
           
            m_battleQ.QueuePlayer(player);

            response.SetStatus(HttpStatus.OK, "OK");
            return response;
        }
    }
}
