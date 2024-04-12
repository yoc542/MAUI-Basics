namespace DogHome.Messenger
{
    public class FormDataMessage<T>
    {
        public T Data { get; set; }
        public RegisterMessage RegisterMessage { get; set; }

        public FormDataMessage(T data, RegisterMessage registerMessage)
        {
            Data = data;
            RegisterMessage = registerMessage;
        }
    }
}
