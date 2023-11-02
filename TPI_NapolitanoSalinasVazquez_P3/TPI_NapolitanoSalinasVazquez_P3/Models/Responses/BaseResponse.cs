namespace TPI_NapolitanoSalinasVazquez_P3.Models.Responses
{
    public class BaseResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }

        public BaseResponse()
        {
            Message = ""; // Inicializar con una cadena vacía, sino da error xq puede ser null.
        }

    }
}
