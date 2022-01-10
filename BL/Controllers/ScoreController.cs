using BL.Helpers;
using BL.ServerClasses;
using DAL;
using MODELS;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Controllers
{
    class ScoreController : HttpController
    {
        private UserRepository m_userRepository;
        private Auth m_authUsers;

        public ScoreController(UserRepository ur, Auth a)
        {
            m_authUsers = a;
            m_userRepository = ur;
        }

        public override MyHttpResponse Get(MyHttpRequest request)
        {
            var response = new MyHttpResponse();
            var resource = request.Path.Split("/");
            string token = request.GetHeaderValue("Authorization");

            if (!m_authUsers.IsAuthenticated(token))
            {
                response.SetStatus(HttpStatus.Forbidden, "Not Authorized");
                return response;
            }

            if (resource[2] == m_authUsers.GetUsername(token))
            {
                return GetScore(m_authUsers.GetId(token));
            }

            if (resource[2] == "score")
            {
                return GetScoreboard();
            }

            response.SetStatus(HttpStatus.BadRequest, "BadRequest");
            return response;
        }

        private MyHttpResponse GetScoreboard()
        {
            var response = new MyHttpResponse();
            List<UserScore> board = new();
            board = m_userRepository.GetScoreBoard();
            string res = JsonConvert.SerializeObject(board);

            response.SetStatus(HttpStatus.OK, "OK");
            response.Body = res;
            return response;
        }

        private MyHttpResponse GetScore(string id)
        {
            var response = new MyHttpResponse();
            UserScore score = m_userRepository.GetUserScore(id);
            string res = JsonConvert.SerializeObject(score);

            response.SetStatus(HttpStatus.OK, "OK");
            response.Body = res;
            return response;
        }
    }
}
