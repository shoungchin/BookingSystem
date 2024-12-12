namespace BookingSystem.Data.ResponseModel
{
    public class ServiceResponse<T>
    {
        public T Data { get; set; }       
        public bool Success { get; set; }  
        public string Message { get; set; }
        public string Token { get; set; }  
    }
}
