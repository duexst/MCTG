using MODELS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IRepository<T>
    {
        public bool Create(T data);
        public T Read(string id);
        public List<T> ReadAll();
        public bool Update(T data);
        public bool Delete(string id);

    }
}
