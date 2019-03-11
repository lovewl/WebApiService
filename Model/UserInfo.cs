using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    [Serializable]
    public class UserInfo
    {
        public int ID { get; set; }
        public string LoginName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LoginTime { get; set; }
        public DateTime LastLoginTime { get; set; }
        public int LoginCount { get; set; }
    }
}
