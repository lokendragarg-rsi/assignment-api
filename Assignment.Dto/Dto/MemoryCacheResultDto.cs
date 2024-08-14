
using Assignment.Dto.Enum;

namespace Assignment.Dto.Dto
{
    public class MemoryCacheResultDto
    {
        /// <summary>
        /// CacheStatus
        /// </summary>
        public MemoryCacheStatusOption CacheStatus { get; set; }

        /// <summary>
        /// Error
        /// </summary>
        public Exception Error { get; set; }
    }

   
}
