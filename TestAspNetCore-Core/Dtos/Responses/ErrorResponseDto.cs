using System.Collections.Generic;

namespace TestAspNetCore_Core.Dtos.Responses
{
    public class ErrorResponseDto
    {
        public List<ErrorDto> Errors { get; set; } = new List<ErrorDto>();
    }
}
