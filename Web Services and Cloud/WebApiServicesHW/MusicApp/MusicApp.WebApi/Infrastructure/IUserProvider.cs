﻿namespace MusicApp.WebApi.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public interface IUserProvider
    {
        string GetUserId();
    }
}