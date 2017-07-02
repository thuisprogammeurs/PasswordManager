using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Realms;
using Zzz.Core.Models.Orm;

namespace Zzz.Core.Repositories
{
    public class DatabaseHelper
    {
        Realm realm;

        public DatabaseHelper()
        {
            try
            {
                RealmConfiguration configuration = new RealmConfiguration()
                {
                    ShouldDeleteIfMigrationNeeded = true
                };

                realm = Realm.GetInstance(configuration);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Password
        public List<PasswordOrm> GetAllPasswords()
        {
            List<PasswordOrm> allPasswords = new List<PasswordOrm>(realm.All<PasswordOrm>());
            return allPasswords;
        }

        public PasswordOrm GetPassword(string id)
        {
            PasswordOrm password = (PasswordOrm)realm.All<PasswordOrm>().Where(p => p.Id == id).First();
            return password;
        }

        public void UpdatePassword(PasswordOrm password)
        {
            try
            {
                realm.Write(() =>
                {
                    realm.Add(password, true);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Group
        public List<GroupOrm> GetAllGroups()
        {
            List<GroupOrm> allGroups = new List<GroupOrm>(realm.All<GroupOrm>());
            return allGroups;
        }

        public GroupOrm GetGroup(string id)
        {
            GroupOrm group = (GroupOrm)realm.All<GroupOrm>().Where(p => p.Id == id).First();
            return group;
        }

        public GroupOrm GetGroupByName(string name)
        {
            GroupOrm group = (GroupOrm)realm.All<GroupOrm>().Where(p => p.Name == name).First();
            return group;
        }

        public void UpdateGroup(GroupOrm group)
        {
            try
            {
                realm.Write(() =>
                {
                    realm.Add(group, true);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        public void GenerateFakeData()
        {
            // Delete all objects in database.
            //CleanDatabase();

            // Create fake data for testing.
            GroupOrm groupEmail = null;
            GroupOrm groupWebShop = null;
            if (GetAllGroups().Count < 1)
            {
                realm.Write(() =>
                    {
                        groupEmail = realm.Add(new GroupOrm { Id = Guid.NewGuid().ToString(), Name = "Email", Description = "All email passwords", IconId = 1 });
                        groupWebShop = realm.Add(new GroupOrm { Id = Guid.NewGuid().ToString(), Name = "Web Shop", Description = "All web shop passwords", IconId = 2 });
                    }
                );
            }
            else
            {
                groupEmail = (GroupOrm)realm.All<GroupOrm>().Where(p => p.Name == "Email").First();
                groupWebShop = (GroupOrm)realm.All<GroupOrm>().Where(p => p.Name == "Web Shop").First();
            }

            if (GetAllPasswords().Count < 1)
            {
                realm.Write(() =>
                    {
                        var outlookPassword = realm.Add(new PasswordOrm { Id = Guid.NewGuid().ToString(), Name = "Outlook.com", Description = "Outlook.com password", PasswordGroup = groupEmail });
                        var workEmailPassword = realm.Add(new PasswordOrm { Id = Guid.NewGuid().ToString(), Name = "Zzz email", Description = "Zzz email password", PasswordGroup = groupEmail });
                        var bolDotComPassword = realm.Add(new PasswordOrm { Id = Guid.NewGuid().ToString(), Name = "Bol.com", Description = "Bol.com password", PasswordGroup = groupWebShop });
                    }
                );
            }
        }

        private void CleanDatabase()
        {
            realm.Write(() =>
            {
                realm.RemoveAll();
            });
        }
    }
}
