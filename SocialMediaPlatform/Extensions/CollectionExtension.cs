using SocialMediaPlatform.Models.Commons;

namespace SocialMediaPlatform.Extensions;

public static class CollectionExtension
{
    public static T Create<T>(this List<T> models, T model) where T : Auditable
    {
        var lastId = models.Count == 0 ? 1 : models.Last().Id + 1;
        model.Id = lastId;
        models.Add(model);
        return model;
    }
}
