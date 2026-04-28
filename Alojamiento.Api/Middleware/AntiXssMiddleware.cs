using Microsoft.AspNetCore.Http;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Alojamiento.Api.Middleware
{
    public class AntiXssMiddleware
    {
        private readonly RequestDelegate _next;

        public AntiXssMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Simple XSS check on query string
            if (!string.IsNullOrEmpty(context.Request.QueryString.Value))
            {
                if (IsDangerousString(context.Request.QueryString.Value))
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await context.Response.WriteAsync("XSS attack detected in query string.");
                    return;
                }
            }

            // Simplification: In a real app we'd also buffer and check the body.
            // For now, this mitigates basic query string XSS.
            
            await _next(context);
        }

        private bool IsDangerousString(string input)
        {
            // Regex to detect basic script tags and common XSS vectors
            var match = Regex.Match(input, @"(<script|\%3Cscript|javascript:|onerror=)", RegexOptions.IgnoreCase);
            return match.Success;
        }
    }
}
