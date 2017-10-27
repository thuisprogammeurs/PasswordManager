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
                // Encryption.
                configuration.EncryptionKey = new byte[64] // key MUST be exactly this size
                {
                  0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08,
                  0x11, 0x12, 0x13, 0x14, 0x15, 0x16, 0x17, 0x18,
                  0x21, 0x22, 0x23, 0x24, 0x25, 0x26, 0x27, 0x28,
                  0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38,
                  0x41, 0x42, 0x43, 0x44, 0x45, 0x46, 0x47, 0x48,
                  0x51, 0x52, 0x53, 0x54, 0x55, 0x56, 0x57, 0x58,
                  0x61, 0x62, 0x63, 0x64, 0x65, 0x66, 0x67, 0x68,
                  0x71, 0x72, 0x73, 0x74, 0x75, 0x76, 0x77, 0x78
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

        public List<PasswordOrm> GetAllPasswordsByGroupId(string groupId)
        {
            GroupOrm groupOrm = GetGroup(groupId);
            List<PasswordOrm> allPasswords = new List<PasswordOrm>(realm.All<PasswordOrm>().Where(p => p.PasswordGroup == groupOrm));

            return allPasswords;
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

        #region Authentication Picture
        public List<AuthPictureOrm> GetAllAuthPictures()
        {
            List<AuthPictureOrm> allAuthPictures = new List<AuthPictureOrm>(realm.All<AuthPictureOrm>());
            return allAuthPictures;
        }

        public AuthPictureOrm GetAuthPicture(string id)
        {
            AuthPictureOrm authPicture = (AuthPictureOrm)realm.All<AuthPictureOrm>().Where(p => p.Id == id).First();
            return authPicture;
        }

        public AuthPictureOrm GetAuthPictureByName(string name)
        {
            AuthPictureOrm authPicture = (AuthPictureOrm)realm.All<AuthPictureOrm>().Where(p => p.Name == name).First();
            return authPicture;
        }

        public void UpdateAuthPicture(AuthPictureOrm authPicture)
        {
            try
            {
                if (authPicture.Id == null)
                {
                    authPicture.Id = Guid.NewGuid().ToString();
                }

                realm.Write(() =>
                {
                    realm.Add(authPicture, true);
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
            CleanDatabase();

            // Create fake data for testing.
            GroupOrm groupEmail = (GroupOrm)realm.All<GroupOrm>().Where(p => p.Name == "Email").First();
            GroupOrm groupWebShop = (GroupOrm)realm.All<GroupOrm>().Where(p => p.Name == "Webshop").First();

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
            InitAuthPictureList();
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
                        realm.Add(new GroupOrm { Id = Guid.NewGuid().ToString(), Name = "Bank account", Description = "Bank accounts", IconName = "category_bank", IsMaster=true });
                        realm.Add(new GroupOrm { Id = Guid.NewGuid().ToString(), Name = "Bank/Creditcard", Description = "Bank cards or creditcards", IconName = "category_bank_creditcard", IsMaster = true });
                        realm.Add(new GroupOrm { Id = Guid.NewGuid().ToString(), Name = "Computer", Description = "Computer accounts", IconName = "category_computer", IsMaster = true });
                        realm.Add(new GroupOrm { Id = Guid.NewGuid().ToString(), Name = "Home", Description = "Home passwords or pincodes", IconName = "category_home", IsMaster = true });
                        realm.Add(new GroupOrm { Id = Guid.NewGuid().ToString(), Name = "Email", Description = "Email accounts", IconName = "category_mailbox", IsMaster = true });
                        realm.Add(new GroupOrm { Id = Guid.NewGuid().ToString(), Name = "Phone/Tablet", Description = "Mobile phone or tablet accounts", IconName = "category_phone_or_tablet", IsMaster = true });
                        realm.Add(new GroupOrm { Id = Guid.NewGuid().ToString(), Name = "Social media", Description = "Social media accounts", IconName = "category_socialmedia", IsMaster = true });
                        realm.Add(new GroupOrm { Id = Guid.NewGuid().ToString(), Name = "VPN", Description = "VPN accounts", IconName = "category_vpn", IsMaster = true });
                        realm.Add(new GroupOrm { Id = Guid.NewGuid().ToString(), Name = "Digital wallet", Description = "Digital wallet accounts", IconName = "category_wallet", IsMaster = true });
                        realm.Add(new GroupOrm { Id = Guid.NewGuid().ToString(), Name = "Webshop", Description = "Webshop accounts", IconName = "category_webshop", IsMaster = true });
                        realm.Add(new GroupOrm { Id = Guid.NewGuid().ToString(), Name = "Website", Description = "Website accounts", IconName = "category_website", IsMaster = true });
                    }
                );
            }
        }

        /// <summary>
        /// Initialize the default list of authentication pictures.
        /// </summary>
        private void InitAuthPictureList()
        {
            if (GetAllAuthPictures().Count < 1)
            {
                realm.Write(() =>
                    {
                        realm.Add(new AuthPictureOrm { Id = Guid.NewGuid().ToString(), Name = "Aircraft", Description = "Aircraft", IconName = "aircraft", SecretCode = "G#!", SortOrder = 1 });
                        realm.Add(new AuthPictureOrm { Id = Guid.NewGuid().ToString(), Name = "American football", Description = "American football", IconName = "american_football", SecretCode = "^!", SortOrder = 2 });
                        realm.Add(new AuthPictureOrm { Id = Guid.NewGuid().ToString(), Name = "Apple", Description = "Apple", IconName = "apple", SecretCode = "!!!", SortOrder = 3 });
                        realm.Add(new AuthPictureOrm { Id = Guid.NewGuid().ToString(), Name = "Banana", Description = "Banana", IconName = "banana", SecretCode = "!y%U", SortOrder = 4 });
                        realm.Add(new AuthPictureOrm { Id = Guid.NewGuid().ToString(), Name = "Baseball", Description = "Baseball", IconName = "baseball", SecretCode = "HE$da", SortOrder = 5 });
                        realm.Add(new AuthPictureOrm { Id = Guid.NewGuid().ToString(), Name = "Basketball", Description = "Basketball", IconName = "basketball", SecretCode = "Nt%a1", SortOrder = 6 });
                        realm.Add(new AuthPictureOrm { Id = Guid.NewGuid().ToString(), Name = "Bigben", Description = "Bigben", IconName = "bigben", SecretCode = "1!$@a", SortOrder = 7 });
                        realm.Add(new AuthPictureOrm { Id = Guid.NewGuid().ToString(), Name = "Bull", Description = "Bull", IconName = "bull", SecretCode = "*^f#", SortOrder = 8 });
                        realm.Add(new AuthPictureOrm { Id = Guid.NewGuid().ToString(), Name = "Camera", Description = "Camera", IconName = "camera", SecretCode = "18H&@", SortOrder = 9 });
                        realm.Add(new AuthPictureOrm { Id = Guid.NewGuid().ToString(), Name = "Champagne", Description = "Champagne", IconName = "champagne", SecretCode = "72Gv^P", SortOrder = 10 });
                        realm.Add(new AuthPictureOrm { Id = Guid.NewGuid().ToString(), Name = "Cherry", Description = "Cherry", IconName = "cherry", SecretCode = "**gGv", SortOrder = 11 });
                        realm.Add(new AuthPictureOrm { Id = Guid.NewGuid().ToString(), Name = "Christmass tree", Description = "Christmass tree", IconName = "christmass_tree", SecretCode = "caT%", SortOrder = 15 });
                        realm.Add(new AuthPictureOrm { Id = Guid.NewGuid().ToString(), Name = "Coconut", Description = "Coconut", IconName = "aircraft", SecretCode = "u^ga$", SortOrder = 16 });
                        realm.Add(new AuthPictureOrm { Id = Guid.NewGuid().ToString(), Name = "Coffee", Description = "Coffee", IconName = "coffee", SecretCode = "B@ba$!", SortOrder = 13 });
                        realm.Add(new AuthPictureOrm { Id = Guid.NewGuid().ToString(), Name = "Colosseum", Description = "Colosseum", IconName = "colosseum", SecretCode = "Mb^(-", SortOrder = 14 });
                        realm.Add(new AuthPictureOrm { Id = Guid.NewGuid().ToString(), Name = "Crown", Description = "Crown", IconName = "crown", SecretCode = "ZxW(#!", SortOrder = 16 });
                        realm.Add(new AuthPictureOrm { Id = Guid.NewGuid().ToString(), Name = "Diamond", Description = "Diamond", IconName = "diamond", SecretCode = "Yr&n$", SortOrder = 17 });
                        realm.Add(new AuthPictureOrm { Id = Guid.NewGuid().ToString(), Name = "Egypt", Description = "Egypt", IconName = "egypt", SecretCode = "96Gr$k", SortOrder = 18 });
                        realm.Add(new AuthPictureOrm { Id = Guid.NewGuid().ToString(), Name = "Eiffel", Description = "Eiffel", IconName = "eiffel", SecretCode = "J&%fx", SortOrder = 19 });
                        realm.Add(new AuthPictureOrm { Id = Guid.NewGuid().ToString(), Name = "Firecracker", Description = "Firecracker", IconName = "firecracker", SecretCode = "l:c+_", SortOrder = 20 });
                        realm.Add(new AuthPictureOrm { Id = Guid.NewGuid().ToString(), Name = "Football", Description = "Football", IconName = "football", SecretCode = "|[6J%", SortOrder = 21 });
                        realm.Add(new AuthPictureOrm { Id = Guid.NewGuid().ToString(), Name = "Globe", Description = "Globe", IconName = "globe", SecretCode = "}ag&.-", SortOrder = 22 });
                        realm.Add(new AuthPictureOrm { Id = Guid.NewGuid().ToString(), Name = "Hammer", Description = "Hammer", IconName = "hammer", SecretCode = "ka{V$", SortOrder = 23 });
                        realm.Add(new AuthPictureOrm { Id = Guid.NewGuid().ToString(), Name = "Hand drill machine", Description = "Hand drill machine", IconName = "hand_drill_machine", SecretCode = "Sx$]+[", SortOrder = 24 });
                        realm.Add(new AuthPictureOrm { Id = Guid.NewGuid().ToString(), Name = "Hand saw", Description = "Hand saw", IconName = "hand_saw", SecretCode = ":?HQa", SortOrder = 25 });
                        realm.Add(new AuthPictureOrm { Id = Guid.NewGuid().ToString(), Name = "Hourglass", Description = "Hourglass", IconName = "hourglass", SecretCode = "Oo0./aW", SortOrder = 26 });
                        realm.Add(new AuthPictureOrm { Id = Guid.NewGuid().ToString(), Name = "Ice cream", Description = "Ice cream", IconName = "ice_cream", SecretCode = "Hr#(I", SortOrder = 27 });
                        realm.Add(new AuthPictureOrm { Id = Guid.NewGuid().ToString(), Name = "Kiwi", Description = "Kiwi", IconName = "kiwi", SecretCode = "K&afB^?", SortOrder = 28 });
                        realm.Add(new AuthPictureOrm { Id = Guid.NewGuid().ToString(), Name = "Lamp", Description = "Lamp", IconName = "lamp", SecretCode = "K7Av1627", SortOrder = 29 });
                        realm.Add(new AuthPictureOrm { Id = Guid.NewGuid().ToString(), Name = "Lego", Description = "Lego", IconName = "lego", SecretCode = "nLhba80nf", SortOrder = 30 });
                        realm.Add(new AuthPictureOrm { Id = Guid.NewGuid().ToString(), Name = "Liberty", Description = "Liberty", IconName = "liberty", SecretCode = "nLhba80nf!", SortOrder = 31 });
                        realm.Add(new AuthPictureOrm { Id = Guid.NewGuid().ToString(), Name = "Moneyjar", Description = "Moneyjar", IconName = "moneyjar", SecretCode = "ajt^vs.?", SortOrder = 32 });
                        realm.Add(new AuthPictureOrm { Id = Guid.NewGuid().ToString(), Name = "Palm tree", Description = "Palm tree", IconName = "palm_tree", SecretCode = "Khsr67122gs*", SortOrder = 33 });
                        realm.Add(new AuthPictureOrm { Id = Guid.NewGuid().ToString(), Name = "Piggybank", Description = "Piggybank", IconName = "piggybank", SecretCode = "Kgat&5g45", SortOrder = 34 });
                        realm.Add(new AuthPictureOrm { Id = Guid.NewGuid().ToString(), Name = "Rainbow", Description = "Rainbow", IconName = "rainbow", SecretCode = "ahts-+*g001", SortOrder = 35 });
                        realm.Add(new AuthPictureOrm { Id = Guid.NewGuid().ToString(), Name = "Raining", Description = "Raining", IconName = "raining", SecretCode = "a7dghg$bh28", SortOrder = 36 });
                        realm.Add(new AuthPictureOrm { Id = Guid.NewGuid().ToString(), Name = "Sailing boat", Description = "Sailing boat", IconName = "sailing_boat", SecretCode = "836{)dfa~", SortOrder = 37 });
                        realm.Add(new AuthPictureOrm { Id = Guid.NewGuid().ToString(), Name = "Santa", Description = "Santa", IconName = "santa", SecretCode = "aktdsg*54a%b", SortOrder = 38 });
                        realm.Add(new AuthPictureOrm { Id = Guid.NewGuid().ToString(), Name = "Scale", Description = "Scale", IconName = "scale", SecretCode = "ajdy012538gv+?fW", SortOrder = 39 });
                        realm.Add(new AuthPictureOrm { Id = Guid.NewGuid().ToString(), Name = "Screwdriver", Description = "Screwdriver", IconName = "screwdriver", SecretCode = "6sag*bsbI#//|", SortOrder = 40 });
                        realm.Add(new AuthPictureOrm { Id = Guid.NewGuid().ToString(), Name = "Snowman", Description = "Snowman", IconName = "snowman", SecretCode = "sj4784360-fsaf4", SortOrder = 41 });
                        realm.Add(new AuthPictureOrm { Id = Guid.NewGuid().ToString(), Name = "Sock", Description = "Sock", IconName = "sock", SecretCode = "sj4784&4a-fsaf4", SortOrder = 42 });
                        realm.Add(new AuthPictureOrm { Id = Guid.NewGuid().ToString(), Name = "Strawberry", Description = "Strawberry", IconName = "strawberry", SecretCode = "&saghas5,<*ah", SortOrder = 43 });
                        realm.Add(new AuthPictureOrm { Id = Guid.NewGuid().ToString(), Name = "Suitcase", Description = "Suitcase", IconName = "suitcase", SecretCode = "sah829&sbk/?", SortOrder = 44 });
                        realm.Add(new AuthPictureOrm { Id = Guid.NewGuid().ToString(), Name = "Sunglasses", Description = "Sunglasses", IconName = "sunglasses", SecretCode = "sj478412+0-fsaf4", SortOrder = 45 });
                        realm.Add(new AuthPictureOrm { Id = Guid.NewGuid().ToString(), Name = "Sunshine", Description = "Sunshine", IconName = "sunshine", SecretCode = "27842ghhasbY1", SortOrder = 46 });
                        realm.Add(new AuthPictureOrm { Id = Guid.NewGuid().ToString(), Name = "Tajmahal", Description = "Tajmahal", IconName = "tajmahal", SecretCode = "Hy%Drw0*%", SortOrder = 47 });
                        realm.Add(new AuthPictureOrm { Id = Guid.NewGuid().ToString(), Name = "Tennis", Description = "Tennis", IconName = "tennis", SecretCode = "K1T&b%f[", SortOrder = 48 });
                        realm.Add(new AuthPictureOrm { Id = Guid.NewGuid().ToString(), Name = "Volleyball", Description = "Volleyball", IconName = "volleyball", SecretCode = "a7dgHg$bh28", SortOrder = 49 });
                        realm.Add(new AuthPictureOrm { Id = Guid.NewGuid().ToString(), Name = "Windmill", Description = "Windmill", IconName = "windmill", SecretCode = "nLhba81Nf!82", SortOrder = 50 });
                        realm.Add(new AuthPictureOrm { Id = Guid.NewGuid().ToString(), Name = "Working bench", Description = "Working bench", IconName = "working_bench", SecretCode = "Kh$r62122gs", SortOrder = 51 });
                        realm.Add(new AuthPictureOrm { Id = Guid.NewGuid().ToString(), Name = "Wrench", Description = "Wrench", IconName = "wrench", SecretCode = "ka{V$LgRe72[?", SortOrder = 52 });
                    }
                );
            }
        }
    }
}
