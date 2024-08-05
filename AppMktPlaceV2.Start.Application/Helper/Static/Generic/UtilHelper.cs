#region IMPORT
using System.Reflection;
using System.Text.RegularExpressions;
using System.Text;
using Newtonsoft.Json;
using Microsoft.Security.Application;
using Microsoft.AspNetCore.Http;
#endregion IMPORT

namespace AppMktPlaceV2.Start.Application.Helper.Static.Generic
{
    public static class UtilHelper
    {
        #region STRING FORMATTER
        public static T TrasnformObjectPropValueToUpper<T>(this T obj)
        {
            if (obj == null)
                throw new ArgumentNullException("Parameter cannot be null");

            Type type = obj.GetType();
            List<PropertyInfo> props = new(type.GetProperties());
            foreach (PropertyInfo prop in props)
            {
                if (prop.GetValue(obj) != null && prop.PropertyType == typeof(string))
                    prop.SetValue(obj, prop.GetValue(obj)?.ToString()?.Trim().ToUpper().RemoveInjections());
            }

            return obj;
        }

        public static string FormatCNPJ(this string cnpj)
        {
            if (cnpj == null || cnpj.Length != 14) return string.Empty;
            if (!long.TryParse(cnpj, out long result)) return string.Empty;
            return result.ToString(@"00\.000\.000\/0000\-00");
        }

        public static string FormatCPF(this string cnpj)
        {
            if (cnpj == null || cnpj.Length != 14) return string.Empty;
            if (!long.TryParse(cnpj, out long result)) return string.Empty;
            return result.ToString(@"000\.000\.000\-00");
        }
        #endregion

        #region SANITIZER REGION
        public static string RemoveInjections(this string param)
        {
            return !string.IsNullOrEmpty(param) ? Regex.Replace(Sanitizer.GetSafeHtmlFragment(param), "[^0-9a-zA-Z ]+", "").Trim() : string.Empty;
        }

        public static string RemoveAllSpecialCaracter(this string word)
        {
            return Regex.Replace(word.Trim(), "[^0-9a-zA-Z ]+", "");
        }

        public static string RemoveAllSpecialCaracterFromEmail(this string word)
        {
            return Regex.Replace(Sanitizer.GetSafeHtmlFragment(word.Trim()), "[^0-9a-zA-Z ]+.+@+_+&+%+$+!+#+-", "");
        }
        #endregion

        #region LOG FORMATTER
        public static string FormatLogInformationMessage(string message, Guid? userId = null, HttpRequest request = null, HttpResponse response = null, string stringData = null, bool isHeaderOrFooter = false)
        {
            var stringBuild = new StringBuilder();

            // STANDAT LOG MESSAGE
            stringBuild.Append(!isHeaderOrFooter ? "\n--------------------------------------------------------------------------------------------------------" : "\n========================================================================================================");
            // stringBuild.Append($"[Date: {DateTime.Now.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss")}]");
            if (!string.IsNullOrEmpty(message)) stringBuild.Append($"\n Message:        {message}");
            if (request != null) stringBuild.Append($"\n Http Method:    {request.Method.ToString()}");
            if (request != null) stringBuild.Append($"\n Request:        {request.Scheme}://{request.Host.ToString()}{request.Path.ToString()}");
            if (userId.HasValue) stringBuild.Append($"\n UserId:         {userId.Value.ToString()}");
            if (response != null) stringBuild.Append($"\n Response:       {response.StatusCode.ToString()}");
            if (!string.IsNullOrEmpty(stringData)) stringBuild.Append($"\n JsonData:       {stringData}");
            stringBuild.Append(!isHeaderOrFooter ? "\n--------------------------------------------------------------------------------------------------------\n" : "\n========================================================================================================\n");

            return stringBuild.ToString();
        }


        #endregion

        #region JSON CONVERTER
        public static string ConverteObjectParaJSon<T>(T obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
        public static T ConverteJSonParaObject<T>(string jsonString)
        {
            return JsonConvert.DeserializeObject<T>(jsonString);
        }
        #endregion
    }
}
