using BlazorMovies.Shared.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace BlazorMovies.SharedBackend.Helpers
{
    public static class QueryableExtensions
    {

        public async static Task<PaginatedResponse<List<T>>> GetPaginatedResponse<T>(
            this IQueryable<T> queryable,
            PaginationDTO paginationDTO)
        {
            double count = await queryable.CountAsync();
            var totalAmountOfPages = (int)Math.Ceiling(count / paginationDTO.RecordsPerPage);
            var records = await queryable.Paginate(paginationDTO).ToListAsync();
            var response = new PaginatedResponse<List<T>>();
            response.TotalAmountPages = totalAmountOfPages;
            response.Response = records;
            return response;
        }

        public static IQueryable<T> Paginate<T>(this IQueryable<T> queryable , PaginationDTO paginationDTO)
        {
            return queryable
                .Skip((paginationDTO.Page - 1) * paginationDTO.RecordsPerPage)
                .Take(paginationDTO.RecordsPerPage);
        }
    }
}
