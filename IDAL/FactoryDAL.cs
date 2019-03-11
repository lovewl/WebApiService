using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Reflection;

namespace IDAL
{
    public class FactoryDAL
    {
        private static readonly IConfiguration config = new ConfigurationBuilder().SetBasePath(Environment.CurrentDirectory).AddJsonFile("appsettings.json").Build();
        public static T CreateDAL<T>() where T : class
        {
            var dbType = config.GetSection("DatabaseType").Value;
            var dalPath = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).FullName, @"SqlServerDAL\bin\Debug\netcoreapp3.0\SqlServerDAL.dll");
            switch (dbType)
            {
                case "Oracle":
                    dalPath = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).FullName, @"OracleDAL\bin\Debug\netcoreapp3.0\OracleDAL.dll");
                    break;
                default:
                    break;
            }

            foreach (var item in Assembly.LoadFrom(dalPath).GetTypes())
            {
                if (item.GetInterface(typeof(T).Name) != null)
                {
                    return Activator.CreateInstance(item) as T;
                }
            }

            throw new Exception("Create DAL Failed!");
        }
    }
}
