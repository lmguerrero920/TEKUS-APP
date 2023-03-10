using System;

namespace Tekus.WebAPI.Errors
{
    public class CodeErrorResponse
    {
        public CodeErrorResponse(int statusCode,string message=null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageStatusCode(statusCode);

        }

        private string GetDefaultMessageStatusCode(int statusCode)
        {
            return statusCode switch
            {
                0 => "Error,No se ha podido procesar la solicitud",
                400 => "El Request enviado tiene errores",
                401 => "No tienes Autorización para este recurso",
                404 => "El recurso no se encuentra o no se halla el item buscado",
                500 => "Error en el Servidor",
                _ => null
            };
        }

        public int StatusCode { get; set; }
        public string  Message { get; set; }
    }
}
