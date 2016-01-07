using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Cache;
using NHibernate.Cfg;
namespace NhibernateDataAccess
{
    public sealed class NHibernateSessionManager
    {
        private ISessionFactory sessionFactory;


        private NHibernateSessionManager()
        {
            this.InitSessionFactory();
        }



        public ISession GetSession()
        {
            return this.GetSession(null);
        }



        private ISession GetSession(IInterceptor interceptor)
        {
                ISession innerSession;

                if (interceptor != null)
                {
                    innerSession = this.sessionFactory.OpenSession(interceptor);
                }
                else
                {
                    innerSession = this.sessionFactory.OpenSession();
                }

            return innerSession;
        }



      

        private void InitSessionFactory()
        {
            this.sessionFactory = new Configuration().Configure().BuildSessionFactory();
        }

    

 
  

        public static NHibernateSessionManager Instance
        {
            get
            {
                return Nested.NHibernateSessionManager;
            }
        }

        private class Nested
        {
            internal static readonly NHibernateSessionManager NHibernateSessionManager = new NHibernateSessionManager();
        }
    }
}
