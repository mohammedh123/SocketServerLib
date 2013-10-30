using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SocketServerLib.Message;

namespace AsyncClientServerLib.Message
{
    /// <summary>
    /// A Basic Message Header. Implements the AbstractMessageHeader.
    /// </summary>
    class BasicHeader : AbstractMessageHeader
    {
        /// <summary>
        /// The message length
        /// </summary>
        private int messageLength = -1;

        /*
         * The header structure is : byte1 byte2 int (message length) guid (message UID) guid (client UID)
         */
        public byte Byte1 { get; set; }                     // 1 +
        public byte Byte2 { get; set; }                     // 1 +
        override public int MessageLength                   // 4 +
        {
            get
            {
                return this.messageLength;
            }
        }
        // 38
        /// <summary>
        /// Default constructor
        /// </summary>
        public BasicHeader() : base()
        {
            this.messageLength = -1;
            Byte1 = 0x01;
            Byte2 = 0xFF;
        }

        /// <summary>
        /// Constructor to instance a Message Header of a Message.
        /// </summary>
        /// <param name="clientUID">The client UID</param>
        /// <param name="messageUID">The message UID</param>
        /// <param name="msg">The message</param>
        public BasicHeader(byte[] msg) 
            : this()
        {
            this.messageLength = msg.Length + this.HeaderLength;
        }

        /// <summary>
        /// Get header size in bytes.
        /// </summary>
        override public int HeaderLength
        {
            get
            {
                return 38;
            }
        }

        #region Read/Write methods

        /// <summary>
        /// Write the header in a buffer.
        /// </summary>
        /// <param name="destBuffer">The buffer</param>
        /// <param name="offset">Offset in the buffer</param>
        /// <returns>The next position in the buffer after the header</returns>
        override public int Write(byte[] destBuffer, int offset)
        {
            destBuffer[offset++] = Byte1;
            destBuffer[offset++] = Byte2;
            offset = Write(destBuffer, offset, this.messageLength);
            return offset;
        }

        /// <summary>
        /// Read a hearer from a buffer.
        /// </summary>
        /// <param name="sourceBuffer">The buffer</param>
        /// <param name="offset">The offset in the buffer</param>
        /// <returns>The next position in the buffer after the header</returns>
        override public int Read(byte[] sourceBuffer, int offset)
        {
            if (sourceBuffer.Length - offset < HeaderLength)
            {
                complete = false;
                return 0;

            }
            complete = true;
            int v = 0;
            this.messageLength = 0;
            this.Byte1 = sourceBuffer[offset++];
            this.Byte2 = sourceBuffer[offset++];
            if (this.Byte1 != 0x1 || this.Byte2 != 0xFF)
            {
                throw new MessageException("Invalid message");
            }
            offset = Read(sourceBuffer, offset, ref v);
            this.messageLength = v;
            return offset;
        }

        /// <summary>
        /// Read a int from a buffer.
        /// </summary>
        /// <param name="sourceBuffer">The buffer</param>
        /// <param name="offset">Offset in the buffer</param>
        /// <param name="value">The int value read</param>
        /// <returns>The next position in the buffer after the int</returns>
        private int Read(byte[] sourceBuffer, int offset, ref int value)
        {
            value = 0;
            for (int i = 24; i >= 0; i -= 8)
            {
                value += sourceBuffer[offset++] << i;
            }
            return offset;
        }

        /// <summary>
        /// Write an int in the buffer.
        /// </summary>
        /// <param name="destBuffer">The buffer</param>
        /// <param name="offset">The offset in the buffer</param>
        /// <param name="value">The int value to write</param>
        /// <returns>The next position in the buffer after the int</returns>
        private int Write(byte[] destBuffer, int offset, int value)
        {
            uint mask = 0xFF000000;

            for (int i = 24; i >= 0; i -= 8)
            {
                destBuffer[offset++] = (byte)((value & mask) >> i);
                mask = mask >> 8;
            }
            return offset;
        }

        /// <summary>
        /// Read a Guid from a buffer.
        /// </summary>
        /// <param name="sourceBuffer">The buffer</param>
        /// <param name="offset">Offset in the buffer</param>
        /// <param name="value">The Guid value read</param>
        /// <returns>The next position in the buffer after the Guid</returns>
        private int Read(byte[] sourceBuffer, int offset, ref Guid value)
        {
            byte[] source = value.ToByteArray();
            System.Buffer.BlockCopy(sourceBuffer, offset, source, 0, source.Length);
            value = new Guid(source);
            offset += source.Length;
            return offset;
        }

        /// <summary>
        /// Write a Guid in the buffer.
        /// </summary>
        /// <param name="destBuffer">The buffer</param>
        /// <param name="offset">The offset in the buffer</param>
        /// <param name="value">The Guid value to write</param>
        /// <returns>The next position in the buffer after the Guid</returns>
        private int Write(byte[] destBuffer, int offset, Guid value)
        {
            byte[] source = value.ToByteArray();
            System.Buffer.BlockCopy(source, 0, destBuffer, offset, source.Length);
            offset += source.Length;
            return offset;
        }

        #endregion

    }
}
