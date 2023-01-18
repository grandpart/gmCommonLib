using Newtonsoft.Json;

namespace Zephry
{
    /// <summary>
    /// A set of static methods that serialize objects to JSON and deserialize JSON to objects.
    /// </summary>
    public static class JsonUtils
    {
        /// <summary>
        /// Object Extension Method returns a string in JSON format.
        /// </summary>
        /// <param name="aObject">An object.</param>
        /// <returns>
        /// A Json representation of aObject as a string
        /// </returns>
        public static string SerializeToJson(this object aObject)
        {
            return JsonConvert.SerializeObject(aObject);
        }

        /// <summary>
        /// String in JSON format Extension Method returns an Object.
        /// </summary>
        /// <typeparam name="T">The type of the object to be deserialized</typeparam>
        /// <param name="aArgument">a String in JSON format</param>
        /// <returns>
        /// An object of type T
        /// </returns>
        public static T DeserializeFromJson<T>(this string aArgument) where T : class
        {
            return JsonConvert.DeserializeObject<T>(aArgument);
        }
    }    
}
