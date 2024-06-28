using NUnit.Framework;
using Client;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ClientTests
{
    [TestFixture]
    public class ClientTests
    {
        private UdpClient _udpClient;
        private IPEndPoint _ipEndPoint;
        private byte[] _receivedBytes;
        private string _receivedMessage;

        [SetUp]
        public void Setup()
        {
            // Инициализация клиента и конечной точки
            _udpClient = new UdpClient();
            _ipEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 12345);

            // Настройка тестового сервера для отправки подтверждения
            _udpClient.Client.Bind(_ipEndPoint);
            _udpClient.BeginReceive(ReceiveCallback, null);
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            // Получение подтверждения от тестового сервера
            _receivedBytes = _udpClient.EndReceive(ar, ref _ipEndPoint);
            _receivedMessage = Encoding.UTF8.GetString(_receivedBytes);
        }

        [Test]
        public void SentMessage_ValidMessage_ReceivesConfirmation()
        {
            // Arrange
            string expectedConfirmation = "Подтверждение от сервера";
            //string testMessage = "Тестовое сообщение";

            // Act
            Client.Client.SendMessage("TestUser", "127.0.0.1");

            // Assert
            Assert.AreEqual(expectedConfirmation, _receivedMessage);
        }

        [TearDown]
        public void Teardown()
        {
            // Закрытие соединения после теста
            _udpClient.Close();
        }
    }
}