using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text.Json;

namespace ChefRisingStar.Helpers
{
    public class JsonFileLoader
    {
        internal static T[] GetCollection<T>(string fileName)
        {
            T[] data = null;

            try
            {
                var assembly = typeof(Views.RecipeDetailPage).GetTypeInfo().Assembly;
                //string[] resources = assembly.GetManifestResourceNames();
                Stream stream = assembly.GetManifestResourceStream($"ChefRisingStar.sample.{fileName}");

                using (var reader = new StreamReader(stream))
                {
                    var json = reader.ReadToEnd();
                    data = JsonSerializer.Deserialize<T[]>(json);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading {fileName}: {ex}");
                throw;
            }

            return data;
        }

    }
}
