namespace CarCareAlliance.Presentation.Client.Common.Constants
{
    public static partial class Constants
    {
        public static partial class Email
        {
            public static class ContactUsSubmitted
            {
                public const string Subject = "Contact user message";
                public const string To = "stanislav.prutean@iis.utm.md";

                public static string GetBody(string name, string text)
                {
                    if (name is null)
                    {
                        return "";
                    }

                    string message = $"<p> User contact request:</br>Name: {name}</br>Message: {text}</p>";

                    return message;
                }
            }

            public const string SendEmailSuccess= "The message was sent successfully!";
            public const string SendEmailFailure = "Failed to send the message.";
        }
    }
}
