using Microsoft.Extensions.Configuration;
using System;
using System.Globalization;
using System.IO;

namespace CadastroMotorista.Infra.Shared.Utils
{
    public static class Constants
    {    
        private static IConfigurationBuilder GetConfigurationBuilder()
        {
            return new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json");
        }   

        public static string GetConnectionString()
        {
            var builder = GetConfigurationBuilder();
            var config = builder.Build();

            return config[$"ConnectionStrings:Connection"];
        }

        public static string GetParameterByID(string key)
        {
            var builder = GetConfigurationBuilder();
            var config = builder.Build();

            return config[$"Parameters:{key}"];
        }

        public static string GetAdquirenteById(string adquirente)
        {
            var builder = GetConfigurationBuilder();
            var config = builder.Build();
            return config[$"Adquirentes:{adquirente.ToUpper().Trim()}"];
        }   
       
        public static string Secret()
        {
            var builder = GetConfigurationBuilder();
            var config = builder.Build();

            return config[$"Seguranca:Token"].ToString();
        }

        public static string NomeBaseMongoDB()
        {
            return GetParameterByID("NameDataBaseMongoDB");
        }

        public static string NomeBaseLog()
        {
            return GetParameterByID("NomeBaseLog");
        }

        public static string PathLog()
        {
            return GetParameterByID("PathLog");
        }

        public static string GetNameAdquirente()
        {
            return GetParameterByID("Adquirente");
        }

        public static string WriteDirectoryLog(string directory)
        {
            CultureInfo culture = new CultureInfo("pt-BR");
            DateTimeFormatInfo dtfi = culture.DateTimeFormat;
            string pathYear = string.Empty;
            pathYear = PathLog() + DateTime.Now.Year;
            string pathMonth2 = string.Empty;
            pathMonth2 = pathYear + "\\" + culture.TextInfo.ToTitleCase(dtfi.GetMonthName(DateTime.Now.Month));
            string pathDay2 = string.Empty;
            pathDay2 = pathMonth2 + "\\" + DateTime.Now.Day + "\\";
            string pathDirectory2 = string.Empty;
            pathDirectory2 = pathDay2 + "\\" + directory + "\\";

            if(!Directory.Exists(pathYear))
            {
                try
                {
                    Directory.CreateDirectory(pathYear);
                }
                catch (Exception)
                {                    
                }
            }

            if(!Directory.Exists(pathMonth2))
            {
                try
                {
                    Directory.CreateDirectory(pathMonth2);
                }
                catch (Exception)
                {                    
                }
            }

            if(!Directory.Exists(pathDay2))
            {
                try
                {
                    Directory.CreateDirectory(pathDay2);
                }
                catch(Exception)
                {                    
                }
            }

            if(!Directory.Exists(pathDirectory2))
            {
                try
                {
                    Directory.CreateDirectory(pathDirectory2);
                    return pathDirectory2;
                }
                catch(Exception)
                {
                    return pathDirectory2;
                }
            }

            return pathDirectory2;
        }
    }
}