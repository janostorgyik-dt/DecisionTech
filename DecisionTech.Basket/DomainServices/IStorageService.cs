namespace DecisionTech.Basket.DomainServices
{
    /// <summary>
    /// Responsible for storing things
    /// </summary>
    public interface IStorageService
    {
        void Put<T>(string key, T value);

        T Get<T>(string key) where T : class;
    }
}
