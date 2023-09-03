using SeatManagementConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SeatManagementConsole.Managers
{
    public class EntityManager<T> : IEntityManager<T> where T : class
    {
        private readonly IAPIService<T> _apiObj;

        public EntityManager(string ep)
        {
            _apiObj = new APIService<T>(ep);
        }

        public int Add(T obj)
        {
            var id = _apiObj.PostData(obj);
            return id;
        }

        public List<T> Get()
        {
            var list = _apiObj.GetData();
            return list;
        }
    }
}