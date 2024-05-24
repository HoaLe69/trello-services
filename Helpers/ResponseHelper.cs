using Microsoft.AspNetCore.Mvc;

namespace trello_services.Helpers
{
    public static class ResponseHelper
    {
        public static IActionResult InternalServerError()
        {
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }
}
