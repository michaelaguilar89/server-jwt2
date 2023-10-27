namespace WebApiClientes.Models.Dto
{
    public class ResponseDto
    {
        public bool IsSuccess { get; set; }

        public object Result { get; set; }
                    
        public string DisplayMessages { get; set; }

        public string ErrorsMessages { get; set; }
    }
}
