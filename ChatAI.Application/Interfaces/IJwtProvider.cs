﻿using ChatAI.Domain.Entities;
using ChatAI.Domain.Enums;

namespace ChatAI.Application.Interfaces;

public interface IJwtProvider
{
    string Generate(User user, JwtType type);
}
