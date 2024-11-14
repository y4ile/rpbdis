namespace lab3.Services
{
    public interface ICachedPlansService
    {
        public IEnumerable<T> GetTableData<T>(string cacheKey) where T : class;
        public void PreloadCache();
    }
}
