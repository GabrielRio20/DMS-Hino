namespace DMS_Hino.Helpers
{
    public static class DocumentHelper
    {
        public static void MapFrom(this object model, object source)
        {
            var modelProperties = model.GetType().GetProperties();
            var sourceProperties = source.GetType().GetProperties();

            foreach (var sourceProperty in sourceProperties)
            {
                if (sourceProperty.GetCustomAttributes(true).Any(x => x.GetType().Name == "NotMappedToModelAttribute")) continue;

                if (modelProperties.Any(x => x.Name == sourceProperty.Name && x.PropertyType == sourceProperty.PropertyType))
                {
                    var value = sourceProperty.GetValue(source, null);

                    var modelProperty = modelProperties.First(x => x.Name == sourceProperty.Name && x.PropertyType == sourceProperty.PropertyType);

                    modelProperty.SetValue(model, value, null);
                }
            }
        }


    }
}
