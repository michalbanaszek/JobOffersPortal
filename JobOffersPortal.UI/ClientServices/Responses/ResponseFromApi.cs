namespace JobOffersPortal.UI.ClientServices.Responses
{
    public class ResponseFromApi<T>
    {
        public T Data { get; set; }
        public bool Success { get; set; }      
    }
}
