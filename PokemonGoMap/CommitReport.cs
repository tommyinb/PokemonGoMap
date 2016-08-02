using LibGit2Sharp;
using PokemonGoMap.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGoMap
{
    public class CommitReport
    {
        public static void Run()
        {
            Logger.LogMessage("Start Commit");

            var account = XmlUtil.ReadFile<Account>("Account.xml");

            var path = Path.GetFullPath(@"..\..\..\");
            using (var repo = new Repository(path))
            {
                var files = Directory.GetFiles(CreateReport.Folder, "*.json").Select(Path.GetFullPath).ToArray();
                repo.Stage(files);

                var message = "Report " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                var signature = new Signature(account.Username, account.Email, DateTimeOffset.Now);
                repo.Commit(message, signature, signature);

                var remote = repo.Network.Remotes["origin"];
                var options = new PushOptions
                {
                    CredentialsProvider = (url, usernameFromUrl, types) => new UsernamePasswordCredentials()
                    {
                        Username = account.Username,
                        Password = account.Password
                    }
                };
                repo.Network.Push(remote, @"refs/heads/master", options);
            }

            Logger.LogMessage("End Commit");
        }
    }

    public class Account
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }
    }
}
