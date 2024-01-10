using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace QrCodeService.Utilities.Extension
{
    public static class JsonExtension
    {
        private static readonly JsonSerializerSettings DefaultJsonSetting = new JsonSerializerSettings().Configure();

        /// <summary>
        /// Serialize object to json
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static async Task<string> SerializeJsonAsync(this object value)
        {
            if (value == null)
            {
                return await Task.FromResult(string.Empty);
            }

            return await Task.Run(() => JsonConvert.SerializeObject(value, DefaultJsonSetting));
        }

        /// <summary>
        /// Deserialize string as json to object
        /// </summary>
        /// <param name="str"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static async Task<T> DeserializeJsonAsync<T>(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                if (typeof(T).GetConstructor(Type.EmptyTypes) != null)
                {
                    return await Task.FromResult(default(T));
                }

                throw new Exception("default string is not valid");
            }

            var obj = Convert.ChangeType(JsonConvert.DeserializeObject<T>(str, DefaultJsonSetting), typeof(T));

            if (obj is null)
            {
                throw new Exception("default string is not valid");
            }

            var validationCtx = new ValidationContext(obj);
            Validator.ValidateObject(obj, validationCtx);

            return await Task.Run(() => (T)obj);
        }
    }
}