﻿using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace CheckLibrary
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени интерфейса "IServiceChecks" в коде и файле конфигурации.
    [ServiceContract(CallbackContract = typeof(IServerGameCallBack))]
    public interface IServiceChecks
    {
        [OperationContract]
        int Connect(string name);

        [OperationContract]
        void Disconnect(int id);

        [OperationContract]
        void CreateGame(ITableDrawer drawer, IDrawingFigureFactory factory);

        [OperationContract]
        bool CompleteMove(Coordinate coordinate1, Coordinate coordinate2);

        [OperationContract]
        List<IDrawingFigure> GetTableDrawingFigures();

        [OperationContract]
        void SetTableDrawer(ITableDrawer drawer);

        [OperationContract]
        WinColor WhoWin();
    }
    public interface IServerGameCallBack
    {
        [OperationContract(IsOneWay = true)]
        void MsgCallback(string msg);
    }
}
