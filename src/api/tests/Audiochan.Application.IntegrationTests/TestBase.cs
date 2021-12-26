﻿using System.Threading.Tasks;
using NUnit.Framework;

namespace Audiochan.Application.IntegrationTests
{
    using static TestFixture;
    
    public class TestBase
    {
        [SetUp]
        public async Task TestSetup()
        {
            await ResetState();
        }
    }
}