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
    class UserController : HttpController
    {
        private UserRepository m_userRepository;
        private Auth m_authUsers;
        public UserController(UserRepository userRepository, Auth a)
        {
            m_userRepository = userRepository;
            m_authUsers = a;
        }

        public override MyHttpResponse Get(MyHttpRequest request)
        {
            var resource = request.Path.Split("/");
            var response = new MyHttpResponse();
            string token = request.GetHeaderValue("Authorization");
            if (!m_authUsers.IsAuthenticated(token) || resource[2] != m_authUsers.GetUsername(token))
            {
                response.SetStatus(HttpStatus.Forbidden, "Not Authorized");
                return response;
            }

            User userdata = m_userRepository.Read(m_authUsers.GetId(token));
            if(userdata == null)
            {
                response.SetStatus(HttpStatus.Notfound, "Not found");
                return response;
            }
            string result = JsonConvert.SerializeObject(userdata);
            response.SetStatus(HttpStatus.OK, "ok");
            response.AddHeader("Content-Type", "application/json");
            response.AddHeader("Content-Length", result.Length.ToString());
            response.Body = result;
            return response;
        }

        public override MyHttpResponse Post(MyHttpRequest request)
        {
            User user = JsonConvert.DeserializeObject<User>(request.Body);
            var response = new MyHttpResponse();

            if(m_userRepository.FindUserByName(user.Username) != null || m_userRepository.Read(user.Guid) != null)
            {
                response.SetStatus(HttpStatus.Conflict, "Conflict");
                return response;
            }

            if (m_userRepository.Create(user)) {

                response.SetStatus(HttpStatus.Created, "Created"); 
                response.AddHeader("Content-Type", "application/json");
                response.AddHeader("Content-Length", "0");

                return response;
            }

            response.SetStatus(HttpStatus.BadRequest, "Bad Request");
            return response;
        }

        public override MyHttpResponse Delete(MyHttpRequest request)
        {
            var response = new MyHttpResponse();

            if (m_userRepository.Read(request.Body) == null)
            {
                response.SetStatus(HttpStatus.Notfound, "Not Found");
                return response;
            }

            if (m_userRepository.Delete(request.Body)){
                response.SetStatus(HttpStatus.OK, "OK");
                return response;
            }

            response.SetStatus(HttpStatus.BadRequest, "Bad Request");
            return response;
        }

        public override MyHttpResponse Put(MyHttpRequest request)
        {
            var resource = request.Path.Split("/");
            var response = new MyHttpResponse();
            string token = request.GetHeaderValue("Authorization");

            if (!m_authUsers.IsAuthenticated(token) || resource[2] != m_authUsers.GetUsername(token))
            {
                response.SetStatus(HttpStatus.Forbidden, "Not Authorized");
                return response;
            }

            if(request.Body.Length == 0)
            {
                response.SetStatus(HttpStatus.BadRequest, "Bad Request");
                return response;
            }
            User userdata = JsonConvert.DeserializeObject<User>(request.Body);
            userdata.Guid = m_authUsers.GetId(token);

            if (!m_userRepository.RestrictedUpdate(userdata)){
                response.SetStatus(HttpStatus.BadRequest, "Bad Request");
            }

            response.SetStatus(HttpStatus.OK, "OK");
            return response;
        }

    }
}
