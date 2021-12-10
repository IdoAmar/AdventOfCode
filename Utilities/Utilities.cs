using System.Net;
using System.Threading.Tasks;

namespace Utilities
{
    public static class ScrapingUtilities
    {
        public static async Task<string> getInputFromUrl(string url, string cookieValue)
        {
            var cookie = "session=" + cookieValue;
            WebClient wb = new WebClient();
            wb.Headers.Add(HttpRequestHeader.Cookie, cookie);
            return await wb.DownloadStringTaskAsync(url);
        }
    }
}
