namespace PersonManagerService.Domain.Configuration
{
    public class ResilientOptions
    {
        public int RetryCount { get; set; } = 1;
        public int AllowedExceptionsBeforeBreaking { get; set; } = 1;
        public int CircutOpenTimeInSec { get; set; } = 60;
    }
}
