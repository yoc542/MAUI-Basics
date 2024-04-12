using CommunityToolkit.Mvvm.Messaging.Messages;

namespace DogHome.Messenger
{
    public class RegisterMessage : ValueChangedMessage<string>
    {
        public RegisterMessage(string value) : base(value)
        {

        }
    }

}
