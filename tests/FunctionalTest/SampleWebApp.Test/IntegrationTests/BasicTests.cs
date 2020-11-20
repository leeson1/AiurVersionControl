﻿using AiurEventSyncer.Models;
using AiurEventSyncer.Remotes;
using SampleWebApp;
using SampleWebApp.Data;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SampleWebApp.Tests.IntegrationTests
{
    public class BasicTests : IClassFixture<SampleWebAppFactory<Startup>>
    {
        private readonly SampleWebAppFactory<Startup> _factory;

        public BasicTests(SampleWebAppFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public void MyTest()
        {
            var repo = new Repository<LogItem>();
            repo.Remotes.Add(new WebSocketRemote<LogItem>("https://localhost:44343/repo.are"));

            repo.Commit(new LogItem { Message = "1" });
            repo.Commit(new LogItem { Message = "2" });
            repo.Commit(new LogItem { Message = "3" });

            repo.Push();

            var repo2 = new Repository<LogItem>();
            repo2.Remotes.Add(new WebSocketRemote<LogItem>("https://localhost:44343/repo.are"));
            repo2.Pull();


            Assert.True(repo2.Commits.Count() == 3);
            Assert.True(repo2.Commits.ToArray()[0].Item.Message == "1");
            Assert.True(repo2.Commits.ToArray()[1].Item.Message == "2");
            Assert.True(repo2.Commits.ToArray()[2].Item.Message == "3");
        }
    }
}