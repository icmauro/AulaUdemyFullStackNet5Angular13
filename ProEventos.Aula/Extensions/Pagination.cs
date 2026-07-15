using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace ProEventos.Aula.Extensions
{
    public static class Pagination
    {
        public static void AddPagination(this HttpResponse response, int currentPage, int itemsPerPage , int totalItens ,int totalPages)
        {
           response.Headers.Add("X-Pagination", JsonSerializer.Serialize(new
           {
               currentPage,
               itemsPerPage,
               totalItens,
               totalPages
           }));

           response.Headers.Add("Access-Control-Expose-Headers", "X-Pagination");
        }
    }
}
