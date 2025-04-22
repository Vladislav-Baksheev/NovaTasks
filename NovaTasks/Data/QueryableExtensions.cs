using NovaTasks.Models;

namespace NovaTasks.Data;

public static class QueryableExtensions
{
    // В дальнейшем, этот метод будет использоваться для проверки принадлежит ли Entity пользователю
    public static IQueryable<T> IsUserEntity<T>(this IQueryable<T> queryable, int? userId)
        where T : User // TODO: наследоваться надо не от модели целого пользователя, а от модели, в которой будет храниться id
    {
        return queryable.Where(entity => entity.UserId == userId);
    }
}