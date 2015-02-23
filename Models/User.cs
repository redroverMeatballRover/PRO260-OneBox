using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Web;

namespace OneBox.Models
{
    /// <summary>
    /// User is a ViewModel used for temporary storing information
    /// about users that will use our application
    /// </summary>
    public class User
    {
        #region Properties
        public static long ID { get; set; }
        private long UserID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; private set; }
        public string ProfilePic { get; private set; }
        public string BasicInfo { get; set; }
        public List<User> FriendsList { get; set; }
        public List<Group> Groups { get; set; }
        public Mailbox UserMailBox { get; set; }
        public List<Notification> UserNotifications { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Standard constructer for the View initialization
        /// </summary>
        public User()
        {

        }

        /// <summary>
        /// Constructor for initializing a new instance of a User
        /// </summary>
        /// <param name="userName">The User's chosen name</param>
        /// <param name="email">The User's email address</param>
        /// <param name="password">The User's password</param>
        public User(string userName, string email, string password)
        {
            this.UserID = ID++;
            this.UserName = userName;
            this.Email = email;
            this.Password = password;

            this.ProfilePic = ""; //this should be a specific default image url
            this.FriendsList = new List<User>();
            this.Groups = new List<Group>();
            this.UserMailBox = new Mailbox();
            this.UserNotifications = new List<Notification>();

            UserNotifications.Add(new Notification(this, "Welcome to OneBox! We hope you pirate a lot!"));
        }

        /// <summary>
        /// Constructor for an existing User
        /// </summary>
        /// <param name="userName">The User's chosen name</param>
        /// <param name="email">The User's email address</param>
        /// <param name="password">The User's password</param>
        /// <param name="profilePic">The URL for the User's current profile picture</param>
        /// <param name="basicInfo">The basic info about the User</param>
        /// <param name="friendsList">The User's list of current friends</param>
        /// <param name="groups">The User's list of current groups</param>
        /// <param name="mailBox">A mailbox for storing messages from other Users</param>
        /// <param name="notifications">A list of system or friendly notifications</param>
        public User(string userName, string email, string password, string profilePic, string basicInfo, List<User> friendsList, List<Group> groups, Mailbox mailBox, List<Notification> notifications)
        {
            this.UserID = ID++;
            this.UserName = userName;
            this.FriendsList = friendsList;
            this.Email = email;
            this.Password = password;
            this.ProfilePic = profilePic;
            this.BasicInfo = basicInfo;
            this.Groups = groups;
            this.UserMailBox = mailBox;
            this.UserNotifications = notifications;
        }
        #endregion
    }
}