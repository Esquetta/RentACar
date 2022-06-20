namespace RentACar.UI.Services.Abstract
{
    public interface ICookieeService<T>
    {
        void SetValue(string key,T value);
        T GetObject(string key);

        
    }
}
