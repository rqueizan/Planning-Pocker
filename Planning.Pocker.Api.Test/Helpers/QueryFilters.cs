using Planning.Pocker.Api.NoAuth.Handlers;
using System.Linq;

namespace Planning.Pocker.Api.Test
{
    public static class QueryFilters
    {
        public static string Filter(this ListarCartasQuery listarCartasQuery)
        {
            var arguments = new[] { (nameof(ListarCartasQuery.Min), listarCartasQuery.Min), (nameof(ListarCartasQuery.Max), listarCartasQuery.Max) };
            return $"?{string.Join("&", arguments.Where(a => a.Item2 != 0).Select(a => $"{a.Item1}={a.Item2}"))}";
        }
    }
}
