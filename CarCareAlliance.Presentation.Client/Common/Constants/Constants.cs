namespace CarCareAlliance.Presentation.Client.Common.Constants
{
    public static partial class Constants
    {
        public const string MediaType = "application/json";
        public const string Client = "HttpClient";

        public static string DeleteConfirmantion(params string?[] itemNames)
        {
            if (itemNames is null)
            {
                return "";
            }

            string message = "Are you sure you want to delete this item(s): ";
            message += string.Join(", ", itemNames);
            message += "?";

            return message;
        }

        public static string AddSuccessfulConfirmantion(params string?[] itemNames)
        {
            if (itemNames is null)
            {
                return "";
            }

            string message = "Item(s): ";
            message += string.Join(", ", itemNames);
            message += "were successfully added!";

            return message;
        }

        public static string DeleteSuccessfulConfirmation(string name) =>
            $"{name} was deleted successfully!";

        public static string UpdateSuccessfulConfirmation(string name) =>
            $"{name} was updated successfully!";

        public static string CreateSuccessfulConfirmation(string name) =>
            $"{name} was created successfully!";

        public static string AddSuccessfulConfirmation(string name) =>
            $"{name} was added successfully!";
    }
}