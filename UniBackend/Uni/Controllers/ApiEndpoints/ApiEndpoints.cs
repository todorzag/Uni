namespace UniBackend.Controllers.ApiEndpoints
{
    public static class ApiEndpoints
    {
        private const string BaseUrl = "api";

        public static class Auth
        {
            public const string Login = $"{BaseUrl}/auth/login";
            public const string Register = $"{BaseUrl}/auth/register";
            public const string ToggleUserIsActive = $"{BaseUrl}auth/{{id}}/toggle-active";
        }

        public static class Computers
        {
            public const string GetAll = $"{BaseUrl}/computers";
            public const string GetById = $"{BaseUrl}/computers/{{id}}";
            public const string Filter = $"{BaseUrl}/computers/filter";
        }

        public static class Orders
        {
            public const string GetMyOrders = $"{BaseUrl}/orders/my";
            public const string CreateOrder = $"{BaseUrl}/orders/create";
        }
    }
}
