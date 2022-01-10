using BL.Helpers;
using BL.ServerClasses;
using DAL;
using MODELS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Controllers
{
    class SessionController : HttpController
    {
        private UserRepository m_userRepository;
        private Auth m_authUsers;
        public SessionController(UserRepository userRepository, Auth authUsers)
        {
            this.m_userRepository = userRepository;
            this.m_authUsers = authUsers;
        }

        public override MyHttpResponse Post(MyHttpRequest request)
        {
            var response = new MyHttpResponse();
            string authString;

            m_authUsers.ListAllUsers();

            //Check if authentication header is set
            if((authString = request.GetHeaderValue("Authorization")) == "")
            {
                response.SetStatus(HttpStatus.Forbidden, "Authentication Failed");
                return response;
            }

            var credentials = authString.Split(":");
            Console.WriteLine($"username: {credentials[0]}");
            Console.WriteLine($"password: {credentials[1]}");


            //Check if user is already logged in
            if (m_authUsers.AlreadyExists(credentials[0])){
                response.SetStatus(HttpStatus.Conflict, "Conflict");
                return response;
            }

            User user = m_userRepository.FindUserByName(credentials[0]);
            //Check if user was found
            if(user == null)
            {
                response.SetStatus(HttpStatus.BadRequest, "Bad Request");
                return response;
            }

            if(user.Username == credentials[0] && user.Password == credentials[1])
            {
                string token = Guid.NewGuid().ToString();
                m_authUsers.AddAuthUser(token, new AuthUser(user.Guid, user.Username));
                response.SetStatus(HttpStatus.OK, "OK");
                response.Body = $"{{'AuthenticationToken': '{token}'}}";
                response.AddHeader("Content-Type", "application/json");
                response.AddHeader("Content-Length", response.Body.Length.ToString());

                m_authUsers.ListAllUsers();

                return response;
            }

            response.SetStatus(HttpStatus.BadRequest, "Bad Request");
            return response;
        }
    }
}
