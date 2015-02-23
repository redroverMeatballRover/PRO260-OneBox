using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OneBox.Models
{
    /// <summary>
    /// A data-model used for storing Messages for a users access
    /// Messages are only stored for 'Outgoing' and 'Incoming' messages
    /// </summary>
    public class Mailbox
    {
        #region Properties
        public List<Message> Outgoing { get; set; }
        public List<Message> Incoming { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Override of default constructor
        /// Sets new instance of 'Outgoing'
        /// Sets new instance of 'Incoming'
        /// </summary>
        public Mailbox()
        {
            this.Outgoing = new List<Message>();
            this.Incoming = new List<Message();
        }
        
        /// <summary>
        /// Sets 'Outgoing' based on parameter
        /// Sets 'Incoming' based on parameter
        /// </summary>
        /// <param name="outgoing">List of messages sent by the user</param>
        /// <param name="incoming">List of messages recieved by the user</param>
        public Mailbox(List<Message> outgoing, List<Message> incoming)
        {
            this.Outgoing = outgoing;
            this.Incoming = incoming;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Iterates through messages and filters using LINQ
        /// </summary>
        /// <param name="criteria">String by which to search for</param>
        /// <returns>Filtered messages based on criteria</returns>
        private IEnumerable<Message> SearchMessages(string criteria) {
            return Incoming.Where(x => x.Text.Contains(criteria)).Concat(Outgoing.Where(x => x.Text.Contains(criteria)));
        }

        /// <summary>
        /// Removes all messages in 'Outgoing' and 'Incoming'
        /// </summary>
        private void DeleteAllMessages()
        {
            Outgoing.RemoveAll(x => true);
            Incoming.RemoveAll(x => true);
        }
        #endregion
    }
}
