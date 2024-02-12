namespace App.Services;

public class DatabaseInitializationService
{
    private readonly UserService _userService;
    private readonly ILogger<DatabaseInitializationService> _logger;

    public DatabaseInitializationService(UserService userService, ILogger<DatabaseInitializationService> logger)
    {
        _userService = userService;
        _logger = logger;
    }

    public async Task<bool> CheckDatabaseConnectionAsync()
    {
        try
        {
            var users = await _userService.GetAsync();
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to connect to the database");
            return false;
        }
    }
}
