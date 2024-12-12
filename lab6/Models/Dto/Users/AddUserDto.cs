/*
 *       @Author: yaile
 */

namespace lab6.Models.Dto.Users;

public class AddUserDto
{
    public string Login { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Email { get; set; } = null!;
}