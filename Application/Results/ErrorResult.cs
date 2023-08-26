namespace Application.Results
{
    public class ErrorResult : Result
    {
        public IEnumerable<string> Errors { get; }

        // Tek bir hata mesajı için yapılandırıcı (constructor)
        public ErrorResult(string message) : base(false,message)
        {
        }

        // Hata mesajları listesi için yapılandırıcı
        public ErrorResult(IEnumerable<string> errors) : base(false)
        {
            Errors = errors;
        }

        public ErrorResult() : base(false)
        {

        }
    }
}
