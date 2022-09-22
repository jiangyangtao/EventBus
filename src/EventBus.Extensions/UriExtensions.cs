

namespace EventBus.Extensions
{
    public static class UriExtensions
    {
        public static string GetSchemeHost(this Uri uri)
        {
            if (uri == null) return string.Empty;

            return $"{uri.Scheme}://{uri.Host}";
        }
    }
}
