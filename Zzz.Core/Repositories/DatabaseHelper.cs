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

                InitGroupList();
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
                if (password.Id == null)
                {
                    password.Id = Guid.NewGuid().ToString(); 
                }

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

        public void DeletePassword(PasswordOrm password)
        {
            try
            {
                PasswordOrm passwordToBeDeleted = GetPassword(password.Id);
                using (var trans = realm.BeginWrite())
                {
                    realm.Remove(passwordToBeDeleted);
                    trans.Commit();
                }
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
                if (group.Id == null)
                {
                    group.Id = Guid.NewGuid().ToString();
                    group.IconName = "category_other";
                }

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

        public void DeleteGroup(GroupOrm group)
        {
            try
            {
                GroupOrm groupToBeDeleted = GetGroup(group.Id);
                using (var trans = realm.BeginWrite())
                {
                    realm.Remove(groupToBeDeleted);
                    trans.Commit();
                }

                //realm.Write(() =>
                //{
                //    realm.Remove(group);
                //});
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Master password
        public MasterSecretOrm GetMasterSecret(string name)
        {
            MasterSecretOrm password = (MasterSecretOrm)realm.All<MasterSecretOrm>().Where(p => p.Name == name).First();
            return password;
        }

        public void UpdateMasterSecret(MasterSecretOrm masterSecretOrm)
        {
            try
            {
                if (masterSecretOrm.Id == null)
                {
                    masterSecretOrm.Id = Guid.NewGuid().ToString();
                }

                realm.Write(() =>
                {
                    realm.Add(masterSecretOrm, true);
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
            //if (GetAllGroups().Count < 1)
            //{
            //    realm.Write(() =>
            //        {
            //            groupEmail = realm.Add(new GroupOrm { Id = Guid.NewGuid().ToString(), Name = "Email", Description = "All email passwords", IconName = "category_mailbox" });
            //            groupWebShop = realm.Add(new GroupOrm { Id = Guid.NewGuid().ToString(), Name = "Web Shop", Description = "All web shop passwords", IconName = "category_webshop" });
            //        }
            //    );
            //}
            //else
            //{
                groupEmail = (GroupOrm)realm.All<GroupOrm>().Where(p => p.Name == "Email").First();
                groupWebShop = (GroupOrm)realm.All<GroupOrm>().Where(p => p.Name == "Webshop").First();
            //}

            if (GetAllPasswords().Count < 1)
            {
                realm.Write(() =>
                    {
                        var outlookPassword = realm.Add(new PasswordOrm { Id = Guid.NewGuid().ToString(), Name = "Outlook", Secret = "T0oMUchS3cR#t", AccessAddress = "www.outlook.com", PasswordGroup = groupEmail });
                        var workEmailPassword = realm.Add(new PasswordOrm { Id = Guid.NewGuid().ToString(), Name = "Zzz email", Secret = "Cle@n1Ng", AccessAddress = "www.gmail.com", PasswordGroup = groupEmail });
                        var bolDotComPassword = realm.Add(new PasswordOrm { Id = Guid.NewGuid().ToString(), Name = "Bol.com", Secret = "B0lLYw00D", AccessAddress = "www.bol.com", PasswordGroup = groupWebShop });
                    }
                );
            }
        }

        private void CleanDatabase()
        {
            // Remove all database records.
            realm.Write(() =>
            {
                realm.RemoveAll();
            });

            InitGroupList();
        }

        /// <summary>
        /// Initialize the default list of password groups.
        /// </summary>
        private void InitGroupList()
        {
            if (GetAllGroups().Count < 1)
            {
                realm.Write(() =>
                    {
                        realm.Add(new GroupOrm { Id = Guid.NewGuid().ToString(), Name = "Bank account", Description = "Bank accounts", IconName = "category_bank" });
                        realm.Add(new GroupOrm { Id = Guid.NewGuid().ToString(), Name = "Bank/Creditcard", Description = "Bank cards or creditcards", IconName = "category_bank_creditcard" });
                        realm.Add(new GroupOrm { Id = Guid.NewGuid().ToString(), Name = "Computer", Description = "Computer accounts", IconName = "category_computer" });
                        realm.Add(new GroupOrm { Id = Guid.NewGuid().ToString(), Name = "Home", Description = "Home passwords or pincodes", IconName = "category_home" });
                        realm.Add(new GroupOrm { Id = Guid.NewGuid().ToString(), Name = "Email", Description = "Email accounts", IconName = "category_mailbox" });
                        realm.Add(new GroupOrm { Id = Guid.NewGuid().ToString(), Name = "Phone/Tablet", Description = "Mobile phone or tablet accounts", IconName = "category_phone_or_tablet" });
                        realm.Add(new GroupOrm { Id = Guid.NewGuid().ToString(), Name = "Social media", Description = "Social media accounts", IconName = "category_socialmedia" });
                        realm.Add(new GroupOrm { Id = Guid.NewGuid().ToString(), Name = "VPN", Description = "VPN accounts", IconName = "category_vpn" });
                        realm.Add(new GroupOrm { Id = Guid.NewGuid().ToString(), Name = "Digital wallet", Description = "Digital wallet accounts", IconName = "category_wallet" });
                        realm.Add(new GroupOrm { Id = Guid.NewGuid().ToString(), Name = "Webshop", Description = "Webshop accounts", IconName = "category_webshop" });
                        realm.Add(new GroupOrm { Id = Guid.NewGuid().ToString(), Name = "Website", Description = "Website accounts", IconName = "category_website" });
                    }
                );
            }
        }
    }
}
