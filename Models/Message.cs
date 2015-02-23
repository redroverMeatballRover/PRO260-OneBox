using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OneBox.Models
{
    public class Message
    {
        #region Properties
        private User UserTo { get; set; }
        private User UserFrom { get; set; }
        public string Text { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new Message containing vital and sometimes arbitrary information
        /// </summary>
        /// <param name="to">User to recieve Notification</param>
        /// <param name="text">Contents of the Message</param>
        /// <param name="text">Contents of the Notification</param>
        public Message(User to, User from, string text) {
            this.UserTo = to;
            this.UserFrom = from;
            this.Text = text;
        }
        #endregion
    }
}
