using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Nekomimi_Rewrite.Services
{
    class TimeService
    {
        private readonly Timer _timer;

        public TimeService()
        {
            _timer = new Timer(_ =>
            {

            },
            null,
            TimeSpan.FromMinutes(10),
            TimeSpan.FromMinutes(30));
        }

    }
}
