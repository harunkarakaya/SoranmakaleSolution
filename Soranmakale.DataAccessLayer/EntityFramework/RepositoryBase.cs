using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soranmakale.DataAccessLayer.EntityFramework
{
    public class RepositoryBase
    {
        protected static DatabaseContext context;
        protected static object locksync = new object();

        public RepositoryBase()
        {
            CreateContext();
        }

        private void CreateContext()
        {
            if(context == null)
            {
                lock(locksync)
                {
                    if (context == null)
                    {
                        context = new DatabaseContext();
                    }
                }
            }
        }


    }
}
