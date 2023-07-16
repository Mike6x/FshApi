using FSH.WebApi.Application.Common.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System.Reflection;

namespace FSH.WebApi.Infrastructure.Common.Services;

public class NewtonSoftService : ISerializerService
{
    public string Serialize<T>(T obj)
    {
        return JsonConvert.SerializeObject(obj, new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            NullValueHandling = NullValueHandling.Ignore,
            Converters =
            [
                new StringEnumConverter { NamingStrategy = new DefaultNamingStrategy() }

                // Change 20240417
                // new StringEnumConverter() { CamelCaseText = true }
            ]
        });
    }

    public string Serialize<T>(T obj, Type type)
    {
        return JsonConvert.SerializeObject(obj, type, new());
    }

    //// Change 20240511
    public T Deserialize<T>(string jsonString)
    {
        return JsonConvert.DeserializeObject<T>(jsonString, new JsonSerializerSettings()
        {
            ContractResolver = new PrivateResolver(),
            ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor
        })!;
    }
}

//// Change 20240511
public class PrivateResolver : DefaultContractResolver
{
    protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
    {
        var prop = base.CreateProperty(member, memberSerialization);
        if (!prop.Writable)
        {
            var property = member as PropertyInfo;
            prop.Writable = property?.GetSetMethod(true) != null;

            // var hasPrivateSetter = property?.GetSetMethod(true) != null;
            // prop.Writable = hasPrivateSetter;
        }

        return prop;
    }
}