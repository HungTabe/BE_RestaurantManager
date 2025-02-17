using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace BE_RestaurantManagement.Configs
{
    public class IgnorePropertiesFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            // Danh sách các thuộc tính mà bạn muốn loại bỏ khỏi Swagger documentation
            var propertiesToIgnore = new List<string> { "twoFactorCode", "twoFactorRecoveryCode" };

            // Kiểm tra xem request body có tồn tại hay không
            if (operation.RequestBody?.Content.Values is IList<OpenApiMediaType> contentMediaTypes)
            {
                // Duyệt qua các media types trong request body
                foreach (var mediaType in contentMediaTypes)
                {
                    // Kiểm tra nếu schema của media type có properties
                    if (mediaType.Schema?.Properties != null)
                    {
                        // Loại bỏ các thuộc tính không cần thiết từ schema
                        foreach (var property in propertiesToIgnore)
                        {
                            mediaType.Schema.Properties.Remove(property);
                        }
                    }
                }
            }
        }
    }
}
