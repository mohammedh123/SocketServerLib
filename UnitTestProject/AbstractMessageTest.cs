using SocketServerLib.Message;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using SocketServerLib.SocketHandler;
using BasicClientServerLib.Message;

namespace UnitTestProject
{
    /// <summary>
    ///This is a test class for AbstractMessageTest and is intended
    ///to contain all AbstractMessageTest Unit Tests
    ///</summary>
    [TestClass()]
    public class AbstractMessageTest
    {
        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        /// <summary>
        ///A test for TryReadMessage
        ///</summary>
        [TestMethod()]
        public void TryReadMessageTestOneCompleteMessage()
        {
            // Create a message to write as received in buffer
            byte[] fakeBody = new byte[10];
            AbstractMessage messageToWrite = new BasicMessage(fakeBody);
            // Prepare the received buffer
            SocketStateObject state = new SocketStateObject();
            int offset = messageToWrite.Header.Write(state.buffer, 0);
            offset += fakeBody.Length;
            // Call the TryRead
            AbstractMessage message = new BasicMessage();
            int byteRead = offset;
            AbstractMessage actual = AbstractMessage.TryReadMessage(message, state, byteRead);
            Assert.IsNotNull(actual);
            Assert.AreEqual(actual.MessageLength, messageToWrite.MessageLength);
            Assert.IsNull(state.message);
            Assert.IsNull(state.pendingBuffer);
        }

        /// <summary>
        ///A test for TryReadMessage
        ///</summary>
        [TestMethod()]
        public void TryReadMessageTestTwoCompleteMessage()
        {
            // Create a message to write as received in buffer
            byte[] fakeBody = new byte[10];
            AbstractMessage messageToWrite1 = new BasicMessage(fakeBody);
            // Prepare the received buffer
            SocketStateObject state = new SocketStateObject();
            int offset = messageToWrite1.Header.Write(state.buffer, 0);
            offset += fakeBody.Length;
            
            fakeBody = new byte[15];
            AbstractMessage messageToWrite2 = new BasicMessage(fakeBody);
            offset = messageToWrite2.Header.Write(state.buffer, offset);
            offset += fakeBody.Length;
            // Call the TryRead
            AbstractMessage message = new BasicMessage();
            int byteRead = offset;
            AbstractMessage actual = AbstractMessage.TryReadMessage(message, state, byteRead);
            Assert.IsNotNull(actual);
            Assert.AreEqual(actual.MessageLength, messageToWrite1.MessageLength);
            Assert.IsNull(state.message);
            Assert.IsNotNull(state.pendingBuffer);
            actual = AbstractMessage.TryReadMessage(message, state, 0);
            Assert.IsNotNull(actual);
            Assert.AreEqual(actual.MessageLength, messageToWrite2.MessageLength);
            Assert.IsNull(state.message);
            Assert.IsNull(state.pendingBuffer);
        }

        /// <summary>
        ///A test for TryReadMessage
        ///</summary>
        [TestMethod()]
        public void TryReadMessageTestOneLongMessage()
        {
            // Create a message to write as received in buffer
            byte[] fakeBody = new byte[SocketStateObject.BufferSize * 2];
            AbstractMessage messageToWrite = new BasicMessage(fakeBody);
            // Prepare the received buffer
            SocketStateObject state = new SocketStateObject();
            int offset = messageToWrite.Header.Write(state.buffer, 0);
            // Call the TryRead
            AbstractMessage message = new BasicMessage();
            int byteRead = SocketStateObject.BufferSize;
            AbstractMessage actual = AbstractMessage.TryReadMessage(message, state, byteRead);
            Assert.IsNull(actual);
            Assert.IsNotNull(state.message);
            Assert.IsNull(state.pendingBuffer);
            byteRead = SocketStateObject.BufferSize;
            actual = AbstractMessage.TryReadMessage(message, state, byteRead);
            Assert.IsNull(actual);
            Assert.IsNotNull(state.message);
            Assert.IsNull(state.pendingBuffer);
            byteRead = messageToWrite.Header.HeaderLength;
            actual = AbstractMessage.TryReadMessage(message, state, byteRead);
            Assert.IsNotNull(actual);
            Assert.AreEqual(actual.MessageLength, messageToWrite.MessageLength);
            Assert.IsNull(state.message);
            Assert.IsNull(state.pendingBuffer);
        }

        /// <summary>
        ///A test for TryReadMessage
        ///</summary>
        [TestMethod()]
        public void TryReadMessageTestOneLongOneShortMessage()
        {
            // Create a message to write as received in buffer
            byte[] fakeBody = new byte[SocketStateObject.BufferSize * 2];
            AbstractMessage messageToWrite1 = new BasicMessage(fakeBody);
            fakeBody = new byte[50];
            AbstractMessage messageToWrite2 = new BasicMessage(fakeBody);
            byte[] bufferToSend = new byte[messageToWrite1.MessageLength + messageToWrite2.MessageLength];
            int offset = messageToWrite1.Header.Write(bufferToSend, 0);
            offset += messageToWrite1.GetBuffer().Length;
            offset = messageToWrite2.Header.Write(bufferToSend, offset);
            offset += messageToWrite2.GetBuffer().Length;
            // Read per block
            int readOffset = 0;
            int counter = 0;
            SocketStateObject state = new SocketStateObject();
            while (readOffset < bufferToSend.Length)
            {
                int size = ((bufferToSend.Length - readOffset) > SocketStateObject.BufferSize) ? 
                    SocketStateObject.BufferSize : (bufferToSend.Length - readOffset);
                // Prepare the received buffer
                Array.Copy(bufferToSend, readOffset, state.buffer, 0, size);
                // Call the TryRead
                AbstractMessage message = new BasicMessage();
                int byteRead = size;
                AbstractMessage actual = AbstractMessage.TryReadMessage(message, state, byteRead);
                switch(counter)
                {
                    case 0:
                        Assert.IsNull(actual);
                        Assert.IsNotNull(state.message);
                        Assert.IsNull(state.pendingBuffer);
                        break;
                    case 1:
                        Assert.IsNull(actual);
                        Assert.IsNotNull(state.message);
                        Assert.IsNull(state.pendingBuffer);
                        break;
                    case 2:
                        Assert.IsNotNull(actual);
                        Assert.AreEqual(actual.MessageLength, messageToWrite1.MessageLength);
                        Assert.IsNull(state.message);
                        Assert.IsNotNull(state.pendingBuffer);
                        break;
                }
                counter++;
                readOffset += size;
            }
            AbstractMessage message2 = new BasicMessage();
            AbstractMessage actual2 = AbstractMessage.TryReadMessage(message2, state, 0);
            Assert.IsNotNull(actual2);
            Assert.AreEqual(actual2.MessageLength, messageToWrite2.MessageLength);
            Assert.IsNull(state.message);
            Assert.IsNull(state.pendingBuffer);
        }
    }
}
