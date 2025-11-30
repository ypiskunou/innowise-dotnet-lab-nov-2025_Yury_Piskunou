using Microsoft.EntityFrameworkCore;
using UserService.Shared.RequestFeatures;

namespace UserService.Repository.Extensions;

public static class RepositoryExtensions
{
    public static async Task<PagedList<T>> ToPagedListAsync<T>(
        this IQueryable<T> source, 
        int pageNumber, 
        int pageSize)
    {
        var count = await source.CountAsync();
        
        var items = await source
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedList<T>(items, count, pageNumber, pageSize);
    }
}