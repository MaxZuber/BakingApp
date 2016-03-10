using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.WebSockets;

namespace Banking.Api
{
    /// <summary>
    /// Summary description for TransactionsSocket
    /// </summary>
    public class TransactionsSocket : IHttpHandler
    {

        // Список всех клиентов
        private static readonly List<WebSocket> Clients = new List<WebSocket>();

        // Блокировка для обеспечения потокабезопасности
        private static readonly ReaderWriterLockSlim Locker = new ReaderWriterLockSlim();

        public void ProcessRequest(HttpContext context)
        {
            if (context.IsWebSocketRequest)
            {
                context.AcceptWebSocketRequest(WebSocketRequest);
            }

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        private async Task WebSocketRequest(AspNetWebSocketContext context)
        {
            // Получаем сокет клиента из контекста запроса
            var socket = context.WebSocket;

            // Добавляем его в список клиентов
            Locker.EnterWriteLock();
            try
            {
                Clients.Add(socket);
            }
            finally
            {
                Locker.ExitWriteLock();
            }

            // Слушаем его
            while (true)
            {
                var buffer = new ArraySegment<byte>(new byte[1024]);

                // Ожидаем данные от него
                var result = await socket.ReceiveAsync(buffer, CancellationToken.None);


                //Передаём сообщение всем клиентам
                for (int i = 0; i < Clients.Count; i++)
                {

                    WebSocket client = Clients[i];

                    try
                    {
                        if (client.State == WebSocketState.Open)
                        {
                            await client.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);
                        }
                    }

                    catch (ObjectDisposedException)
                    {
                        Locker.EnterWriteLock();
                        try
                        {
                            Clients.Remove(client);
                            i--;
                        }
                        finally
                        {
                            Locker.ExitWriteLock();
                        }
                    }
                }

            }
        }
    }
}