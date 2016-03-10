using Banking.Common.Identifiers;

namespace Banking.Common.RequestResult
{
    public class RequestResult<T>
    {
        public T Obj { get; set; }
        public string Message { get; set; }
        public RequestStatus Status { get; set; }
    }
}
