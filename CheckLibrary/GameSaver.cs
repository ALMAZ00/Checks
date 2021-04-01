using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace CheckLibrary
{
    [Serializable]
    public static class GameSaver
    {
        public static void SavePredTable(Game game)
        {
            IFormatter serializator = new BinaryFormatter();
            try
            {
                using (FileStream flStream = new FileStream("predTable.txt", FileMode.Create))
                {
                    serializator.Serialize(flStream, game);
                }
            }
            catch (FileNotFoundException)
            {
                
            }
        }
        public static Game BackMove()
        {
            IFormatter serializator = new BinaryFormatter();
            try
            {
                using (FileStream flStream = new FileStream("predTable.txt", FileMode.Open))
                {
                    return (Game)serializator.Deserialize(flStream);
                }
            }
            catch (FileNotFoundException)
            {
                return null;
            }
        }
        public static void SaveGame(Game game)
        {
            IFormatter serializator = new BinaryFormatter();
            try
            {
                using (FileStream flStream = new FileStream("game.txt", FileMode.Create))
                {
                    serializator.Serialize(flStream, game);
                }
            }
            catch (FileNotFoundException)
            {
                
            }
        }
        public static Game LoadGame()
        {
            IFormatter serializator = new BinaryFormatter();
            try
            {
                using (FileStream flStream = new FileStream("game.txt", FileMode.Open))
                {
                    return (Game)serializator.Deserialize(flStream);
                }
            }
            catch (FileNotFoundException)
            {
                return null;   
            }
        }
    }
}
