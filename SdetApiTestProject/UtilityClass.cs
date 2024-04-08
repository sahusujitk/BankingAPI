using Newtonsoft.Json;

namespace SdetApiTestProject
{
    public static class UtilityClass
    {

        public static TEntity DeserializeObject<TEntity>(this string stringToDeserialize)

        {

            if (string.IsNullOrEmpty(stringToDeserialize))

            {

                return default(TEntity);

            }



            using (var sr = new StringReader(stringToDeserialize))

            {

                using (var jsonReader = new JsonTextReader(sr))

                {
                    JsonSerializer serializer = new JsonSerializer();
                    var entity = serializer.Deserialize<TEntity>(jsonReader);



                    return entity;

                }

            }
        }
    }
}
