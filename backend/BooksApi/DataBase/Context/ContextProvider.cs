namespace BooksApi.DataBase.Context
{
    public class ContextProvider
    {
        public MyContext GetContext()
        {
            return new MyContext();
        }
    }
}