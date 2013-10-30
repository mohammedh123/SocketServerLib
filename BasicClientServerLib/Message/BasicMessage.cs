using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SocketServerLib.Message;

namespace BasicClientServerLib.Message
{
    /// <summary>
    /// Basic Message. Implements the AbstractMessage. The message header class is BasicHeader.
    /// </summary>
    public class BasicMessage : AbstractMessage
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public BasicMessage() 
            : base()
        {
        }

        /// <summary>
        /// Constructor for a message.
        /// </summary>
        /// <param name="clientUID">The client UID</param>
        /// <param name="buffer">The message buffer</param>
        public BasicMessage(byte[] buffer)
            : base()
        {
            if (buffer == null)
            {
                buffer = new byte[0];
            }
            header = new BasicHeader(buffer);
            body = buffer;
        }

        /// <summary>
        /// Return an empty BasicHeader instance.
        /// </summary>
        /// <returns>The BasicHeader instance</returns>
        protected override AbstractMessageHeader GetMessageHeaderInstance()
        {
            return new BasicHeader();
        }
    }
}
