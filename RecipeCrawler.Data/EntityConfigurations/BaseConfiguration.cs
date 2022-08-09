using Microsoft.EntityFrameworkCore;

namespace RecipeCrawler.Data.EntityConfigurations
{
    public abstract class BaseConfiguration
    {
        public abstract void CreateEntityModel(ModelBuilder builder);
    }
}
