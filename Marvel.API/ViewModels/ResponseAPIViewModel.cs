namespace Marvel.API.ViewModels
{
    public class ResponseAPIViewModel<T>
    {
        public int Code { get; set; }
        public string Status { get; set; }
        public DataResult<T> Data { get; set; }
    }
}
