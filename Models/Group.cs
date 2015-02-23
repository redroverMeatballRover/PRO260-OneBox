using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace OneBox.Models
{
    /// <summary>
    /// Group is a data-model for storing information about users file permissions towards other users.
    /// A user who is in the list of 'Members' can access 'Files'
    /// Owner is the admin of the group file permissions
    /// </summary>
    public class Group
    {
        #region Properties
        private User Owner { get; set; }
        private IEnumerable<User> Members { get; set; }
        private List<string> Files { get; set; }
        #endregion

        #region Constructors
        public Group(User owner)
        {
            this.Owner = owner;
            this.Members = new List<User>();
            this.Files = new List<string>();
        }
        #endregion
    }
}
