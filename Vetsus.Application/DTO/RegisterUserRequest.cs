﻿namespace Vetsus.Application.DTO
{
    public record RegisterUserRequest(string UserName, string Email, string Role, string Password);
}
