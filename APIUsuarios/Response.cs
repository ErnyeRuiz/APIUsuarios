namespace APIUsuarios
{
    public static class Response
    {
        public static IResult BadRequestResponse(string message) =>
        Results.BadRequest(new { error = 400, mensaje = message });
        public static IResult InternalServerErrorResponse(string message) =>
          Results.Json(new { codigo = -1, mensaje = message }, statusCode: StatusCodes.Status500InternalServerError);
    }
}
