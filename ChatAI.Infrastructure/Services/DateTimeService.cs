﻿using ChatAI.Application.Common.Interfaces;

namespace ChatAI.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}
