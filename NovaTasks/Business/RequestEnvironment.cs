using NovaTasks.Models;

namespace NovaTasks.Business;

public class RequestEnvironment // Потом этот класс будет использоваться в middleware, для заполнения id пользователя
{
    private int? _userId; 
    
    public int UserId
    {
        get => _userId ?? throw new Exception("Извините, но идентификатор пользователя не указан.");
        set => _userId = value;
    }

    public User? AuthUser { get; set; }
}