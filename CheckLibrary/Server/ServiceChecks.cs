using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace CheckLibrary.Server
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "ServiceChecks" в коде и файле конфигурации.
    public class ServiceChecks : IServiceChecks
    {
        private static Game game;
        private readonly List<ServerUser> users = new List<ServerUser>();
        int nextId = 1;

        public int Connect(string name)
        {

            ServerUser user = new ServerUser()
            {
                ID = nextId,
                Name = name,
                operationContext = OperationContext.Current
            };
            nextId++;

            SendMessage("Игрок " + user.Name + " подключился к игре!", 0);
            users.Add(user);
            return user.ID;
        }

        public void Disconnect(int id)
        {
            var user = users.FirstOrDefault(i => i.ID == id);
            if (user != null)
            {
                users.Remove(user);
                SendMessage("Игрок " + user.Name + " покинул игру!", 0);
            }
        }
        public void SendMessage(string msg, int id)
        {
            foreach (var item in users)
            {
                string answer = DateTime.Now.ToShortTimeString();

                var user = users.FirstOrDefault(i => i.ID == id);
                if (user != null)
                {
                    answer += " : " + user.Name + " ";
                }
                answer += msg;
                item.operationContext.GetCallbackChannel<IServerGameCallBack>().MessageCallback(answer);
            }
        }
        public bool CompleteMove(Coordinate coordinate1, Coordinate coordinate2)
        {
            return game.OneMove(coordinate1, coordinate2);
        }
        public void CreateGame(ITableDrawer drawer, IDrawingFigureFactory factory)
        {
            game = new Game(drawer, factory);
        }

        public Game GetGame()
        {
            return game;
        }

        public List<IDrawingFigure> GetTableDrawingFigures()
        {
            return game.GetTableDrawingFigures();
        }

        public void SetTableDrawer(ITableDrawer drawer)
        {
            game.SetTableDrawer(drawer);
        }

        public WinColor WhoWin()
        {
            return game.WhoWin();
        }
    }
}
