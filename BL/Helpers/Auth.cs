using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Helpers
{
    public class Auth
    {
        private Dictionary<string, AuthUser> m_Authenticated;
        private static readonly Object m_ListLock = new Object();
        private string m_admin = "198a01f1-b12d-47d2-8c2a-8172d61ff577";

        public Auth()
        {
            this.m_Authenticated = new Dictionary<string, AuthUser>();
            this.m_Authenticated.Add("AutomatedTest", new AuthUser("b298eb4f-7b90-449a-9628-03a422f9eba3", "Auto1"));
            this.m_Authenticated.Add("AutomatedTest2", new AuthUser("87ace846-0427-4736-ad4d-73fdfedf9930", "Auto3"));
        }
        public bool AddAuthUser(string key, AuthUser authUser)
        {
            lock (m_ListLock)
            {
                AuthUser f;
                if (m_Authenticated.TryGetValue(key, out f))
                    return false;

                m_Authenticated.Add(key, authUser);
                return true;
            }
        }

        public bool DelAuthUser(string token)
        {
            lock (m_ListLock)
            {
                return m_Authenticated.Remove(token);

            }
        }

        public AuthUser GetAuthUser(string token)
        {
            lock (m_ListLock)
            {
                if(m_Authenticated.ContainsKey(token))
                    return m_Authenticated[token];

                return null;
            }
        }

        public void ListAllUsers()
        {
            lock (m_ListLock)
            {
                Console.WriteLine("Authenticated users:");
                m_Authenticated.ToList().ForEach(x => Console.WriteLine($"key: {x.Key} value: {x.Value.uid}, {x.Value.username}"));
            }
        }

        public bool IsAuthenticated(string token)
        {
            lock (m_ListLock)
            {
                AuthUser f;
                if (m_Authenticated.TryGetValue(token, out f))
                    return true;

                return false;
            }
        }

        public bool AlreadyExists(string name)
        {
            lock (m_ListLock)
            {
                foreach (KeyValuePair<string, AuthUser> entry in m_Authenticated)
                {
                    if (entry.Value.username == name)
                        return false;
                }
                return false;
            }
        }

        public bool IsAdmin(string token)
        {
            lock (m_ListLock)
            {
                if (token == m_admin)
                    return true;

                return false;
            }
        }

        public string GetId(string token)
        {
            lock (m_ListLock)
            {
                AuthUser id;

                if (m_Authenticated.TryGetValue(token, out id))
                {
                    return id.uid;
                }

                return "";
            }
        }

        public string GetUsername(string token)
        {
            lock (m_ListLock)
            {
                AuthUser id;

                if (m_Authenticated.TryGetValue(token, out id))
                {
                    return id.username;
                }

                return "";
            }
        }
    }
}
