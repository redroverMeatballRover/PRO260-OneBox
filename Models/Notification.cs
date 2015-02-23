using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OneBox.Models
{
    /// <summary>
    /// A data-model for storing information about a Notification
    /// Holds user to recieve Notification
    /// Holds the message text to show to the user
    /// </summary>
    public class Notification
    {
        #region Properties
        private User UserTo { get; set; }
        private User UserFrom { get; set; }
        public string Text { get; set; }
        #endregion

        #region Constructors
        public Notification()
        {

        }

        /// <summary>
        /// Creates a new Notification containg vital and sometimes arbitrary information
        /// </summary>
        /// <param name="to">User to recieve Notification</param>
        /// <param name="text">Contents of the Notification</param>
        public Notification(User to, string text)
        {
            UserTo = to;
            Text = text;
        }
        #endregion
    }
}
