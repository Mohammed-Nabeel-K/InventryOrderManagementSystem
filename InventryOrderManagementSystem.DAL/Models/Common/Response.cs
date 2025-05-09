using System.Net;

namespace InventryOrderManagementSystem.DAL.Models.Common
{
    public class Response<T>
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
